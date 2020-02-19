using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ConsoleApp1
{
    interface IReadFiles
    {
        void ReadingFile(string fileName);
    }
    interface IWriteToFiles
    {
        void WriteToFiles(string fileName);
    }
    abstract class Sladosti
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public double Weith { get; set; }
        public double Colorios { get; set; }
        public double PriceFor1Kg { get; set; }
        public Sladosti(string type, string name, string weith, string colorios, string priceFor1Kg)
        {
            Type = type;
            Name = name;
            Weith = Convert.ToDouble(weith);
            Colorios = Convert.ToDouble(colorios); 
            PriceFor1Kg = Convert.ToDouble(priceFor1Kg);
        }
        public override string ToString()
        {
            return String.Format("{4} {0},Weith: {1},Colorios: {2},PriceFor1Kg: {3}",
                Name, Weith, Colorios, PriceFor1Kg, Type);
        }
        // abstract public int ReturnsIncome(short hoursOrlength);

    }
    class Frut : Sladosti
    {
        public string VitaminA { get; set; }
        public string VitaminC { get; set; }
        public Frut(string type, string name, string weith, string colorios, string priceFor1Kg, string vitaminA, string vitaminC)
            : base(type, name, weith, colorios, priceFor1Kg)
        {
            VitaminA = vitaminA;
            VitaminC = vitaminC;
        }
        public override string ToString()
        {
            return base.ToString() + String.Format("VitaminA: {0},VitaminC: {1}", VitaminA, VitaminC);
        }
    }
    class Konfeta : Sladosti
    {
        public double PercentOfCacao { get; set; }
        public string Nachinca { get; set; }
        public Konfeta(string type, string name, string weith, string colorios, string priceFor1Kg, string percentOfCacao, string nachinca)
            : base(type, name, weith, colorios, priceFor1Kg)
        {
            PercentOfCacao = Convert.ToDouble(percentOfCacao);
            Nachinca = nachinca;
        }
        public override string ToString()
        {
            return base.ToString() + String.Format("PercentOfCacao: {0},Nachinca: {1}", PercentOfCacao, Nachinca);
        }
    }
    class Vafel : Sladosti
    {
        public string Vkys { get; set; }
        public bool Glazur { get; set; }
        public Vafel(string type, string name, string weith, string colorios, string priceFor1Kg, string vkys, string glazur)
            : base(type, name, weith, colorios, priceFor1Kg)
        {
            Vkys = vkys;
            Glazur = Convert.ToBoolean(glazur);
        }
        public override string ToString()
        {
            return base.ToString() + String.Format("Vkys: {0},Glazur: {1}", Vkys, Glazur);
        }
    }
    class Sladostii : IReadFiles, IWriteToFiles
    {
        List<Sladosti> podarokObj = new List<Sladosti>();
        static int income;

        internal List<Sladosti> PodarokObj { get => podarokObj; set => podarokObj = value; }
        public int Income { get => income; set => income = value; }

        public void ReadingFile(string fileName)
        {
            string str; short countOfFrutInFile = 0;
            using (StreamReader reader = new StreamReader(@fileName, Encoding.Default))
            {
                while ((str = reader.ReadLine()) != null)
                {
                    string[] words = str.Split(' ');
                    if (words[0] == "Frut")
                    {
                        countOfFrutInFile++;
                        PodarokObj.Add(new Frut(words[0], words[1], words[2], words[3], words[4], words[5], words[6]));
                    }
                    else if (words[0] == "Vafel")
                        PodarokObj.Add(new Vafel(words[0], words[1], words[2], words[3], words[4], words[5], words[6]));
                    else if (words[0] == "Konfeta")
                        PodarokObj.Add(new Konfeta(words[0], words[1], words[2], words[3], words[4], words[5], words[6]));
                }
            }
            if (countOfFrutInFile == 0)
            {
                PodarokObj.Add(new Frut("Frut", "Orange", "2100", "150", "15", "have", "no"));
                PodarokObj.Add(new Frut("Frut", "Ananas", "1000", "220", "20,3", "no", "have"));
            }
        }
        public override string ToString()
        {
            string str = String.Empty;
            foreach (var element in PodarokObj)
            {
                str += element.ToString() + "\r\n";
            }
            return str;
        }
        public double WeithOfPresent()
        {
            double weithOfPodarok = 0;
            for (int i = 0; i < PodarokObj.Count; i++)
            {
                weithOfPodarok = weithOfPodarok + PodarokObj[i].Weith;
            }
            return weithOfPodarok;
        }
        public double PriceOfPodarok()
        {
            double price = 0;
            for (int i = 0; i < PodarokObj.Count; i++)
            {
                Console.WriteLine(PodarokObj[i].PriceFor1Kg);
                Console.WriteLine(PodarokObj[i].Weith);
                price = price + PodarokObj[i].PriceFor1Kg * (PodarokObj[i].Weith / 1000.0);
            }
            return price;
        }
        public void RemoveMaxWeithKonfetaFromPodarok()
        {
            PodarokObj.Sort((b, a) => a.Weith.CompareTo(b.Weith));
            for (int i = 0; i < PodarokObj.Count; i++)
            {
                if (PodarokObj[i].Type == "Konfeta")
                {
                    PodarokObj.RemoveAt(i); break;
                }
            }
        }
        public void SortByColorios()
        {
            PodarokObj.Sort((b, a) => a.Colorios.CompareTo(b.Colorios));
        }
        public void WriteToFiles(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(@fileName))
            {
                writer.Write(ToString());
                writer.WriteLine("Sostav" + income);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Default;
            Sladostii sladostii = new Sladostii();
            sladostii.ReadingFile("E:\\111o.txt");
            Console.Write(sladostii);
            Console.WriteLine(sladostii.WeithOfPresent());
            if (sladostii.WeithOfPresent() > 1000)
                sladostii.RemoveMaxWeithKonfetaFromPodarok();
            Console.WriteLine(sladostii);
            sladostii.SortByColorios();
            Console.WriteLine(sladostii);
            Console.WriteLine(sladostii.PriceOfPodarok());
            sladostii.WriteToFiles("E:\\111i.txt");
        }
    }
}
