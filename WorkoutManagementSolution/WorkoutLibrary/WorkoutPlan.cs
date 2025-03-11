using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExerciseLibrary;

namespace WorkoutLibrary
{
    public class WorkoutPlan
    {
        public string PlanName { get; set; }
        public string Description { get; set; }
        public List<Exercise> Exercises { get; set; }

        public WorkoutPlan(string planName, string description)
        {
            PlanName = planName;
            Description = description;
            Exercises = new List<Exercise>();
        }

        public void AddExercise(Exercise exercise)
        {
            Exercises.Add(exercise);
        }

        public void ShowPlan()
        {
            Console.WriteLine($"Workout Plan: {PlanName}\nDescription: {Description}\nExercises:");
            foreach (var exercise in Exercises)
            {
                Console.WriteLine(" - " + exercise);
            }
        }
    }
}