namespace ProjetoFoodTracker.Services
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(IFormFile ufile);
        void UploadtoDb(/*IFormFile file*/);

    }
}
