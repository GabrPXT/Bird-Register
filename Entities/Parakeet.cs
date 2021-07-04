using System;
using System.Text;
using Periquitos_Dados.Entities.Enums;

namespace Periquitos_Dados.Entities
{
    class Parakeet
    {
        public int Id {get; set; }
        public Phase BirdPhase {get; set; }
        public DateTime EggDate {get; set; }
        public DateTime Born {get; set; }
        public string Name {get; set; }
        public Parakeet? Mother {get; set; }
        public Parakeet? Father {get; set; }
        public Sexes Sex {get; set; }
        public DateTime DathDate {get; set; }
        public TimeSpan Age {get; set; }

        public Parakeet()
        {

        }

        public Parakeet(int id, DateTime eggDate, Parakeet mother, Parakeet father)
        {
            Id = id;
            BirdPhase = Phase.EGG;
            EggDate = eggDate;
            Mother = mother;
            Father = father;
            Sex = Sexes.UNDENTFIED;
        }

        public Parakeet(int id ,Phase birdPhase, DateTime born, string name, Sexes sex)
        {
            BirdPhase = birdPhase;
            Born = born;
            Name = name;
            Sex = sex;
            Id = id;
        }

        public void SetID(int number)
        {
            Id = number;
        }

        public void SetParents(Parakeet mother, Parakeet father)
        {
            Mother = mother;
            Father = father;
        }

        public void SetSex(Sexes sex)
        {
            Sex = sex;
        }

        public void SetBornDate(DateTime born)
        {
            BirdPhase = Phase.NEWBORN;
            Born = born;

        }

        public void SetName(string name)
        {
            Name = name;
        }

        //ATUALIZA DADOS
        public void UpdateData()
        {
            Age = DateTime.Now - Born;

            switch (Age.Days)
            {
                case (>= 365): BirdPhase = Phase.ADULT; break;
                case (>= 122): BirdPhase = Phase.YOUNG; break;
                case (>= 30): BirdPhase = Phase.PUPP; break;
                case (<= 30): BirdPhase = Phase.NEWBORN; break;
            }    
        }

        public override string ToString()
        {
            if (Mother == null)
            {
                return $" ID: {Id.ToString("D3")} | Name: {Name.ToUpper()} | Age: {Age.Days} days\n"
                    + $"Born: {Born.ToShortDateString()} | Phase: {BirdPhase}\n"
                    + $"Filiation:\n"
                    + $" NULL\n"
                    + $" Sex: {Sex} | Egg Date: {EggDate.ToShortDateString()}";
            }
            else 
            {
                return $" ID: {Id.ToString("D3")} | Name: {Name.ToUpper()} | Age: {Age.Days} days\n"
                    + $"Born: {Born.ToShortDateString()} | Phase: {BirdPhase}\n"
                    + $"Filiation:\n"
                    + $" {Mother.Name.ToUpper()} and {Father.Name.ToUpper()}\n"
                    + $"Sex: {Sex} | Egg Date: {EggDate.ToShortDateString()}";
            }
            
        }
    }
}