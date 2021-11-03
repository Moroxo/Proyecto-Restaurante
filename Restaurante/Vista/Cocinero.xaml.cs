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
    /// Lógica de interacción para Cocinero.xaml
    /// </summary>
    public partial class Cocinero : Window
    {
        //String Conexion = "Data Source=localhost:1521/xe; password=123456; User id=RESTAURANT";
        OracleConnection cone = new OracleConnection("Data Source = localhost:1521 / xe; password=123456; User id = RESTAURANT;");
        public Cocinero()
        {
            InitializeComponent();
            listardatos();
            Cargarcombobox();
        }

        public void listardatos()
        {
            OracleCommand comando = new OracleCommand("select dp.nro_pedido, dp.cant_pedidos, pl.nom_plato as nombre, dp.estado_pedido from detalle_pedido dp "
                + "join plato pl on dp.id_plato = pl.id_plato where estado_pedido = 'en proceso'", cone);
            cone.Open();
            DataTable dt = new DataTable();
            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            adaptador.Fill(dt);
            dgOrdenes.ItemsSource = dt.AsDataView();
            dgOrdenes.Items.Refresh();
            cone.Close();
        }
        public void Cargarcombobox()
        {
            OracleCommand comando = new OracleCommand("select nro_pedido from detalle_pedido  where estado_pedido = 'en proceso'", cone);
            cone.Open();
            OracleDataReader registro = comando.ExecuteReader();
            while (registro.Read())
            {
                cbnropedido.Items.Add(registro["nro_pedido"].ToString());
            }
            registro.Close();
            cone.Close();

        }
        private void Finalizar_Click(object sender, RoutedEventArgs e)
        {
            int i = 1 + cbnropedido.SelectedIndex;
            //Se envia el numero de pedido a finalizar
            String lector = "update detalle_pedido set estado_pedido = 'finalizado' where nro_pedido = "+ i;
            cone.Open();
            OracleDataAdapter adaptador = new OracleDataAdapter(lector, cone);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            cone.Close();

        }

        private void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            listardatos();
        }
    }
}
