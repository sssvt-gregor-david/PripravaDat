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
            int userId;
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
                        

                        StringBuilder sb = new();
                        using SqlConnection conn = new(conString);
                        {
                            SqlDataAdapter da = new("select top 1 Id from tbUsers order by Id desc", conn);
                            DataSet ds = new();

                            da.Fill(ds);

                            ds.Tables[0].TableName = "tbUsers";

                            sb.Append("Id");
                            sb.Append("\r\n");

                            foreach (DataRow dr in ds.Tables["tbUsers"].Rows)
                            {
                                userId = Convert.ToInt32(dr["ID"]);



                                addRecord(userId, nameTextBox.Text, mainSurnameTextBox.Text, secondarySurnameTextBox.Text, degreeBeforeNameTextBox.Text, degreeAfterNameTextBox.Text,
                                    homeAddressTextBox.Text, organizationalUnitTextBox.Text, telNumberWorkTextBox.Text, telNumberPrivateTextBox.Text, emailWorkTextBox.Text,
                                    emailPrivateTextBox.Text, employmentFromTextBox.Text, employmentToTextBox.Text, maternityOrParentalLeaveTextBox.Text);
                            }

                            

                        }


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
                    MessageBox.Show("Error : uncorrect or missing organizational Unit.");
                }

            }
        }

        public static void addRecord (int userId, string name, string surname, string secondarySurname, string degreeBeforeName, string degreeAfterName, string homeAddress,
            string organizationalUnit, string workNumber, string privateNumber, string workEmail, string privateEmail, string employmentFrom, string employmentTo, string maternityOrParentalLeave)
        {
            try
            {

                StreamWriter file = new(@"C:\Users\david\PripravaDat\PripravaDat\ExportedData\updatableCSV\UpdatableCSV5.csv", true);
                file.WriteLine(userId + "," + name + "," + surname + "," + secondarySurname + "," + degreeBeforeName + "," + degreeAfterName + "," + homeAddress
                         + "," + organizationalUnit + "," + workNumber + "," + privateNumber + "," + workEmail + "," + privateEmail + "," + employmentFrom + "," + employmentTo
                          + "," + maternityOrParentalLeave);
                 file.Close();
                
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error :", ex);
            }
        }

    }
}
