using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CottonCandy.Domain.Entities;
using CottonCandy.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CottonCandy.Repositories
{
    public class ComentarioRepository : IComentarioRepository
    {

        private readonly IConfiguration _configuration;

        public ComentarioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> InserirComentario(Comentario comentario)
        {
            using (var conexao = new SqlConnection(_configuration["ConnectionString"]))
            {

                var comandoSql = @"INSERT INTO
                                          Comentario (UsuarioId,
                                                      PostagemId,
                                                      Texto,
                                                      DataComentario)
                                          VALUES (@usuarioId,
                                                  @postagemId,
                                                  @texto,
                                                  @datacriacao); SELECT scope_identity();";

                using (var comando = new SqlCommand(comandoSql, conexao))
                {
                    comando.CommandType = CommandType.Text;

                    comando.Parameters.AddWithValue("usuarioId", comentario.IdUsuario);
                    comando.Parameters.AddWithValue("postagemId", comentario.IdPostagem);
                    comando.Parameters.AddWithValue("texto", comentario.Texto);
                    comando.Parameters.AddWithValue("datacriacao", comentario.DataCriacao);

                    conexao.Open();
                    var id = await comando
                                    .ExecuteScalarAsync()
                                    .ConfigureAwait(false);

                    return int.Parse(id.ToString());
                }
            }
        }

        public async Task<List<Comentario>> ObterComentarios(int idPostagem)
        {
            using (var conexao = new SqlConnection(_configuration["ConnectionString"]))
            {
                var comandoSql = @$"SELECT 
                                        Id,
	                                    UsuarioId,
                                        PostagemId,
                                        Texto,
                                        DataComentario
                                    FROM 
	                                    Comentario
                                    WHERE 
	                                    PostagemId= '{idPostagem}'";

                using (var comando = new SqlCommand(comandoSql, conexao))
                {
                    comando.CommandType = CommandType.Text;
                    conexao.Open();

                    var reader = await comando
                                         .ExecuteReaderAsync()
                                         .ConfigureAwait(false);

                    var comentariosDaPostagem = new List<Comentario>();

                    while (reader.Read())
                    {
                        var comentario = new Comentario(int.Parse(reader["Id"].ToString()),
                                                        int.Parse(reader["PostagemId"].ToString()),
                                                        int.Parse(reader["UsuarioId"].ToString()),
                                                        reader["Texto"].ToString(),
                                                        DateTime.Parse(reader["DataComentario"].ToString()));

                        comentariosDaPostagem.Add(comentario);
                    }

                    return comentariosDaPostagem;
                }
            }
        }
    }
}