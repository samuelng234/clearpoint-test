using Microsoft.Extensions.Logging;

namespace TodoList.Api.Repositories
{
    public class BaseRepository
    {
        protected readonly ILogger _logger;

        public BaseRepository(ILogger logger)
        {
            _logger = logger;
        }
    }
}
