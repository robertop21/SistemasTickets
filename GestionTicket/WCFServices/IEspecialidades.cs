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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEspecialidades" in both code and config file together.
    [ServiceContract]
    public interface IEspecialidades
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Especialidades", ResponseFormat = WebMessageFormat.Json)]
        Especialidad CrearEspecialidad(Especialidad especialidadCrear);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Especialidades/{codigo}", ResponseFormat = WebMessageFormat.Json)]
        Especialidad ObtenerEspecialidad(string codigo);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "Especialidades", ResponseFormat = WebMessageFormat.Json)]
        Especialidad ModificarEspecialidad(Especialidad especialidadModificar);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "Especialidades/{codigo}", ResponseFormat = WebMessageFormat.Json)]
        void EliminarEspecialidad(string codigo);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Especialidades", ResponseFormat = WebMessageFormat.Json)]
        List<Especialidad> ListarEspecialidad();


        //TAREA
        /*[OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "EmpleadosNombre/{Nombre}", ResponseFormat = WebMessageFormat.Json)]
        Empleado ObtenerEmpleado_x_Nombre(string Nombre);*/


        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "EspecialidadesInactivo/{Codigo}", ResponseFormat = WebMessageFormat.Json)]

        void EliminarEspecialidadInactivo(string codigo);

    }
}
