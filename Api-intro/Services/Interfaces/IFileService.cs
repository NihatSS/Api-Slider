using Api_intro.Helpers.Responses;

namespace Api_intro.Services.Interfaces
{
    public interface IFileService
    {
        Task<UploadResponse> UploadAsync(IFormFile file);
        void Delete(string fileName);
    }
}
