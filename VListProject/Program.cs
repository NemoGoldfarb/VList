using System;
using System.IO;
namespace VListServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
        }
    }
    static class Data {
        private int normallistsize;
        private int speciallistsize;
        
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
        public Pokemon (int tid, string tname)
        {
            id=tid;
            name=tname;
        }
        public Pokemon (int tid, string tname, bool twanted)
        {
            id=tid;
            name=tname;
            wanted=twanted;
        }
    }
}