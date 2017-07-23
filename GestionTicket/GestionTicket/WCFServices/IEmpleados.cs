using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WCFServices.Dominio;
using WCFServices.Errores;

namespace WCFServices
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IEmpleados" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IEmpleados
    { 
        [OperationContract]
        [WebInvoke(Method= "POST", UriTemplate="Empleados",ResponseFormat=WebMessageFormat.Json)]
        Empleado CrearEmpleado(Empleado EmpleadoACrear);
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Empleados/{dni}", ResponseFormat = WebMessageFormat.Json)]
        Empleado ObtenerEmpleado(string dni);
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "Empleados", ResponseFormat = WebMessageFormat.Json)]
        Empleado ModificarEmpleado(Empleado EmpleadoAModificar);
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "Empleados/{dni}", ResponseFormat = WebMessageFormat.Json)]
        void EliminarEmpleado(string dni);
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Empleados", ResponseFormat = WebMessageFormat.Json)]
        List<Empleado> ListarEmpleado();



        //TAREA
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "EmpleadosNombre/{Nombre}", ResponseFormat = WebMessageFormat.Json)]
        Empleado ObtenerEmpleado_x_Nombre(string Nombre);


        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "EmpleadosInactivo/{dni}", ResponseFormat = WebMessageFormat.Json)]

        void EliminarEmpleadoInactivo(string dni);

    }
}
