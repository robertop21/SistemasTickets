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
    public partial class Clientes : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (IsPostBack == false)
            {
               
            }

        }

        protected void cmdNuevo_Click(object sender, EventArgs e)
        {
            this.txtRuc.Focus();
            Limpiar();
            this.lblMensaje.Text = "";
        }
        void Limpiar()
        {
            this.txtRuc.Text = "";
            this.TxtNombre.Text = "";
            
            this.ddlEstado.SelectedIndex = 0;
            
        }

        protected void cmdGrabar_Click(object sender, EventArgs e)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            Cliente oCliente = new Cliente();
            oCliente.Ruc = Convert.ToInt64(this.txtRuc.Text);
            oCliente.Razonsocial = this.TxtNombre.Text;
            oCliente.Estado = this.ddlEstado.Text;
     

            string postdata = js.Serialize(oCliente);
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:36485/Clientes.svc/Clientes");
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

                Cliente ClienteCreado = js.Deserialize<Cliente>(tramaJson);
                this.lblMensaje.Text = "Se grabo correctamente. RUC : " + ClienteCreado.Ruc.ToString();
                Limpiar();
                this.txtRuc.Focus();
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

        protected void txtRuc_TextChanged(object sender, EventArgs e)
        {


        }

        protected void cmdBuscar_Click(object sender, EventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.
              Create("http://localhost:36485/Clientes.svc/Clientes/" + this.txtRuc.Text);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Cliente ClienteObtenido = js.Deserialize<Cliente>(tramaJson);

            if (ClienteObtenido != null)
            {
                this.txtRuc.Text = ClienteObtenido.Ruc.ToString();
                this.TxtNombre.Text = ClienteObtenido.Razonsocial;
                this.ddlEstado.Text = ClienteObtenido.Estado;
            }
            else
            {
                this.TxtNombre.Text = "";
                this.ddlEstado.SelectedIndex = 0;
                this.lblMensaje.Text = "No se encontro el Cliente";
            }

        }

        protected void cmdModificar_Click(object sender, EventArgs e)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            Cliente oCliente = new Cliente();
            oCliente.Ruc = Convert.ToInt64(this.txtRuc.Text);
            oCliente.Razonsocial = this.TxtNombre.Text;
            oCliente.Estado = this.ddlEstado.Text;


            string postdata = js.Serialize(oCliente);
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:36485/Clientes.svc/Clientes");
            request.Method = "PUT";
            request.ContentLength = data.Length;
            request.ContentType = "application/json";

            var requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();

            Cliente ClienteCreado = js.Deserialize<Cliente>(tramaJson);
            this.lblMensaje.Text = "Se modifico correctamente. RUC : " + ClienteCreado.Ruc.ToString();
            Limpiar();
            this.txtRuc.Focus();

        }

        protected void cmdEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                HttpWebRequest request1 = (HttpWebRequest)WebRequest.
                    Create("http://localhost:36485/Clientes.svc/ClientesInactivo/" + this.txtRuc.Text);
                request1.Method = "DELETE";
                HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
                this.lblMensaje.Text = "Se elimino correctamente. ";
                Limpiar();
                this.txtRuc.Focus();
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