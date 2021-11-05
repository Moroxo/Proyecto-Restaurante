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
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data;
using System.Data.OracleClient;

namespace Restaurante.Vista
{
    /// <summary>
    /// Lógica de interacción para Bodeguero.xaml
    /// </summary>
    public partial class Bodeguero : Window
    {
        //CAMBIO FINAL
        String Conexion = "Data Source=localhost:1521/xe; password=123456; User id=RESTAURANT";
        OracleConnection cone = new OracleConnection();
        public Bodeguero()
        {
            InitializeComponent();
        }
        public void mostrarIngresar()
        {
            lblDescripcion.Visibility = Visibility.Visible;
            lblinsumo.Visibility = Visibility.Visible;
            lblPrecio.Visibility = Visibility.Visible;
            lblStock.Visibility = Visibility.Visible;
            lblTipo.Visibility = Visibility.Visible;
            txtDescripcion.Visibility = Visibility.Visible;
            txtInsumo.Visibility = Visibility.Visible;
            txtPrecio.Visibility = Visibility.Visible;
            txtStock.Visibility = Visibility.Visible;
            cbTipo.Visibility = Visibility.Visible;
            btnConfirmar.Visibility = Visibility.Visible;
        }
        public void ocultarIngresar()
        {
            lblDescripcion.Visibility = Visibility.Hidden;
            lblinsumo.Visibility = Visibility.Hidden;
            lblPrecio.Visibility = Visibility.Hidden;
            lblStock.Visibility = Visibility.Hidden;
            lblTipo.Visibility = Visibility.Hidden;
            txtDescripcion.Visibility = Visibility.Hidden;
            txtInsumo.Visibility = Visibility.Hidden;
            txtPrecio.Visibility = Visibility.Hidden;
            txtStock.Visibility = Visibility.Hidden;
            cbTipo.Visibility = Visibility.Hidden;
            btnConfirmar.Visibility = Visibility.Hidden;
        }
        public void mostrarSolicitar()
        {
            lblCant.Visibility = Visibility.Visible;
            lblnom2.Visibility = Visibility.Visible;
            txtNom2.Visibility = Visibility.Visible;
            txtCant.Visibility = Visibility.Visible;
            btnsolicitar.Visibility = Visibility.Visible;
        }
        public void ocultarSolicitar()
        {
            lblCant.Visibility = Visibility.Hidden;
            lblnom2.Visibility = Visibility.Hidden;
            txtCant.Visibility = Visibility.Hidden;
            txtNom2.Visibility = Visibility.Hidden;
            btnsolicitar.Visibility = Visibility.Hidden;
        }
        private int tipo_Insumo()
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
        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            mostrarIngresar();
            ocultarSolicitar();
        }

        private void controlar_Click(object sender, RoutedEventArgs e)
        {
            ocultarIngresar();
            mostrarSolicitar();
        }

        private void Listar_Click(object sender, RoutedEventArgs e)
        {
            ocultarIngresar();
            cone.ConnectionString = Conexion;
            cone.Open();
            DataTable dt = new DataTable();
            String lector = "select * from Insumos";
            OracleDataAdapter adaptador = new OracleDataAdapter(lector, Conexion);
            adaptador.Fill(dt);
            dgInsumos.ItemsSource = dt.AsDataView();
            dgInsumos.Items.Refresh();
            cone.Close();
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (txtInsumo.Text != "" && txtPrecio.Text != "" && txtDescripcion.Text != "" && txtStock.Text != ""
              && int.TryParse(txtPrecio.Text, out int result) == true
              && int.TryParse(txtStock.Text, out int result2) == true)
            {
                //llamamos a la funcion antes mencionada para obtener que tipo de insumo es y le asignamos un valor local
                int tipo = tipo_Insumo();
                //Abrimos la conexion con la bd
                cone.ConnectionString = Conexion;
                cone.Open();
                //Realizamos el insert con todos sus parametros necesarios
                String lector = "insert into insumos (nom_insumo, precio_unitario, descripcion, stock, id_tipoinsumo) values ('" + txtInsumo.Text + "'," + txtPrecio.Text + ",'" + txtDescripcion.Text + "', " + txtStock.Text + "," + tipo + ")";
                OracleDataAdapter adaptador = new OracleDataAdapter(lector, Conexion);
                
                DataTable dt = new DataTable();
                adaptador.Fill(dt);
                dgInsumos.ItemsSource = dt.AsDataView();
                dgInsumos.Items.Refresh();
                cone.Close();
                MessageBox.Show("Insert Ingresado correctamente");
            }
            else
            {
                if (int.TryParse(txtPrecio.Text, out int result3) == true ||
                    int.TryParse(txtStock.Text, out int result4) == true)
                    MessageBox.Show("Stock o precio invalido, solo debe ingresar numeros");
                else
                    MessageBox.Show("faltan campos por rellenar");
            }
        }

        private void btnsolicitar_Click(object sender, RoutedEventArgs e)
        {
            if(txtCant.Text != "" && txtNom2.Text != "" && int.TryParse(txtCant.Text, out int result) == true)
            {
                string id = "";
                cone.ConnectionString = Conexion;
                cone.Open();
                OracleCommand comando = new OracleCommand("select id_insumo from insumos where nom_insumo = '" + txtNom2.Text + "'", cone);
                OracleDataAdapter adaptador = new OracleDataAdapter();
                OracleDataReader registro = comando.ExecuteReader();
                while (registro.Read())
                {
                    id = registro["id_insumo"].ToString();
                }
                string desc = txtNom2.Text + ", se necesitan  " + txtCant.Text + ", " + DateTime.Today.ToShortDateString();
                String lector = "insert into solicitud_reposicion (descripcion, estado, id_insumo) values ('"+desc + "','en espera de aprobacion', " + id + ")";
                OracleDataAdapter adaptador2 = new OracleDataAdapter(lector, Conexion);
                DataTable dt = new DataTable();
                adaptador2.Fill(dt);
                MessageBox.Show("Solicitud enviada correctamente");
                cone.Close();
            }
            else
            {
                if (int.TryParse(txtCant.Text, out int result2) == false)
                    MessageBox.Show("Cantidad ingresada invalida, recuerde solo ingresar numeros");
                else
                    MessageBox.Show("Faltan datos por ingresar");
            }
        }
    }
}
