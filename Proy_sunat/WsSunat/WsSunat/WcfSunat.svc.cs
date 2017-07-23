using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WsSunat.Dominio;
using WsSunat.Errores;
using WsSunat.Persistencia;

namespace WsSunat
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfSunat" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfSunat.svc o WcfSunat.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfSunat : IWcfSunat
    {
        private RucDAO rucDAO = new RucDAO();
        public Ruc ObtenerRUC(string ruc)
        {
            

            if (rucDAO.Obtener_ruc(ruc) == null)
            {
                throw new WebFaultException<nullExcepcion>(
                    new nullExcepcion()
                    {
                        Codigo = "110",
                        Descripcion = "El ruc no existe o no se ha ingresado corectamente."
                    }, HttpStatusCode.Conflict);
            }

            return rucDAO.Obtener_ruc(ruc);
        }
    }
}
