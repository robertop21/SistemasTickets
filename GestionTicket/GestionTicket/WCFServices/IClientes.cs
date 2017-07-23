using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WCFServices.Dominio;

namespace WCFServices
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IClientes" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IClientes
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Clientes", ResponseFormat = WebMessageFormat.Json)]
        Cliente CrearCliente(Cliente clienteACrear);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Clientes/{ruc}", ResponseFormat = WebMessageFormat.Json)]
        Cliente ObtenerCliente(string ruc);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "Clientes", ResponseFormat = WebMessageFormat.Json)]
        Cliente ModificarCliente(Cliente clienteAModificar);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "Clientes/{ruc}", ResponseFormat = WebMessageFormat.Json)]
        void EliminarCliente(string ruc);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Clientes", ResponseFormat = WebMessageFormat.Json)]
        List<Cliente> ListarClientes();

        //TAREA
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "ClientesRazonSocial/{razonsocial}", ResponseFormat = WebMessageFormat.Json)]
        Cliente ObtenerCliente_x_Razonsocial(string razonsocial);


        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "ClientesInactivo/{ruc}", ResponseFormat = WebMessageFormat.Json)]

        void EliminarClienteInactivo(string ruc);
    }
}
