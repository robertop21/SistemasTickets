using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WCFServices.Dominio;



namespace WCFServices.Persistencia
{
    public class ClienteDAO
    {
        private string CadenaConexion = "Data Source=(local);Initial Catalog=BD_GestionTickets;Integrated Security=SSPI;";

        public Cliente Crear(Cliente clienteACrear)
        {
            Cliente clienteCreado = null;
            string sentencia = "INSERT INTO t_cliente VALUES (@ruc, @raz, @est)";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sentencia, conexion))
                {
                    comando.Parameters.Add(new SqlParameter("@ruc", clienteACrear.Ruc));
                    comando.Parameters.Add(new SqlParameter("@raz", clienteACrear.Razonsocial));
                    comando.Parameters.Add(new SqlParameter("@est", clienteACrear.Estado));
                    comando.ExecuteNonQuery();
                }
            }
            clienteCreado = Obtener(clienteACrear.Ruc);
            return clienteCreado;
        }

        internal Cliente Crear(long ruc)
        {
            throw new NotImplementedException();
        }

        public Cliente Obtener(Int64 ruc)
        {
            Cliente clienteEncontrado = null;
            string sentencia = "SELECT * FROM t_cliente WHERE Ruc=@ruc";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sentencia, conexion))
                {
                    comando.Parameters.Add(new SqlParameter("@ruc", ruc));
                    using (SqlDataReader resultado = comando.ExecuteReader())
                    {
                        if (resultado.Read())
                        {
                            clienteEncontrado = new Cliente()
                            {
                                Ruc = Convert.ToInt64(resultado["Ruc"]),
                                Razonsocial = (string)resultado["RazonSocial"],
                                Estado = (string)resultado["Estado"]
                            };
                        }
                    }
                }
            }
            return clienteEncontrado;
        }
        public Cliente ObtenerCliente_x_Razonsocial(string razonsocial)
        {
            Cliente ClienteEncontrado = null;
            string sql = "select * from t_cliente where RazonSocial=@razonsocial";

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add(new SqlParameter("@razonsocial", razonsocial));
                    using (SqlDataReader resultado = comando.ExecuteReader())
                    {
                        if (resultado.Read())
                        {
                            ClienteEncontrado = new Cliente();
                            ClienteEncontrado.Ruc = (int)resultado["Ruc"];
                            ClienteEncontrado.Razonsocial = (string)resultado["RazonSocial"];
                            ClienteEncontrado.Estado = (string)resultado["Estado"];
                        }
                    }
                }
            }
            return ClienteEncontrado;

        }
        public Cliente Modificar(Cliente clienteAModificar)
        {
            Cliente clienteModificado = null;
            string sentencia = "UPDATE t_cliente SET RazonSocial=@raz, Estado=@est WHERE Ruc=@ruc";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sentencia, conexion))
                {
                    comando.Parameters.Add(new SqlParameter("@raz", clienteAModificar.Razonsocial));
                    comando.Parameters.Add(new SqlParameter("@est", clienteAModificar.Estado));
                    comando.Parameters.Add(new SqlParameter("@ruc", clienteAModificar.Ruc));
                    comando.ExecuteNonQuery();
                }
            }
            clienteModificado = Obtener(clienteAModificar.Ruc);
            return clienteModificado;
        }

        public void Eliminar(Int64 ruc)
        {
            string sentencia = "DELETE FROM t_cliente WHERE Ruc=@ruc";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sentencia, conexion))
                {
                    comando.Parameters.Add(new SqlParameter("@ruc", ruc));
                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<Cliente> Listar()
        {
            List<Cliente> clientesEncontrados = new List<Cliente>();
            Cliente clienteEncontrado = null;
            string sentencia = "SELECT * FROM t_cliente";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sentencia, conexion))
                {
                    using (SqlDataReader resultado = comando.ExecuteReader())
                    {
                        while (resultado.Read())
                        {
                            clienteEncontrado = new Cliente()
                            {
                                Ruc = (int)resultado["Ruc"],
                                Razonsocial = (string)resultado["RazonSocial"],
                                Estado = (string)resultado["Estado"]
                            };
                            clientesEncontrados.Add(clienteEncontrado);
                        }
                    }
                }
            }
            return clientesEncontrados;
        }

    }
}