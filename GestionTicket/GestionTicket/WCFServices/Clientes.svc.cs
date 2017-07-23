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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Clientes" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Clientes.svc o Clientes.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Clientes : IClientes
    {
        private ClienteDAO clienteDAO = new ClienteDAO();

        public Cliente CrearCliente(Cliente clienteACrear)
        {
            Cliente clienteExistente = clienteDAO.Obtener(clienteACrear.Ruc);
           
            if (clienteExistente != null) // ya existe el cliente con este ruc
             {
                 throw new WebFaultException<RepetidoException>(new RepetidoException()
                 {
                     Codigo = "101",
                     Descripcion = "Cliente Duplicado"
                 }, HttpStatusCode.Conflict);
             }

            if (clienteACrear.Ruc.ToString().Substring(0, 1) == "1")
            {
                throw new WebFaultException<RepetidoException>(new RepetidoException()
                {
                    Codigo = "102",
                    Descripcion = "El sistema no permite el registro de clientes naturales"
                }, HttpStatusCode.Conflict);
            }


            return clienteDAO.Crear(clienteACrear);
        }

        public Cliente ObtenerCliente(string ruc)
        {
            return clienteDAO.Obtener(Convert.ToInt64(ruc));
        }

        public Cliente ModificarCliente(Cliente clienteAModificar)
        {
            return clienteDAO.Modificar(clienteAModificar);
        }

        public void EliminarCliente(string ruc)
        {
            clienteDAO.Eliminar(int.Parse(ruc));
        }

        public List<Cliente> ListarClientes()
        {
            return clienteDAO.Listar();
        }
        //TAREA

        public Cliente ObtenerCliente_x_Razonsocial(string razonsocial)
        {
            return clienteDAO.ObtenerCliente_x_Razonsocial(razonsocial);
        }

        public void EliminarClienteInactivo(string ruc)
        {
            if (clienteDAO.Obtener(Convert.ToInt64(ruc)) != null)
            {
                if (clienteDAO.Obtener(Convert.ToInt64(ruc)).Estado != "INACTIVO")
                {
                    throw new WebFaultException<RepetidoException>(
                         new RepetidoException()
                         {
                             Codigo = "102",
                             Descripcion = "Solo se pueden eliminar Clientes Inactivos"
                         }, HttpStatusCode.Conflict);
                }
            }
            clienteDAO.Eliminar(Convert.ToInt64(ruc));
        }
    }
}
