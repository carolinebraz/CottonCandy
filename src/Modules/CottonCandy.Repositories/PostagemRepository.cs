using CottonCandy.Domain.Entities;
using CottonCandy.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CottonCandy.Repositories
{
    public class PostagemRepository : IPostagemRepository
    {
        private readonly IConfiguration _configuration;
        public PostagemRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<Postagem>> ObterInformacoesPorIdAsync(int usuarioId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT Id,
                                       Texto,
                                       DataPostagem,
                                       FotoPost,
                                       UsuarioId
                                  FROM 
	                                   Postagem
                                  WHERE 
	                                   UsuarioId= '{usuarioId}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    var postagensUsuario = new List<Postagem>();

                    while (reader.Read())
                    {
                        var postagem = new Postagem(reader["Texto"].ToString(),
                                                    reader["FotoPost"].ToString(),
                                                    int.Parse(reader["UsuarioId"].ToString()),
                                                    DateTime.Parse(reader["DataPostagem"].ToString()));

                        postagem.SetId(int.Parse(reader["Id"].ToString()));

                        postagensUsuario.Add(postagem);
                    }

                    return postagensUsuario;
                }
            }
        }

        public async Task<int> GetUsuarioIdByPostagemId(int postagemId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT 
                                    UsuarioId
                                FROM 
	                                Postagem
                                WHERE 
	                                Id= '{postagemId}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    var postagensUsuario = new List<Postagem>();

                    int id = -300;

                    while (reader.Read())
                    {
                        id = int.Parse(reader["UsuarioId"].ToString());
                    }

                    return id;
                }
            }
        }

        public async Task<int> InsertAsync(Postagem postagem)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @"INSERT INTO
                                      Postagem (Texto,
                                                DataPostagem,
                                                FotoPost,
                                                UsuarioId)
                                      VALUES (@texto,
                                              @dataPostagem,
                                              @fotoPost,
                                              @usuarioId); SELECT scope_identity();";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("texto", postagem.Texto);
                    cmd.Parameters.AddWithValue("dataPostagem", postagem.DataPostagem);
                    cmd.Parameters.AddWithValue("fotoPost", postagem.FotoPost);
                    cmd.Parameters.AddWithValue("usuarioId", postagem.UsuarioId);

                    con.Open();
                    var id = await cmd.ExecuteScalarAsync().ConfigureAwait(false);

                    return int.Parse(id.ToString());
                }
            }
        }

        public async Task<List<string>> GetByUserIdOnlyPhotosAsync(int usuarioId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT Id,
                                       DataPostagem,
                                       FotoPost,
                                       UsuarioId
                                FROM 
	                                Postagem
                                WHERE
                                    FotoPost <>'' AND
	                                UsuarioId= '{usuarioId}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    var fotosUsuario = new List<string>();

                    while (reader.Read())
                    {
                        var foto = reader["FotoPost"].ToString();

                        fotosUsuario.Add(foto);
                    }

                    return fotosUsuario;
                }
            }
        }

        public async Task<List<Postagem>> GetLinhaDoTempoIdAsync(int idUsuarioLogado)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var SqlCmd = @$"SELECT P.Id,
                                       P.Texto,
                                       P.DataPostagem,
                                       P.FotoPost,
                                       P.UsuarioId,
	                                   U.Nome,
                                       U.FotoPerfil
                                  FROM 
	                                   Postagem P
                                  INNER JOIN 
	                                   Amigos A ON A.IdSeguido = P.UsuarioId
                                  INNER JOIN 
	                                   Usuario U ON U.Id = A.IdSeguido 
                                  WHERE
	                                   A.IdSeguidor = '{idUsuarioLogado}'";

                using (var cmd = new SqlCommand(SqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    List<Postagem> listaPostagensDosAmigos = new List<Postagem>();

                    while (reader.Read())
                    {
                        var postagem = new Postagem(reader["Texto"].ToString(),
                                                    reader["FotoPost"].ToString(),
                                                    int.Parse(reader["UsuarioId"].ToString()),
                                                    DateTime.Parse(reader["DataPostagem"].ToString()));

                        postagem.SetId(int.Parse(reader["Id"].ToString()));

                        listaPostagensDosAmigos.Add(postagem);
                    }

                    //adicionar postagens do usuario

                    /* List<Postagem> ListaOrdenadaDePostagens =
                         listaPostagensDosAmigos.OrderBy(o => o.DataPostagem).ToList();*/

                    return listaPostagensDosAmigos;
                }
            }
        }
    }
}