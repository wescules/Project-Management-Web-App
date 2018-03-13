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
        Response.Redirect("~/Welcome.aspx");
    }

    //validates userinput for password. safegaurds against SQL injection
    public bool validateUserInput(String password)
    {
        var positiveIntRegex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9]*$");
        return !positiveIntRegex.IsMatch(password) ? false : true;
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

            //updated to accomodate for case sensitive user/passwords
            string query = "select count(*) from People where FirstName COLLATE Latin1_General_CS_AS ='" + txtemail.Text + "' and LastName COLLATE Latin1_General_CS_AS ='" + txtpass.Text + "' ";
            Response.Write(query);
            SqlCommand command = new SqlCommand(query, attach);
            int output = Convert.ToInt32(command.ExecuteScalar().ToString().Replace(" ", ""));

            attach.Close();
            if (output == 1)
            {
                //create session
                Session["user"] = txtemail.Text;
                //Response.Write("Login Success");
                Response.Redirect("~/Welcome.aspx");
            }
            else
            {
                Response.Write("Login Failed");
            }
        }
        else
        {
            Response.Write("Login Failed");
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