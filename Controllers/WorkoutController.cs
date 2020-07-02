using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StrengthAndHonor.Documents;
using StrengthAndHonor.Repositories;

namespace StrengthAndHonor.Controllers
{
    [ApiController]
    [Route("api/workouts")]
    public class WorkoutController : ControllerBase
    {
        private readonly IMongoRepository<Workout> _workoutRepository;

        public WorkoutController(IMongoRepository<Workout> workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

        [HttpGet("")]
        public IEnumerable<Workout> Get()
        {
            var workouts = _workoutRepository.FilterBy(x => true);
            return workouts;
        }

        [HttpGet("")]
        public Workout GetByDate(string date)
        {
            var workout = _workoutRepository.FindOne(x => x.Date == date);
            return workout;
        }

        [HttpPost("")]
        public async Task Post(Workout workout)
        {
            await _workoutRepository.InsertOneAsync(workout);
        }

        [HttpGet("fitNotesImportImperialWeights")]
        public IActionResult FitNotesImportImperialWeights() 
        {            
            var fullAssemblyPath = Assembly.GetExecutingAssembly().Location;
            var dirInfo = new DirectoryInfo(fullAssemblyPath);
            var parentDir = dirInfo.Parent;
            var pathToCsv = Path.Combine(parentDir.FullName, "FitNotes_Export.csv");
            var lines = System.IO.File.ReadAllLines(pathToCsv);
            var workouts = new Dictionary<string, Workout>();

            foreach (var line in lines)
            {
                var s = line.Split(',');
                var date = s[0];
                var exercise = s[1];
                var category = s[2];

                float? weight;
                ushort? reps;
                float? distance;

                if (string.IsNullOrWhiteSpace(s[3]))
                    weight = null;
                else
                    weight = float.Parse(s[3]);

                if (string.IsNullOrWhiteSpace(s[4]))
                    reps = null;
                else
                    reps = ushort.Parse(s[4]);

                if (string.IsNullOrWhiteSpace(s[5]))
                    distance = null;
                else
                    distance = float.Parse(s[5]);

                var distanceUnit = s[6];
                var time = s[7];
                Workout workout;

                if (workouts.ContainsKey(date))
                {
                    workout = workouts[date];
                }
                else
                {
                    workout = new Workout
                    {
                        Date = date,
                        Sets = new List<WorkoutSet>()
                    };
                    workouts.Add(date, workout);
                    _workoutRepository.InsertOne(workout);
                }

                workout.Sets.Add(new WorkoutSet
                {
                    ExerciseName = exercise,
                    WeightAndReps = weight.HasValue ? new WeightAndReps
                    {
                        Weight = weight.Value,
                        WeightUnit = "lbs",
                        Reps = reps.Value
                    } : null,
                    DistanceAndTime = distance.HasValue ? new DistanceAndTime
                    {
                        Distance = distance.Value,
                        DistanceUnit = distanceUnit,
                        Time = time
                    } : null
                });

                _workoutRepository.ReplaceOne(workout);
            }

            return Ok(lines);
        }
    }
}
