using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WCFServices.Dominio;
using WCFServices.Errores;
using WCFServices.Persistencia;

namespace WCFServices
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Tickets" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Tickets.svc o Tickets.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Tickets : ITickets
    {
        private TicketDAO TicketDAO = new TicketDAO();

        public Ticket CrearTicket(Ticket TicketACrear)
        {
            DateTime fecha_sistema = Convert.ToDateTime("22:00:00");
            //DateTime fecha_registro = new DateTime(TicketACrear.Fecha.Day, TicketACrear.Fecha.Hour,TicketACrear.Fecha.Minute,TicketACrear.Fecha.Second);
            DateTime fecha_registro = DateTime.Now;
            if (fecha_registro > fecha_sistema) { 
                throw new WebFaultException<RepetidoException>(new RepetidoException()
                {
                    Codigo = "404",
                    Descripcion = "En estos momentos no podemos atenderlo"
                }, HttpStatusCode.Conflict);
            }
            return TicketDAO.RegistrarTicket(TicketACrear);
        }
        public List<Ticket> ListarTickets()
        {
            return TicketDAO.ListarTicket();
        }


        public Ticket AtenderTicket(string CodigoTicket, string Respuesta, string DniEmpleado)
        {
            Ticket oTicket = new Ticket();
            List<Ticket> TicketsPrioritarios = TicketDAO.ListarTicket();
            string strEspecialidad = "";
            string strEstado = "";

            foreach (var datos in TicketsPrioritarios)
            {
                if (datos.CodigoTicket.ToString() == CodigoTicket)
                {
                    strEspecialidad = datos.CodigoEspecialidad;
                    strEstado = datos.Estado;
                }
            }

            string stresultado = "";
            foreach (var respuesta in TicketsPrioritarios)
            {
      
                if (respuesta.CodigoEspecialidad == "E002" && respuesta.Estado == "Generado")
                {
                    stresultado = "True";
                    break;
                }
                else
                {
                    stresultado = "False";
                }
               
            }

             
                if (strEspecialidad != "E002" && strEstado == "Generado" && stresultado == "False")
                {
                    oTicket.CodigoTicket = int.Parse(CodigoTicket);
                    oTicket.Respuesta = Respuesta;
                    oTicket.DniEmpleado = int.Parse(DniEmpleado);

                    string rutaCola = @".\private$\RespuestaAtencion";
                    if (!MessageQueue.Exists(rutaCola))
                        MessageQueue.Create(rutaCola);
                    MessageQueue cola = new MessageQueue(rutaCola);
                    Message mensaje = new Message();
                    mensaje.Label = "MensajeRespuesta";
                    mensaje.Body = oTicket;
                    cola.Send(mensaje);

                }
                else if (strEspecialidad == "E002" && strEstado == "Generado" && stresultado == "True")
                {

                oTicket.CodigoTicket = int.Parse(CodigoTicket);
                oTicket.Respuesta = Respuesta;
                oTicket.DniEmpleado = int.Parse(DniEmpleado);

                string rutaCola = @".\private$\RespuestaAtencion";
                if (!MessageQueue.Exists(rutaCola))
                    MessageQueue.Create(rutaCola);
                MessageQueue cola = new MessageQueue(rutaCola);
                Message mensaje = new Message();
                mensaje.Label = "MensajeRespuesta";
                mensaje.Body = oTicket;
                cola.Send(mensaje);
                }

                else if (strEspecialidad != "E002" && strEstado == "Generado" && stresultado =="True")
                {
                   
                    throw new WebFaultException<RepetidoException>(new RepetidoException()
                    {
                        Codigo = "501",
                        Descripcion = "Existe una Atencion Prioritaria Servidor que debe ser atendida "
                    }, HttpStatusCode.Conflict);
                }
            
            return oTicket;

        }

        public string CapturarMensaje()
        {
            string rutaCola = @".\private$\RespuestaAtencion";
            if (!MessageQueue.Exists(rutaCola))
                MessageQueue.Create(rutaCola);
            MessageQueue cola = new MessageQueue(rutaCola);

            System.Messaging.Message[] msg;
            msg = cola.GetAllMessages();

            for (int i = 0; i < msg.Length; i++)
            {
                cola.Formatter = new XmlMessageFormatter(new Type[] { typeof(Ticket) });
                Message mensaje = cola.Receive();

                Ticket oTicket = (Ticket)mensaje.Body;
                TicketDAO.AtenderTicket(oTicket.CodigoTicket, oTicket.Respuesta, (int)oTicket.DniEmpleado);
          
            }
      
            return "Terminado"; 
        }

    }
}
