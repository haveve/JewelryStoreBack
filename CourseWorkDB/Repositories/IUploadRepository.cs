namespace CourseWorkDB.Repositories
{
    public interface IUploadRepository
    {
        Task<string> SaveImgAsync(IFormFile formFile, int categoryId);
        void DeleteFile(string relatingPath);
    }
}
