using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel;
namespace GestionTicket
{
    public partial class Tickets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Listar();
            }
        }


        void Listar()
        {
            SW_AtencionTicket.TicketsSOAClient proxy = new SW_AtencionTicket.TicketsSOAClient();
            this.dvAtencionTickets.DataSource = proxy.ListarTickets();
            this.dvAtencionTickets.DataBind();
        }

        protected void dvAtencionTickets_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
            this.txtRespuesta.Text = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.
              Create("http://localhost:36485/Empleados.svc/Empleados");
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Empleado> ListardoEmpleado = js.Deserialize<List<Empleado>>(tramaJson);

            ddlEmpleado.DataSource = ListardoEmpleado.Where(x => x.CodigoEspecialidad == dvAtencionTickets.SelectedRow.Cells[5].Text);
            ddlEmpleado.DataTextField = "Nombre";
            ddlEmpleado.DataValueField = "dni";
            ddlEmpleado.DataBind();
        }

        protected void CmdAtender_Click(object sender, EventArgs e)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                string sCodigoTicket = dvAtencionTickets.SelectedRow.Cells[1].Text;
                string sRespuesta = this.txtRespuesta.Text;
                string sDniEmpleado = this.ddlEmpleado.SelectedValue.ToString();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:36485/Tickets.svc/AtenderTicket/" + sCodigoTicket + "/" + sRespuesta + "/" + sDniEmpleado);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = 0;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string tramaJson = reader.ReadToEnd();
                Ticket oTicketMensaje = js.Deserialize<Ticket>(tramaJson);
                this.lblMensaje.Text = "Se envió la respuesta correctamente.";
                this.txtRespuesta.Text = "";
            }
             catch (WebException ex)
            {
                HttpStatusCode codigo = ((HttpWebResponse)ex.Response).StatusCode;
                StreamReader reader = new StreamReader(ex.Response.GetResponseStream());
                string tramaJson = reader.ReadToEnd();
                RepetidoException error = js.Deserialize<RepetidoException>(tramaJson);
                this.lblMensaje.Text = error.Codigo + "  " + error.Descripcion;
                //    Assert.AreEqual(HttpStatusCode.Conflict, codigo);
                //    Assert.AreEqual("El Cliente ya existe", error.Descripcion);
            }
        }
    }
}