using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UWP_AccesoDatos_SQLServer_Northwind_Empleados.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace UWP_AccesoDatos_SQLServer_Northwind_Empleados
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Empleado> Listado_Empleados;
        public MainPage()
        {
            this.InitializeComponent();
            Listado_Empleados = new ObservableCollection<Empleado>(Empleado.ObtenerEmpleados());
        }


        private void Alta_Empleado_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EditarEmpleado));
        }

        private void Editar_Empleado_Click(object sender, RoutedEventArgs e)
        {
            if (GV_Empleados.SelectedItem != null)
            {
                this.Frame.Navigate(typeof(EditarEmpleado), GV_Empleados.SelectedItem);
            }
        }

        private async void Borrar_Empleado_Click(object sender, RoutedEventArgs e)
        {
            if (GV_Empleados.SelectedItem != null)
            {
                 bool borrado = await Borrar_Preguntando();
                if (borrado)
                {
                    Listado_Empleados.Remove((Empleado)GV_Empleados.SelectedItem);
                }
            }
        }

        private void GV_Empleados_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EditarEmpleado), GV_Empleados.SelectedItem);
        }

        private async Task<bool> Borrar_Preguntando()
        {
            ContentDialog deleteFileDialog = new ContentDialog
            {
                Title = "¿ Desea borrar el empleado seleccionado ? ",
                Content = "Si borra los datos del empleado no podrá volver a recuperarlos",
                PrimaryButtonText = "Borrarlo",
                CloseButtonText = "Cancelar"
            };

            ContentDialogResult result = await deleteFileDialog.ShowAsync();

            // Delete the file if the user clicked the primary button.
            /// Otherwise, do nothing.
            if (result == ContentDialogResult.Primary)
            {
                
                return ((Empleado)GV_Empleados.SelectedItem).Borrar_Empleado();
            }
            else
            {
                // The user clicked the CLoseButton, pressed ESC, Gamepad B, or the system back button.
                // Do nothing.
                return false;
                     
            }
        }
    }
}
