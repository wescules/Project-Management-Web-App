using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] != null)
        {
            Response.Redirect("~/Welcome.aspx");
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select DepartmentID, DepartmentName from Department";
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        dptDropDown.DataSource = dt;
        dptDropDown.DataValueField = "DepartmentID";
        dptDropDown.DataTextField = "DepartmentName";
        dptDropDown.DataBind();
    }

    protected void login_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Login.aspx");
    }

    //validates userinput for txtPass. safegaurds against SQL injection
    public bool ValidateUserInput(String txtPass)
    {
        var positiveIntRegex = new System.Text.RegularExpressions.Regex(@"^[.@a-zA-Z0-9]*$");
        return !positiveIntRegex.IsMatch(txtPass) ? false : true;
    }

    private void register(string email, string password, string fName, string lName, string position, string department, string isManagerStr, string payRate, string startDate)
    {
        int isMgr = isManagerStr == "yes" ? 1 : 0;
        //encrypt user/pass and create new connection
        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        attach.Open();
        SqlCommand cmd = attach.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO Employee (FirstName, LastName, Email, Password, Position, DepartmentID, isManager, PayRate, StartDate) " +
                            "VALUES ('" + fName + "','" + lName + "', '" + email + "', '" + password + "', '" + position + "', " + Int32.Parse(department) + ", " + isMgr + ", " + Double.Parse(payRate) + ", '" + startDate + "')";

        cmd.ExecuteNonQuery();

        attach.Close();
    }

    protected void Submitbtn_Click(object sender, EventArgs e)
    {
        bool usernameValidated = ValidateUserInput(txtEmail.Text);
        bool txtPassValidated = ValidateUserInput(txtPass.Text);

        //access database ONLY IF username and txtPass are validated
        if (usernameValidated)
        {
            if (txtPassValidated)
            {
                //encrypt user/pass and create new connection
                SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
                attach.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Employee";
                cmd.Connection = attach;

                SqlDataReader rd = cmd.ExecuteReader();
                System.Diagnostics.Debug.WriteLine(rd);
                bool flag = false;
                while (rd.Read())
                {
                    if (rd[1].ToString().ToLower() == txtEmail.Text.ToLower())
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag)
                {
                    Response.Write("SORRY USER ALREADY EXISTS");
                }
                else
                {
                    register(txtEmail.Text, txtPass.Text, txtFName.Text, txtLName.Text, txtPosition.Text, dptDropDown.SelectedValue, isManagerRadio.SelectedValue, txtPayRate.Text, txtStartDate.Text);

                    Response.Write("Student registeration Successfully!!!thank you");
                }


                attach.Close();
            }

            else
            {
                Response.Write("PASSWORD INVALID");
            }
            
            
        }
        else
        {
            Response.Write("USERNAME INVALID");
        }

    }

    protected void valDateRange_ServerValidate(object source, ServerValidateEventArgs args)
    {
        DateTime minDate = DateTime.Parse("2000/01/01");
        DateTime maxDate = DateTime.Today;
        DateTime dt;

        args.IsValid = (DateTime.TryParse(args.Value, out dt)
                        && dt <= maxDate
                        && dt >= minDate);
    }
}









//protected void loginbtn_Click(object sender, EventArgs e)
//{
//    //encrypt user/pass and create new connection
//    SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
//    attach.Open();
//    string query = "select count(*) from People where FirstName='" + txtuser.Text + "' and LastName='" + txtpass.Text + "' ";
//    Response.Write(query);
//    SqlCommand command = new SqlCommand(query, attach);
//    int output = Convert.ToInt32(command.ExecuteScalar().ToString().Replace(" ", ""));

//    attach.Close();
//    if (output == 1)
//    {
//        //create session
//        Session["user"] = txtuser.Text;
//        //Response.Write("Login Success");
//        Response.Redirect("~/Welcome.aspx");
//    }
//    else
//    {
//        Response.Write("Login Failed");
//    }
//}

//protected void loginbtn_Click(object sender, EventArgs e)
//{
//    //encrypt user/pass and create new connection
//    SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
//    attach.Open();
//    string checkUser = "select count(*) from People where FirstName='" + txtuser.Text + "' ";

//    Response.Write(checkUser);
//    SqlCommand command = new SqlCommand(checkUser, attach);
//    int output = Convert.ToInt32(command.ExecuteScalar().ToString().Replace(" ", ""));

//    attach.Close();

//    //check matching txtPass
//    if (output == 1)
//    {
//        attach.Open();
//        string checkPass = "select count(*) from People where LastName='" + txtpass.Text + "' ";

//        //Response.Write(checkPass);
//        SqlCommand com = new SqlCommand(checkPass, attach);
//        string txtPass = com.ExecuteScalar().ToString().Replace(" ", "");

//        if (txtPass == txtpass.Text)
//        {
//            Session["New"] = txtuser.Text;
//            Response.Write("txtPass is correct");
//        }
//        else
//        {
//            Response.Write("txtPass is NOT correct");
//        }
//        attach.Close();

//        //Response.Write("Login Success");
//        Response.Redirect("~/Welcome.aspx");
//    }
//    else
//    {
//        Response.Write("User Name NOT correct. Login Failed");
//    }
//}