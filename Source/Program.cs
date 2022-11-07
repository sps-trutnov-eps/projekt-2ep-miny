﻿using System.Security.AccessControl;

namespace Miny
{
    internal class Program
    {
        // vyctovy datovy typ
        enum TypPolicka
        {
            Zakryte,
            Odkryte,
            Vlajka,
        }

        enum StavHry
        {
            Bezi,
            Vyhra,
            Prohra,
        }

        static readonly int SirkaHerniPlochy = 9;
        static readonly int VyskaHerniPlochy = 9;

        static int[,] MinovePole;
        static TypPolicka[,] Maska;

        static int KurzorX = 0;
        static int KurzorY = 0;

        static void Main(string[] args)
        {
            MinovePole = new int[SirkaHerniPlochy, VyskaHerniPlochy];
            Maska = new TypPolicka[SirkaHerniPlochy, VyskaHerniPlochy];

            VygenerovatHerniPlochu();
           
            StavHry stavHry = StavHry.Bezi;
            DateTime casZacatku = DateTime.Now;

            while (stavHry == StavHry.Bezi)
            {
                SmazatObrazovku();
                ZobrazitNadpis();

                ZobrazitHerniPlochu();
                ZobrazitKurzor();

                stavHry = InterakceSUzivatelem();
            }

            TimeSpan dobaHry = DateTime.Now - casZacatku;

            ZobrazitTabulkuNejHracu();
            ZobrazitDobuProbehleHry(dobaHry);

            if (stavHry == StavHry.Vyhra)
            {
                ZapisDoTabulkyNejHracu(dobaHry);
            }

            ZobrazitRozlouceni();
        }

        static void VygenerovatHerniPlochu()
        {
            Random nahoda = new Random();
            int z = 0;
            while(z<10)
            {
                int Random_x = nahoda.Next(MinovePole.GetLength(0));
                int Random_y = nahoda.Next(MinovePole.GetLength(1));
                if(MinovePole[Random_x, Random_y] != -1)
                {
                    MinovePole[Random_x, Random_y] = -1;
                    z++;
                }
            }

            for (int a = 0; a < MinovePole.Length; a++)
            {

                int x = a % VyskaHerniPlochy;
                int y = a / SirkaHerniPlochy;
                if (MinovePole[(x % SirkaHerniPlochy), y] != -1)
                {
                    if (y != 0 && y % VyskaHerniPlochy != VyskaHerniPlochy - 1)
                    {
                        if (x % SirkaHerniPlochy != 0 && x % SirkaHerniPlochy != SirkaHerniPlochy - 1)
                        {
                            if (MinovePole[x + 1, y] == -1) MinovePole[x, y]++;
                            if (MinovePole[x - 1, y] == -1) MinovePole[x, y]++;
                            if (MinovePole[x, y - 1] == -1) MinovePole[x, y]++;
                            if (MinovePole[x + 1, y - 1] == -1) MinovePole[x, y]++;
                            if (MinovePole[x - 1, y - 1] == -1) MinovePole[x, y]++;
                            if (MinovePole[x, y + 1] == -1) MinovePole[x, y]++;
                            if (MinovePole[x + 1, y + 1] == -1) MinovePole[x, y]++;
                            if (MinovePole[x - 1, y + 1] == -1) MinovePole[x, y]++;
                        }
                        else if (x % SirkaHerniPlochy == 0)
                        {
                            if (MinovePole[x + 1, y] == -1) MinovePole[x, y]++;
                            if (MinovePole[x, y - 1] == -1) MinovePole[x, y]++;
                            if (MinovePole[x + 1, y - 1] == -1) MinovePole[x, y]++;
                            if (MinovePole[x, y + 1] == -1) MinovePole[x, y]++;
                            if (MinovePole[x + 1, y + 1] == -1) MinovePole[x, y]++;
                        }
                        else if (x % SirkaHerniPlochy == SirkaHerniPlochy - 1)
                        {
                            if (MinovePole[x - 1, y] == -1) MinovePole[x, y]++;
                            if (MinovePole[x, y - 1] == -1) MinovePole[x, y]++;
                            if (MinovePole[x - 1, y - 1] == -1) MinovePole[x, y]++;
                            if (MinovePole[x, y + 1] == -1) MinovePole[x, y]++;
                            if (MinovePole[x - 1, y + 1] == -1) MinovePole[x, y]++;
                        }
                    }
                    else if (y == 0)
                    {
                        if (x % SirkaHerniPlochy != 0 && x % SirkaHerniPlochy != SirkaHerniPlochy - 1)
                        {
                            if (MinovePole[x + 1, y] == -1) MinovePole[x, y]++;
                            if (MinovePole[x - 1, y] == -1) MinovePole[x, y]++;
                            if (MinovePole[x, y + 1] == -1) MinovePole[x, y]++;
                            if (MinovePole[x + 1, y + 1] == -1) MinovePole[x, y]++;
                            if (MinovePole[x - 1, y + 1] == -1) MinovePole[x, y]++;
                        }
                        else if (x % SirkaHerniPlochy == 0)
                        {
                            if (MinovePole[x + 1, y] == -1) MinovePole[x, y]++;
                            if (MinovePole[x, y + 1] == -1) MinovePole[x, y]++;
                            if (MinovePole[x + 1, y + 1] == -1) MinovePole[x, y]++;
                        }
                        else if (x % SirkaHerniPlochy == SirkaHerniPlochy - 1)
                        {
                            if (MinovePole[x - 1, y] == -1) MinovePole[x, y]++;
                            if (MinovePole[x, y + 1] == -1) MinovePole[x, y]++;
                            if (MinovePole[x - 1, y + 1] == -1) MinovePole[x, y]++;
                        }
                    }
                    else if (y % SirkaHerniPlochy == SirkaHerniPlochy - 1)
                    {
                        if (x % SirkaHerniPlochy != 0 && x % SirkaHerniPlochy != SirkaHerniPlochy - 1)
                        {
                            if (MinovePole[x + 1, y] == -1) MinovePole[x, y]++;
                            if (MinovePole[x - 1, y] == -1) MinovePole[x, y]++;
                            if (MinovePole[x, y - 1] == -1) MinovePole[x, y]++;
                            if (MinovePole[x + 1, y - 1] == -1) MinovePole[x, y]++;
                            if (MinovePole[x - 1, y - 1] == -1) MinovePole[x, y]++;
                        }
                        else if (x % SirkaHerniPlochy == 0)
                        {
                            if (MinovePole[x + 1, y] == -1) MinovePole[x, y]++;
                            if (MinovePole[x, y - 1] == -1) MinovePole[x, y]++;
                            if (MinovePole[x + 1, y - 1] == -1) MinovePole[x, y]++;
                        }
                        else if (x % SirkaHerniPlochy == SirkaHerniPlochy - 1)
                        {
                            if (MinovePole[x - 1, y] == -1) MinovePole[x, y]++;
                            if (MinovePole[x, y - 1] == -1) MinovePole[x, y]++;
                            if (MinovePole[x - 1, y - 1] == -1) MinovePole[x, y]++;
                        }
                    }
                }
            }
        }

