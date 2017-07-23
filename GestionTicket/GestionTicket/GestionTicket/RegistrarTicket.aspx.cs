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

namespace GestionTicket
{
    public partial class RegistrarTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {


                HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:36485/Especialidades.svc/Especialidades");
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string tramaJson = reader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                List<Especialidad> ListardoEspecialidad = js.Deserialize<List<Especialidad>>(tramaJson);

                ddlEspecialidad.DataSource = ListardoEspecialidad;
                ddlEspecialidad.DataTextField = "Descripcion";
                ddlEspecialidad.DataValueField = "Codigo";
                ddlEspecialidad.DataBind();

                Listar();
            }
        }

        void Limpiar() {
            this.ddlEspecialidad.SelectedIndex = 0;
            this.txtDescripcion.Text = "";
            this.txtRucCliente.Text = "";
        }
        protected void cmdNuevo_Click(object sender, EventArgs e)
        {
            this.ddlEspecialidad.SelectedIndex = 0;
            this.txtDescripcion.Text = "";
            this.txtRucCliente.Text = "";
            this.txtRucCliente.Focus();
        }

        void Listar() {
            HttpWebRequest request = (HttpWebRequest)WebRequest.
            Create("http://localhost:36485/Tickets.svc/ListarTickets");
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Ticket> Listado = js.Deserialize<List<Ticket>>(tramaJson);


            this.GridView1.DataSource = Listado;
            this.GridView1.DataBind();
        

        }

        bool ValidarExistenciaCliente(string sRuc) {
            HttpWebRequest request = (HttpWebRequest)WebRequest.
              Create("http://localhost:36485/Clientes.svc/Clientes/" + sRuc);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Cliente ClienteObtenido = js.Deserialize<Cliente>(tramaJson);

            if (ClienteObtenido != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void cmdGrabar_Click(object sender, EventArgs e)
        {
            if (ValidarExistenciaCliente(this.txtRucCliente.Text) == false) {
                this.lblMensaje.Text = "No se grabaron los datos. No se encontro el Cliente";
                return;
            }

            JavaScriptSerializer js = new JavaScriptSerializer();

            Ticket oTicket = new Ticket();
            oTicket.RucCliente = Convert.ToInt64(this.txtRucCliente.Text);
            oTicket.Descripcion = this.txtDescripcion.Text;
            oTicket.CodigoEspecialidad = ddlEspecialidad.SelectedValue.ToString();
            oTicket.Estado = "Generado";
            oTicket.Fecha = DateTime.Now;
            oTicket.DniEmpleado = 0;
            oTicket.CodigoTicket = 0;

            string postdata = js.Serialize(oTicket);
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:36485/Tickets.svc/RegistrarTickets");
            request.Method = "POST";
            request.ContentLength = data.Length;
            request.ContentType = "application/json";

            var requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string tramaJson = reader.ReadToEnd();

                Ticket TicketCreado = js.Deserialize<Ticket>(tramaJson);
                this.lblMensaje.Text = "Se grabo correctamente. Codigo Ticket : " + TicketCreado.CodigoTicket.ToString();
                Limpiar();
                Listar();
                this.txtRucCliente.Focus();
            }
            catch (WebException ex)
            {
                HttpStatusCode codigo = ((HttpWebResponse)ex.Response).StatusCode;
                StreamReader reader = new StreamReader(ex.Response.GetResponseStream());
                string tramaJson = reader.ReadToEnd();
                RepetidoException error = js.Deserialize<RepetidoException>(tramaJson);
                this.lblMensaje.Text = error.Codigo + "  " + error.Descripcion;
                //    Assert.AreEqual(HttpStatusCode.Conflict, codigo);
                //    Assert.AreEqual("El Ticket ya existe", error.Descripcion);
            }
        }

        protected void CmdVerEstado_Click(object sender, EventArgs e)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.
                 Create("http://localhost:36485/Tickets.svc/CapturarMensaje");
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            string sRespuesta = js.Deserialize<string>(tramaJson);
            Listar();
        }

        protected void CmdValidarRuc_Click(object sender, EventArgs e)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:13398/WcfSunat.svc/Ruc/" + this.txtRucCliente.Text);
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string tramaJson = reader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                Ruc oRuc = js.Deserialize<Ruc>(tramaJson);
                this.lblMensaje.Text = "";
            }
            catch (WebException ex)
            {
                HttpStatusCode codigo = ((HttpWebResponse)ex.Response).StatusCode;
                StreamReader reader = new StreamReader(ex.Response.GetResponseStream());
                string tramaJson = reader.ReadToEnd();
                this.lblMensaje.Text = codigo + "  " + tramaJson;
            }

        }
    }
}