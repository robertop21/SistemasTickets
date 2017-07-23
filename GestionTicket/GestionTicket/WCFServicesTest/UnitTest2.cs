using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace WCFServicesTest
{
    class UnitTest2
    {
        [TestMethod]
        public void Test1CrearEspecialidad()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Especialidad especialidadCrear = new Especialidad()
            {
                Codigo = "MICRO-100",
                Descripcion = "OTROS",
                Estado = "OTROS"
            };
            string postdata = js.Serialize(especialidadCrear);
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:53139/Especialidades.svc/Especialidades");
            request.Method = "POST";
            request.ContentLength = data.Length;
            request.ContentType = "application/json";
            var requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            var response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            Especialidad especialidadCreada = js.Deserialize<Especialidad>(tramaJson);
            Assert.AreEqual("MICRO-100", especialidadCreada.Codigo);
            Assert.AreEqual("OTROS", especialidadCreada.Descripcion);
            Assert.AreEqual("OTROS", especialidadCreada.Estado);
        }

        [TestMethod]
        public void Test1CrearEspecialidadDuplicado()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Especialidad especialidadCrear = new Especialidad()
            {
                Codigo = "MICRO-11",
                Descripcion = "OTROS",
                Estado = "OTROS"
            };
            string postdata = js.Serialize(especialidadCrear);
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:53139/Especialidades.svc/Especialidades");
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
                Especialidad especialidadCreada = js.Deserialize<Especialidad>(tramaJson);
                Assert.AreEqual("MICRO-11", especialidadCreada.Codigo);
                Assert.AreEqual("OTROS", especialidadCreada.Descripcion);
                Assert.AreEqual("OTROS", especialidadCreada.Estado);
            }
            catch (WebException e)
            {
                HttpStatusCode codigo = ((HttpWebResponse)e.Response).StatusCode;
                StreamReader reader = new StreamReader(e.Response.GetResponseStream());
                string tramaJson = reader.ReadToEnd();
                DuplicadoException error = js.Deserialize<DuplicadoException>(tramaJson);
                Assert.AreEqual(HttpStatusCode.Conflict, codigo);
                Assert.AreEqual("Especialidad Duplicada", error.Descripcion);
            }
        }

        [TestMethod]
        public void Test1ObtenerEspecialidad()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:53139/Especialidades.svc/Especialidades/MICRO-11");
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Especialidad especialidadObtenida = js.Deserialize<Especialidad>(tramaJson);
            Assert.AreEqual("MICRO-11", especialidadObtenida.Codigo);
            Assert.AreEqual("OTROS", especialidadObtenida.Descripcion);

        }

        [TestMethod]
        public void Test1ObtenerEspecialidadInexistente()
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:53139/Especialidades.svc/Especialidades/MICRO-52");
            request.Method = "GET";
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string tramaJson = reader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                Especialidad especialidadObtenida = js.Deserialize<Especialidad>(tramaJson);
                Assert.AreEqual("MICRO-52", especialidadObtenida.Codigo);
            }
            catch (WebException e)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                HttpStatusCode codigo = ((HttpWebResponse)e.Response).StatusCode;
                StreamReader reader = new StreamReader(e.Response.GetResponseStream());
                string tramaJson = reader.ReadToEnd();
                DuplicadoException error = js.Deserialize<DuplicadoException>(tramaJson);
                Assert.AreEqual(HttpStatusCode.Conflict, codigo);
                Assert.AreEqual("Especialidad Inexistente", error.Descripcion);

            }

        }

        [TestMethod]
        public void Test1EliminarEspecialidad()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Especialidad especialidadCrear = new Especialidad()
            {
                Codigo = "MICRO-12",

            };
            string postdata = js.Serialize(especialidadCrear);
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:53139/Especialidades.svc/Especialidades/MICRO-12");
            request.Method = "DELETE";
            request.ContentLength = data.Length;
            request.ContentType = "application/json";
            var requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            var response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            Especialidad especialidadCreada = js.Deserialize<Especialidad>(tramaJson);

        }
    }
}
