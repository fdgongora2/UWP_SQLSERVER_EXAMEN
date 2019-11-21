using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UWP_AccesoDatos_SQLServer_Northwind_Empleados.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_AccesoDatos_SQLServer_Northwind_Empleados
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class EditarEmpleado : Page
    {
        private Empleado Empleado_Actual;
        public EditarEmpleado()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                // Editando
                Empleado_Actual = (Empleado)e.Parameter;

            }
            else
            {
                // Alta
                Empleado_Actual = new Empleado();
            }

            base.OnNavigatedTo(e);
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (Empleado_Actual.EmployeeID > 0)
            {
                if (Empleado_Actual.Actualizar_Datos_Empleado())
                {
                    this.Frame.Navigate(typeof(MainPage));
                }

            }
            else
            {
                if  (Empleado_Actual.Alta_Empleado() > 0)
                {
                    this.Frame.Navigate(typeof(MainPage));
                }


            }

        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));

        }

        private async void Img_Empleado_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

            MemoryStream memoryStream = new MemoryStream();

            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                var bitmap = new BitmapImage();
                using (var stream = await file.OpenReadAsync())
                {
                    await bitmap.SetSourceAsync(stream);

                }


                Img_Empleado.Source = bitmap;

                Empleado_Actual.Photo = await ImageToBytes (bitmap);

            }
        }

        public async static Task<byte[]> ImageToBytes(BitmapImage image)
        {
            RandomAccessStreamReference streamRef = RandomAccessStreamReference.CreateFromFile(image);
            IRandomAccessStreamWithContentType streamWithContent = await streamRef.OpenReadAsync();
            byte[] buffer = new byte[streamWithContent.Size];
            await streamWithContent.ReadAsync(buffer.AsBuffer(), (uint)streamWithContent.Size, InputStreamOptions.None);
            return buffer;
        }
    }
}
