using System;
using WorkoutLibrary;

namespace WorkoutLibrary
{
    public class User
    {
        public string Name { get; set; }
        public WorkoutPlan WorkoutPlan { get; set; } // Assigned workout plan

        public User(string name)
        {
            Name = name;
            WorkoutPlan = null;
        }
    }
}
