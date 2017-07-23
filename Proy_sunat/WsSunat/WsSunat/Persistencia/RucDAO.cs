using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WsSunat.Dominio;

namespace WsSunat.Persistencia
{
    public class RucDAO
    {
        private string CadenaConexion = "Data Source =.; initial catalog=BD_GestionTickets; integrated security=true";

        public Ruc Obtener_ruc(string ruc)
        {
            Ruc rucEncontrado = null;
            string sql = "select * from t_ruc where ruc_cli=@ruc";

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add(new SqlParameter("@ruc", ruc));
                    using (SqlDataReader resultado = comando.ExecuteReader())
                    {
                        if (resultado.Read())
                        {
                            rucEncontrado = new Ruc();
                            rucEncontrado.codigo = (string)resultado["cod_cli"];
                            rucEncontrado.ruc = (string)resultado["ruc_cli"];
                        }
                    }
                }
            }
            return rucEncontrado;

        } 
    }
}