using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// 共享资源同步
    /// </summary>
    public class Program2
    {
        static int sharedResource = 0;
        static readonly object lockObject = new object();

        static void Increment()
        {
            for (int i = 0; i < 100000; i++)
            {
                lock (lockObject) // 使用锁来确保线程安全
                {
                    sharedResource++;
                }
            }
        }

        static void Main()
        {
            Thread[] threads = new Thread[5];

            // 创建多个线程
            for (int i = 0; i < 5; i++)
            {
                threads[i] = new Thread(Increment);
                threads[i].Start();
            }

            // 等待所有线程完成
            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine($"最终的共享资源值: {sharedResource}");
        }
    }
}
