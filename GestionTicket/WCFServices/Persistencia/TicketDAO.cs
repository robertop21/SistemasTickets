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

        private string CadenaConexion = "Data Source =.; initial catalog=BD_GestionTickets; integrated security=true";

        public List<Ticket> Listar()
        {
          
            List<Ticket> TicketEncontrados = new List<Ticket>();
            Ticket TicketEncontrado = new Ticket();
           
            string sql = "select * from Ticket tk inner join t_cliente c on c.Ruc=tk.RucCliente where tk.Estado='Generado'";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    using (SqlDataReader resultado = comando.ExecuteReader())
                    {
                        while (resultado.Read())
                        {
                           
                            //TicketEncontrado = new Ticket();
                            //TicketEncontrado. = (int)resultado["Dni"];
                            //TicketEncontrado. = (string)resultado["Nombre"];
                            //TicketEncontrado.Estado = (string)resultado["Estado"];
                            //TicketEncontrado.Edad = (int)resultado["Edad"];
                            //TicketEncontrado.Certificado = (bool)resultado["Certificado"];
                            //TicketEncontrados.Add(TicketEncontrado);
                        }
                    }
                }
            }

            return TicketEncontrados;
        }
    }
}