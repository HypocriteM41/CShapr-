using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Port=5432;Database=testik;Username=postgres;Password=1234");
            try
            {
                con.Open();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Error");
                con.Close();
            }

            string sql = $"SELECT * FROM test";
            NpgsqlCommand command = new NpgsqlCommand(sql, con);

            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader(CommandBehavior.CloseConnection));
            DataGrid1.DataContext = dt;
            DataGrid1.ItemsSource = dt.AsDataView();
        }

    }
}
