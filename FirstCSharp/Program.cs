using System;

namespace FirstCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = int.MaxValue;
            unchecked 
            {
                int y = int.MaxValue + 1;
            System.Console.WriteLine(y);
            }
            System.Console.WriteLine(x);
            Console.WriteLine("Hello World!");
            System.Console.WriteLine(new Program().Freed(1));
        }

        public int Freed(int fre){
            return fre * 100;
        }
    }
}
