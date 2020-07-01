using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StrengthAndHonor.Documents;
using StrengthAndHonor.Repositories;

namespace StrengthAndHonor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkoutController : ControllerBase
    {
        private readonly IMongoRepository<Workout> _workoutRepository;

        public WorkoutController(IMongoRepository<Workout> workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

        [HttpPost("")]
        public async Task Post(Workout workout)
        {
            await _workoutRepository.InsertOneAsync(workout);
        }

        [HttpGet("")]
        public IEnumerable<Workout> Get()
        {
            var workouts = _workoutRepository.FilterBy(x => true);
            return workouts;
        }
    }
}
