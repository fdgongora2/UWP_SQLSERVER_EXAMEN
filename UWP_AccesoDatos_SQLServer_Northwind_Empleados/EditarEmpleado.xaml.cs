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

                if (Empleado_Actual.Photo != null)
                {

                    using (InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream())
                    {
                        // Writes the image byte array in an InMemoryRandomAccessStream
                        // that is needed to set the source of BitmapImage.
                        using (DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0)))
                        {
                            writer.WriteBytes((byte[])Empleado_Actual.Photo);

                            // The GetResults here forces to wait until the operation completes
                            // (i.e., it is executed synchronously), so this call can block the UI.
                            writer.StoreAsync().GetResults();
                        }

                        var image = new BitmapImage();
                        image.SetSource(ms);
                        Img_Empleado.Source = image;
                    }


                    // Viejo

                    //MemoryStream streamEnMemoria = new MemoryStream(Empleado_Actual.Photo);
                    //streamEnMemoria.Seek(0,SeekOrigin.Begin);                         
                    //BitmapImage imagen = new BitmapImage();
                    //streamEnMemoria.WriteTo(imagen);

                    //using (var stream = await file.OpenReadAsync())
                    //{
                    //    await bitmap.SetSourceAsync(stream);

                    //    Stream stream2 = await file.OpenStreamForReadAsync();
                    //    stream2.CopyTo(memoryStream);
                    //    Empleado_Actual.Photo = memoryStream.ToArray();


                    //}
                }
                else
                {
                    // Cargamos una imagen "vacía"
                    Img_Empleado.Source = new BitmapImage(new Uri("ms-appx:///Assets/SplashScreen.scale-200.png"));
                }

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
                if (Empleado_Actual.Alta_Empleado() > 0)
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


            var memoryStream = new MemoryStream();

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

                    Stream stream2 = await file.OpenStreamForReadAsync();
                    stream2.CopyTo(memoryStream);
                    Empleado_Actual.Photo = memoryStream.ToArray();
                  

                }

                Img_Empleado.Source = bitmap;

                // Empleado_Actual.Photo = await ImageToBytes (bitmap);

            }
        }

       
    }
}
