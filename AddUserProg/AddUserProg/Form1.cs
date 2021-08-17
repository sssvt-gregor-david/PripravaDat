using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;


namespace AddUserProg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string conString = "Data Source=DESKTOP-BV2DTN3\\MSSQLSERVER02;Initial Catalog=bankUsers;Integrated Security=True";
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new(conString);
            con.Open();
            if (con.State==System.Data.ConnectionState.Open)
            {
                string q = "insert into tbUsers(name, mainSurname, secondarySurname, degreeBeforeName, degreeAfterName, homeAddress, organizationalUnit," +
                    " telNumberWork, telNumberPrivate, emailWork, emailPrivate, employmentFrom, employmentTo, maternityOrParentalLeave)" +
                    "values('" + nameTextBox.Text.ToString() + "','" + mainSurnameTextBox.Text.ToString() + "','" + secondarySurnameTextBox.Text.ToString()
                    + "','" + degreeBeforeNameTextBox.Text.ToString() + "','" + degreeAfterNameTextBox.Text.ToString() + "','" + homeAddressTextBox.Text.ToString()
                    + "','" + organizationalUnitTextBox.Text.ToString() + "','" + telNumberWorkTextBox.Text.ToString() + "','" + telNumberPrivateTextBox.Text.ToString()
                    + "','" + emailWorkTextBox.Text.ToString() + "','" + emailPrivateTextBox.Text.ToString() + "','" + employmentFromTextBox.Text.ToString()
                    + "','" + employmentToTextBox.Text.ToString() + "','" + maternityOrParentalLeaveTextBox.Text.ToString() + "')";

                if (organizationalUnitTextBox.Text == "Executive Officer" || organizationalUnitTextBox.Text == "Sales Department" ||
                    organizationalUnitTextBox.Text == "IT Department" || organizationalUnitTextBox.Text == "Property Management Department")
                {
                    if (nameTextBox.Text == "" || mainSurnameTextBox.Text == "" || homeAddressTextBox.Text == "" ||
                        telNumberPrivateTextBox.Text == "" || emailPrivateTextBox.Text == "")
                    {
                        MessageBox.Show("Error : not enough data");
                    }
                    else
                    {
                        SqlCommand cmd = new(q, con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User added successfully.");

                        nameTextBox.Text = "";
                        mainSurnameTextBox.Text = "";
                        secondarySurnameTextBox.Text = "";
                        degreeBeforeNameTextBox.Text = "";
                        degreeAfterNameTextBox.Text = "";
                        homeAddressTextBox.Text = "";
                        organizationalUnitTextBox.Text = "";
                        telNumberWorkTextBox.Text = "";
                        telNumberPrivateTextBox.Text = "";
                        emailWorkTextBox.Text = "";
                        emailPrivateTextBox.Text = "";
                        employmentFromTextBox.Text = "";
                        employmentToTextBox.Text = "";
                        maternityOrParentalLeaveTextBox.Text = "";
                    }

                }
                else
                {
                    MessageBox.Show("Error : uncorrect organizational Unit.");
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string cs = ConfigurationManager.ConnectionStrings[conString].ConnectionString;
            StringBuilder sb = new();
            using SqlConnection con = new(conString);
            {
                SqlDataAdapter da = new("select * from tbUsers", con);
                DataSet ds = new();

                da.Fill(ds);

                ds.Tables[0].TableName = "tbUsers";

                sb.Append("Id,name,mainSurname,secondarySurname,degreeBeforeName,degreeAfterName,homeAddress,ogranizationalUnit," +
                    "telNumberWork,telNumberPrivate,emailWork,emailPrivate,employmentFrom,employmentTo,maternityOrParentalLeave");
                sb.Append("\r\n");

                foreach (DataRow dr in ds.Tables["tbUsers"].Rows)
                {
                    int userId = Convert.ToInt32(dr["ID"]);
                    sb.Append(userId.ToString() + ",");

                    sb.Append(dr["name"].ToString() + ",");

                    sb.Append(dr["mainSurname"].ToString() + ",");

                    sb.Append(dr["secondarySurname"].ToString() + ",");

                    sb.Append(dr["degreeBeforeName"].ToString() + ",");

                    sb.Append(dr["degreeAfterName"].ToString() + ",");

                    sb.Append(dr["homeAddress"].ToString() + ",");

                    sb.Append(dr["organizationalUnit"].ToString() + ",");

                    int userTNW = Convert.ToInt32(dr["telNumberWork"]);
                    sb.Append(userTNW.ToString() + ",");

                    int userTNP = Convert.ToInt32(dr["telNumberPrivate"]);
                    sb.Append(userTNP.ToString() + ",");

                    sb.Append(dr["emailWork"].ToString() + ",");

                    sb.Append(dr["emailPrivate"].ToString() + ",");

                    DateTime userEF = Convert.ToDateTime(dr["employmentFrom"]);
                    sb.Append(userEF.ToString() + ",");

                    DateTime userET = Convert.ToDateTime(dr["employmentTo"]); 
                    sb.Append(userET.ToString() + ",");

                    int userMPL = Convert.ToInt32(dr["maternityOrParentalLeave"]);
                    sb.Append(userMPL.ToString() + ",");

                    sb.Append("\r\n");

                }
            }

            StreamWriter file = new(@"C:\Users\david\PripravaDat\PripravaDat\ExportedData\tbUsers.csv");
            file.WriteLine(sb.ToString());
            file.Close();
            MessageBox.Show("SCV created successfully.");
        }
    }
}
