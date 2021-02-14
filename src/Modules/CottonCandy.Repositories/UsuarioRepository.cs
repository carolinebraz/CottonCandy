using CottonCandy.Domain.Entities;
using CottonCandy.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CottonCandy.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConfiguration _configuration;

        public UsuarioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var SqlCmd = @$"SELECT u.Id,
                                      u.Nome,
                                      u.Email,
                                      u.Senha,
                                      u.DataNascimento,
                                      u.FotoPerfil,
                                      u.Cargo,
                                      u.Cidade,
                                      g.Id as GeneroId,
                                      g.Descricao
                                FROM
                                      Usuario u
                                INNER JOIN
                                      Genero g ON g.Id = u.GeneroId
                                WHERE
                                       u.Email= '{id}'";
                using (var cmd = new SqlCommand(SqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);

                    while (reader.Read())
                    {
                        var usuario = new Usuario(reader["Nome"].ToString(),
                                                  reader["Email"].ToString(),
                                                  reader["Senha"].ToString(),
                                                  DateTime.Parse(reader["DataNascimento"].ToString()),
                                                  new Genero(reader["Descricao"].ToString()),
                                                  reader["FotoPerfil"].ToString(),
                                                  reader["Cargo"].ToString(),
                                                  reader["Cidade"].ToString());

                        usuario.SetId(int.Parse(reader["Id"].ToString()));
                        usuario.Genero.SetId(int.Parse(reader["GeneroId"].ToString()));

                        return usuario;
                    }
                }

                return default;
            }
        }

        public async Task<Usuario> GetByLoginAsync(string login)
        {
            using(var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var SqlCmd = @$"SELECT u.Id,
                                      u.Nome,
                                      u.Email,
                                      u.Senha,
                                      u.DataNascimento,
                                      u.FotoPerfil,
                                      u.Cargo,
                                      u.Cidade,
                                      g.Id as GeneroId,
                                      g.Descricao
                                FROM
                                      Usuario u
                                INNER JOIN
                                      Genero g ON g.Id = u.GeneroId
                                WHERE
                                       u.Email= '{login}'";
                using (var cmd = new SqlCommand(SqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);

                    while(reader.Read())
                    {
                        var usuario = new Usuario (reader["Nome"].ToString(),
                                                  reader["Email"].ToString(),
                                                  reader["Senha"].ToString(),
                                                  DateTime.Parse(reader["DataNascimento"].ToString()),
                                                  new Genero(reader["Descricao"].ToString()),
                                                  reader["FotoPerfil"].ToString(),
                                                  reader["Cargo"].ToString(),
                                                  reader["Cidade"].ToString());

                        usuario.SetId(int.Parse(reader["Id"].ToString()));
                        usuario.Genero.SetId(int.Parse(reader["GeneroId"].ToString()));
                        
                        return usuario;
                    }
                }

                return default;
            }
        }

        public async Task<int> InsertAsync(Usuario usuario)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @"INSERT INTO
                                 Usuario (GeneroId,
                                            Nome,
                                            Email,
                                            Senha,
                                            DataNascimento,
                                            FotoPerfil,
                                            Cargo,
                                            Cidade)
                                VALUES (@generoId,
                                        @nome,
                                        @email,
                                        @senha,
                                        @dataNascimento,
                                        @fotoPerfil,
                                        @cargo,
                                        @cidade); SELECT scope_identity();";

                using(var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("generoId", usuario.Genero.Id);
                    cmd.Parameters.AddWithValue("nome", usuario.Nome);
                    cmd.Parameters.AddWithValue("email", usuario.Email);
                    cmd.Parameters.AddWithValue("senha", usuario.Senha);
                    cmd.Parameters.AddWithValue("dataNascimento", usuario.DataNascimento);
                    cmd.Parameters.AddWithValue("fotoPerfil", usuario.FotoPerfil);
                    cmd.Parameters.AddWithValue("cargo", usuario.Cargo);
                    cmd.Parameters.AddWithValue("cidade", usuario.Cidade);

                    con.Open();
                    var id = await cmd.ExecuteScalarAsync().ConfigureAwait(false);

                    return int.Parse(id.ToString());


                }
            }
        }
    }
}
