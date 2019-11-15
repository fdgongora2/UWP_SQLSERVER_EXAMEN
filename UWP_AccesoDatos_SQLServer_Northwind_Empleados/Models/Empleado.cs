using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_AccesoDatos_SQLServer_Northwind_Empleados.Models
{
    class Empleado : INotifyPropertyChanged
    {
        // Cadena de conexión
        private const string cadenaConexionServidor = @"Data Source=DESKTOP-D21RT13;Initial Catalog=NORTHWIND;Integrated Security=SSPI";

        private int _EmployeeID;

        private string _LastName;

        private string _FirstName;

        private string _Title;

        private string _TitleOfCourtesy;

        private System.Nullable<System.DateTime> _BirthDate;

        private System.Nullable<System.DateTime> _HireDate;

        private string _Address;

        private string _City;

        private string _Region;

        private string _PostalCode;

        private string _Country;

        private string _HomePhone;

        private string _Extension;

        private byte[] _Photo;

        private string _Notes;

        private System.Nullable<int> _ReportsTo;

        private string _PhotoPath;


        public event PropertyChangedEventHandler PropertyChanged;


        public int EmployeeID
        {
            get
            {
                return this._EmployeeID;
            }
            set
            {
                if ((this._EmployeeID != value))
                {
                    this._EmployeeID = value;
                    this.SendPropertyChanged("EmployeeID");
                }
            }
        }

        public string LastName
        {
            get
            {
                return this._LastName;
            }
            set
            {
                if ((this._LastName != value))
                {
                    this._LastName = value;
                    this.SendPropertyChanged("LastName");
                }
            }
        }

        public string FirstName
        {
            get
            {
                return this._FirstName;
            }
            set
            {
                if ((this._FirstName != value))
                {
                    this._FirstName = value;
                    this.SendPropertyChanged("FirstName");
                }
            }
        }

        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                if ((this._Title != value))
                {
                    this._Title = value;
                    this.SendPropertyChanged("Title");
                }
            }
        }

        public string TitleOfCourtesy
        {
            get
            {
                return this._TitleOfCourtesy;
            }
            set
            {
                if ((this._TitleOfCourtesy != value))
                {
                    this._TitleOfCourtesy = value;
                    this.SendPropertyChanged("TitleOfCourtesy");
                }
            }
        }

        public System.Nullable<System.DateTime> BirthDate
        {
            get
            {
                return this._BirthDate;
            }
            set
            {
                if ((this._BirthDate != value))
                {
                    this._BirthDate = value;
                    this.SendPropertyChanged("BirthDate");
                }
            }
        }

        public System.Nullable<System.DateTime> HireDate
        {
            get
            {
                return this._HireDate;
            }
            set
            {
                if ((this._HireDate != value))
                {
                    this._HireDate = value;
                    this.SendPropertyChanged("HireDate");
                }
            }
        }

        public string Address
        {
            get
            {
                return this._Address;
            }
            set
            {
                if ((this._Address != value))
                {
                    this._Address = value;
                    this.SendPropertyChanged("Address");
                }
            }
        }

        public string City
        {
            get
            {
                return this._City;
            }
            set
            {
                if ((this._City != value))
                {
                    this._City = value;
                    this.SendPropertyChanged("City");
                }
            }
        }

        public string Region
        {
            get
            {
                return this._Region;
            }
            set
            {
                if ((this._Region != value))
                {
                    this._Region = value;
                    this.SendPropertyChanged("Region");
                }
            }
        }

        public string PostalCode
        {
            get
            {
                return this._PostalCode;
            }
            set
            {
                if ((this._PostalCode != value))
                {
                    this._PostalCode = value;
                    this.SendPropertyChanged("PostalCode");
                }
            }
        }

        public string Country
        {
            get
            {
                return this._Country;
            }
            set
            {
                if ((this._Country != value))
                {
                    this._Country = value;
                    this.SendPropertyChanged("Country");
                }
            }
        }

        public string HomePhone
        {
            get
            {
                return this._HomePhone;
            }
            set
            {
                if ((this._HomePhone != value))
                {
                    this._HomePhone = value;
                    this.SendPropertyChanged("HomePhone");
                }
            }
        }

        public string Extension
        {
            get
            {
                return this._Extension;
            }
            set
            {
                if ((this._Extension != value))
                {
                    this._Extension = value;
                    this.SendPropertyChanged("Extension");
                }
            }
        }

        public byte[] Photo
        {
            get
            {
                return this._Photo;
            }
            set
            {
                if ((this._Photo != value))
                {
                    this._Photo = value;
                    this.SendPropertyChanged("Photo");
                }
            }
        }

        public string Notes
        {
            get
            {
                return this._Notes;
            }
            set
            {
                if ((this._Notes != value))
                {
                    this._Notes = value;
                    this.SendPropertyChanged("Notes");
                }
            }
        }

        public System.Nullable<int> ReportsTo
        {
            get
            {
                return this._ReportsTo;
            }
            set
            {
                if ((this._ReportsTo != value))
                {
                    this._ReportsTo = value;
                    this.SendPropertyChanged("ReportsTo");
                }
            }
        }

        public string PhotoPath
        {
            get
            {
                return this._PhotoPath;
            }
            set
            {
                if ((this._PhotoPath != value))
                {
                    this._PhotoPath = value;
                    this.SendPropertyChanged("PhotoPath");
                }
            }
        }
        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public static ObservableCollection<Empleado> ObtenerEmpleados()
        {

            const string GetProductsQuery =
             " SELECT *                         " +
             " FROM Employees                   " +
             " ORDER BY FirstName, LastName     ";

            var empleados = new ObservableCollection<Empleado>();
            try
            {
                using (SqlConnection conn = new SqlConnection(cadenaConexionServidor))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = GetProductsQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var empleadoAux = new Empleado();

                                    // Para evitar el problema de que los valores sean Nulos en la BBDD 
                                    // y eso genere un error al asignarlos, se han creado unas funciones en que
                                    // comprueban si el valor es Nulo. Todas las funciones están en la clase "Utiles".

                                    empleadoAux.EmployeeID = Utiles.SafeGetInt32(reader, 0);
                                    empleadoAux.LastName = Utiles.SafeGetString(reader, 1);
                                    empleadoAux.FirstName = Utiles.SafeGetString(reader, 2);
                                    empleadoAux.Title = Utiles.SafeGetString(reader, 3);
                                    empleadoAux.TitleOfCourtesy = Utiles.SafeGetString(reader, 4);
                                    empleadoAux.BirthDate = Utiles.SafeGetDateTime(reader, 5);
                                    empleadoAux.HireDate = Utiles.SafeGetDateTime(reader, 6);
                                    empleadoAux.Address = Utiles.SafeGetString(reader, 7);
                                    empleadoAux.City = Utiles.SafeGetString(reader, 8);
                                    empleadoAux.Region = Utiles.SafeGetString(reader, 9);
                                    empleadoAux.PostalCode = Utiles.SafeGetString(reader, 10);
                                    empleadoAux.Country = Utiles.SafeGetString(reader, 11);
                                    empleadoAux.HomePhone = Utiles.SafeGetString(reader, 12);
                                    empleadoAux.Extension = Utiles.SafeGetString(reader, 13);
                                    empleadoAux.Photo = Utiles.SafeGetImage(reader, 14);
                                    empleadoAux.Notes = Utiles.SafeGetString(reader, 15);
                                    empleadoAux.ReportsTo = Utiles.SafeGetInt32(reader, 16);
                                    empleadoAux.PhotoPath = Utiles.SafeGetString(reader, 17);

                                    //pedido.OrderID = Utiles.SafeGetInt32(reader, 0);
                                    //pedido.CustomerID = Utiles.SafeGetString(reader, 1);
                                    //pedido.EmployeeID = Utiles.SafeGetInt32(reader, 2);
                                    //pedido.OrderDate = Utiles.SafeGetDateTime(reader, 3);
                                    //pedido.RequiredDate = Utiles.SafeGetDateTime(reader, 4);
                                    //pedido.ShippedDate = Utiles.SafeGetDateTime(reader, 5);
                                    //pedido.ShipVia = Utiles.SafeGetInt32(reader, 6);
                                    //pedido.Freight = Utiles.SafeGetDecimal(reader, 7);
                                    //pedido.ShipName = Utiles.SafeGetString(reader, 8);
                                    //pedido.ShipAddress = Utiles.SafeGetString(reader, 9);
                                    //pedido.ShipCity = Utiles.SafeGetString(reader, 10);
                                    //pedido.ShipRegion = Utiles.SafeGetString(reader, 11);
                                    //pedido.ShipPostalCode = Utiles.SafeGetString(reader, 12);
                                    //pedido.ShipCountry = Utiles.SafeGetString(reader, 13);
                                    //pedido.Empleado = Utiles.SafeGetString(reader, 14);
                                    //pedido.Cliente = Utiles.SafeGetString(reader, 15);
                                    //pedido.CompaniaTransporte = Utiles.SafeGetString(reader, 16);

                                    empleados.Add(empleadoAux);
                                }
                            }
                        }
                    }

                    return empleados;
                }
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;

        }
        public int Alta_Empleado()
        {
            // <param> Parámetro de salida .... Número del nuevo pedido o -1 si ha habido un error</param>
            // A la hora de crear la sentencia a ejecutar podemos crearla concatenando cadenas. Esto tiene dos problemas:            
            //
            //    1. Un ataque SQLInjection en los valores que pasamos.
            //    2. La dificultad de crear la cadena de la senytencia ... poner comillas, etc.
            //
            // Si utilizamos parámetros solucionamos los problemas anteriores.

            int returnvalue = -1;

            string Consulta = " INSERT  INTO Employees    " +
               " VALUES  ( @customerid, @EmployeeID, @OrderDate, @RequiredDate, @ShippedDate , " +
                " @ShipVia , @Freight , @ShipName , @ShipAddress , @ShipCity , @ShipRegion, @ShipPostalCode , @ShipCountry ) ;" +

                // Sentencia para que devuelva el nuevo ID de pedido que es autoincremental
                " SELECT SCOPE_IDENTITY();";



            try
            {
                using (SqlConnection conn = new SqlConnection((App.Current as App).ConnectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = Consulta;

                            // Un problema con los parámetros es que si tienen un valor no establecido válido
                            // nos dará un error. Para solucionarlo ponemos al valor ((object)XXXXX) ?? DBNull.Value
                            // de esta forma si el valor el nulo se le pasa un valor Nulo reconocido por el servidor de 
                            // bases de datos.
                            cmd.Parameters.AddWithValue("@customerid", ((object)CustomerID) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@EmployeeID", ((object)EmployeeID) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@OrderDate", ((object)OrderDate) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@RequiredDate", ((object)RequiredDate) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ShippedDate", ((object)ShippedDate) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ShipVia", ((object)ShipVia) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Freight", ((object)Freight) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ShipName", ((object)ShipName) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ShipAddress", ((object)ShipAddress) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ShipCity", ((object)ShipCity) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ShipRegion", ((object)ShipRegion) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ShipPostalCode", ((object)ShipPostalCode) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ShipCountry", ((object)ShipCountry) ?? DBNull.Value);

                            // EL valor devuelto corresponde con las filas afectadas por la sentencia

                            object returnObj = cmd.ExecuteScalar();

                            if (returnObj != null)
                            {
                                int.TryParse(returnObj.ToString(), out returnvalue);
                            }



                        }
                    }
                }

            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }

            return returnvalue;


        }

    }
}



}
