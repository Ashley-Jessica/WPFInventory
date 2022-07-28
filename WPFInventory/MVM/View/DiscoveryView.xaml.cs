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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.Entity.Core.Objects;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data.SqlClient;

namespace WPFInventory.MVM.View
{
    /// <summary>
    /// Interaction logic for DiscoveryView.xaml
    /// </summary>
    public partial class DiscoveryView : UserControl
    {


        public DiscoveryView()
        {
            InitializeComponent();
            LoadGrid();

        }

        public void clearData()
        {
            CatName.Clear();
            measurement.Clear();
            quantity.Clear();
        }

        public void LoadGrid()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Ashley\\source\\repos\\WPFInventory\\WPFInventory\\db_test.mdf;Integrated Security=True"))
                {
                    var dbslct = "SELECT * FROM categorytbl";
                    using (SqlCommand cmd = new SqlCommand(dbslct, conn))
                    {
                        DataTable dt = new DataTable();
                        conn.Open();
                        SqlDataReader sdr = cmd.ExecuteReader();
                        dt.Load(sdr);
                        conn.Close();
                        CategoryDG.ItemsSource = dt.DefaultView;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Reading Failed: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ashley\source\repos\WPFInventory\WPFInventory\db_test.mdf;Integrated Security=True"))
            {
                var dbinsert = "INSERT INTO categorytbl VALUES(@CategoryName, @measurement, @quantity)";
                using (SqlCommand cmd = new SqlCommand(dbinsert, con))
                {
                    try
                    {
                        con.Open();
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@CategoryName", CatName.Text);
                        cmd.Parameters.AddWithValue("@measurement", measurement.Text);
                        cmd.Parameters.AddWithValue("@quantity", quantity.Text);
                        cmd.ExecuteNonQuery();
                        LoadGrid();
                        MessageBox.Show("Successfully Registered", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                        clearData();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Not inserted, " + ex.Message);
                    }
                }
            }
        }


        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ashley\source\repos\WPFInventory\WPFInventory\db_test.mdf;Integrated Security=True"))
            {
                var dbdlt = "DELETE FROM categorytbl WHERE CategoryID = " + CatID.Text + " ";
                using (SqlCommand cmd = new SqlCommand(dbdlt, con))
                {
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record has been deleted", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                        clearData();
                        LoadGrid();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Not deleted" + ex.Message);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }
    }
}
