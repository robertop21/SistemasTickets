
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFServices.Dominio
{
    [DataContract]
    public class Empleado
    {
        [DataMember]
        public int dni { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember(IsRequired=false)]
        public string Estado { get; set; }
        [DataMember]
        public bool Certificado { get; set; }
        [DataMember]
        public int Edad { get; set; }
    }


}