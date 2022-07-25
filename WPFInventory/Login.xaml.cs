using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;

namespace WPFInventory
{
    public partial class LOGIN : Window
    {
        SqlConnection con = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=C:\\Users\\Ashley\\source\\repos\\WPFInventory\\WPFInventory\\db_test.mdf;Integrated Security = True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        public LOGIN()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();

        }

        private void btnlogin_Click(object sender, RoutedEventArgs e)
        {
            if(con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            if(VerifyUser(txtUsername.Text, txtPassword.Password))
            {
                MessageBox.Show("Login Successfully.", "Welcome to the Inventory System.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Username or Password is Incorrect.", "Error Reading Database.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool VerifyUser(string UserName, string Password)
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT Status from usertbl WHERE UserName ='"+UserName+"' and Password = '" + Password + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToBoolean(dr["Status"]) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
