using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StrengthAndHonor.Documents;
using StrengthAndHonor.Repositories;

namespace StrengthAndHonor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {
        private readonly IMongoRepository<User> _userRepository;

        public SampleController(IMongoRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("registerUser")]
        public async Task AddUser(string firstName, string lastName)
        {
            var user = new User()
            {
                FirstName = firstName,
                LastName = lastName
            };

            await _userRepository.InsertOneAsync(user);
        }

        [HttpGet("getUserData")]
        public IEnumerable<string> GetUserData()
        {
            var users = _userRepository
                .FilterBy(x => x.FirstName != "test", projection => projection.FirstName);

            return users;
        }
    }
}
