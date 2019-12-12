using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWP_AccesoDatos_SQLServer_NorthWind_Clientes.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_AccesoDatos_SQLServer_NorthWind_Clientes
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class DatosCliente : Page
    {
        private Cliente clienteActual;
        public DatosCliente()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                // Editando
                clienteActual = (Cliente)e.Parameter;
                tb_IDCLiente.IsReadOnly = true;
                tb_IDCLiente.Background = new SolidColorBrush(Colors.Gray);
            }
            else
            {
                // Alta
                clienteActual = new Cliente();
            }

            base.OnNavigatedTo(e);
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (tb_IDCLiente.IsReadOnly)
            {
                clienteActual.Actualizar_Datos_Cliente();
            }
            else
            {
                clienteActual.Alta_Cliente();
            }

            this.Frame.Navigate(typeof(Pagina_Clientes));

        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pagina_Clientes));
        }
    }
}
