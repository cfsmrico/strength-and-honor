using System;
using System.Collections.Generic;

namespace StrengthAndHonor.Documents
{
    public class Workout
    {
        public DateTime Date { get; set; }
        public List<Exercise> Excercises { get; set; }
        public List<WorkoutSet> Sets { get; set; }
    }

    public class Exercise
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public ExerciseType Type { get; set; }
        public string Notes { get; set; }        
    }

    public enum ExerciseType
    {
        WeightAndReps,
        DistanceAndTime
    }

    public class WorkoutSet
    {
        public WeightAndReps WeightAndReps { get; set; }
        public DistanceAndTime DistanceAndTime { get; set; }
        public string Comment { get; set; }
        public float? RPE { get; set; }
    }

    public class WeightAndReps
    {
        public float Weight { get; set; }
        public WeightUnit WeightUnit { get; set; }
        public int Reps { get; set; }
    }

    public class DistanceAndTime
    {
        public float Distance { get; set; }
        public DistanceUnit DistanceUnit { get; set; }
        public DateTime Time { get; set; }
    }

    public enum WeightUnit
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
}
