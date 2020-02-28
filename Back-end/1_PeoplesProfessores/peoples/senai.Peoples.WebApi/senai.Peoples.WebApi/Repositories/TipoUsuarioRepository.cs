using senai.Peoples.WebApi.Domains;
using senai.Peoples.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.Peoples.WebApi.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private string stringConexao = "Data Source=DEV1001\\SQLEXPRESS; initial catalog=Peoples; user Id=sa; pwd=sa@132";


        public List<TipoUsuarioDomain> Listar()
        {

            List<TipoUsuarioDomain> tipoUsuarios = new List<TipoUsuarioDomain>();


            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT IdTipoUsuario, TipoUsuario FROM TipoUsuario";


                con.Open();
                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        TipoUsuarioDomain tipoUsuario =  new TipoUsuarioDomain
                        {
                            IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),
                            TipoUsuario = rdr["TipoUsuario"].ToString()
                        };
                        tipoUsuarios.Add(tipoUsuario);
                    }
                }
            }
            return tipoUsuarios;
        }

        public TipoUsuarioDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT IdTipoUsuario, TipoUsuario FROM TipoUsuario" +
                                        " WHERE IdTipoUsuario = @ID";
                con.Open();
                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        TipoUsuarioDomain tipoUsuario = new TipoUsuarioDomain
                        {
                            IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),
                            TipoUsuario = rdr["TipoUsuario"].ToString()

                        };

                        return tipoUsuario;
                    }
                    return null;
                }
            }
        }


        public void Atualizar(int id, TipoUsuarioDomain TipoUsuarioAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdate = "UPDATE TipoUsuario " +
                                     "SET TipoUsuario = @TipoUsuario" +
                                     "WHERE IdTipoUsuario = @ID";
                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@TipoUsuario", TipoUsuarioAtualizado.TipoUsuario);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM TipoUsuario WHERE IdTipoUsuario = @ID";
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
