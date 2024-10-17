using Contracts;
using Entities.Models;


namespace Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly RepositoryContext _context;

        public LogRepository(RepositoryContext context)
        {
            _context = context;
        }

        public async Task LogErrorAsync(string errorMessage)
        {
            var errorLog = new Log
            {
                ErrorMessage = errorMessage,
                ErrorDate = DateTime.UtcNow,
            };

            await _context.Logs.AddAsync(errorLog);
            await _context.SaveChangesAsync(); // Сохранение логов в отдельной транзакции
        }
    }
}
