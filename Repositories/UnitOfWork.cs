using RepoInterfaces;

namespace Repositories
{
    internal class UnitOfWork
    {
        private readonly NetflixDbContext _context;
        private Lazy<IMovieRepository> _movieRepository;
        private Lazy<IUserRepository> _userRepository;
        private Lazy<IUserRatingsRepository> _userRatingRepository;

        public UnitOfWork(NetflixDbContext context)
        {
            _context = context;
            _movieRepository = new Lazy<IMovieRepository>(() => new MovieRepository(_context));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));
            _userRatingRepository = new Lazy<IUserRatingsRepository>(() => new UserRatingsRepository(_context));
        }

        public IMovieRepository MovieRepository => _movieRepository.Value;
        public IUserRepository UserRepository => _userRepository.Value;
        public IUserRatingsRepository UserRatingRepository => _userRatingRepository.Value;

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void BeginTransaction()
        {
            if (_context.Database.CurrentTransaction != null)
            {
                throw new InvalidOperationException("A Transaction is already in progress.");
            }

            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            try
            {
                _context.Database.CurrentTransaction?.Commit();
            }
            catch
            {
                _context.Database.CurrentTransaction?.Rollback();
                throw;
            }
            finally
            {
                _context.Database.CurrentTransaction?.Dispose();
            }
        }

        public void RollBack()
        {
            try
            {
                _context.Database.CurrentTransaction?.Rollback();
            }
            finally
            {
                _context.Database.CurrentTransaction?.Dispose();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
