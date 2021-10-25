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
        //entregamos los parametros de conexion
        String Conexion = "Data Source=localhost:1521/xe; password=123456; User id=RESTAURANT";
        OracleConnection cone = new OracleConnection();
        EntitiesRestaurant db = new EntitiesRestaurant();

        public MainWindow()
        {
            InitializeComponent();
        }
        //funcion para realizar el login
        private void Entrar_Click(object sender, RoutedEventArgs e)
        {
            //abrimos la conexion
            cone.ConnectionString = Conexion;
            cone.Open();
            String login = "";
            //realizamos la consulta verificando que hayan datos correspondientes a los ingresados por el usuario
            String lector = "select id_tipo_usuario from usuario where nom_usuario = ('" + user.Text + "') and password = '" + password.Password.ToString()+ "'";
            OracleDataAdapter adaptador = new OracleDataAdapter(lector, Conexion);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            //si la consulta devuelve 1 fila esto significa que los datos son correctos y podemos iniciar el loign
            if (dt.Rows.Count>0)
            {
                login ="login exitoso";
                //Estos if son para filtrar segun el tipo de usuario logeado al sistema y enviarlo a su vista correspondiente
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
