using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WCFServices.Dominio;

namespace WCFServices.Persistencia
{
    public class TicketDAO
    {

        private string CadenaConexion = "Data Source =(local); initial catalog=BD_GestionTickets; integrated security=true";

        public Ticket RegistrarTicket(Ticket TicketACrear)
        {
            Ticket TicketCreado = null;
            string sql = "INSERT INTO t_Ticket VALUES (@RucCliente, getdate(), @Descripcion, @CodigoEspecialidad, @Estado, null, NULL)";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add(new SqlParameter("@RucCliente", TicketACrear.RucCliente));
                    comando.Parameters.Add(new SqlParameter("@Descripcion", TicketACrear.Descripcion));
                    comando.Parameters.Add(new SqlParameter("@CodigoEspecialidad", TicketACrear.CodigoEspecialidad));
                    comando.Parameters.Add(new SqlParameter("@Estado", TicketACrear.Estado));
                    comando.ExecuteNonQuery();
                }
            }
            TicketCreado = ObtenerUltimoTicket();
            return TicketCreado;
        }


        public Ticket AtenderTicket(int CodigoTicket, string Respuesta, int DniEmpleado)
        {
            Ticket TicketAtendido = null;
            string sql = "UPDATE t_Ticket SET Respuesta=@Respuesta, Estado='Atendido', DniEmpleado=@DniEmpleado WHERE CodigoTicket=@CodigoTicket";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add(new SqlParameter("@Respuesta", Respuesta));
                    comando.Parameters.Add(new SqlParameter("@CodigoTicket", CodigoTicket));
                    comando.Parameters.Add(new SqlParameter("@DniEmpleado", DniEmpleado));
                    comando.ExecuteNonQuery();
                }
            }
            TicketAtendido = Obtener(CodigoTicket);
            return TicketAtendido;
        }

        public Ticket ObtenerUltimoTicket()
        {
            Ticket TicketEncontrado = null;
            string sql = "select * from t_Ticket where CodigoTicket= (select max(CodigoTicket) from t_Ticket)";

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    using (SqlDataReader resultado = comando.ExecuteReader())
                    {
                        if (resultado.Read())
                        {
                            TicketEncontrado = new Ticket();
                            TicketEncontrado.CodigoTicket = (int)resultado["CodigoTicket"];
                            TicketEncontrado.RucCliente = Convert.ToInt64(resultado["RucCliente"]);
                            TicketEncontrado.Fecha = (DateTime)resultado["Fecha"];
                            TicketEncontrado.Descripcion = (string)resultado["Descripcion"];
                            TicketEncontrado.CodigoEspecialidad = (string)resultado["CodigoEspecialidad"];
                            TicketEncontrado.Estado = (string)resultado["Estado"];
                        }
                    }
                }
            }
            return TicketEncontrado;

        }

        public Ticket Obtener(int CodigoTicket)
        {
            Ticket TicketEncontrado = null;
            string sql = "select * from t_Ticket where CodigoTicket=@CodigoTicket";

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add(new SqlParameter("@CodigoTicket", CodigoTicket));
                    using (SqlDataReader resultado = comando.ExecuteReader())
                    {
                        if (resultado.Read())
                        {
                            TicketEncontrado = new Ticket();
                            TicketEncontrado.CodigoTicket = Convert.ToInt32(resultado["CodigoTicket"]);
                            TicketEncontrado.RucCliente = Convert.ToInt64(resultado["RucCliente"]);
                            TicketEncontrado.Fecha = (DateTime)resultado["Fecha"];
                            TicketEncontrado.Descripcion = (string)resultado["Descripcion"];
                            if (resultado["CodigoEspecialidad"] == DBNull.Value)
                            {
                                TicketEncontrado.CodigoEspecialidad = null;
                            }
                            else
                            {
                                TicketEncontrado.CodigoEspecialidad = (string)resultado["CodigoEspecialidad"];
                            }

                            TicketEncontrado.Estado = (string)resultado["Estado"];
                            if (resultado["DniEmpleado"] == DBNull.Value)
                            {
                                TicketEncontrado.DniEmpleado = null;
                            }
                            else
                            {
                                TicketEncontrado.DniEmpleado = (int)resultado["DniEmpleado"];
                            }

                            if (resultado["Respuesta"] == DBNull.Value)
                            {
                                TicketEncontrado.Respuesta = null;
                            }
                            else
                            {
                                TicketEncontrado.Respuesta = (string)resultado["Respuesta"];
                            }
                        }
                    }
                }
            }
            return TicketEncontrado;

        }

        public List<Ticket> ListarTicket()
        {
          
            List<Ticket> TicketEncontrados = new List<Ticket>();
            Ticket TicketEncontrado = new Ticket();

            string sql = "select [CodigoTicket], c.[RazonSocial] , [RucCliente], [Fecha], tk.[Descripcion], e.Descripcion as 'Especialidad', tk.Estado, tk.DniEmpleado, em.Nombre, tk.CodigoEspecialidad, tk.Respuesta from [dbo].[t_Ticket] tk  inner join t_cliente c on c.Ruc=tk.RucCliente  inner join [dbo].[t_Especialidad] e on e.[Codigo]=tk.[CodigoEspecialidad]  left join [dbo].[t_Empleado] em on em.Dni=tk.DniEmpleado ";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    using (SqlDataReader resultado = comando.ExecuteReader())
                    {
                        while (resultado.Read())
                        {
                           
                            TicketEncontrado = new Ticket();
                            TicketEncontrado.CodigoTicket = Convert.ToInt32(resultado["CodigoTicket"]);
                            TicketEncontrado.RucCliente = Convert.ToInt64( resultado["RucCliente"]);
                            TicketEncontrado.RazonSocial = (string)resultado["RazonSocial"];
                            TicketEncontrado.Fecha = (DateTime)resultado["Fecha"];
                            TicketEncontrado.Descripcion = (string)resultado["Descripcion"];
                            if (resultado["CodigoEspecialidad"] == DBNull.Value)
                            {
                                TicketEncontrado.CodigoEspecialidad = null;
                            }
                            else
                            {
                                TicketEncontrado.CodigoEspecialidad = (string)resultado["CodigoEspecialidad"];
                            }

                       
                            TicketEncontrado.Especialidad = (string)resultado["Especialidad"];
                            TicketEncontrado.Estado = (string)resultado["Estado"];
                            if (resultado["DniEmpleado"] == DBNull.Value)
                            {
                                TicketEncontrado.DniEmpleado = null;
                            }
                            else {
                                TicketEncontrado.DniEmpleado = (int)resultado["DniEmpleado"];
                            }
                            if (resultado["Nombre"] == DBNull.Value)
                            {
                                TicketEncontrado.NombreEmpleado = null;
                            }
                            else {
                                TicketEncontrado.NombreEmpleado = (string)resultado["Nombre"];
                            }
                            if (resultado["Respuesta"] == DBNull.Value)
                            {
                                TicketEncontrado.Respuesta = null;
                            }
                            else
                            {
                                TicketEncontrado.Respuesta = (string)resultado["Respuesta"];
                            }
                            TicketEncontrados.Add(TicketEncontrado);
                        }
                    }
                }
            }

            return TicketEncontrados;
        }
    }
}