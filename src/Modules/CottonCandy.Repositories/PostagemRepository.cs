using CottonCandy.Domain.Entities;
using CottonCandy.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
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
                                                    int.Parse(reader["UsuarioId"].ToString()));


                        postagensUsuario.Add(postagem);
                    }

                    return postagensUsuario;
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

        public async Task<List<String>> GetByUserIdOnlyPhotosAsync(int usuarioId)
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
                                    FotoPost = <>'';
	                                UsuarioId= '{usuarioId}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    var fotosUsuario = new List<String>();

                    while (reader.Read())
                    {
                        var foto = reader["FotoPost"].ToString();


                        fotosUsuario.Add(foto);
                    }

                    return fotosUsuario;
                }
            }
        }
    }
    
}
