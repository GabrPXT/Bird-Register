using System;
using System.IO;
using System.Collections.Generic;
using Periquitos_Dados.Entities;
using Periquitos_Dados.Entities.Enums;

namespace Periquitos_Dados
{
    class Program
    {
        static void Header()
        {
            Console.Clear();
            Console.WriteLine("Parakeet Data\n\n");
        }

        static char Menu()
        {
            Console.WriteLine("Options\n");
            Console.WriteLine("[ 1 ] - Add an Egg\n"
                + "[ 2 ] - Add an Bird\n"
                + "[ 3 ] - Set Bird Parents\n"
                + "[ 4 ] - Set Bird Birthday\n"
                + "[ 5 ] - Set Bird Sex\n"
                + "[ 6 ] - Set Bird Name\n"
                + "[ 7 ] - Print Birds\n"
                + "[ 8 ] - Print Bird Complete Data\n");
            Console.WriteLine(" [  ]");

            Console.SetCursorPosition(3, 14);
            return Console.ReadKey().KeyChar;
        }

        static Parakeet SelectParent(List<Parakeet> parakeets, bool isMother)
        {
            int aux = 0;

            if(isMother)
            {
                foreach (Parakeet parakeet in parakeets)
                {
                    if (parakeet.Sex == Sexes.FEMALE)
                    {
                        Console.WriteLine($"{parakeet.Id.ToString("D3")} - {parakeet.Name}");
                    }
                }
            }
            
            else 
            {
                foreach (Parakeet parakeet in parakeets)
                {
                    if (parakeet.Sex == Sexes.MALE)
                    {
                        Console.WriteLine($"{parakeet.Id.ToString("D3")} - {parakeet.Name}");
                    }
                }
            }

            Console.Write("\nEnter the parakeet ID: ");
            int id = int.Parse(Console.ReadLine());

            if (parakeets[id-1] != null)
            {
                return parakeets[id-1];
            }

            Console.WriteLine("ERROR: Invalid ID");
            Console.ReadKey();

            return null;
        }

        static Parakeet NewBird(List<Parakeet> parakeets)
        {
            Header();

            int id = parakeets.Count+1;

            Console.Write("Enter the bird Phase: ");
            Phase phase = (Phase)int.Parse(Console.ReadLine());

            Console.Write("Enter the bird Birthday (dd/MM/yyyy): ");
            DateTime born = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter the bird Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter the bird Sex (F/M/U): ");
            Sexes sex = (Sexes)char.Parse(Console.ReadLine());

            return new Parakeet(id, phase, born, name, sex);
        }

        static Parakeet NewEgg(List<Parakeet> parakeets)
        {
            Header();

            int id = parakeets.Count + 1;

            Console.Write("Enter the Egg Lay date (dd/MM/yyyy): ");
            DateTime eggLay = DateTime.Parse(Console.ReadLine());
            
            Console.WriteLine("\nSelect the Mother");
            Parakeet mother = SelectParent(parakeets, true);

            Console.WriteLine("\nSelect the Father");
            Parakeet father = SelectParent(parakeets, false);
            
            return new Parakeet(id, eggLay, mother, father);
        }

        static void SetBirdParents(List<Parakeet> parakeets)
        {
            Header();

            Console.WriteLine(" ID | NAME or NICK");

            foreach (Parakeet parakeet in parakeets)
            {
                Console.WriteLine($"{parakeet.Id.ToString("D3")} - {parakeet.Name}");
            }

            Console.Write("Enter the bird ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Select the Mother");
            Parakeet mother = SelectParent(parakeets, true);

            Console.WriteLine("Select the Father");
            Parakeet father = SelectParent(parakeets, false);

            parakeets[--id].SetParents(mother, father);
        }

