namespace RepoInterfaces;

public interface IUnitOfWork : IDisposable
{
    IMovieRepository MovieRepository { get; }
    IUserRepository UserRepository { get; }
    IUserRatingsRepository UserRatingRepository { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync();
    void BeginTransaction();
    void CommitTransaction();
    void RollBack();
}
