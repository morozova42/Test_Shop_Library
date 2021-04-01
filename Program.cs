using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write(">> ");

                Random random = new Random();
                Thread thread1 = new Thread(() => SendRequest(1, random.Next(1, 4)));
                thread1.Start();

                Thread thread2 = new Thread(() => SendRequest(2, random.Next(1, 4)));
                thread2.Start();

                Thread thread3 = new Thread(() => SendRequest(3, random.Next(1, 4)));
                thread3.Start();

                Thread thread4 = new Thread(() => SendRequest(4, random.Next(1, 4)));
                thread4.Start();

                Thread thread5 = new Thread(() => SendRequest(5, random.Next(1, 4)));
                thread5.Start();

                Thread thread6 = new Thread(() => SendRequest(6, random.Next(1, 4)));
                thread6.Start();

                Thread thread7 = new Thread(() => SendRequest(7, random.Next(1, 4)));
                thread7.Start();

                Thread thread8 = new Thread(() => SendRequest(8, random.Next(1, 4)));
                thread8.Start();

                Thread thread9 = new Thread(() => SendRequest(9, random.Next(1, 4)));
                thread9.Start();

                Thread thread10 = new Thread(() => SendRequest(10, random.Next(1, 4)));
                thread10.Start();

                if (Console.ReadLine().ToLower().Trim() == "exit")
                {
                    break;
                }
            }

            Console.WriteLine("Программа завершена");
            Console.ReadLine();
        }

        static void SendRequest(int user_id, int reserve_qty)
        {
            ShopLib.ShopAgent agent = new ShopLib.ShopAgent();
            for (int i = 0; i < 1000; i++)
            {
                agent.RegisterDelegate(new ShopLib.ShopAgent.ReserveStateHandler(Console.WriteLine));
                agent.Reserve(user_id, 0, reserve_qty);
            }
        }

    }
}
