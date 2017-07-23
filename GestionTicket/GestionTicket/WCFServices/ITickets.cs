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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ITickets" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ITickets
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "RegistrarTickets", ResponseFormat = WebMessageFormat.Json)]
        Ticket CrearTicket(Ticket TicketACrear);
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "ListarTickets", ResponseFormat = WebMessageFormat.Json)]
        List<Ticket> ListarTickets();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AtenderTicket/{CodigoTicket}/{Respuesta}/{DniEmpleado}", ResponseFormat = WebMessageFormat.Json)]
        Ticket AtenderTicket(string CodigoTicket, string Respuesta, string DniEmpleado);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "CapturarMensaje", ResponseFormat = WebMessageFormat.Json)]
        string CapturarMensaje();
    }
}
