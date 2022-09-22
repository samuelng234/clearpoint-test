using AutoMapper;
using Microsoft.Extensions.Logging;

namespace TodoList.Api.Services
{
    public class BaseService
    {
        protected readonly ILogger _logger;
        protected readonly IMapper _mapper;

        public BaseService(ILogger logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }
    }
}
