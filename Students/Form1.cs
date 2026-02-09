using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using System.Globalization;
using System.IO;
using MySql.Data.MySqlClient;


namespace Students
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            
            EnsureDatabaseAndTable();
            LoadStudentsToGrid();
        }
        private void EnsureDatabaseAndTable()
        {
            
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string sql = @"
                CREATE TABLE IF NOT EXISTS Students (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    Name VARCHAR(100) NOT NULL,
                    Email VARCHAR(100) NOT NULL,
                    Age INT NOT NULL
                );";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string connStr = "server=127.0.0.1;port=3307;database=studentdb;user=root;password=;";

        private List<Student> ReadCsv(string path)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<Student>().ToList();
            }
        }

        private void SaveStudentsToDatabase(List<Student> students)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                foreach (var s in students)
                {
                    string query = "INSERT INTO Students (Name, Email, Age) VALUES (@n, @e, @a)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@n", s.Name);
                    cmd.Parameters.AddWithValue("@e", s.Email);
                    cmd.Parameters.AddWithValue("@a", s.Age);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void LoadStudentsToGrid()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                MySqlDataAdapter da = new MySqlDataAdapter(
                    "SELECT Id, Name, Email, Age FROM Students", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.Refresh();
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLoadCsv_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV files (*.csv)|*.csv";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var students = ReadCsv(ofd.FileName);
                    SaveStudentsToDatabase(students);
                    LoadStudentsToGrid();
                    MessageBox.Show("CSV sikeresen betöltve!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba történt: " + ex.Message);
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
    string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Név és email kötelező!");
                return;
            }

            if (!int.TryParse(txtAge.Text, out int age))
            {
                MessageBox.Show("Az életkor csak szám lehet!");
                return;
            }

            try
            {
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connStr))
                {
                    conn.Open();
                    string q = "INSERT INTO Students (Name, Email, Age) VALUES (@n, @e, @a)";
                    var cmd = new MySql.Data.MySqlClient.MySqlCommand(q, conn);
                    cmd.Parameters.AddWithValue("@n", txtName.Text);
                    cmd.Parameters.AddWithValue("@e", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@a", age);
                    cmd.ExecuteNonQuery();
                }

                LoadStudentsToGrid();
                txtName.Clear();
                txtEmail.Clear();
                txtAge.Clear();
                MessageBox.Show("Sikeres mentés!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba mentéskor: " + ex.Message);
            }
        }
    }
}
