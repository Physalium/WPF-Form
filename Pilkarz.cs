using System;

namespace Lab1
{


    internal class Pilkarz
    {
        #region prop
        private string forename;
        private string surname;
        private double age;
        private double weight;

        public string Forename { get => forename; set => forename = value; }
        public string Surname { get => surname; set => surname = value; }
        public double Age { get => age; set => age = value; }
        public double Weight { get => weight; set => weight = value; }
        #endregion

        #region constructor
        public Pilkarz(string forename, string surname, double age, double weight)
        {
            this.Forename = forename;
            this.Age = age;
            this.Surname = surname;
            this.Weight = weight;
        }
        #endregion


        public override string ToString()
        {
            return $"{Forename} {Surname}, Wiek: {Age} lat, {Weight}kg";
        }

        public bool isTheSame(Pilkarz pilkarz)
        {
            if (pilkarz.Surname != Surname) return false;
            if (pilkarz.Forename != Forename) return false;
            if (pilkarz.Age != Age) return false;
            if (pilkarz.Weight != Weight) return false;
            return true;
        }

        public string ToFileFormat()
        {
            return $"{Surname}|{Forename}|{Age}|{Weight}";
        }

        public static Pilkarz CreateFromString(string sPilkarz)
        {
            string imie, nazwisko;
            uint wiek, waga;
            var pola = sPilkarz.Split('|');
            if (pola.Length == 4)
            {
                nazwisko = pola[1];
                imie = pola[0];
                wiek = uint.Parse(pola[2]);
                waga = uint.Parse(pola[3]);
                return new Pilkarz(imie, nazwisko, wiek, waga);
            }
            throw new Exception("Błędny format danych z pliku");
        }

        public void Copy(Pilkarz pilkarz)
        {
            Forename = pilkarz.Forename;
            Surname = pilkarz.Surname;
            Age = pilkarz.Age;
            Weight = pilkarz.Weight;
        }
    }

}