namespace RepoInterfaces;

public interface IUnitOfWork : IDisposable
{
    IMovieRepository MovieRepository { get; }
    IUserRepository UserRepository { get; }
    IUserRatingsRepository UserRatingRepository { get; }
    int SaveChanges();
    void BeginTransaction();
    void CommitTransaction();
    void RollBack();
}
