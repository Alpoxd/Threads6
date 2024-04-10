using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Threads6
{
    internal class Program
    {
        static double res = 0;
        static int a;
        static object locker = new object();

        static void Main()
        {
            int[] Thd = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 20, 30, 40, 50, 100, 200, 300, 400, 500 };
            a = Convert.ToInt32(Console.ReadLine());
            
            foreach (int n in Thd)
            {
                var options = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = n
                };
                res = 0;
                Stopwatch sw = Stopwatch.StartNew();
                Parallel.For(0, 1000000000, options, () => 0.0, (i, state, localRes) =>
                {
                    if (i % 2 == 1)
                        return localRes + Math.Sqrt(i + a);
                    else
                        return localRes - Math.Sqrt(i + a);
                },
            localRes => {
                lock (locker)
                    res += localRes;
            });
                sw.Stop();
                Console.WriteLine($"{n} {sw.ElapsedMilliseconds}");
            }
            Console.Read();
        }
    }
}