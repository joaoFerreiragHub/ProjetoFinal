namespace ProjetoFoodTracker.Services
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(IFormFile file);
        void UploadtoDb(IFormFile file);

    }
}
