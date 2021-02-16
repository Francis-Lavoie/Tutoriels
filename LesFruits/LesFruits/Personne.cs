using System;
using System.Collections.Generic;
using System.Text;

namespace LesFruits
{
    public class Personne
    {
        public string Nom { get; set; }
        public int Age { get; set; }
        public char Genre { get; set; }

        public IList<Fruit> FruitsAimes { get; set; }

        public override bool Equals(object obj)
        {
            Personne personne = obj as Personne;
            return personne != null &&
                   Nom == personne.Nom &&
                   Age == personne.Age &&
                   Genre == personne.Genre &&
                   EqualityComparer<IList<Fruit>>.Default.Equals(FruitsAimes, personne.FruitsAimes);
        }

        public override int GetHashCode()
        {
            var hashCode = -821498492;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nom);
            hashCode = hashCode * -1521134295 + Age.GetHashCode();
            hashCode = hashCode * -1521134295 + Genre.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<IList<Fruit>>.Default.GetHashCode(FruitsAimes);
            return hashCode;
        }

        public override string ToString()
        {
            return $"{Nom} aime {string.Join(", ", FruitsAimes)}";
        }

    }
}
