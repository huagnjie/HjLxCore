using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HJ001.Busines
{    public class StudyTask
    {
        public StudyTask()
        {
            
        }

        private static object obj = new object();

        public void Send(){
            Console.WriteLine("111 My Thread Id is ："+Thread.CurrentThread.ManagedThreadId);
            MethodAsync();
            Console.WriteLine("222 My Thread Id is : "+Thread.CurrentThread.ManagedThreadId);;
        }

        private async Task MethodAsync(){
            var ResultFromTimeConsumingMethod = TimeConsumingMethod();
            string Result = await ResultFromTimeConsumingMethod + 
            " + AsyncMethod. My Thread Id is : "+
            Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine(Result);
        }

        private Task<string> TimeConsumingMethod(){
            var task = Task.Run(()=>{
                Console.WriteLine(" TimeConsumingMethod Thread ID is : "+ Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(5000);
                Console.WriteLine(" TimeConsmingMethod after Sleep(5000). Thread ID is : "+
                Thread.CurrentThread.ManagedThreadId);     
                return "TimeConsumingMethod";          
            });
            return task;           
        }

        public void LockSomething()
        {
            lock (obj)
            {
                dosomething();
            }
        }

        public void MonitorSomeThing()
        {
            try
            {
                Monitor.TryEnter(obj, 6000);
                dosomething();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Monitor.Exit(obj);
            }
        }

        public void dosomething()
        {
            //做具体的事情
        }

        public void DisplayCounts() {
            for (int i = 0; i < 10; i++)
            {
                System.Diagnostics.Debug.WriteLine(GetPrimesCount(i * 1000000 + 2, 1000000) + "betwwen"
                    + (i * 1000000) + "and" + ((i + 1) * 1000000 - 1));
            }
        }

        int GetPrimesCount(int start,int count)
        {
            return ParallelEnumerable.Range(start, count).Count(n =>
             Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0));
        }

    }
}