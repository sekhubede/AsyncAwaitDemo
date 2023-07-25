using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace AsyncAwaitDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string[] frames = { "0_0", "-_-", "0_0", "-_-", "0_0" };
            Stopwatch watch = Stopwatch.StartNew();

            await TextAnimationUtils.FramesParallelAsync(frames);

            watch.Stop();

            var elapsedMS = watch.ElapsedMilliseconds;

            Console.WriteLine();
            Console.WriteLine(elapsedMS);
            Console.ReadKey();
        }
    }

    class TextAnimationUtils
    {
        public static async Task FramesParallelAsync(string[] frames)
        {
            List<Task> tasks = new List<Task>();

            Console.CursorVisible = false;
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Run(() => Animate(frames)));
            }

            await Task.WhenAll(tasks);

            Console.CursorVisible = true;
        }

        private static void Animate(string[] frames)
        {
            foreach (var frame in frames)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(frame);
                Thread.Sleep(100);
            }
        }
    }
}
