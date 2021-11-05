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
        string filename="";
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
            listar();
        }
        private void listar()
        {
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
            if (eliminartxt.Text != "")
            {
                //Se abre la conexion
                cone.ConnectionString = Conexion;
                cone.Open();
                //Se envia el nombre del insumo a eliminar
                String lector = "delete from plato where nom_plato ='" + eliminartxt.Text + "'";
                OracleDataAdapter adaptador = new OracleDataAdapter(lector, Conexion);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);
                cone.Close();
            }
            else
            {
                MessageBox.Show("ingrese insumo a eliminar");
            }
        }
        private void Crear_confirmacion_Click(object sender, RoutedEventArgs e)
        {
            if (txtReceta.Text != "" && txtPrecio.Text != "" && filename != ""
                && txtDescripcion.Text != "" && int.TryParse(txtPrecio.Text, out int result) == true)
            {

                cone.ConnectionString = Conexion;
                cone.Open();
                string id = "";
                DataTable dt = new DataTable();
                //insertamos el plato sin la imagen
                String lector = "insert into plato(nom_plato, precio_plato, descripcion) values('" + txtReceta.Text + "', " + txtPrecio.Text + ", '" + txtDescripcion.Text + "')";
                OracleDataAdapter adaptador = new OracleDataAdapter(lector, Conexion);
                adaptador.Fill(dt);


                //obtenemos la id del plato recien insertado(debido a que la id se ingresa automaticamente en la base de datos)
                OracleCommand comando = new OracleCommand("select id_plato from plato where nom_plato = '" + txtReceta.Text+"'", cone);
                OracleDataAdapter adaptador2 = new OracleDataAdapter();
                OracleDataReader registro = comando.ExecuteReader();
                while (registro.Read())
                {
                    id = registro["id_plato"].ToString();
                }

                //realizamos la insercion de la imagen mediante el plsql
                OracleCommand plsql = new OracleCommand("cargar_imagen_plato", cone);
                plsql.Parameters.Add("ins_id_plato", OracleType.Int32, 6).Value = int.Parse(id);
                plsql.Parameters.Add("ins_filename", OracleType.VarChar, 15).Value = filename;
                plsql.CommandType = System.Data.CommandType.StoredProcedure;
                plsql.ExecuteNonQuery();
                cone.Close();
                registro.Close();
                cone.Close();
            }
            else
            {
                if(int.TryParse(txtPrecio.Text, out int result2) == false)
                    MessageBox.Show("Precio invalido, solo debe contener numeros");
                else
                MessageBox.Show("Faltan campos por rellenar");
            }
        }

        private void abririmagen_click(object sender, RoutedEventArgs e)
        {
            string filepath = "";
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? respuesta = openFileDialog.ShowDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg";
            if (respuesta == true)
            {
                 filepath = openFileDialog.SafeFileName;

            }
            filename = filepath;
        }
    }
}
