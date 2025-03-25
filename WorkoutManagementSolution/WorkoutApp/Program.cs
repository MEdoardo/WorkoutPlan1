using System;
using System.Collections.Generic;
using System.IO;
using WorkoutLibrary;
using ExerciseLibrary;

namespace WorkoutApp
{
    class Program
    {
        static List<User> users = new List<User>();

        static void Main(string[] args)
        {
            LoadData(); // Load existing data from file

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Workout Planner ===");
                Console.WriteLine("1. Add User");
                Console.WriteLine("2. Create Workout Plan");
                Console.WriteLine("3. Assign Workout to User");
                Console.WriteLine("4. Show User's Workout");
                Console.WriteLine("5. Save and Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddUser();
                        break;
                    case "2":
                        CreateWorkoutPlan();
                        break;
                    case "3":
                        AssignWorkoutToUser();
                        break;
                    case "4":
                        ShowUserWorkout();
                        break;
                    case "5":
                        SaveData();
                        return;
                    default:
                        Console.WriteLine("Invalid choice! Press Enter to try again...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void AddUser()
        {
            Console.Write("Enter User Name: ");
            string name = Console.ReadLine();
            users.Add(new User(name));
            Console.WriteLine("User added successfully!");
            Console.ReadLine();
        }

        static void CreateWorkoutPlan()
        {
            Console.Write("Enter Workout Plan Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Description: ");
            string description = Console.ReadLine();

            Console.WriteLine("Choose Workout Type:");
            foreach (var type in Enum.GetValues(typeof(WorkoutType)))
            {
                Console.WriteLine($"{(int)type}. {type}");
            }
            Console.Write("Enter Type Number: ");
            if (!int.TryParse(Console.ReadLine(), out int typeChoice) || !Enum.IsDefined(typeof(WorkoutType), typeChoice))
            {
                Console.WriteLine("Invalid workout type! Press Enter to continue...");
                Console.ReadLine();
                return;
            }

            WorkoutType typeEnum = (WorkoutType)typeChoice;
            WorkoutPlan plan = new WorkoutPlan(name, description, typeEnum);

            Console.WriteLine("Add Exercises (Enter 'done' to finish):");
            while (true)
            {
                Console.Write("Exercise Name: ");
                string exName = Console.ReadLine();
                if (exName.ToLower() == "done") break;

                Console.Write("Repetitions: ");
                if (!int.TryParse(Console.ReadLine(), out int reps))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                Console.Write("Sets: ");
                if (!int.TryParse(Console.ReadLine(), out int sets))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                plan.AddExercise(new Exercise(exName, reps, sets));
            }

            WorkoutPlan.WorkoutPlans.Add(plan);
            Console.WriteLine("Workout plan created successfully!");
            Console.ReadLine();
        }

        static void AssignWorkoutToUser()
        {
            if (users.Count == 0 || WorkoutPlan.WorkoutPlans.Count == 0)
            {
                Console.WriteLine("Users or workout plans are missing!");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Select User:");
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {users[i].Name}");
            }
            if (!int.TryParse(Console.ReadLine(), out int userChoice) || userChoice < 1 || userChoice > users.Count)
            {
                Console.WriteLine("Invalid choice! Press Enter to continue...");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Select Workout Plan:");
            for (int i = 0; i < WorkoutPlan.WorkoutPlans.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {WorkoutPlan.WorkoutPlans[i].PlanName}");
            }
            if (!int.TryParse(Console.ReadLine(), out int planChoice) || planChoice < 1 || planChoice > WorkoutPlan.WorkoutPlans.Count)
            {
                Console.WriteLine("Invalid choice! Press Enter to continue...");
                Console.ReadLine();
                return;
            }

            users[userChoice - 1].WorkoutPlan = WorkoutPlan.WorkoutPlans[planChoice - 1];
            Console.WriteLine("Workout assigned successfully!");
            Console.ReadLine();
        }

        static void ShowUserWorkout()
        {
            Console.Write("Enter User Name: ");
            string name = Console.ReadLine();
            User user = users.Find(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (user != null && user.WorkoutPlan != null)
            {
                Console.WriteLine($"Workout Plan for {user.Name}:");
                user.WorkoutPlan.ShowPlan();
            }
            else
            {
                Console.WriteLine("No workout assigned.");
            }
            Console.ReadLine();
        }

        static void SaveData()
        {
            using (StreamWriter writer = new StreamWriter("WorkoutData.txt"))
            {
                foreach (var user in users)
                {
                    writer.WriteLine($"User: {user.Name}");
                    if (user.WorkoutPlan != null)
                    {
                        writer.WriteLine($"Workout Plan: {user.WorkoutPlan.PlanName}");
                        writer.WriteLine($"Description: {user.WorkoutPlan.Description}");
                        writer.WriteLine($"Type: {user.WorkoutPlan.Type}");
                        foreach (var ex in user.WorkoutPlan.Exercises)
                        {
                            writer.WriteLine($"{ex.Name} - {ex.Sets} sets x {ex.Repetitions} reps");
                        }
                    }
                }
            }
            Console.WriteLine("Data saved successfully!");
        }

        static void LoadData()
        {
            if (File.Exists("WorkoutData.txt"))
            {
                string[] lines = File.ReadAllLines("WorkoutData.txt");
                User currentUser = null;
                WorkoutPlan currentPlan = null;

                foreach (var line in lines)
                {
                    if (line.StartsWith("User:"))
                    {
                        currentUser = new User(line.Substring(6));
                        users.Add(currentUser);
                    }
                    else if (line.StartsWith("Workout Plan:"))
                    {
                        currentPlan = new WorkoutPlan(line.Substring(14), "Loaded from file", WorkoutType.General);
                        if (currentUser != null)
                            currentUser.WorkoutPlan = currentPlan;
                    }
                    else if (line.StartsWith("Description:"))
                    {
                        if (currentPlan != null)
                            currentPlan.Description = line.Substring(12);
                    }
                    else if (line.StartsWith("Type:"))
                    {
                        if (currentPlan != null && Enum.TryParse(line.Substring(5), out WorkoutType type))
                            currentPlan.Type = type;
                    }
                    else if (line.Contains("sets x"))
                    {
                        string[] parts = line.Split(new[] { " - " }, StringSplitOptions.None);

                        if (parts.Length < 2)
                        {
                            Console.WriteLine($"Skipping invalid exercise entry: {line}");
                            continue;
                        }

                        string exerciseName = parts[0].Trim();
                        string[] exDetails = parts[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        if (exDetails.Length < 4) // Ensure proper format
                        {
                            Console.WriteLine($"Skipping invalid exercise format: {line}");
                            continue;
                        }

                        if (int.TryParse(exDetails[2], out int reps) && int.TryParse(exDetails[0], out int sets))
                        {
                            currentPlan?.AddExercise(new Exercise(exerciseName, reps, sets));
                        }
                        else
                        {
                            Console.WriteLine($"Skipping invalid numbers in: {line}");
                        }
                    }

                }
            }
        }
    }
}
