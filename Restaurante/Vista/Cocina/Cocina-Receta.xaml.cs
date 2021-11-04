using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
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

namespace Restaurante.Vista.Cocina
{
    /// <summary>
    /// Lógica de interacción para Cocina_Receta.xaml
    /// </summary>
    public partial class Cocina_Receta : Window
    {
        public Cocina_Receta()
        {
            InitializeComponent();
        }
        String Conexion = "Data Source=localhost:1521/xe; password=123456; User id=RESTAURANT";
        OracleConnection cone = new OracleConnection();
        EntitiesRestaurant db = new EntitiesRestaurant();
        //Esta funcion sirve para mostrar los controles necesarios para crear un nuevo insumo siendo administrador, solo cuando se desee crearlo
        private void mostrarCrear()
        {
            lblDescripcion.Visibility = Visibility.Visible;
            lblprecio.Visibility = Visibility.Visible;
            lblreceta.Visibility = Visibility.Visible;
            txtPrecio.Visibility = Visibility.Visible;
            txtReceta.Visibility = Visibility.Visible;
            txtDescripcion.Visibility = Visibility.Visible;
            Crear_confirmacion.Visibility = Visibility.Visible;
            abririmagen.Visibility = Visibility.Visible;
            lblImagen.Visibility = Visibility.Visible;
        }
        //Ocultamos los controles para crear un nuevo insumos en caso de no ser necesario su uso(al querer eliminar, listar o actualizar)
        private void ocultarCrear()
        {

            lblDescripcion.Visibility = Visibility.Hidden;
            lblprecio.Visibility = Visibility.Hidden;
            lblreceta.Visibility = Visibility.Hidden;
            txtPrecio.Visibility = Visibility.Hidden;
            txtReceta.Visibility = Visibility.Hidden;
            txtDescripcion.Visibility = Visibility.Hidden;
            abririmagen.Visibility = Visibility.Hidden;
            Crear_confirmacion.Visibility = Visibility.Hidden;
            lblImagen.Visibility = Visibility.Hidden;
        }
        //Lo mismo de arriba, en este caso mostrando los controles al querer eliminar un insumo
        private void mostrarEliminar()
        {
            eliminartxt.Visibility = Visibility.Visible;
            confirmar.Visibility = Visibility.Visible;
        }
        //Otra vez ocultamos los controles cuando seran utilizados 
        private void ocultarEliminar()
        {
            eliminartxt.Visibility = Visibility.Hidden;
            confirmar.Visibility = Visibility.Hidden;
        }
        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            mostrarCrear();
            ocultarEliminar();
        }

        private void btnListar_Click(object sender, RoutedEventArgs e)
        {
            ocultarCrear();
            ocultarEliminar();
            cone.ConnectionString = Conexion;
            cone.Open();
            DataTable dt = new DataTable();
            String lector = "select * from plato";
            OracleDataAdapter adaptador = new OracleDataAdapter(lector, Conexion);
            adaptador.Fill(dt);
            dgRecetas.ItemsSource = dt.AsDataView();
            dgRecetas.Items.Refresh();
            cone.Close();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            ocultarCrear();
            mostrarEliminar();
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
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? respuesta = openFileDialog.ShowDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg";
            txtimg.Visibility = Visibility.Visible;
            if (respuesta == true)
            {
                string filepath = openFileDialog.FileName;
            }

            */

        }

    }
}
