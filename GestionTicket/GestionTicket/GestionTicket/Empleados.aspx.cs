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
    public partial class Empleados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false) { 
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
            }

        }

        protected void cmdNuevo_Click(object sender, EventArgs e)
        {
            this.txtDni.Focus();
            Limpiar();
            this.lblMensaje.Text = "";
        }
        void Limpiar() {
            this.txtDni.Text = "";
            this.TxtNombre.Text = "";
            this.TxtEdad.Text = "";
            this.ddlEstado.SelectedIndex = 0;
            this.ChkCertificado.Checked = false;
        }

        protected void cmdGrabar_Click(object sender, EventArgs e)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            Empleado oEmpleado = new Empleado();
            oEmpleado.dni = int.Parse(this.txtDni.Text);
            oEmpleado.Nombre =  this.TxtNombre.Text;
            oEmpleado.Estado = this.ddlEstado.Text;
            oEmpleado.Edad = int.Parse(this.TxtEdad.Text);
            oEmpleado.CodigoEspecialidad = ddlEspecialidad.SelectedValue.ToString();
            oEmpleado.Certificado = this.ChkCertificado.Checked;

            string postdata = js.Serialize(oEmpleado);
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:36485/Empleados.svc/Empleados");
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

                Empleado EmpleadoCreado = js.Deserialize<Empleado>(tramaJson);
                this.lblMensaje.Text = "Se grabo correctamente. DNI : " + EmpleadoCreado.dni.ToString();
                Limpiar();
                this.txtDni.Focus();
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

        protected void txtDni_TextChanged(object sender, EventArgs e)
        {
            

        }

        protected void cmdBuscar_Click(object sender, EventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.
              Create("http://localhost:36485/Empleados.svc/Empleados/" + this.txtDni.Text);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Empleado EmpleadoObtenido = js.Deserialize<Empleado>(tramaJson);

            if (EmpleadoObtenido != null)
            {
                this.txtDni.Text = EmpleadoObtenido.dni.ToString();
                this.TxtNombre.Text = EmpleadoObtenido.Nombre;
                this.TxtEdad.Text = EmpleadoObtenido.Edad.ToString();
                this.ddlEstado.Text = EmpleadoObtenido.Estado;
                this.ChkCertificado.Checked = EmpleadoObtenido.Certificado;
                this.ddlEspecialidad.SelectedValue = EmpleadoObtenido.CodigoEspecialidad;
            }
            else {
                this.TxtNombre.Text = "";
                this.TxtEdad.Text = "";
                this.ddlEstado.SelectedIndex = 0;
                this.ChkCertificado.Checked = false;
                this.lblMensaje.Text = "No se encontro el Empleado";
            }
            
        }

        protected void cmdModificar_Click(object sender, EventArgs e)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            Empleado oEmpleado = new Empleado();
            oEmpleado.dni = int.Parse(this.txtDni.Text);
            oEmpleado.Nombre = this.TxtNombre.Text;
            oEmpleado.Estado = this.ddlEstado.Text;
            oEmpleado.Edad = int.Parse(this.TxtEdad.Text);
            oEmpleado.Certificado = this.ChkCertificado.Checked;


            string postdata = js.Serialize(oEmpleado);
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:36485/Empleados.svc/Empleados");
            request.Method = "PUT";
            request.ContentLength = data.Length;
            request.ContentType = "application/json";

            var requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();

            Empleado EmpleadoCreado = js.Deserialize<Empleado>(tramaJson);
            this.lblMensaje.Text = "Se modifico correctamente. DNI : " + EmpleadoCreado.dni.ToString();
            Limpiar();
            this.txtDni.Focus();
          
        }

        protected void cmdEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                HttpWebRequest request1 = (HttpWebRequest)WebRequest.
                    Create("http://localhost:36485/Empleados.svc/EmpleadosInactivo/" + this.txtDni.Text);
                request1.Method = "DELETE";
                HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
                this.lblMensaje.Text = "Se elimino correctamente. ";
                Limpiar();
                this.txtDni.Focus();
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