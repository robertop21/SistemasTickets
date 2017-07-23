using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionTicket
{
    public class Empleado
    {
        public int dni { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public string CodigoEspecialidad { get; set; }
        public bool Certificado { get; set; }

        public int Edad { get; set; }

    }
}