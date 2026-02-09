# Students CSV – MySQL – WinForms Application

Ez a projekt egy C# Windows Forms alkalmazás, amely diákok adatait kezeli CSV fájlból és MySQL (MariaDB) adatbázisból.

A program képes CSV fájl beolvasására, az adatok deszerializálására objektumokká, az adatok adatbázisba mentésére, valamint azok megjelenítésére egy DataGridView vezérlőben. Emellett lehetőséget biztosít új diákok manuális felvitelére is.

## Funkciók

- CSV fájl beolvasása CsvHelper könyvtár segítségével  
- CSV adatok deszerializálása `Student` objektumokká  
- Adatok mentése MySQL (MariaDB) adatbázisba  
- Adatok megjelenítése DataGridView-ban  
- Új diák hozzáadása regisztrációs űrlapon keresztül  
- Alapvető hibakezelés és adatvalidálás  

## CSV fájl formátuma

A CSV fájlnak pontosan az alábbi formátumban kell lennie:
Id,Name,Email,Age
1,Alice,alice@example.com,20
2,Bob,bob@example.com,22


Az első sor fejléc, az elválasztó karakter vessző (,), a fájl kódolása UTF-8.

## Szükséges környezet

- Windows operációs rendszer  
- Visual Studio 2019 vagy 2022  
- XAMPP (Apache + MySQL / MariaDB)  
- .NET Framework (WinForms alkalmazás)  

## Telepítés és futtatás

Először indítsd el a XAMPP Control Panelt, majd indítsd el az Apache és a MySQL szolgáltatásokat.

Ezután nyisd meg a böngészőben a phpMyAdmin felületet az alábbi címen:

http://localhost/phpmyadmin

Az SQL fülön futtasd le a projektben található `db/init.sql` fájlt. Ez létrehozza a `studentdb` adatbázist és a `Students` táblát.

Nyisd meg a `Students.sln` fájlt Visual Studio-ban. Ellenőrizd a connection stringet a `Form1.cs` fájlban. XAMPP alapértelmezett beállítás esetén a port 3307, például:

server=127.0.0.1;port=3307;database=studentdb;user=root;password=;


Ezután indítsd el az alkalmazást a Visual Studio Start gombjával.

A programban lehetőség van CSV fájl betöltésére a „CSV betöltése” gombbal, valamint új diákok manuális felvitelére a Submit gomb segítségével.

## Projekt felépítése

Students/
├── Students.sln
├── Students/
│ ├── Form1.cs
│ ├── Student.cs
│ └── App.config
├── db/
│ └── init.sql
└── README.md


## Használt technológiák

- C# Windows Forms  
- CsvHelper  
- MySQL / MariaDB  
- DataGridView  
- XAMPP  

## Megjegyzés

Az adatbázis nem kerül feltöltésre a GitHub repóba, kizárólag az adatbázis és a tábla létrehozásához szükséges SQL script (`db/init.sql`) található meg a projektben.

Készítette: saját beadandó projekt


