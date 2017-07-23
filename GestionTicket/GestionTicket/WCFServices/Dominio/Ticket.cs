
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFServices.Dominio
{
    [DataContract]
    public class Ticket
    {
        [DataMember]
        public int CodigoTicket { get; set; }
        [DataMember]
        public Int64 RucCliente { get; set; }
        [DataMember]
        public DateTime Fecha { get; set; }
        [DataMember]
        public string RazonSocial { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public Nullable<int> DniEmpleado { get; set; }
        [DataMember]
        public string NombreEmpleado { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public string Especialidad { get; set; }
        [DataMember]
        public string CodigoEspecialidad { get; set; }
        [DataMember]
        public string Respuesta { get; set; }
        
    }
}