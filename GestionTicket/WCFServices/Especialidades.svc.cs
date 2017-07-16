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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Especialidades" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Especialidades.svc or Especialidades.svc.cs at the Solution Explorer and start debugging.
    public class Especialidades : IEspecialidades
    {
        private EspecialidadDAO especialidadDAO = new EspecialidadDAO();
        public Especialidad CrearEspecialidad(Especialidad especialidadCrear)
        {
            Especialidad especialidadexistente = especialidadDAO.Obtener(especialidadCrear.Codigo);
            if (especialidadexistente != null)
            {
                throw new WebFaultException<RepetidoException>(new RepetidoException()
                {

                    Codigo = "102",
                    Descripcion = "Especialidad Duplicado",
                    

                }, HttpStatusCode.Conflict);
            }
            return especialidadDAO.Crear(especialidadCrear);
        }

        public Especialidad ObtenerEspecialidad(string codigo)
        {
            Especialidad especialidadexistente = especialidadDAO.Obtener(codigo);
            if (especialidadexistente == null)
            {
                throw new WebFaultException<RepetidoException>(new RepetidoException()
                {

                    Codigo = "103",
                    Descripcion = "Especialidad no Existe",
                 

                }, HttpStatusCode.Conflict);
            }
            return especialidadDAO.Obtener(codigo);
        }

        public Especialidad ModificarEspecialidad(Especialidad especialidadModificar)
        {
            return especialidadDAO.Modificar(especialidadModificar);
        }

        public void EliminarEspecialidad(string codigo)
        {
            especialidadDAO.Eliminar(codigo);
        }

        public List<Especialidad> ListarEspecialidad()
        {
            return especialidadDAO.Listar();
        }

        public void EliminarEspecialidadInactivo(string codigo)
        {
            if (especialidadDAO.Obtener(string.Format(codigo)) != null)
            {
                if (especialidadDAO.Obtener(string.Format(codigo)).Estado != "INACTIVO")
                {
                    throw new WebFaultException<RepetidoException>(
                         new RepetidoException()
                         {
                             Codigo = "107",
                             Descripcion = "Solo se pueden eliminar Empleados Inactivos"
                         }, HttpStatusCode.Conflict);

                }
            }

            especialidadDAO.Eliminar(string.Format(codigo));
        }

    }
    }
