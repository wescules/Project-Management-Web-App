using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Admin_report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {



        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select Employee.FirstName as fname, Works_On.ProjectID as pid, MonHours, TuesHours, WedsHours, ThursHours, FriHours, SatHours, SunHours from Hours_Worked, Works_On inner join Employee on Employee.EmployeeID = Works_On.EmployeeID where Works_On.DepartmentID = " + Session["department"] + " order by Works_On.EmployeeID";
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        Repeater2.DataSource = dt;
        Repeater2.DataBind();


        SqlCommand command = con.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = "select Projects.ProjectName as pname, Projects.Budget as budget, Projects.StartDate as startdate, Projects.EndDate as endDate from Projects where DepartmentID = 1";
        command.ExecuteNonQuery();
        DataTable dt1 = new DataTable();
        SqlDataAdapter da1 = new SqlDataAdapter(command);
        da1.Fill(dt1);
        Repeater3.DataSource = dt1;
        Repeater3.DataBind();


    }
}