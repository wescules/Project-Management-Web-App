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
        SqlCommand command = con.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = "select Projects.ProjectName as pname, Projects.Budget as budget, Projects.StartDate as startdate, Projects.Deadline as deadline from Projects where DepartmentID =" + Session["department"];
        command.ExecuteNonQuery();
        DataTable dt1 = new DataTable();
        SqlDataAdapter da1 = new SqlDataAdapter(command);

        da1.Fill(dt1);
        DataTable dt2 = TotalcostTable(dt1);
        Repeater3.DataSource = dt2;
        Repeater3.DataBind();

        LoadPhases();
    }

    private DataTable TotalcostTable(DataTable og)
    {
        og.Columns.Add("totalcost", typeof(string));
        for (int i = 0; i < og.Rows.Count; i++)
        {
            og.Rows[i]["totalcost"] = gettotalCost((string)og.Rows[i]["pname"]).ToString("0.00");
        }
        return og;

    }

    private decimal gettotalCost(String pname)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "Select Employee.FirstName, Projects.ProjectName, Employee.PayRate*Hours_Worked.totalHours as ActualPay From(Employee inner join Timesheet ON Employee.EmployeeID = Timesheet.EmployeeID) inner join Hours_Worked ON Timesheet.TimeSheetID = Hours_Worked.TimesheetID, Projects where Hours_Worked.ProjectID = Projects.ProjectID and Projects.ProjectName = '" + pname + "'";
        cmd.Connection = con;

        SqlDataReader rd = cmd.ExecuteReader();

        //int timesheetID;

        //do
        //{
        //    rd.Read();
        //    timesheetID = (int)rd[0];
        //}
        //while (rd.Read());

        //con.Close();

        //return timesheetID;


        decimal totalcost = 0;

        while (rd.Read())
        {
            decimal actualPay = rd.GetDecimal(2);
            totalcost += actualPay;



        }
        return totalcost;


    }




    //load in values for Phases
    private void LoadPhases()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select distinct Employee.FirstName as Firstname, Employee.EmployeeID as empid from Employee, Works_On where Works_On.EmployeeID=Employee.EmployeeID and Works_On.DepartmentID=" + Session["department"] + " group by Employee.FirstName, Employee.EmployeeID";
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        this.ParentRepeater.DataSource = dt;
        this.ParentRepeater.DataBind();
        con.Close();
    }

    //load in values foreach task in phase
    protected void ItemBound(object sender, RepeaterItemEventArgs args)
    {
        if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lbl = args.Item.FindControl("lblName") as Label;      //Assignment.groupID
            var a = lbl.Text;



            Repeater childRepeater = (Repeater)args.Item.FindControl("ChildRepeater");
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "select Hours_Worked.ProjectID as pid, Employee.FirstName as fname, SunHours, MonHours, TuesHours, WedsHours, ThursHours,FriHours,SatHours, totalHours from(Employee inner join Timesheet on Employee.EmployeeID = Timesheet.EmployeeID) inner join Hours_Worked on Timesheet.TimeSheetID = Hours_Worked.TimesheetID where Employee.DepartmentID =" + Session["department"] +" and Employee.EmployeeID=" + a +"";
            cmd.CommandText = "select Projects.ProjectName as ProjectorName, SunHours, MonHours, TuesHours, WedsHours, ThursHours,FriHours,SatHours, totalHours from(Employee inner join Timesheet on Employee.EmployeeID = Timesheet.EmployeeID) inner join Hours_Worked on Timesheet.TimeSheetID = Hours_Worked.TimesheetID, Projects where Employee.DepartmentID = " + Session["department"] + " and Employee.EmployeeID=" + a + " and Hours_Worked.ProjectID=Projects.ProjectID";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            childRepeater.DataSource = dt;
            childRepeater.DataBind();
            con.Close();
        }
    }
}