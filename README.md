# Registration System with Database (C# WinForms)

## 📖 Description
This project is a **C# Windows Forms Application** that implements a simple **registration system** connected to a **local SQL Server database**. It demonstrates basic form handling, database connectivity, and CRUD-related logic commonly taught in introductory programming courses.

This project was developed as a requirement for  
**CS301A – Computer Programming**.

---

## 🛠️ Technologies Used
- **C#**
- **Windows Forms (WinForms)**
- **SQL Server LocalDB**
- **Visual Studio**

---

## ✨ Features
- User registration form
- Local database integration
- Insert and update member records
- Simple and beginner-friendly interface
- Designed for academic and learning purposes

---

## 🗂️ Project Structure


Macasilhig_KhyleMyrvin_RegistrationWithDB/
│
├── ClubForm/ # Main WinForms project
│ ├── ClubDB.mdf # Local database file
│ ├── ClubForm.cs # Main form logic
│ ├── FrmClubRegistration.cs
│ ├── FrmUpdateMember.cs
│ └── Properties/
│
├── resources/ # Images and other resources
└── README.md


---

## ▶️ How to Run the Project
1. Open **Visual Studio**
2. Click **Open a project or solution**
3. Select the `.sln` or `.csproj` file
4. Restore any required packages if prompted
5. Run the project using **Start (F5)**

> ⚠️ **SQL Server LocalDB must be installed** on your system.

---

## ⚠️ Important Notice: Database File Path Configuration

This project uses a **local database (`.mdf`) with an absolute file path** defined in the connection string.

Because of this:
- The application **may throw a database connection error** when opened on another computer
- This happens because the **database file path differs per user and machine**

### 🔧 How to Fix the Error
When running this project on a new system, the user must **update the database file path** in the code.

1. Open the project in **Visual Studio**
2. Locate the **connection string** in the source code (commonly found in form files or database-related classes)
3. Update the path to match the location of `ClubDB.mdf` on your local machine
4. Save the changes and run the project again

Example:
```csharp
@"Data Source=(LocalDB)\MSSQLLocalDB;
  AttachDbFilename=C:\Your\Local\Path\ClubDB.mdf;
  Integrated Security=True"

```
## 📌 Notes
- This project is intended for **educational purposes only**
- Some generated folders (`bin`, `obj`, `.vs`) should normally be excluded using `.gitignore`
- The use of an absolute path is common in beginner projects but not recommended for production applications


## 👤 Author
**Khyle Myrvin Macasilhig**  
BS Computer Science  
CS301A – Computer Programming
