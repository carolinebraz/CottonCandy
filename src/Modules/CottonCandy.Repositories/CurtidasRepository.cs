using CottonCandy.Domain.Entities;
using CottonCandy.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CottonCandy.Repositories
{
    public class CurtidasRepository : ICurtidasRepository
    {
        private readonly IConfiguration _configuration;

        public CurtidasRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> Curtir(Curtidas curtida)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @"INSERT INTO
                                    Curtidas (PostagemId,
                                              UsuarioId)
                                    VALUES (@postagemId,
                                            @usuarioId); SELECT scope_identity();";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("postagemId", curtida.PostagemId);
                    cmd.Parameters.AddWithValue("usuarioId", curtida.UsuarioId);

                    con.Open();
                    var id = await cmd.ExecuteScalarAsync().ConfigureAwait(false);

                    return "Você curtiu a postagem número " + curtida.PostagemId;
                }
            }
        }
        public async Task<string> Descurtir(int id)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = $@"DELETE FROM
                                   Curtidas
                                WHERE 
                                   Id={id}";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    await cmd.ExecuteScalarAsync().ConfigureAwait(false);
                }
            }

            return "Você descurtiu essa postagem";
        }

        public async Task<int> ObterCurtidas(int postagemId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT
                                    COUNT(*) AS Quantidade
                                FROM 
	                                Curtidas
                                WHERE 
	                                PostagemId={postagemId}";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    while (reader.Read())
                    {
                        return int.Parse(reader["Quantidade"].ToString());
                    }

                    return default;
                }
            }
        }

        public async Task<List<Curtidas>> GetByUsuarioIdAsync(int usuarioId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT Id,
                                       PostagemId,
                                       UsuarioId
                                FROM 
	                                   Curtidas
                                WHERE 
	                                   UsuarioId= '{usuarioId}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    var curtidasUsuario = new List<Curtidas>();

                    while (reader.Read())
                    {
                        var curtidas = new Curtidas(int.Parse(reader["UsuarioId"].ToString()),
                                                    int.Parse(reader["PostagemId"].ToString()));

                        curtidasUsuario.Add(curtidas);
                    }

                    return curtidasUsuario;
                }
            }
        }

        public async Task<List<Curtidas>> GetByPostagemIdAsync(int postagemId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT Id,
                                       PostagemId,
                                       UsuarioId
                                  FROM 
	                                   Curtidas
                                  WHERE 
	                                   PostagemId= '{postagemId}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    var curtidasPostagem = new List<Curtidas>();

                    while (reader.Read())
                    {
                        var curtidas = new Curtidas(int.Parse(reader["UsuarioId"].ToString()),
                                                    int.Parse(reader["PostagemId"].ToString()));

                        curtidasPostagem.Add(curtidas);
                    }

                    return curtidasPostagem;
                }
            }
        }

        public async Task<Curtidas> GetByUsuarioIdAndPostagemIdAsync(int usuarioId, int postagemId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT Id,
	                                   UsuarioId,
                                       PostagemId
                                  FROM 
	                                   Curtidas
                                  WHERE 
	                                   UsuarioId= '{usuarioId}'
                                  AND 
                                       PostagemId= '{postagemId}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    while (reader.Read())
                    {
                        var curtida = new Curtidas(int.Parse(reader["UsuarioId"].ToString()),
                                                int.Parse(reader["PostagemId"].ToString()));

                        curtida.SetId(int.Parse(reader["Id"].ToString()));
                        return curtida;
                    }

                    return default;
                }
            }
        }
    }
}