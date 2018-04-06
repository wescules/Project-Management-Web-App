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
    }

    protected void login_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Login.aspx");
    }

    //validates userinput for password. safegaurds against SQL injection
    public bool validateUserInput(String password)
    {
        var positiveIntRegex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9]*$");
        return !positiveIntRegex.IsMatch(password) ? false : true;
    }
    protected void register(string a, string b)
    {
        //encrypt user/pass and create new connection
        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        attach.Open();
        SqlCommand cmd = attach.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO[dbo].[Employee] ([FirstName] ,[LastName])VALUES ('" + a + "','" + b + "')";

        cmd.ExecuteNonQuery();

        attach.Close();
    }
    protected void loginbtn_Click(object sender, EventArgs e)
    {
        bool usernameValidated = validateUserInput(txtemail.Text);
        bool passwordValidated = validateUserInput(txtpass.Text);

        //access database ONLY IF username and password are validated
        if (usernameValidated && passwordValidated)
        {
            //encrypt user/pass and create new connection
            SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
            attach.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Employee";
            cmd.Connection = attach;

            SqlDataReader rd = cmd.ExecuteReader();
            bool flag = false;
            while (rd.Read())
            {
                if (rd[1].ToString().ToLower() == txtemail.Text.ToLower())
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
                register(txtemail.Text, txtpass.Text);

                Response.Write("Student registeration Successfully!!!thank you");
            }


            attach.Close();
        }
        else
        {
            Response.Write("USERNAME OR PASSWORD INVALID");
        }

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

//    //check matching password
//    if (output == 1)
//    {
//        attach.Open();
//        string checkPass = "select count(*) from People where LastName='" + txtpass.Text + "' ";

//        //Response.Write(checkPass);
//        SqlCommand com = new SqlCommand(checkPass, attach);
//        string password = com.ExecuteScalar().ToString().Replace(" ", "");

//        if (password == txtpass.Text)
//        {
//            Session["New"] = txtuser.Text;
//            Response.Write("Password is correct");
//        }
//        else
//        {
//            Response.Write("Password is NOT correct");
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