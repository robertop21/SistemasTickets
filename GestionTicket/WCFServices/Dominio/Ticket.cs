
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
        public string Codigo { get; set; }
        [DataMember]
        public Int32 RucCliente { get; set; }
        [DataMember]
        public string RazonSocial { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public int DniEmpleado { get; set; }
        [DataMember]
        public string Estado { get; set; }
    }
}