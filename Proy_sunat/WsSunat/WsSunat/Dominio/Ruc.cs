﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WsSunat.Dominio
{
    [DataContract]
    public class Ruc
    {
        [DataMember]
        public string codigo { get; set; }
        [DataMember]
        public string ruc { get; set; }
    }
}