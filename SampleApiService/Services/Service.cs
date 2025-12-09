using Elastic.CommonSchema;
using Repository.DapperRepo;
using Repository.Models;
using Repository.Repo;
using SampleApiService.Controllers;

namespace SampleApiService.Services
{
    public class Service : IService
    {
        private readonly ILogger<Service> _logger;
        private IRepoService repo;
        private IProfileRepository _profileRepository;
        public Service(IRepoService _repo, ILogger<Service> logger, IProfileRepository profileRepository)
        {
            repo = _repo;
            _logger = logger;
            _profileRepository = profileRepository;
        }

        public string SaveMoRequests()
        {
            _logger.LogDebug("Log written in Service Layer");
            return repo.SaveMoRequests();
        }

        public string UpdateMoRequests()
        {
            _logger.LogDebug("Log written in Service Layer");
            return repo.UpdateMoRequests();
        }
    }
}
