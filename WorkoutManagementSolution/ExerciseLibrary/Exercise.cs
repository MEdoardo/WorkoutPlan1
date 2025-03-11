using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseLibrary
{
    public class Exercise
    {
        public string Name { get; set; }
        public int Repetitions { get; set; }
        public int Sets { get; set; }

        public Exercise(string name, int repetitions, int sets)
        {
            Name = name;
            Repetitions = repetitions;
            Sets = sets;
        }

        public override string ToString()
        {
            return $"{Name}: {Sets} sets x {Repetitions} reps";
        }
    }
}