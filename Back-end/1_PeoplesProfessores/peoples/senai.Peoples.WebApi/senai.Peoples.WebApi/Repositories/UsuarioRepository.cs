using senai.Peoples.WebApi.Domains;
using senai.Peoples.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.Peoples.WebApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string stringConexao = "Data Source=DEV1001\\SQLEXPRESS; initial catalog=Peoples; user Id=sa; pwd=sa@132";



        List<UsuarioDomain> IUsuarioRepository.Listar()
        {
            List<UsuarioDomain> Usuarios = new List<UsuarioDomain>();


            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT IdUsuario, Nome, Sobrenome, Email, Senha FROM Usuario";


                con.Open();
                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        UsuarioDomain Usuario = new UsuarioDomain
                        {
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"]),
                            Nome = rdr["Nome"].ToString(),
                            Sobrenome = rdr["Sobrenome"].ToString(),
                            Email = rdr ["Email"].ToString(),
                            Senha = rdr ["Senha"].ToString()
                        };
                        Usuarios.Add(Usuario);
                    }
                }
            }
            return Usuarios;
        }

        public void Cadastrar(UsuarioDomain novoUsuario)
        {
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    string queryInsert = "INSERT INTO Usuario(Nome, Sobrenome, Email, Senha) " +
                                         "VALUES (@Nome, @Sobrenome, @Email, @Senha)";

                    using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                    {
                        cmd.Parameters.AddWithValue("@Nome", novoUsuario.Nome);
                        cmd.Parameters.AddWithValue("@Sobrenome", novoUsuario.Sobrenome);
                        cmd.Parameters.AddWithValue("@Email", novoUsuario.Email);
                        cmd.Parameters.AddWithValue("@Senha", novoUsuario.Senha);

                    con.Open();

                        // Executa o comando
                        cmd.ExecuteNonQuery();
                    }
                }
            }

        UsuarioDomain IUsuarioRepository.BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT IdUsuario, Nome, Sobrenome, Email, Senha FROM Usuario" +
                                        " WHERE IdUsuario = @ID";
                con.Open();
                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        UsuarioDomain Usuario = new UsuarioDomain
                        {
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"]),
                            Nome = rdr["Nome"].ToString(),
                            Sobrenome = rdr["Sobrenome"].ToString(),
                            Email = rdr["Email"].ToString(),
                            Senha = rdr["Senha"].ToString()

                        };

                        return Usuario;
                    }
                    return null;
                }
            }
        }

        public void Atualizar(int id, UsuarioDomain UsuarioAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdate = "UPDATE Usuario " +
                                     "SET Nome = @Nome, Sobrenome = @Sobrenome, Email = @Email, Senha = @Senha" +
                                     "WHERE IdUsuario = @ID";
                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Nome", UsuarioAtualizado.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", UsuarioAtualizado.Sobrenome);
                    cmd.Parameters.AddWithValue("@Email", UsuarioAtualizado.Email);
                    cmd.Parameters.AddWithValue("@Senha", UsuarioAtualizado.Senha);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM Usuario WHERE IdUsuario = @ID";
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
