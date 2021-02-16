using System;
using System.Collections.Generic;
using System.Text;

namespace LesFruits
{
    public class Fruit
    {
        public string Nom { get; set; }

        public override bool Equals(object obj)
        {
            Fruit fruit = obj as Fruit;
            return fruit != null &&
                   Nom == fruit.Nom; 
        }

        public override int GetHashCode()
        {
            return 217408413 + EqualityComparer<string>.Default.GetHashCode(Nom);
        }

        public override string ToString()
        {
            return Nom;
        }

    }
}
