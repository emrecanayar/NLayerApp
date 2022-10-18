using Microsoft.Extensions.Logging;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Models;
using NLayerApp.Core.Repositories;
using NLayerApp.Core.Services;
using NLayerApp.Core.UnitOfWorks;
using NLayerApp.Repository.Contexts;
using NLayerApp.Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Service.Services
{
    public class LogService : Service<Log>, ILogService
    {
        private readonly ILogger _logger;
        protected readonly ApplicationDbContext _context;
        private CurrentUserService _currentUserService;
        private const string STR_LOG_FILE_NAME = "Log.json";
        private const string STR_ERROR_FILE_NAME = "Error.json";
        private const string STR_DATE_FORMAT = "yyyyMMdd";

        public LogService(IGenericRepository<Log> repository, IUnitOfWork unitOfWork, ApplicationDbContext context, ILoggerFactory loggerFactory, CurrentUserService currentUserService) : base(repository, unitOfWork)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task CreateLog(LogCreateModel logModel)
        {
            Log log = ObjectMapper.Mapper.Map<Log>(logModel);
            await LogToDb(log);
        }

        private Task LogToDb(Log log)
        {
            try
            {
                _context.Set<Log>().Add(log);
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                _logger.LogError($"DB Logging Exception: {exception.Message} {Environment.NewLine} " +
                    $"Source: {exception.Source} {Environment.NewLine}" +
                    $"Stack Tree: {exception.StackTrace}");
            }
            return Task.CompletedTask;
        }
    }
}
