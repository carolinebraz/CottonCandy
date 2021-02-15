using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CottonCandy.Domain.Entities;
using CottonCandy.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace CottonCandy.Repositories
{
    public class ComentarioRepository : IComentarioRepository //sublinhado pois  ainda não implementei os dois métodos


    {

        private readonly IConfiguration _configuration;

        public ComentarioRepository(IConfiguration configuration)
        {

            _configuration = configuration;

        }

         async Task<List<Comentario>> IComentarioRepository.PegarComentariosPorIdPostagemAsync(int idPostagem)
            
        {

            //método para pegar lista de comentários da postagem de id idPostagem

            using (var conexao = new SqlConnection(_configuration["ConnectionString"]))
            {
                var comandoSql = @$"SELECT Id,
	                                   UsuarioId,
                                       PostagemId,
                                       Texto,
                                       Criacao
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
                                                    int.Parse(reader["IdPostagem"].ToString()),
                                                    int.Parse(reader["IdUsuario"].ToString()),
                                                    reader["Texto"].ToString(),
                                                    DateTime.Parse(reader["DataCriacao"].ToString()));

                        comentariosDaPostagem.Add(comentario);
                    }//while

                    return comentariosDaPostagem;
                }//using
            }//1o using




        } //fim de PegarComentariosPorIdPostagemAsync





        //terminar depois que eu fizer o get
        async Task<int> IComentarioRepository.InserirAsync(Comentario comentario)
        {
            using (var conexao = new SqlConnection(_configuration["ConnectionString"]))
            {

                var comandoSql = @"INSERT INTO
                                Comentario (UsuarioId,
                                             PostagemId,
                                             Texto,
                                             Criacao)
                                VALUES (@idUsuario,
                                        @idPostagem,
                                        @texto,
                                        @datacriacao); SELECT scope_identity();";

                //essa nomenclatura  quem define é o método get acima

                using (var comando = new SqlCommand(comandoSql, conexao))
                {
                    comando.CommandType = CommandType.Text;

                    comando.Parameters.AddWithValue("usuarioId", comentario.IdUsuario);
                    comando.Parameters.AddWithValue("postagemId", comentario.IdPostagem);
                    comando.Parameters.AddWithValue("texto", comentario.Texto);
                    comando.Parameters.AddWithValue("criacao", comentario.DataCriacao);

                    conexao.Open();
                    var id = await comando
                                    .ExecuteScalarAsync()
                                    .ConfigureAwait(false);

                    return int.Parse(id.ToString());
                } //retorna a id do Comentario inserido



            }

        }//fim de InserirAsync(Comentario)


    }
}
