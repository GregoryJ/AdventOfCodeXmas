using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            double totalElapsedMs = 0D;
            int numberReps = 1000;

            Stopwatch stopWatch = new Stopwatch();
            for (int i = 0; i < numberReps; i++)
            {
                stopWatch.Start();
                Method01();
                //stopWatch.Stop();
                totalElapsedMs += stopWatch.ElapsedMilliseconds;
                stopWatch.Reset();
            }

            Console.WriteLine("Average elapsed MS: {0}", totalElapsedMs / numberReps);
        }

        static void Method01()
        {
            string inputFile = Path.Combine(Assembly.GetExecutingAssembly().Location, "..\\..\\..\\Input.txt");
            string[] inputLines = File.ReadAllLines(inputFile);
            Box[] giftBoxes = new Box[inputLines.Length];
            for (int i = 0; i < inputLines.Length; i++)
            {
                string[] lineArray = inputLines[i].Split('x');
                int[] lineIntArray = lineArray.Select(int.Parse).ToArray();
                giftBoxes[i] = new Box()
                {
                    Length = lineIntArray[0],
                    Width = lineIntArray[1],
                    Height = lineIntArray[2]
                };
                Array.Sort(lineIntArray);
                giftBoxes[i].SmallestSide = lineIntArray[0] * lineIntArray[1];
            }

            int totalSquareFeet = 0;

            foreach (Box giftBox in giftBoxes)
            {
                // debug
                //Console.WriteLine("Length: {0}, Width: {1}, Height: {2}, SmallestSide: {3}",
                //    giftBox.Length, giftBox.Width, giftBox.Height, giftBox.SmallestSide);

                totalSquareFeet += giftBox.Total;
            }

            Console.WriteLine("Total square feet: {0}", totalSquareFeet);
        }

        private static void Method02()
        {
            //Stopwatch stopWatch = Stopwatch.StartNew();

            string inputFile = Path.Combine(Assembly.GetExecutingAssembly().Location, "..\\..\\..\\Input.txt");
            var result = File.ReadAllLines(inputFile)
                .Select(s => s.Split('x'))
                .Select(x => x.Select(Int32.Parse))
                .Select(w => w.OrderBy(x => x).ToArray())
                .Select(w => 3*w[0]*w[1] + 2*w[0]*w[2] + 2*w[1]*w[2])
                .Sum();

            Console.WriteLine("Total square feet: {0}", result);

            //stopWatch.Stop();
            //Console.WriteLine("Elapsed milliseconds: {0}", stopWatch.ElapsedMilliseconds);
        }
    }

    struct Box
    {
        public int Length;
        public int Width;
        public int Height;
        public int SmallestSide;

        public int Total => (2 * Length * Width) + (2 * Width * Height) + (2 * Length * Height) + SmallestSide;
    }
}
