using CottonCandy.Domain.Entities.ValueObject;
using System.IO;
using System.Threading.Tasks;

namespace CottonCandy.Domain.Core.Interfaces
{
    public interface IStorageHelper
    {
        Task<ImageBlob> Upload(Stream stream, string nameFile);
        bool IsImage(string nameFile);
    }
}