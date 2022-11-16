Sports Store Management Project

This is a desktop software for managing a sport store, including sales & Stock management capabilities. An employee using the system for the first time will have to register with his personal details, the default user type is Salesman, the admin can change user type if needed.

The software's main view is divided into 6 sections:

*Stock - this is the default view; salesmen can view the stock & sell from that screen.

*Customers - this is the cusomers info section; salesmen can view & edit customers info, get statistics & more.

*Sales - this view is limited for manager access; the manager can inspect the store's sales from that view.

*Edit Stock - this view is limited for manager access; the manager can manage stock from that screen.

*Users - this view is limited for admin only; the admin can edit users from that view.

*Logs - this view is limited for admin only; the admin can inspect logs & save them to txt files.

///////////Features:

*User Registration: - In this module user must register himself by filling some personal details.

*User Login: - After registration user will enter Email and password for logging in order to get access to the system.

*Search in Stock: - User can search & sort stock.

*Search in Customers:  Users can inspect & manage custumers info.

*Edit Stock: - Manager can inspect, add stock & add items easily.

*Sales Inspection: - Manager can inspect the store's sales by using different views.

*Users Edit tab: - Admin can easily inspect & edit user's details, reset passwords & more.

*Logs: - Every single operation is recorded to the log. Admin can inspect the logs easily by using different views that then can be saved to txt files.

///////////Software Requirements:

*Windows 10
*Microsoft SQL Server
*Visual Studio

///////////Hardware Components:

*Processor - i5
*Hard Disk Free Space - 250 MB
*Memory - 1GB RAM

///////////Advantages:

*Salesmen can easily search for items & customers in database in order to make sales.

*Managers can easily inspect sales & salesmen performance.

*Admin can easily inspect workers performance & more via the logs which are created for every single user action.

///////////Disadvantages:

*Desktop only.

///////////Application:

*This application is useful for many Stores. This application can be used for selling other products with little modification of the system.

///////////Project Features:

*User Registration/ Login module.
*Top menu including edit user details, this file, logoff, exit.  
*Salesmen may search through the store's stock.
*Salesmen may sell selected items.
*Managers can monitor sales.
*Mangers can monitor worker's performance.
*Admin can monitor everything through the logs.
*Logs can be saved to txt file.

///////////Code Features:

*The program is divided into 3 projects for implementing the MVC design pattern.
*Item's are created using Abstaract Factory design pattern.
*Database is created with Entity Framework Core.
*All queries are written in LINQ.
*The Db connector in the controller is Singleton, accessible via Dependency Injection only.
*The entity handlers in the controller are interface driven.
*Extention Methods in the controller simplifies DB actions.
*Validations are made using Regex.
*Main UI is divided into different components for better handling.
*All the UI Components & Design was made from scratch, using Resource Dictionaries & Custom Controls. 
*Passwords are encoded to MD5 Hash.

///////////Installation:

1. download & install Microsoft SQL Server - https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-2017
2. download & intall Microsoft Visual Studio - https://visualstudio.microsoft.com/
3. clone this project from github - https://github.com/yehonatan604/SportsStore
4. if not installed in project, install NuGet packages - 
	1. Microsoft.EntityFrameworkCore.SqlServer
	2. Microsoft.EntityFrameworkCore.Design
5. at the app folder, as an admin, open powershell & type this commands:
	1. dotnet ef migrations add migartion1
	2. dotnet dotnet ef databse update
6. make sure that SportsStore.View is the startip project.
7. you can login with that details: 
   email - Admin, 
   password - Admin1234
8. it is strongly recomended that you will change those details in the settings window!