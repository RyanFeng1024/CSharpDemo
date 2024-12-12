using System;
using System.Threading;

class Program
{
    /// <summary>
    /// ---------- 多线程 并发执行 ----------
    /// </summary>
    /// <param name="threadId"></param>
    static void Worker(object threadId)
    {
        Console.WriteLine($"线程 {threadId} 开始工作");
        Thread.Sleep(2000); // 模拟工作
        Console.WriteLine($"线程 {threadId} 完成工作");
    }

    static void Main()
    {
        Thread[] threads = new Thread[5];

        // 创建多个线程
        for (int i = 0; i < 5; i++)
        {
            threads[i] = new Thread(Worker);
            threads[i].Start(i);
        }

        // 等待所有线程完成
        foreach (var thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine("所有线程完成");
    }


    /// <summary>
    /// ---------- 【线程池】多线程。。 ----------
    /// </summary>
    static void MultiThread()
    {
        // 设置线程池的最小工作者线程和异步I/O线程数量
        ThreadPool.SetMinThreads(4, 4);

        // 往线程池中提交若干工作项
        for (int i = 0; i < 10; i++)
        {
            int jobNumber = i;
            ThreadPool.QueueUserWorkItem(DoWork, jobNumber);
        }

        // 主线程等待用户输入，以避免程序过快结束
        Console.WriteLine("主线程执行完毕，按任意键结束...");
        Console.ReadKey();
    }

    static void DoWork(object state)
    {
        int jobNumber = (int)state;
        Console.WriteLine($"工作项 {jobNumber} 开始执行，线程ID: {Thread.CurrentThread.ManagedThreadId}");
        Thread.Sleep(1000); // 模拟计算或IO延迟
        Console.WriteLine($"工作项 {jobNumber} 执行完成");
    }


    /// <summary>
    /// ---------- 并发执行 ----------
    /// </summary>
    static void Concurrent()
    {
        Task[] tasks = new Task[5];

        // 创建多个任务
        for (int i = 0; i < 5; i++)
        {
            int threadId = i; // 捕获变量
            tasks[i] = Task.Run(() =>
            {
                Console.WriteLine($"任务 {threadId} 开始工作");
                Thread.Sleep(2000); // 模拟工作
                Console.WriteLine($"任务 {threadId} 完成工作");
            });
        }

        // 等待所有任务完成
        Task.WaitAll(tasks);
        Console.WriteLine("所有任务完成");
    }

}
