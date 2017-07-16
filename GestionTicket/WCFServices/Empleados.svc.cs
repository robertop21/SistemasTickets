using System;
using System.Collections.Generic;
using System.Linq;
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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Empleados" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Empleados.svc o Empleados.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Empleados : IEmpleados
    {
        private EmpleadoDAO empleadoDAO = new EmpleadoDAO();
        public Empleado CrearEmpleado(Empleado EmpleadoACrear)
        {
            if (empleadoDAO.Obtener(EmpleadoACrear.dni) != null)
            {
                throw new WebFaultException<RepetidoException>(
                     new RepetidoException()
                     {
                         Codigo = "101",
                         Descripcion = "El empleado ya existe"
                     }, HttpStatusCode.Conflict);
            }

            if (EmpleadoACrear.Certificado == false)
            {
                throw new WebFaultException<RepetidoException>(
                     new RepetidoException()
                     {
                         Codigo = "102",
                         Descripcion = "El empleado no cumple certificacion"
                     }, HttpStatusCode.Conflict);
            }


            if (EmpleadoACrear.Edad < 25)
            {
                throw new WebFaultException<RepetidoException>(
                     new RepetidoException()
                     {
                         Codigo = "103",
                         Descripcion = "No se aceptan empleados menores a 25 años"
                     }, HttpStatusCode.Conflict);
            }
            return empleadoDAO.Crear(EmpleadoACrear);
        }

        public Empleado ObtenerEmpleado( string dni)
        {
            return empleadoDAO.Obtener(int.Parse(dni));
        }

        public Empleado ModificarEmpleado(Empleado EmpleadoAModificar)
        {
            return empleadoDAO.Modificar(EmpleadoAModificar);
        }

        public void EliminarEmpleado(string dni)
        {
            empleadoDAO.Eliminar(int.Parse(dni));
        }


        public List<Empleado> ListarEmpleado()
        {
            return empleadoDAO.Listar();
        }


        //TAREA


        public Empleado ObtenerEmpleado_x_Nombre(string Nombre)
        {
            return empleadoDAO.ObtenerEmpleado_x_Nombre(Nombre);
        }


        public void EliminarEmpleadoInactivo(string dni)
        {
            if (empleadoDAO.Obtener(int.Parse(dni)) != null)
            {
                if (empleadoDAO.Obtener(int.Parse(dni)).Estado != "INACTIVO")
                {
                    throw new WebFaultException<RepetidoException>(
                         new RepetidoException()
                         {
                             Codigo = "107",
                             Descripcion = "Solo se pueden eliminar Empleados Inactivos"
                         }, HttpStatusCode.Conflict);

                }
            }

            empleadoDAO.Eliminar(int.Parse(dni));
        }



    }
}
