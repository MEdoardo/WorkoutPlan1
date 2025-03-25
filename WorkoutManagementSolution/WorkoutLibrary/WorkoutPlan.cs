using System;
using System.Collections.Generic;
using ExerciseLibrary;

namespace WorkoutLibrary
{
    public class WorkoutPlan
    {
        public string PlanName { get; set; }
        public string Description { get; set; }
        public WorkoutType Type { get; set; }
        public List<Exercise> Exercises { get; set; }


        public static List<WorkoutPlan> WorkoutPlans = new List<WorkoutPlan>();

        public WorkoutPlan(string planName, string description, WorkoutType type)
        {
            PlanName = planName;
            Description = description;
            Type = type;
            Exercises = new List<Exercise>();
        }

        public void AddExercise(Exercise exercise)
        {
            Exercises.Add(exercise);
        }

        public void ShowPlan()
        {
            Console.WriteLine($"Workout Plan: {PlanName} ({Type})\nDescription: {Description}\nExercises:");
            foreach (var exercise in Exercises)
            {
                Console.WriteLine(" - " + exercise);
            }
        }
    }
}
