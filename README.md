# Project-Management-Web-App

Lightweight project management web app that uses ASP.NET and SQL Server.

# Installation
```
git clone https://github.com/wescules/Project-Management-Web-App.git
```
1) Open in Visual Studio as Website
2) Restore database (Database Backup/Updated_Dask.bak) through SQL Server Management Studio
3) Change Web.conf file to your local machine name:
```
<add name="connect" connectionString="Data Source=<INSERT LOCAL MACHINE NAME>\SQLEXPRESS1;Database=UpdatedDash;Trusted_Connection=True;" providerName="System.Data.SqlClient"/>
```
