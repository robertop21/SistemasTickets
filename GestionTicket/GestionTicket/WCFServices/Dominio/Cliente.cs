using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFServices.Dominio
{
    [DataContract]
    public class Cliente
    {
        [DataMember]
        public Int64 Ruc { get; set; }

        [DataMember]
        public string Razonsocial { get; set; }

        [DataMember(IsRequired = false)]
        public string Estado { get; set; }
    }
}