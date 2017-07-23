using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFServices.Dominio;
using WCFServices.Persistencia;

namespace WCFServices
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "TicketsSOA" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione TicketsSOA.svc o TicketsSOA.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class TicketsSOA : ITicketsSOA
    {
        private TicketDAO ticketDAO = new TicketDAO();
        public List<Ticket> ListarTickets()
        {
            return ticketDAO.ListarTicket();
        }
    }
}
