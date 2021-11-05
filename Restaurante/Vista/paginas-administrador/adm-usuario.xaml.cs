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
        public adm_usuario()
        {
            InitializeComponent();
        }
        //Esta funcion sirve para mostrar los controles necesarios para crear un nuevo usuario siendo administrador, solo cuando se desee crearlo
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
        //listar
        private void listar()
        {
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
        //Ocultamos los controles para crear un nuevo usuario en caso de no ser necesario su uso(al querer eliminar, listar o actualizar)
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
        //Lo mismo de arriba, en este caso mostrando los controles al querer eliminar un usuario
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
        //Segun el ComboItem elegido para el tipo de usuario, le entregaremos un valor entero para poder enviarlo a la base de datos
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
        //Cuando el administrador desee crear un nuevo usuario apareceran los controles necesarios
        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            mostrarCrear();
            ocultarEliminar();
        }
        //Funcion para Listar o mostrar los datos que hay actualmente en usuarios
        private void btnListar_Click(object sender, RoutedEventArgs e)
        {
            ocultarCrear();
            ocultarEliminar();
            listar();
        }
        //Cuando el administrador desee eliminar un usuario apareceran los controles necesarios
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            mostrarEliminar();
            ocultarCrear();
            eliminartxt.IsEnabled = true;
            confirmar.IsEnabled = true;
        }
        //El administrador confirma que desea eliminar y se realiza el proceso junto a la base de datos
        private void confirmar_Click(object sender, RoutedEventArgs e)
        {
            if(eliminartxt.Text!="")
            {
                //Se abre la conexion
                cone.ConnectionString = Conexion;
                cone.Open();
                //Se envia el nombre del usuario a eliminar
                String lector = "delete from usuario where nom_usuario ='" + eliminartxt.Text + "'";
                OracleDataAdapter adaptador = new OracleDataAdapter(lector, Conexion);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);
                dgUsuarios.ItemsSource = dt.AsDataView();
                dgUsuarios.Items.Refresh();
                cone.Close();
            }else
            {
                MessageBox.Show("Debe ingresar nombre de usuario a eliminar");
            }

        }
        //El administrador confirma que desea crear y se realiza el proceso junto a la base de datos
        private void Crear_confirmacion_Click(object sender, RoutedEventArgs e)
        {
            if (txtnomusuario.Text != "" && pwboxpassword.Password.ToString() != "" && txtCorreo.Text != ""
                && txtEstado.Text != "")
            {
                //llamamos a la funcion antes mencionada para obtener que tipo de insumo es y le asignamos un valor local
                int tipo = tipo_usuario();
                //Abrimos la conexion con la bd
                cone.ConnectionString = Conexion;
                cone.Open();
                //Realizamos el insert con todos sus parametros necesarios
                String lector = "insert into usuario (nom_usuario, password ,correo, estado, id_tipo_usuario) values ('" + txtnomusuario.Text + "','" + pwboxpassword.Password.ToString() + "','" + txtCorreo.Text + "', '" + txtEstado.Text + "'," + tipo + ")";
                OracleDataAdapter adaptador = new OracleDataAdapter(lector, Conexion);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);
                dgUsuarios.ItemsSource = dt.AsDataView();
                dgUsuarios.Items.Refresh();
                cone.Close();
            }else
            {
                MessageBox.Show("faltan campos por rellenar");
            }
        }
    }
}
