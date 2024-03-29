﻿using System;
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
        private const string cadenaConexionServidor = @"Data Source=1C1B0D57A991\SQLEXPRESS;Initial Catalog=NORTHWIND;Integrated Security=SSPI";

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

        public Empleado()
        {
            this.PhotoPath = "Assets/SplashScreen.scale-200.png";
            this.BirthDate = DateTime.Now.AddDays(-5000);
            this.HireDate = DateTime.Now;
            // Hay una validación de integridad referencial con la propia tabla
            this.ReportsTo = null;
 
        }

        public static ObservableCollection<Empleado> ObtenerEmpleados()
        {

            const string GetProductsQuery =
             " SELECT *                         " +
             " FROM Employees                   " +
             " ORDER BY LastName, FirstName     ";

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
               " VALUES  ( @LastName, @FirstName, @Title, @TitleofCourtesy, @BirthDate, @HireDate , " +
                " @Adress , @City , @Region , @PostalCode , @Country , @HomePhone, @Extension , @Photo, @Notes, @ReportsTo, @PhotoPath ) ;" +

                // Sentencia para que devuelva el nuevo ID de pedido que es autoincremental
                " SELECT SCOPE_IDENTITY();";



            try
            {
                using (SqlConnection conn = new SqlConnection(cadenaConexionServidor))
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
                            cmd.Parameters.AddWithValue("@LastName", ((object)LastName) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@FirstName", ((object)FirstName) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Title", ((object)Title) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@TitleofCourtesy", ((object)TitleOfCourtesy) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@BirthDate", ((object)BirthDate) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@HireDate", ((object)HireDate) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Adress", ((object)Address) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@City", ((object)City) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Region", ((object)Region) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@PostalCode", ((object)PostalCode) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Country", ((object)Country) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@HomePhone", ((object)HomePhone) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Extension", ((object)Extension) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Notes", ((object)Notes) ?? DBNull.Value);

                            if ((ReportsTo != null) && (ReportsTo > 0))
                            {
                                cmd.Parameters.AddWithValue("@ReportsTo", ReportsTo);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ReportsTo", DBNull.Value);

                            }
                            
                            cmd.Parameters.AddWithValue("@PhotoPath", ((object)PhotoPath) ?? DBNull.Value);

                            if (Photo != null)
                            {
                                cmd.Parameters.AddWithValue("@Photo", ((object)Photo) ?? DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Photo", DBNull.Value);
                            }
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

        public bool Actualizar_Datos_Empleado()
        {
            // <param> Parámetro de salida .... Número del nuevo pedido o -1 si ha habido un error</param>
            // A la hora de crear la sentencia a ejecutar podemos crearla concatenando cadenas. Esto tiene dos problemas:            
            //
            //    1. Un ataque SQLInjection en los valores que pasamos.
            //    2. La dificultad de crear la cadena de la senytencia ... poner comillas, etc.
            //
            // Si utilizamos parámetros solucionamos los problemas anteriores.

            

            string Consulta = " Update Employees    " +
               " SET " +
               " LastName = @LastName , " +
               " FirstName = @FirstName, " +
               " Title = @Title, " +
               " TitleofCourtesy = @TitleofCourtesy, " +
               " BirthDate = @BirthDate, " +
               " HireDate = @HireDate , " +
               " Address = @Adress , " +
               " City = @City , " +
               " Region = @Region , " +
               " PostalCode = @PostalCode , " +
               " Country = @Country , " +
               " HomePhone = @HomePhone, " +
               " Extension = @Extension , " +
               " Photo = @Photo, " +
               " Notes = @Notes, " +
               " ReportsTo = @ReportsTo," +
               " PhotoPath =  @PhotoPath " +
               " WHERE EmployeeID = @EmployeeID ;";

            try
            {
                using (SqlConnection conn = new SqlConnection(cadenaConexionServidor))
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
                            cmd.Parameters.AddWithValue("@EmployeeID", ((object)EmployeeID) ?? DBNull.Value);

                            cmd.Parameters.AddWithValue("@LastName", ((object)LastName) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@FirstName", ((object)FirstName) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Title", ((object)Title) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@TitleofCourtesy", ((object)TitleOfCourtesy) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@BirthDate", ((object)BirthDate) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@HireDate", ((object)HireDate) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Adress", ((object)Address) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@City", ((object)City) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Region", ((object)Region) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@PostalCode", ((object)PostalCode) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Country", ((object)Country) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@HomePhone", ((object)HomePhone) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Extension", ((object)Extension) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Notes", ((object)Notes) ?? DBNull.Value);
                            if ((ReportsTo != null) && (ReportsTo > 0))
                            {
                                cmd.Parameters.AddWithValue("@ReportsTo", ReportsTo);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ReportsTo", DBNull.Value);

                            }
                            cmd.Parameters.AddWithValue("@PhotoPath", ((object)PhotoPath) ?? DBNull.Value);

                            if (Photo != null)
                            {
                                cmd.Parameters.AddWithValue("@Photo", ((object)Photo) ?? DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Photo", DBNull.Value);
                            }
                            // EL valor devuelto corresponde con las filas afectadas por la sentencia

                            return (cmd.ExecuteNonQuery() == 1);


                        }
                    }
                }

            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }

            return false;


        }

        public bool Borrar_Empleado()
        {
            // <param> Parámetro de salida .... </param>
            // A la hora de crear la sentencia a ejecutar podemos crearla concatenando cadenas. Esto tiene dos problemas:            
            //
            //    1. Un ataque SQLInjection en los valores que pasamos.
            //    2. La dificultad de crear la cadena de la senytencia ... poner comillas, etc.
            //
            // Si utilizamos parámetros solucionamos los problemas anteriores.

            if (this.EmployeeID > 0)

            {

                string Consulta = " DELETE                  " +
                                  " FROM Employees             " +
                                  " WHERE EmployeeID = @EmployeeID";

                try
                {
                    using (SqlConnection conn = new SqlConnection(cadenaConexionServidor))
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
                                cmd.Parameters.AddWithValue("@EmployeeID", ((object)EmployeeID) ?? DBNull.Value);

                                // EL valor devuelto corresponde con las filas afectadas por la sentencia

                                return (cmd.ExecuteNonQuery() == 1);

                            }
                        }
                    }

                }
                catch (Exception eSql)
                {
                    Debug.WriteLine("Exception: " + eSql.Message);
                }
            }
            else
            {
                Debug.WriteLine("Error : Intento de borrar un pedido sin número de pedido válido");
            }

            return false;

        }


    }
}



