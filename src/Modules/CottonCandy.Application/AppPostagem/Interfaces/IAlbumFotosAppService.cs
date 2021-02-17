using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppPostagem.Interfaces
{
    public interface IAlbumFotosAppService
    {
        Task<List<String>> GetByUserIdOnlyPhotoAsync();
    }
}