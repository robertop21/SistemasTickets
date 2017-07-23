using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WsSunat.Dominio;

namespace WsSunat
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWcfSunat" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWcfSunat
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Ruc/{ruc}", ResponseFormat = WebMessageFormat.Json)]
        Ruc ObtenerRUC(string ruc);
    }
}
