using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlan
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkoutPlan plan = new WorkoutPlan("Full Body Workout", "A simple full-body routine.");
            plan.AddExercise(new Exercise("Push-ups", 15, 3));
            plan.AddExercise(new Exercise("Squats", 20, 3));
            plan.AddExercise(new Exercise("Plank", 60, 3)); // 60 seconds

            plan.ShowPlan();

            Console.ReadLine();
        }
    }
}