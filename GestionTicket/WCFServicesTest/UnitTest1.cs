using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using System.Web.Script.Serialization;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace WCFServicesTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test1CrearEmpleadoOk()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            Empleado prueba = new Empleado();
            prueba.dni = 91111177;
            prueba.Nombre = "Roberta";
            prueba.Estado = "acti";
            prueba.Edad = 30;
            prueba.Certificado = true;

            string postdata = js.Serialize(prueba);
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest  request= (HttpWebRequest)WebRequest.
                Create("http://localhost:36485/Empleados.svc/Empleados");
            request.Method = "POST";
            request.ContentLength  = data.Length;
            request.ContentType = "application/json";

            var requestStream= request.GetRequestStream();
            requestStream.Write(data, 0 , data.Length);
            HttpWebResponse response= (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson= reader.ReadToEnd();

            Empleado EmpleadoCreado = js.Deserialize<Empleado>(tramaJson);

            Assert.AreEqual(91111177, EmpleadoCreado.dni);
            Assert.AreEqual("Roberta", EmpleadoCreado.Nombre);
            Assert.AreEqual("acti", EmpleadoCreado.Estado);

        }


        [TestMethod]
        public void Test1CrearEmpleadoDuplicado()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            Empleado prueba = new Empleado();
            prueba.dni = 91111177;
            prueba.Nombre = "Roberta";
            prueba.Estado = "acti";
            prueba.Edad = 30;
            prueba.Certificado = true;

            string postdata = js.Serialize(prueba);
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

                Assert.AreEqual(91111177, EmpleadoCreado.dni);
                Assert.AreEqual("Roberta", EmpleadoCreado.Nombre);
                Assert.AreEqual("acti", EmpleadoCreado.Estado);
            }
            catch (WebException e)
            {
                HttpStatusCode codigo = ((HttpWebResponse)e.Response).StatusCode;
                StreamReader reader = new StreamReader(e.Response.GetResponseStream());
                string tramaJson = reader.ReadToEnd();
                DuplicadoException error= js.Deserialize<DuplicadoException>(tramaJson);
                Assert.AreEqual(HttpStatusCode.Conflict, codigo);
                Assert.AreEqual("El empleado ya existe", error.Descripcion);
            }
        }

        [TestMethod]
        public void Test2ObtenerEmpleado()
        { 

            HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:36485/Empleados.svc/Empleados/91111177");
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Empleado EmpleadoCreado = js.Deserialize<Empleado>(tramaJson);

            Assert.AreEqual("Roberta Perez", EmpleadoCreado.Nombre);
            Assert.AreEqual("Modificado", EmpleadoCreado.Estado);
            
        }

        [TestMethod]
        public void Test3ModificarEmpleado()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            Empleado prueba = new Empleado();
            prueba.dni = 91111177;
            prueba.Nombre = "Roberta Perez";
            prueba.Estado = "Modificado";
            prueba.Edad = 30;
            prueba.Certificado = true;

            string postdata = js.Serialize(prueba);
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

            Assert.AreEqual(91111177, EmpleadoCreado.dni);
            Assert.AreEqual("Roberta Perez", EmpleadoCreado.Nombre);
            Assert.AreEqual("Modificado", EmpleadoCreado.Estado);

        }


        [TestMethod]
        public void Test4EliminarAlumno()
        {


            HttpWebRequest request1 = (HttpWebRequest)WebRequest.
                Create("http://localhost:36485/Empleados.svc/Empleados/91111177");
            request1.Method = "DELETE";
            HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();


            HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:36485/Empleados.svc/Empleados/91111177");
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Empleado EmpleadoObtenido = js.Deserialize<Empleado>(tramaJson);

            Assert.IsNull(EmpleadoObtenido);

        }

        [TestMethod]
        public void Test5ListarEmpleado()
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:36485/Empleados.svc/Empleados");
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Empleado> EmpleadosObtenidos = js.Deserialize<List<Empleado>>(tramaJson);

            Assert.AreEqual(2, EmpleadosObtenidos.Count);

        }

        //TAREA

        [TestMethod]
        public void Test2ObtenerEmpleado_x_Nombre()
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:36485/Empleados.svc/EmpleadosNombre/Samuel");
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Empleado EmpleadoCreado = js.Deserialize<Empleado>(tramaJson);

            Assert.AreEqual("Samuel", EmpleadoCreado.Nombre);
            Assert.AreEqual("Activo", EmpleadoCreado.Estado);

        }

        [TestMethod]
        public void TestEliminarInactivo()
        {
            
            try
            {
                HttpWebRequest request1 = (HttpWebRequest)WebRequest.
                    Create("http://localhost:36485/Empleados.svc/EmpleadosInactivo/18888991");
                request1.Method = "DELETE";
                HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();

            }
            catch (WebException e)
            {
                HttpStatusCode codigo = ((HttpWebResponse)e.Response).StatusCode;
                StreamReader reader = new StreamReader(e.Response.GetResponseStream());
                string tramaJson = reader.ReadToEnd();
                Assert.AreEqual(HttpStatusCode.Conflict, codigo);
                Assert.AreEqual("Solo se pueden eliminar Empleados Inactivos", "Solo se pueden eliminar Empleados Inactivos");
            }
        }


    }
}
