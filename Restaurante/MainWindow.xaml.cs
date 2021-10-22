using System;
using System.Collections.Generic;
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
using System.Linq;

namespace Restaurante
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        String Conexion = "Data Source=localhost:1521/xe; password=123456; User id=RESTAURANT";
        OracleConnection cone = new OracleConnection();
        EntitiesRestaurant db = new EntitiesRestaurant();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Entrar_Click(object sender, RoutedEventArgs e)
        {
            cone.ConnectionString = Conexion;
            cone.Open();
            String login = "";
            String lector = "select id_tipo_usuario from usuario where nom_usuario = ('" + user.Text + "') and password = '" + password.Password.ToString()+ "'";
            OracleDataAdapter adaptador = new OracleDataAdapter(lector, Conexion);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            if (dt.Rows.Count>0)
            {
                login ="login exitoso";
                if (dt.Rows[0]["id_tipo_usuario"].ToString().Equals("1"))
                {
                    MessageBox.Show(login);
                    Vista.Administrador adm = new Vista.Administrador();
                    adm.ShowDialog();
                }else if (dt.Rows[0]["id_tipo_usuario"].ToString().Equals("4"))
                {
                    MessageBox.Show(login);
                    Vista.Cocinero coc = new Vista.Cocinero();
                    coc.ShowDialog();
                }else if (dt.Rows[0]["id_tipo_usuario"].ToString().Equals("6"))
                {
                    MessageBox.Show(login);
                    Vista.Bodeguero bod = new Vista.Bodeguero();
                    bod.ShowDialog();
                }
            }else
            {
                login = "credenciales erroneas";
                MessageBox.Show(login);
            }
            cone.Close();
        }
    }
}
