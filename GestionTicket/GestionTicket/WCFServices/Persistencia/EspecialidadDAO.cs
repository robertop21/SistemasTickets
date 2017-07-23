using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WCFServices.Dominio;

namespace WCFServices.Persistencia
{
    public class EspecialidadDAO
    {
        private string CadenaConexion = "Data Source =(local); initial catalog=BD_GestionTickets; integrated security=true";

        public Especialidad Crear(Especialidad especialidadCrear)
        {
            Especialidad especialidadCreada = null;
            string sql = "INSERT INTO t_Especialidad values(@codigo, @descripcion, @estado)";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add(new SqlParameter("@codigo", especialidadCrear.Codigo));
                    comando.Parameters.Add(new SqlParameter("@descripcion", especialidadCrear.Descripcion));
                    comando.Parameters.Add(new SqlParameter("@estado", especialidadCrear.Estado));
                    comando.ExecuteNonQuery();
                }
            }
            especialidadCreada = Obtener(especialidadCrear.Codigo);
            return especialidadCreada;
        }

        public Especialidad Obtener(string codigo)
        {
            Especialidad especialidadencontrada = null;
            string sql = "SELECT  * FROM t_Especialidad WHERE Codigo = @codigo";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add(new SqlParameter("@codigo", codigo));
                    using (SqlDataReader resultado = comando.ExecuteReader())
                    {
                        if (resultado.Read())
                        {
                            especialidadencontrada = new Especialidad()
                            {
                                Codigo = (string)resultado["Codigo"],
                                Descripcion = (string)resultado["Descripcion"],
                                Estado = (string)resultado["Estado"],

                            };
                        }
                    }
                }
            }
            return especialidadencontrada;
        }

        public Especialidad Modificar(Especialidad especialidadModificar)
        {
            Especialidad especialidadModificada = null;
            string sql = "UPDATE t_Especialidad SET Nombre = @nombre , Descripcion = @descripcion WHERE Codigo = @codigo";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add(new SqlParameter("@codigo", especialidadModificar.Codigo));
                    comando.Parameters.Add(new SqlParameter("@nombre", especialidadModificar.Descripcion));
                    comando.Parameters.Add(new SqlParameter("@descripcion", especialidadModificar.Estado));
                    comando.ExecuteNonQuery();
                }
            }
            especialidadModificada = Obtener(especialidadModificar.Codigo);
            return especialidadModificada;

        }


        public void Eliminar(string codigo)
        {
            string sql = "DELETE FROM t_Especialidad WHERE Codigo= @codigo";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add(new SqlParameter("@codigo", codigo));
                    comando.ExecuteNonQuery();
                }

            }
        }

        public List<Especialidad> Listar()
        {
            List<Especialidad> especialidadesEncontradas = new List<Especialidad>();
            Especialidad especialidadEncontrada = null;
            string sql = "SELECT * FROM t_Especialidad";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    using (SqlDataReader resultado = comando.ExecuteReader())
                    {
                        while (resultado.Read())
                        {
                            especialidadEncontrada = new Especialidad()
                            {

                                Codigo = (string)resultado["Codigo"],
                                Descripcion = (string)resultado["Descripcion"],
                                Estado = (string)resultado["Estado"]
                            };
                            especialidadesEncontradas.Add(especialidadEncontrada);
                        }
                    }
                }

            }
            return especialidadesEncontradas;
        }
    }
}