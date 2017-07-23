using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WCFServices.Dominio;

namespace WCFServices.Persistencia
{
    public class EmpleadoDAO
    {
        private string CadenaConexion = "Data Source =(local); initial catalog=BD_GestionTickets; integrated security=true";

        public Empleado Crear(Empleado EmpleadoACrear)
        {
            Empleado EmpleadoCreado = null;
            string sql = "INSERT INTO t_Empleado VALUES (@dni, @nombre, @Certificado, @Edad, @estado, @CodigoEspecialidad)";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))  
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add(new SqlParameter("@dni", EmpleadoACrear.dni));
                    comando.Parameters.Add(new SqlParameter("@nombre", EmpleadoACrear.Nombre));
                    comando.Parameters.Add(new SqlParameter("@Certificado", EmpleadoACrear.Certificado));
                    comando.Parameters.Add(new SqlParameter("@Edad", EmpleadoACrear.Edad));
                    comando.Parameters.Add(new SqlParameter("@estado", EmpleadoACrear.Estado));
                    comando.Parameters.Add(new SqlParameter("@CodigoEspecialidad", EmpleadoACrear.CodigoEspecialidad));
                    comando.ExecuteNonQuery();
                }
            }
            EmpleadoCreado = Obtener(EmpleadoACrear.dni);
            return EmpleadoCreado;
        }

        public Empleado Obtener(int dni)
        {
            Empleado EmpleadoEncontrado = null;
            string sql = "select * from t_Empleado where Dni=@dni";

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add(new SqlParameter("@dni", dni));
                    using (SqlDataReader resultado = comando.ExecuteReader())
                    {
                        if (resultado.Read())
                        {
                            EmpleadoEncontrado = new Empleado();
                            EmpleadoEncontrado.dni = (int)resultado["Dni"];
                            EmpleadoEncontrado.Nombre = (string)resultado["Nombre"];
                            EmpleadoEncontrado.Edad = (int)resultado["Edad"];
                            EmpleadoEncontrado.Certificado = (bool)resultado["Certificado"];
                            EmpleadoEncontrado.Estado = (string)resultado["Estado"];
                            EmpleadoEncontrado.CodigoEspecialidad = (string)resultado["CodigoEspecialidad"];
                        }
                    }
                }
            }
            return EmpleadoEncontrado;

        }

        public Empleado ObtenerEmpleado_x_Nombre(string Nombre)
        {
            Empleado EmpleadoEncontrado = null;
            string sql = "select * from t_Empleado where Nombre=@Nombre";

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                    using (SqlDataReader resultado = comando.ExecuteReader())
                    {
                        if (resultado.Read())
                        {
                            EmpleadoEncontrado = new Empleado();
                            EmpleadoEncontrado.dni = (int)resultado["Dni"];
                            EmpleadoEncontrado.Nombre = (string)resultado["Nombre"];
                            EmpleadoEncontrado.Edad = (int)resultado["Edad"];
                            EmpleadoEncontrado.Certificado = (bool)resultado["Certificado"];
                            EmpleadoEncontrado.Estado = (string)resultado["Estado"];
                        }
                    }
                }
            }
            return EmpleadoEncontrado;

        }

        public Empleado Modificar(Empleado EmpleadoAModificar)
        {
            Empleado EmpleadoModificado = null;
            string sql = "UPDATE t_Empleado SET Nombre=@nombre, Estado= @estado, Certificado=@Certificado, Edad=@Edad, CodigoEspecialidad=@CodigoEspecialidad where Dni=@dni ";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add(new SqlParameter("@dni", EmpleadoAModificar.dni));
                    comando.Parameters.Add(new SqlParameter("@nombre", EmpleadoAModificar.Nombre));
                    comando.Parameters.Add(new SqlParameter("@estado", EmpleadoAModificar.Estado));
                    comando.Parameters.Add(new SqlParameter("@Certificado", EmpleadoAModificar.Certificado));
                    comando.Parameters.Add(new SqlParameter("@Edad", EmpleadoAModificar.Edad));
                    comando.Parameters.Add(new SqlParameter("@CodigoEspecialidad", EmpleadoAModificar.CodigoEspecialidad));
                    comando.ExecuteNonQuery();
                }
            }
            EmpleadoModificado = Obtener(EmpleadoAModificar.dni);
            return EmpleadoModificado;
        }

        public void Eliminar(int dni)
        {
            Empleado EmpleadoEliminado = null;
            string sql = "delete from t_Empleado where Dni=@dni ";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add(new SqlParameter("@dni", dni));
                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<Empleado> Listar()
        {
            List<Empleado> EmpleadoEncontrados = new List<Empleado>();
            Empleado EmpleadoEncontrado = null;
            string sql = "select * from t_Empleado ";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    using (SqlDataReader resultado = comando.ExecuteReader())
                    {
                        while (resultado.Read())
                        {
                            EmpleadoEncontrado = new Empleado();
                            EmpleadoEncontrado.dni = (int)resultado["Dni"];
                            EmpleadoEncontrado.Nombre = (string)resultado["Nombre"];
                            EmpleadoEncontrado.Estado = (string)resultado["Estado"];
                            EmpleadoEncontrado.Edad = (int)resultado["Edad"];
                            EmpleadoEncontrado.Certificado = (bool)resultado["Certificado"];
                            EmpleadoEncontrado.CodigoEspecialidad = (string)resultado["CodigoEspecialidad"];
                            EmpleadoEncontrados.Add(EmpleadoEncontrado);
                        }
                    }
                }
            }

            return EmpleadoEncontrados;
        }
    }
}