using System;
namespace VListServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
        }
    }
    class Pokemon {
        public readonly int id;
        public readonly string name;
        public bool wanted;
        public void flip ()
        {
            if (wanted)
            {
                wanted=false;
            }
            else
            {
                wanted=true;
            }
        }
    }
}