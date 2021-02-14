using CottonCandy.Application.AppPostagem.Interfaces;
using CottonCandy.Domain.Entities;
using CottonCandy.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppPostagem
{
    public class AlbumFotosAppService : IAlbumFotosAppService
    {
        private readonly IPostagemRepository _postagemRepository;
        //private readonly ILogged _logged;
        public AlbumFotosAppService(IPostagemRepository postagemRepository)
        //  ILogged logged)
        {
            _postagemRepository = postagemRepository;
            // _logged = logged;
        }

        public async Task<List<String>> GetByUserIdOnlyPhotoAsync(int userId)
        {
            var userId = 1; //_logged.GetUserLoggedId();

            var fotos = await _postagemRepository.GetByUserIdOnlyPhotosAsync(userId)
                                    .ConfigureAwait(false);
            return fotos;
        }
    }
}
