﻿namespace Miny
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
            MinovePole = new int[2, 3] {
                {0, 0, 0 },
                {0, 0, 0 },
            };
            Maska = new TypPolicka[2, 3]
            {
                {TypPolicka.Zakryte, TypPolicka.Zakryte,TypPolicka.Zakryte},
                {TypPolicka.Zakryte, TypPolicka.Zakryte,TypPolicka.Zakryte},
            };

            StavHry stavHry = StavHry.Bezi;

            while (true)
            {
                for(int y = 0; y < 2; y++)
                {
                    for(int x = 0; x < 3; x++)
                    {
                        Console.Write(MinovePole[y, x]);
                    }
                    Console.WriteLine();
                }
                for (int y = 0; y < 2; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        Console.Write(Maska[y, x]);
                    }
                    Console.WriteLine();
                }




                stavHry = InterakceSUzivatelem();

            }
        }

        static void VygenerovatHerniPlochu()
        {
            throw new NotImplementedException();
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
            Console.Write("Stiskni klávesu");
            ConsoleKeyInfo klavesa = Console.ReadKey();
            Console.WriteLine();
            if (klavesa.Key == ConsoleKey.RightArrow)
            {
                Console.WriteLine("Pravá klávesa");         
                if (KurzorX < MinovePole.GetLength(0) && KurzorX >= 0 )
                {
                    KurzorX += 1;
                }
                Console.WriteLine("x:" + KurzorX);

            }

            if (klavesa.Key == ConsoleKey.LeftArrow)
            {
                Console.WriteLine("Levá klávesa");
                if (KurzorX <= MinovePole.GetLength(0) && KurzorX >= 0)
                {
                    KurzorX -= 1;
                }
                Console.WriteLine("x:" + KurzorX);
            }
            
            if (klavesa.Key == ConsoleKey.DownArrow)
            {
                Console.WriteLine("Dolní klávesa");
                if (KurzorY < MinovePole.GetLength(0) && KurzorY >= 0)
                {
                    KurzorY += 1;
                }
                Console.WriteLine("y:" + KurzorY);
            }

            if (klavesa.Key == ConsoleKey.UpArrow)
            {
                Console.WriteLine("Horní klávesa");
                if (KurzorY <= MinovePole.GetLength(0) && KurzorY >= 0)
                {
                    KurzorY -= 1;
                }
                Console.WriteLine("y:" + KurzorY);
            }
            

            return StavHry.Bezi;
        }

        static void ZobrazitKurzor()
        {
            throw new NotImplementedException();
        }

        static void ZobrazitHerniPlochu()
        {
            throw new NotImplementedException();
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
