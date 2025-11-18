# Student Information Management System

This project is a simple desktop application for managing student records, built with C# and .NET 8 on the Windows Forms framework. It provides basic CRUD (Create, Read, Update, Delete) functionality for student information and grades.


**Key Features:**

- **CRUD Operations**: Add, search, edit, and delete student records through an intuitive UI.

- **Flat-File Database**: Uses simple .txt files for data storage, making it lightweight and portable.

- **Automatic Grade Calculation**: Automatically computes a student's final grade based on weighted scores (Lecture 30%, Lab 30%, Exam 40%).

- **Data Recovery**: A "Retrieve" feature allows for a one-step undo of the last data modification.

- **Data Validation**:

  -> Required Fields: All text fields must be filled before saving.
  
  -> First Name and Last Name must be non-numeric.
  
  -> Lecture, Lab, and Exam scores must be valid numbers.
  
  -> Grade Range: All scores must be between 0 and 100.
  
  -> The Student ID must match the predefined format 0000-00000-SM-0.
