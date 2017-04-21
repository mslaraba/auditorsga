using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using static GA.Tools;

namespace GA
{
    public class Functions
    {
        public static void GenerateNames(string data_dir)
        {
            try
            {
                int count = 100;// int.Parse(Tools.ReadColor("Auditors Count : "));
                int comp_count = 17;// int.Parse(Tools.ReadColor("Competences Count : "));
                decimal salary_min = 2500;// decimal.Parse(Tools.ReadColor("Salary Min : "));
                decimal salary_max = 7000;// decimal.Parse(Tools.ReadColor("Salary Max : "));

                Directory.CreateDirectory(Path.Combine(data_dir, "out"));
                var names_path = Path.Combine(data_dir, "names.txt");
                var first_path = Path.Combine(data_dir, "firsts.txt");
                var comps_path = Path.Combine(data_dir, "competences.txt");
                var output_path = Path.Combine(data_dir,"out", "output.xml");
                var text_file = Path.Combine(data_dir, "out", "output.txt");


                var file_names = File.ReadAllLines(names_path).ToList();
                var file_firsts = File.ReadAllLines(first_path).ToList();
                var file_competences = File.ReadAllLines(comps_path).ToList();

                WriteLineColor("===============================================================", ConsoleColor.Gray);
                WriteLineColor("Names file : " + names_path, ConsoleColor.Gray);
                WriteLineColor("First names file : " + first_path, ConsoleColor.Gray);
                WriteLineColor("Competences file : " + comps_path, ConsoleColor.Gray);
                WriteLineColor("===============================================================", ConsoleColor.Gray);

                var rnd = new Random(DateTime.Now.Second * DateTime.Now.Minute * DateTime.Now.Hour);

                List<string> names = new List<string>();
                List<Auditor> auditors = new List<Auditor>();

                for (int i = 0; i < count; i++)
                {
                    var new_name = file_names[rnd.Next(file_names.Count - 1)] + " " + file_firsts[rnd.Next(file_firsts.Count - 1)];
                    while (names.Contains(new_name))
                    {
                        new_name = file_names[rnd.Next(file_names.Count - 1)] + " " + file_firsts[rnd.Next(file_firsts.Count - 1)];
                    }

                    names.Add(new_name);
                    var audi = new Auditor(new_name);
                    auditors.Add(audi);

                }

                ShuffleList(auditors);
                int m = 1;
                foreach (var item in auditors)
                {
                    item.GenerateCompetences(file_competences, m);
                    m++;
                }


                ShuffleList(auditors);
                m = 1;
                foreach (var item in auditors)
                {
                    item.GenerateSalary(m);
                    m++;
                }

                foreach (var item in auditors)
                    item.WriteMe();



                ReadColor("Press any key to save as a file !", ConsoleColor.Green);

                List<string> rr = new List<string>();
                foreach (var item in auditors)
                    rr.Add(item.Name + "\t" + item.Salary.ToString() + $"\t({item.Competences.Count}) =>" + string.Join("//", item.Competences) + "\t" + item.Benefice);

                File.WriteAllLines(text_file, rr);
                WriteLineColor("Out put file ===> " + text_file, ConsoleColor.Green);
                try
                {
                    File.WriteAllText(output_path, poco2Xml(auditors));
                    WriteLineColor("Out put file ===> " + output_path, ConsoleColor.Green);
                }
                catch (Exception ex)
                {
                    WriteLineColor(ex.Message, ConsoleColor.Red);
                }

            }
            catch (Exception ex)
            {
                Tools.WriteLineColor(ex.Message, ConsoleColor.Red);
            }


        }
        static string poco2Xml(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            StringBuilder result = new StringBuilder();
            using (var writer = XmlWriter.Create(result))
            {
                serializer.Serialize(writer, obj);
            }
            return result.ToString();
        }


        static void ShuffleList(IList<Auditor> list)
        {

            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = rnd.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        static Random rnd = new Random(DateTime.Now.Second * DateTime.Now.Minute);
    }
    public class Auditor
    {


        public Auditor()
        {
        }

        public Auditor(string new_name)
        {
            this.Name = new_name;

        }

        internal void GenerateCompetences(List<string> All, int Id)
        {

            Competences = new List<string>();
            var comp_count = 0;
            if (Id <= 7)
                comp_count = rnd.Next(3, 5 - 1);
            else if (Id <= 22)
                comp_count = rnd.Next(5, 7 - 1);
            else if (Id <= 53)
                comp_count = rnd.Next(7, 9 - 1);
            else if (Id <= 77)
                comp_count = rnd.Next(9, 11 - 1);
            else if (Id <= 90)
                comp_count = rnd.Next(11, 13 - 1);
            else if (Id <= 96)
                comp_count = rnd.Next(13, 15 - 1);
            else if (Id <= 99)
                comp_count = rnd.Next(15, 17 - 1);
            else if (Id <= 100)
                comp_count = 17;

            for (int i = 0; i < comp_count; i++)
            {
                var new_list = All.Except(Competences).ToList();
                Competences.Add(new_list[rnd.Next(new_list.Count - 1)]);

            }


        }

        internal void GenerateSalary(int Id)
        {


            if (Id <= 18)
                Salary = rnd.Next(2500, 3100 - 1);
            else if (Id <= 27)
                Salary = rnd.Next(3100, 3600 - 1);
            else if (Id <= 43)
                Salary = rnd.Next(3600, 4200 - 1);
            else if (Id <= 53)
                Salary = rnd.Next(4200, 4800 - 1);
            else if (Id <= 61)
                Salary = rnd.Next(4800, 5300 - 1);
            else if (Id <= 72)
                Salary = rnd.Next(5300, 5900 - 1);
            else if (Id <= 91)
                Salary = rnd.Next(5900, 6500 - 1);
            else if (Id <= 100)
                Salary = rnd.Next(6500, 7000 - 1);

            GenerateBenefits();

        }

        public static Random rnd = new Random(DateTime.Now.Second * DateTime.Now.Minute);
        public string Name { get; set; }
        public List<string> Competences { get; set; }
        public int Salary { get; set; }
        public int Benefit { get; set; }

        public int Benefice => 100 + CalcB() - CalcC();

        public int CalcB()
        {
            var b = Salary;
            if (b == 2500)
                return 1 / 45;
            return (b - 2500) / 45;
        }
        public int CalcC()
        {
            var c = Competences.Count;
            if (c == 3)
                return 50 / 7;
            return (c - 3) * 50 / 7;
        }

        void GenerateBenefits()
        {
            Benefit = Benefice;
        }

        internal void WriteMe()
        {
            WriteColor(Name, ConsoleColor.Green);
            WriteColor($"({Competences.Count})", ConsoleColor.DarkYellow);
            if (Name.Length > 15)
                WriteColor("\t");
            else if (Name.Length < 8)
                WriteColor("\t\t\t");
            else
                WriteColor("\t\t");
            WriteColor(" Salary : ");
            WriteColor(Salary.ToString(), ConsoleColor.Magenta);
            WriteColor("\t  ===> Benefit : " + Benefit.ToString(), ConsoleColor.Cyan);
            WriteLineColor("");
            foreach (var item in Competences)
            {
                WriteLineColor($"----[{item}]", ConsoleColor.DarkYellow);
            }
            WriteLineColor("");
        }
    }
}
