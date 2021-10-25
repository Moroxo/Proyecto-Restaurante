using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.IO;
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

namespace Restaurante.Vista.paginas_administrador
{
    /// <summary>
    /// Lógica de interacción para Adm_Recetas.xaml
    /// </summary>
    public partial class Adm_Recetas : Page
    {
        String Conexion = "Data Source=localhost:1521/xe; password=123456; User id=RESTAURANT";
        OracleConnection cone = new OracleConnection();
        EntitiesRestaurant db = new EntitiesRestaurant();
        public Adm_Recetas()
        {
            InitializeComponent();
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnListar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }
        private void confEliminar_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Crear_confirmacion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void abririmagen_click(object sender, RoutedEventArgs e)
        {
            /*
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"D:\SALVADO";
            if (openFileDialog.ShowDialog() == true)
                txtimg.Text = File.ReadAllText(openFileDialog.FileName);
                openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            */
        }

    }
}
