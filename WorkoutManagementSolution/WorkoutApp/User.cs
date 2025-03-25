namespace WorkoutLibrary
{
    public class User
    {
        public string Name { get; set; }
        public WorkoutPlan WorkoutPlan { get; set; }

        public User(string name)
        {
            Name = name;
            WorkoutPlan = null;
        }
    }
}
