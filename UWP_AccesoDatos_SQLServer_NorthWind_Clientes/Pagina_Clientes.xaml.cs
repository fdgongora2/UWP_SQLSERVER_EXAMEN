using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWP_AccesoDatos_SQLServer_NorthWind_Clientes.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Pagina_Clientes : Page
    {
        private ObservableCollection<Cliente> listado_Clientes;

        public Pagina_Clientes()
        {
            this.InitializeComponent();
            listado_Clientes = Cliente.Obtener_Clientes("");
            gv_Clientes.ItemsSource = listado_Clientes;
        }

        private void Tb_Buscador_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void as_buscar_cliente_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (as_buscar_cliente.Text.Trim() != "")
            {
                listado_Clientes = Cliente.Obtener_Clientes(as_buscar_cliente.Text.Trim());
                gv_Clientes.ItemsSource = listado_Clientes;
            }
        }

        private void Alta_Cliente_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DatosCliente));
        }

        private void Editar_Cliente_Click(object sender, RoutedEventArgs e)
        {
            if (gv_Clientes.SelectedItem != null)
            {
                this.Frame.Navigate(typeof(DatosCliente), gv_Clientes.SelectedItem);

            }

        }

        private void Borrar_Cliente_Click(object sender, RoutedEventArgs e)
        {
            if (gv_Clientes.SelectedItem != null)
            {
                ((Cliente)gv_Clientes.SelectedItem).Borrar_Cliente();              

            }
        }

        private void gv_Clientes_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (gv_Clientes.SelectedItem != null)
            {
                this.Frame.Navigate(typeof(DatosCliente), gv_Clientes.SelectedItem);

            }
        }
    }
}
