using System;
using System.Collections.Generic;
using System.Linq;

namespace LesFruits
{
    public class Program
    {
        static void Main(string[] args)
        {
            var fruits = new List<Fruit>()
            {
                new Fruit() { Nom = "Abricot"},   new Fruit() { Nom = "Banane"},    new Fruit() { Nom = "Cerise"},  new Fruit() { Nom = "Datte"},
                new Fruit() { Nom = "Framboise"}, new Fruit() { Nom = "Grenade"},   new Fruit() { Nom = "Kiwi"},    new Fruit() { Nom = "Lime"},
                new Fruit() { Nom = "Mangue"},    new Fruit() { Nom = "Nectarine"}, new Fruit() { Nom = "Olive"},   new Fruit() { Nom = "Pomme"}
            };

            var personnes = new List<Personne>()
            {
                new Personne() { Nom = "Alice", Genre = 'F', Age = 22,   FruitsAimes = new List<Fruit>() { fruits[0], fruits[1], fruits[10] } },
                new Personne() { Nom = "Bob", Genre = 'M', Age = 12,     FruitsAimes = new List<Fruit>() { fruits[4], fruits[5], fruits[6], fruits[7], fruits[8] } },
                new Personne() { Nom = "Charlie", Genre = 'M', Age = 31, FruitsAimes = new List<Fruit>() { fruits[0], fruits[1], fruits[4], fruits[11] } },
                new Personne() { Nom = "Diane", Genre = 'F', Age = 45,   FruitsAimes = new List<Fruit>() { fruits[2], fruits[4] } },
                new Personne() { Nom = "Eve", Genre = 'F', Age = 4,      FruitsAimes = new List<Fruit>() { } },
            };

            Console.WriteLine("Les fruits qui contiennent la lettre A sont : ");
            IEnumerable<Fruit> reponse = fruits.Where(AvecA);

            Console.WriteLine($"{string.Join(separator: ", ", values: reponse)}");
            double ageMoyenM = personnes.Where(p => p.Genre == 'M').Average(p => p.Age);
            Console.WriteLine($"Age moyen des hommes {ageMoyenM}");

            var query = personnes.Where(p => p.Genre == 'M').
                            OrderBy(p => p.Age).
                            Select(p => new { p.Age, p.Genre });
            Console.WriteLine($"Age et genre des hommes : {string.Join(separator: ", ", values: query)}");

            /*Custom*/
            Console.WriteLine('\n');
            PrintIEnumerable(GetEnfantsExtension(personnes));
            PrintIEnumerable(GetEnfantsRequete(personnes));

            Console.WriteLine('\n');
            Console.WriteLine(GetPlusVieilleRequete(personnes));
            Console.WriteLine(GetPlusVieilleExtension(personnes));

            Console.WriteLine('\n');
            PrintIEnumerableFruits(GetFruitFavorisExtension(personnes));
            PrintIEnumerableFruits(GetFruitsFavorisRequete(personnes));

            Console.WriteLine('\n');
            ParGenre(personnes);

            Console.ReadKey();

        }
        static bool AvecA(Fruit fruit)
        {
            return fruit.Nom.ToUpper().Contains("A");
        }

        static void PrintIEnumerable(IEnumerable<Personne> list)
        {
            foreach (Personne personne in list)
                Console.WriteLine(personne);
        }

        static void PrintIEnumerableFruits(IEnumerable<Fruit> list)
        {
            foreach (Fruit fruit in list)
                Console.WriteLine(fruit);
        }

        static IEnumerable<Personne> GetEnfantsExtension(IEnumerable<Personne> personnes)
        {
            var enfants = personnes.Where(personne => personne.Age < 18);
            return enfants;
        }

        static IEnumerable<Personne> GetEnfantsRequete(IEnumerable<Personne> personnes)
        {
            var enfants = from personne in personnes
                          where personne.Age < 18
                          select personne;
            return enfants;
        }

        static Personne GetPlusVieilleExtension(IEnumerable<Personne> personnes)
        {
            return personnes.OrderByDescending(personne => personne.Age).First();
        }

        static Personne GetPlusVieilleRequete(IEnumerable<Personne> personnes)
        {
            var list = from personne in personnes
                       orderby personne.Age descending
                       select personne;
            return list.First();
        }

        static IEnumerable<Fruit> GetFruitFavorisExtension(IEnumerable<Personne> personnes)
        {
            var list = personnes.SelectMany(personne => personne.FruitsAimes).GroupBy(fruit => fruit).OrderByDescending(g => g.Count()).Select(g => g.Key);
            return list;
        }

        static IEnumerable<Fruit> GetFruitsFavorisRequete(IEnumerable<Personne> personnes)
        {
            var list = from personne in personnes
                       from f in personne.FruitsAimes
                       group f by f into g
                       orderby g.Count() descending
                       select g.Key;
            return list;    
        }

        static void ParGenre(IEnumerable<Personne> personnes)
        {
            var list = personnes.GroupBy(p => p.Genre).Select(p => new { number = p.Count(), minAge = p.Min(p => p.Age), maxAge = p.Max(p => p.Age), gender = p.Key });
            foreach (var item in list)
                Console.WriteLine("Genre : " + item.gender + "\nNombre de personne de ce genre : " + item.number + 
                                  "\nL'age de la personne la plus vieille de ce genre : " + item.maxAge +
                                  "\nL'age de la personne la plus jeune de ce genre : " + item.minAge + "\n");
        }
    }
}
