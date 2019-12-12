using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_AccesoDatos_SQLServer_NorthWind_Clientes.Models
{
    class Cliente : INotifyPropertyChanged
    {
        // Cadena de conexión
        private const string cadenaConexionServidor = @"Data Source=1C1B0D57A991\SQLEXPRESS;Initial Catalog=NORTHWIND;Integrated Security=SSPI";

        private string _CustomerID;

        private string _CompanyName;

        private string _ContactName;

        private string _ContactTitle;

        private string _Address;

        private string _City;

        private string _Region;

        private string _PostalCode;

        private string _Country;

        private string _Phone;

        private string _Fax;


        public string CustomerID
        {
            get
            {
                return this._CustomerID;
            }
            set
            {
                if ((this._CustomerID != value))
                {
                    this._CustomerID = value;
                    this.SendPropertyChanged("CustomerID");                 
                }
            }
        }

        public string CompanyName
        {
            get
            {
                return this._CompanyName;
            }
            set
            {
                if ((this._CompanyName != value))
                {
                    this._CompanyName = value;
                    this.SendPropertyChanged("CompanyName");      
                }
            }
        }

        public string ContactName
        {
            get
            {
                return this._ContactName;
            }
            set
            {
                if ((this._ContactName != value))
                {
                    this._ContactName = value;
                    this.SendPropertyChanged("ContactName");
        
                }
            }
        }

        public string ContactTitle
        {
            get
            {
                return this._ContactTitle;
            }
            set
            {
                if ((this._ContactTitle != value))
                {
                    this._ContactTitle = value;
                    this.SendPropertyChanged("ContactTitle");
        
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

        public string Phone
        {
            get
            {
                return this._Phone;
            }
            set
            {
                if ((this._Phone != value))
                {
                    this._Phone = value;
                    this.SendPropertyChanged("Phone");
                }
            }
        }

        public string Fax
        {
            get
            {
                return this._Fax;
            }
            set
            {
                if ((this._Fax != value))
                {
                    this._Fax = value;
                    this.SendPropertyChanged("Fax");
                }
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
                              
        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Cliente()
        {            

        }

        public static ObservableCollection<Cliente> Obtener_Clientes(String filtro_nombre)
        {

            string GetProductsQuery =
             " SELECT *                            " +
             " FROM Customers                      ";

            if (filtro_nombre.Trim() != "")
            {
                GetProductsQuery += " WHERE UPPER(companyname) LIKE '%"+ filtro_nombre.Trim().ToUpper() + "%' ";

            }

             GetProductsQuery += " ORDER BY CompanyName, ContactName   ";

            var clientes = new ObservableCollection<Cliente>();
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
                            //cmd.Parameters.AddWithValue("@CompanyName", ((object)filtro_nombre.ToUpper()) ?? DBNull.Value);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var clienteAux = new Cliente();

                                    // Para evitar el problema de que los valores sean Nulos en la BBDD 
                                    // y eso genere un error al asignarlos, se han creado unas funciones en que
                                    // comprueban si el valor es Nulo. Todas las funciones están en la clase "Utiles".

                                    clienteAux.CustomerID = Utiles.SafeGetString(reader, 0);
                                    clienteAux.CompanyName = Utiles.SafeGetString(reader, 1);
                                    clienteAux.ContactName = Utiles.SafeGetString(reader, 2);
                                    clienteAux.ContactTitle = Utiles.SafeGetString(reader, 3);
                                    clienteAux.Address = Utiles.SafeGetString(reader, 4);
                                    clienteAux.City = Utiles.SafeGetString(reader, 5);
                                    clienteAux.Region = Utiles.SafeGetString(reader, 6);
                                    clienteAux.PostalCode = Utiles.SafeGetString(reader, 7);
                                    clienteAux.Country = Utiles.SafeGetString(reader, 8);
                                    clienteAux.Phone = Utiles.SafeGetString(reader, 9);
                                    clienteAux.Fax = Utiles.SafeGetString(reader, 10);

                                    clientes.Add(clienteAux);
                                }
                            }
                        }
                    }

                    return clientes;
                }
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;

        }

        public bool Alta_Cliente()
        {
            // <param> Parámetro de salida .... Número del nuevo pedido o -1 si ha habido un error</param>
            // A la hora de crear la sentencia a ejecutar podemos crearla concatenando cadenas. Esto tiene dos problemas:            
            //
            //    1. Un ataque SQLInjection en los valores que pasamos.
            //    2. La dificultad de crear la cadena de la senytencia ... poner comillas, etc.
            //
            // Si utilizamos parámetros solucionamos los problemas anteriores.

            int returnvalue = -1;

            string Consulta = " INSERT  INTO Customers    " +
               " VALUES  ( @CustomerID, @CompanyName, @ContactName , @ContactTitle, @Address, " +
               "           @City, @Region, @PostalCode, @Country, @Phone, @Fax) ";

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
                            // de esta forma si el valor es nulo se le pasa un valor Nulo reconocido por el servidor de 
                            // bases de datos.

                 
                            cmd.Parameters.AddWithValue("@CustomerID", ((object)CustomerID) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@CompanyName", ((object)CompanyName) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ContactName", ((object)ContactName) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ContactTitle", ((object)ContactTitle) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Address", ((object)Address) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@City", ((object)City) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Region", ((object)Region) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@PostalCode", ((object)PostalCode) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Country", ((object)Country) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Phone", ((object)Phone) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Fax", ((object)Fax) ?? DBNull.Value);
                          
                            object returnObj = cmd.ExecuteNonQuery();

                            if ((returnObj != null))
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

            return returnvalue == 1;
        }

        public bool Actualizar_Datos_Cliente()
        {
            // <param> Parámetro de salida .... Número del nuevo pedido o -1 si ha habido un error</param>
            // A la hora de crear la sentencia a ejecutar podemos crearla concatenando cadenas. Esto tiene dos problemas:            
            //
            //    1. Un ataque SQLInjection en los valores que pasamos.
            //    2. La dificultad de crear la cadena de la senytencia ... poner comillas, etc.
            //
            // Si utilizamos parámetros solucionamos los problemas anteriores.



            string Consulta = " Update Customers    " +
               " SET " +
               " CompanyName = @CompanyName , " +
               " ContactName = @ContactName, " +
               " ContactTitle = @ContactTitle, " +
               " Address = @Address, " +
               " City = @City, " +
               " Region = @Region, " +
               " PostalCode = @PostalCode , " +
               " Country = @Country , " +
               " Phone = @Phone , " +
               " Fax = @Fax   " +
               " WHERE CustomerID  = @CustomerID ";

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
                            cmd.Parameters.AddWithValue("@CustomerID", ((object)CustomerID) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@CompanyName", ((object)CompanyName) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ContactName", ((object)ContactName) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ContactTitle", ((object)ContactTitle) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Address", ((object)Address) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@City", ((object)City) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Region", ((object)Region) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@PostalCode", ((object)PostalCode) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Country", ((object)Country) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Phone", ((object)Phone) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Fax", ((object)Fax) ?? DBNull.Value);
                            
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

        public bool Borrar_Cliente()
        {
            // <param> Parámetro de salida .... </param>
            // A la hora de crear la sentencia a ejecutar podemos crearla concatenando cadenas. Esto tiene dos problemas:            
            //
            //    1. Un ataque SQLInjection en los valores que pasamos.
            //    2. La dificultad de crear la cadena de la senytencia ... poner comillas, etc.
            //
            // Si utilizamos parámetros solucionamos los problemas anteriores.

            if (this.CustomerID.Length > 0)

            {

                string Consulta = " DELETE                  " +
                                  " FROM Customers             " +
                                  " WHERE CustomerID = @CustomerID";

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
                                cmd.Parameters.AddWithValue("@CustomerID", ((object)CustomerID) ?? DBNull.Value);

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
                Debug.WriteLine("Error : Intento de borrar un cliente sin código de cliente válido");
            }

            return false;

        }


    }
}