        static void ZapisDoTabulkyNejHracu(TimeSpan dobaHry)
        {
            throw new NotImplementedException();
        }

        static void ZobrazitDobuProbehleHry(TimeSpan dobaHry)
        {
            throw new NotImplementedException();
        }

        static void ZobrazitTabulkuNejHracu()
        {
            throw new NotImplementedException();
        }

        static StavHry InterakceSUzivatelem()
        {
            throw new NotImplementedException();
        }

        static void ZobrazitKurzor()
        {
            throw new NotImplementedException();
        }

        static void ZobrazitHerniPlochu()
        {

            for (int pocitacka_radku = 0; pocitacka_radku < VyskaHerniPlochy; pocitacka_radku++)
            {
             
                Console.WriteLine("+-+-+-+-+-+-+-+-+-+");
                Console.WriteLine();
               
                for (int pocitacka_pozic = 0; pocitacka_pozic < SirkaHerniPlochy; pocitacka_pozic++)
                {
                    string znak = "nic";
                    if(MinovePole[pocitacka_radku, pocitacka_pozic] == -1)
                    {
                        znak = "M";
                    }
                    else
                    {
                        znak = Convert.ToString(MinovePole[pocitacka_radku, pocitacka_pozic]);
                    }
                    Console.Write("|"+ znak);
                }
                Console.Write("|");
                Console.WriteLine();
                
                
                
            
            }
        }

        static void SmazatObrazovku()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
        }

        static void ZobrazitNadpis()
        {
            Console.WriteLine(" ASCII Miny ");
            Console.WriteLine(" ---------- ");
            Console.WriteLine();
        }

        static void ZobrazitRozlouceni()
        {
            Console.WriteLine("Děkujeme za zahrání!");
            Console.WriteLine();
            Console.WriteLine("Stiskněte libovolnou klávesu pro ukončení...");
            Console.CursorVisible = false;
            Console.ReadKey();
        }
    }
}
