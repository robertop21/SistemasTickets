using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionTicket
{
    public class Ticket
    {
   
        public int CodigoTicket { get; set; }
        public Int64 RucCliente { get; set; }
        public DateTime Fecha { get; set; }
        public string RazonSocial { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> DniEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string Estado { get; set; }
        public string Especialidad { get; set; }
        public string CodigoEspecialidad { get; set; }
        public string Respuesta { get; set; }
    }
}