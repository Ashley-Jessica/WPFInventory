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

        public void LoadGrid()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Ashley\\source\\repos\\WPFInventory\\WPFInventory\\db_test.mdf;Integrated Security=True"))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM categorytbl;", conn);
                    DataTable dt = new DataTable();
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dt.Load(sdr);
                    conn.Close();
                    CategoryDG.ItemsSource = dt.DefaultView;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Reading Failed: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Ashley\\source\\repos\\WPFInventory\\WPFInventory\\db_test.mdf;Integrated Security=True");
            conn.Open();

            SqlCommand cmd = new SqlCommand("Insert INTO categorytbl(Category ID, Category Name, quantity, measurement) VALUES (@Category ID, @Category Name, @quantity, @measurement)", conn);
            cmd.Parameters.AddWithValue("@Category ID", int.Parse(CatID.Text));
            cmd.Parameters.AddWithValue("@Category Name", CatName.Text);
            cmd.Parameters.AddWithValue("@quantity", quantity.Text);
            cmd.Parameters.AddWithValue("@measurement", measurement.Text);

            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Successfully Added.");
        }
    }
}
