namespace MyNetflixClone.Interfaces;

public interface IS3Service
{
    Task<string> UploadMovieAsync(Stream fileStream, string fileName);
}
