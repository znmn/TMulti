using System;
using System.Diagnostics;
using StaticSys;

namespace StaticSys
{
    internal static class Sys
    {
        public static string Ask(this string question)
        {
            Console.Write(question);
            return Console.ReadLine();
        }

        public static void Print(this string text)
        {
            Console.WriteLine(text);
        }
        public static int To(this int min, int max)
        {
            return new Random().Next(min, max);
        }
    }
}

namespace TeleMulti
{
    internal class TMulti
    {
        protected string aFolder, fFolder, pFolder;
        string tFolder = "./";
        string choose = "0";
        int num;

        private string[] list = new string[99];

        public TMulti(string aFolder, string fFolder, string pFolder)
        {
            this.aFolder = aFolder;
            this.fFolder = fFolder;
            this.pFolder = pFolder;
        }

        public void Start()
        {
            Console.Title = "Telegram Multi Instance";
            "========== Telegram Multi ==========\n[I] Created by : Zainul Muhaimin\n[I] Github : https://github.com/znmn\n[I] Instagram : https://www.instagram.com/xznmnx/\n========== Telegram Multi ==========".Print();
            this.Inialize();
        start:
            "\n- ACTION -\n[1] Run Folder\n[2] Create Folder\n[3] Delete Folder\n[4] Exit".Print();
            choose = "[?] Select Action : ".Ask();
            switch (choose)
            {
                case "1":
                    this.ListF();
                    if (this.Run()) { Console.ReadKey(); Environment.Exit(0); }
                    else { Console.ReadKey(); Console.Clear(); goto case "1"; }
                    break;
                case "2":
                    this.Create();
                    Console.ReadKey();
                    "[I] Back to Start...".Print();
                    Console.Clear();
                    goto start;
                case "3":
                    this.ListF();
                    this.Delete();
                    Console.ReadKey();
                    "[I] Back to Start...".Print();
                    Console.Clear();
                    goto start;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    "[!] Select Properly!".Print();
                    Console.ReadKey();
                    Console.Clear();
                    goto start;
            }
        }

        protected void Inialize()
        {
            if (!File.Exists(this.fFolder))
            {
            back:
                this.tFolder = Sys.Ask("[?] Select default Folder : ");
                this.tFolder = this.tFolder.TrimEnd(new char[] { '\\', '/' });
                if (File.Exists(this.tFolder + "/" + "Telegram.exe"))
                {
                    File.WriteAllText(this.fFolder, this.tFolder);
                    Directory.CreateDirectory(this.pFolder);
                }
                else
                {
                    "[!] Invalid Telegram Default Path!".Print();
                    goto back;
                }
            }
            else
            {
                this.tFolder = File.ReadAllText(this.fFolder);
            }

            if (!File.Exists(this.aFolder))
            {
                string[] fold = Directory.GetDirectories(this.pFolder);
                string isi = "Default," + Environment.NewLine;
                num = 0;
                foreach (string f in fold)
                {
                    isi += char.ToUpper(Path.GetFileName(f).First()) + Path.GetFileName(f)[1..].ToLower() + ",/" + Path.GetFileName(f) + Environment.NewLine;
                    num++;
                }
                File.WriteAllText(this.aFolder, isi);
            }

        }

        protected bool Run()
        {
            num = 0;
            foreach (string a in list)
            {
                num++;
                string[] data = a.Trim().Split(',', (char)StringSplitOptions.RemoveEmptyEntries);
                ("[" + num + "] " + data[0]).Print();
            }
            choose = "[?] Select : ".Ask();
            num = 0;
            foreach (string a in list)
            {
                num++;
                string[] data = a.Trim().Split(',', (char)StringSplitOptions.RemoveEmptyEntries);
                var isNum = int.TryParse(choose, out int n);
                if (num == n)
                {
                    ("[I] Running " + data[0] + "...").Print();
                    if (n == 1)
                    {
                        Process.Start(this.tFolder + "/telegram.exe", "-many");
                    }
                    else
                    {
                        Process.Start(this.tFolder + "/telegram.exe", "-many -workdir \"" + this.pFolder + data[1] + "\"");
                    }
                    "[I] Success...".Print();
                    return true;

                }
            }
            "[!] Select Properly!".Print();
            return false;
        }

        protected bool Create()
        {
            string? name = "\n[?] Name Account : ".Ask();
            try
            {
                Directory.CreateDirectory(this.pFolder + "/" + name);
                File.AppendAllText(this.aFolder, char.ToUpper(name.First()) + name[1..].ToLower() + ",/" + name + Environment.NewLine);
                ("[I] Success Creating Folder " + name + "...").Print();
                return true;
            }
            catch (Exception e)
            {
                ("[!] Error Creating Folder " + name + "...").Print();
                return false;
            }
        }

        protected bool Delete()
        {
            num = 0;
            foreach (string a in list)
            {
                num++;
                string[] data = a.Split(',', (char)StringSplitOptions.RemoveEmptyEntries);
                ("[" + num + "] " + data[0]).Print();
            }
            choose = "[?] Select : ".Ask();
            if (choose == "1")
            {
                ("[!] Can't Delete Default Folder!").Print();
                return false;
            }
            num = 0;
            foreach (string a in list)
            {
                num++;
                string[] data = a.Trim().Split(',', (char)StringSplitOptions.RemoveEmptyEntries);
                var isNum = int.TryParse(choose, out int n);
                if (num == n)
                {
                    Directory.Delete(pFolder + data[1], true);
                    File.WriteAllText(this.aFolder, File.ReadAllText(this.aFolder).Replace(a + Environment.NewLine, ""));
                    ("[I] Success Deleting " + data[0] + "...").Print();
                    return true;
                }
            }
            "[!] Select Properly!".Print();
            return false;
        }

        public string[] ListF()
        {
            this.list = File.ReadAllLines(this.aFolder);
            "\n- Folder Selection -".Print();
            return this.list;
        }
    }
}
