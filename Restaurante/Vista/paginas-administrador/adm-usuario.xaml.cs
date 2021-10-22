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
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data;
using System.Data.OracleClient;


namespace Restaurante.Vista.paginas_administrador
{
    /// <summary>
    /// Lógica de interacción para adm_usuario.xaml
    /// </summary>
    public partial class adm_usuario : Page
    {
        String Conexion = "Data Source=localhost:1521/xe; password=123456; User id=RESTAURANT";
        OracleConnection cone = new OracleConnection();
        EntitiesRestaurant db = new EntitiesRestaurant();
        public adm_usuario()
        {
            InitializeComponent();
        }
        private void mostrarCrear()
        {
            lblCorreo.Visibility = Visibility.Visible;
            lblestado.Visibility = Visibility.Visible;
            lblpass.Visibility = Visibility.Visible;
            lbltipo.Visibility = Visibility.Visible;
            lblnomusuario.Visibility = Visibility.Visible;
            txtCorreo.Visibility = Visibility.Visible;
            txtEstado.Visibility = Visibility.Visible;
            txtnomusuario.Visibility = Visibility.Visible;
            pwboxpassword.Visibility = Visibility.Visible;
            cbtipo.Visibility = Visibility.Visible;
            Crear_confirmacion.Visibility = Visibility.Visible;
        }
        private void ocultarCrear()
        {

            lblCorreo.Visibility = Visibility.Hidden;
            lblestado.Visibility = Visibility.Hidden;
            lblpass.Visibility = Visibility.Hidden;
            lbltipo.Visibility = Visibility.Hidden;
            lblnomusuario.Visibility = Visibility.Hidden;
            txtCorreo.Visibility = Visibility.Hidden;
            txtEstado.Visibility = Visibility.Hidden;
            txtnomusuario.Visibility = Visibility.Hidden;
            pwboxpassword.Visibility = Visibility.Hidden;
            cbtipo.Visibility = Visibility.Hidden;
            Crear_confirmacion.Visibility = Visibility.Hidden;
        }
        private void mostrarEliminar()
        {
            eliminartxt.Visibility = Visibility.Visible;
            confirmar.Visibility = Visibility.Visible;
        }
        private void ocultarEliminar()
        {
            eliminartxt.Visibility = Visibility.Hidden;
            confirmar.Visibility = Visibility.Hidden;
        }
        private int tipo_usuario()
        {
            int tipo = 0;
            if (_1.IsSelected)
                tipo = 1;
            else if (_2.IsSelected)
                tipo = 2;
            else if (_3.IsSelected)
                tipo = 3;
            else if (_4.IsSelected)
                tipo = 4;
            else if (_5.IsSelected)
                tipo = 5;
            else if (_6.IsSelected)
                tipo = 6;
            return tipo;
        }
        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            mostrarCrear();

        }

        private void btnListar_Click(object sender, RoutedEventArgs e)
        {
            ocultarCrear();
            ocultarEliminar();
            cone.ConnectionString = Conexion;
            cone.Open();
            DataTable dt = new DataTable();
            String lector = "select * from usuario";
            OracleDataAdapter adaptador = new OracleDataAdapter(lector, Conexion);
            adaptador.Fill(dt);
            dgUsuarios.ItemsSource = dt.AsDataView();
            dgUsuarios.Items.Refresh();
            cone.Close();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            mostrarEliminar();
            ocultarCrear();
            eliminartxt.IsEnabled = true;
            confirmar.IsEnabled = true;
        }

        private void confirmar_Click(object sender, RoutedEventArgs e)
        {
            cone.ConnectionString = Conexion;
            cone.Open();
            String lector = "delete from usuario where nom_usuario ='" + eliminartxt.Text + "'";
            OracleDataAdapter adaptador = new OracleDataAdapter(lector, Conexion);
            cone.Close();
            cone.Open();
            DataTable dt = new DataTable();
            String lector2 = "select * from usuario";
            OracleDataAdapter adaptador2 = new OracleDataAdapter(lector2, Conexion);
            adaptador.Fill(dt);
            dgUsuarios.ItemsSource = dt.AsDataView();
            dgUsuarios.Items.Refresh();
            cone.Close();
        }

        private void Crear_confirmacion_Click(object sender, RoutedEventArgs e)
        {
            int tipo = tipo_usuario();
            cone.ConnectionString = Conexion;
            cone.Open();
            String lector = "insert into usuario (nom_usuario, password ,correo, estado, id_tipo_usuario) values ('" + txtnomusuario.Text + "','" + pwboxpassword.Password.ToString() + "','" + txtCorreo.Text + "', '" + txtEstado.Text + "'," + tipo + ")";
            OracleDataAdapter adaptador = new OracleDataAdapter(lector, Conexion);
            cone.Close();
            cone.Open();
            DataTable dt = new DataTable();
            String lector2 = "select * from usuario";
            OracleDataAdapter adaptador2 = new OracleDataAdapter(lector2, Conexion);
            adaptador.Fill(dt);
            dgUsuarios.ItemsSource = dt.AsDataView();
            dgUsuarios.Items.Refresh();
            cone.Close();
        }
    }
}
