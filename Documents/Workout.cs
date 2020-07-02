using System;
using System.Collections.Generic;

namespace StrengthAndHonor.Documents
{
    [BsonCollection("workouts")]
    public class Workout : Document
    {
        public string Date { get; set; }
        public List<Exercise> Excercises { get; set; }
        public List<WorkoutSet> Sets { get; set; }
    }

    public class Exercise
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }        
    }

    /*public enum ExerciseType
    {
        WeightAndReps,
        DistanceAndTime
    }*/

    public class WorkoutSet
    {
        public string ExerciseName { get; set; }
        public WeightAndReps WeightAndReps { get; set; }
        public DistanceAndTime DistanceAndTime { get; set; }
        public string Comment { get; set; }
        public float? RPE { get; set; }
    }

    public class WeightAndReps
    {
        public float Weight { get; set; }
        public string WeightUnit { get; set; }
        public ushort Reps { get; set; }
    }

    public class DistanceAndTime
    {
        public float Distance { get; set; }
        public string DistanceUnit { get; set; }
        public string Time { get; set; }
    }

/*    public enum WeightUnit
    {
        Kilograms,
        Pounds
    }

    public enum DistanceUnit
    {
        Meters,
        Kilometers,
        Feet,
        Miles
    }
*/
}