        static void SetBirdBirthday(List<Parakeet> parakeets)
        {
            Header();

            Console.WriteLine(" ID | EGG DATE\n");

            foreach (Parakeet parakeet in parakeets)
            {
                if (parakeet.BirdPhase == Phase.EGG)
                {
                    Console.WriteLine($"{parakeet.Id.ToString("D3")} - {parakeet.EggDate.ToShortDateString()}");
                }
            }

            Console.Write("Egg ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter the Born Date: ");
            parakeets[--id].SetBornDate(DateTime.Parse(Console.ReadLine()));

            Console.Write("Enter a nickname: ");
            parakeets[id].SetName(Console.ReadLine());
        }

        static void SetBirdSex(List<Parakeet> parakeets)
        {
            Header();

            Console.WriteLine(" ID | NICK\n");

            foreach (Parakeet parakeet in parakeets)
            {
                Console.WriteLine($"{parakeet.Id.ToString("D3")} - {parakeet.Name}");
            }

            Console.Write("\nEnter the bird ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter the sex (M/F): ");
            Sexes sex = (Sexes)char.Parse(Console.ReadLine());

            parakeets[--id].SetSex(sex);
        }

        static void SetBirdName(List<Parakeet> parakeets)
        {
            Header();

            Console.WriteLine(" ID | NICK\n");
            
            foreach (Parakeet parakeet in parakeets)
            {
                Console.WriteLine($"{parakeet.Id.ToString("D3")} - {parakeet.Name}");
            }

            Console.Write("\nSelect the bird by ID: ");
            int id = int.Parse(Console.ReadLine());
            
            Console.Write("Enter the name: ");
            parakeets[--id].SetName(Console.ReadLine());
        }

        static void PrintData(List<Parakeet> parakeets)
        {
            string ask;

            do
            {
                PrintBirds(parakeets);

                Console.Write("Enter the Bird ID: ");
                int id = int.Parse(Console.ReadLine());

                Console.WriteLine(parakeets[--id]);
                Console.ReadKey();

                Console.Write("\n\nOther(Y/N)? ");
                ask = Console.ReadLine().ToUpper();
            } while (ask == "Y");
        }

        static void PrintBirds(List<Parakeet> parakeets)
        {
            Header();

            Console.WriteLine(" - Birds List -\n");
            Console.WriteLine(" ID | NAME | AGE");

            foreach (Parakeet parakeet in parakeets)
            {
                parakeet.UpdateData();
                Console.WriteLine($"{parakeet.Id.ToString("D3")} - {parakeet.Name} - {parakeet.Age.Days}");
            }
        }

        static void Main(string[] args)
        {
            List<Parakeet> parakeets = new List<Parakeet>();
            char ask;

            parakeets.Add(new Parakeet(parakeets.Count + 1, Phase.YOUNG, DateTime.Parse("01/10/2020"), "Tom", Sexes.MALE));
            parakeets.Add(new Parakeet(parakeets.Count + 1, Phase.YOUNG, DateTime.Parse("01/12/2020"), "Emma", Sexes.FEMALE));
            parakeets[0].EggDate = DateTime.Parse("10/09/2020");
            parakeets[1].EggDate = DateTime.Parse("10/11/2020");
            parakeets[0].Mother = null;
            parakeets[0].Father = null;
            parakeets[1].Mother = null;
            parakeets[1].Father = null;

            do 
            {
                Header();

                ask = Menu();

                switch (ask)
                {
                    case '1': parakeets.Add(NewEgg(parakeets)); break;

                    case '2': parakeets.Add(NewBird(parakeets)); break;

                    case '3': SetBirdParents(parakeets); break;

                    case '4': SetBirdBirthday(parakeets); break;

                    case '5': SetBirdSex(parakeets); break;

                    case '6': SetBirdName(parakeets); break;

                    case '7': 
                        PrintBirds(parakeets); 
                        Console.ReadKey();
                        break;

                    case '8': PrintData(parakeets); break;
                }
            } while (ask != '0');

            Header();

            Console.WriteLine("Bye.");
            Console.ReadKey();
        }
    }
}
