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
    public partial class Especialidades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdNuevo_Click(object sender, EventArgs e)
        {
            this.txtCodigo.Focus();
            Limpiar();
            this.lblMensaje.Text = "";
        }

        void Limpiar()
        {
            this.txtCodigo.Text = "";
            this.txtDescripcion.Text = "";
            //this.TxtEdad.Text = "";
            this.ddlEstado.SelectedIndex = 0;
            //this.ChkCertificado.Checked = false;
        }

        protected void cmdGrabar_Click(object sender, EventArgs e)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            Especialidad oEspeciaidad = new Especialidad();
            oEspeciaidad.Codigo = string.Format(this.txtCodigo.Text);
            oEspeciaidad.Descripcion =this.txtDescripcion.Text;
            oEspeciaidad.Estado = this.ddlEstado.Text;
            //oEmpleado.Edad = int.Parse(this.TxtEdad.Text);
            //oEmpleado.Certificado = this.ChkCertificado.Checked;

            string postdata = js.Serialize(oEspeciaidad);
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:36485/Especialidades.svc/Especialidades");
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

                Especialidad EspecialidadCreada = js.Deserialize<Especialidad>(tramaJson);
                this.lblMensaje.Text = "Se grabo correctamente. Especialidad : " + EspecialidadCreada.Codigo.ToString();
                Limpiar();
                this.txtCodigo.Focus();
            }
            catch (WebException ex)
            {
                HttpStatusCode codigo = ((HttpWebResponse)ex.Response).StatusCode;
                StreamReader reader = new StreamReader(ex.Response.GetResponseStream());
                string tramaJson = reader.ReadToEnd();
                RepetidoException error = js.Deserialize<RepetidoException>(tramaJson);
                this.lblMensaje.Text = error.Codigo + "  " + error.Descripcion;
                //    Assert.AreEqual(HttpStatusCode.Conflict, codigo);
                //    Assert.AreEqual("El empleado ya existe", error.Descripcion);
            }
        }


        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {


        }
        protected void cmdBuscar_Click(object sender, EventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.
              Create("http://localhost:36485/Especialidades.svc/Especialidades/" + this.txtCodigo.Text);
            request.Method = "GET";
            request.ContentLength = 0;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Especialidad EspecialidadObtenida = js.Deserialize<Especialidad>(tramaJson);

            if (EspecialidadObtenida != null)
            {
                this.txtCodigo.Text = EspecialidadObtenida.Codigo.ToString();
                this.txtDescripcion.Text = EspecialidadObtenida.Descripcion;
                this.ddlEstado.Text = EspecialidadObtenida.Estado;
            }
            else
            {
                this.txtDescripcion.Text = "";
                this.ddlEstado.SelectedIndex = 0;
                this.lblMensaje.Text = "No se encontro la especialidad";
            }

        }

        protected void cmdModificar_Click(object sender, EventArgs e)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            Especialidad oEspecialidad = new Especialidad();
            oEspecialidad.Codigo = string.Format(this.txtCodigo.Text);
            oEspecialidad.Descripcion = this.txtDescripcion.Text;
            oEspecialidad.Estado = this.ddlEstado.Text;


            string postdata = js.Serialize(oEspecialidad);
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:36485/Especialidades.svc/Especialidades");
            request.Method = "PUT";
            request.ContentLength = data.Length;
            request.ContentType = "application/json";

            var requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();

            Especialidad EspecialidadCreada = js.Deserialize<Especialidad>(tramaJson);
            this.lblMensaje.Text = "Se modifico correctamente. Especialidad : " + EspecialidadCreada.Codigo.ToString();
            Limpiar();
            this.txtCodigo.Focus();

        }

        protected void cmdEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                HttpWebRequest request1 = (HttpWebRequest)WebRequest.
                    Create("http://localhost:36485/Especialidades.svc/EspecialidadesInactivo" + this.txtCodigo.Text);
                request1.Method = "DELETE";
                HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
                this.lblMensaje.Text = "Se elimino correctamente. ";
                Limpiar();
                this.txtCodigo.Focus();
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