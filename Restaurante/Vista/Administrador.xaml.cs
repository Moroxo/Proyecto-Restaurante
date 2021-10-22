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
    /// Lógica de interacción para Administrador.xaml
    /// </summary>
    public partial class Administrador : Window
    {
        public Administrador()
        {
            InitializeComponent();
        }

        private void AdmUsuario_Click(object sender, RoutedEventArgs e)
        {
           VistaAdm.Content = new Vista.paginas_administrador.adm_usuario();
        }

        private void AdmMesas_Click(object sender, RoutedEventArgs e)
        {
            VistaAdm.Content = new Vista.paginas_administrador.Adm_mesas();
        }
    }
}
