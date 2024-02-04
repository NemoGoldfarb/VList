using System;
using System.Collections;
using System.IO;
using System.Text.Json;
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
 /*       private int normallistsize;
        public int Normallistsize {
            get { return normallistsize; }
        }
        private int speciallistsize;
        public int Speciallistsize {
            get { return speciallistsize; }
        }*/
        private static Pokemon[] normal_list;
        public static Pokemon[] Normal_list {
           get { return normal_list; }
        }
        private static Pokemon[] special_list;
        public static Pokemon[] Special_list {
           get { return special_list; }
        }
        private static Pokemon[] all_list;
        public static Pokemon[] All_list {
           get { return all_list; }
        }
        private static int lastid;
        public static int Lastid {
            get { return lastid; }
        }
        public static void LoadData () {
            string tmp = File.ReadAllText("normal_pokemons.json");
            normal_list = JsonSerializer.Deserialize<Pokemon[]>(tmp);
            tmp = File.ReadAllText("special_pokemons.json");
            special_list = JsonSerializer.Deserialize<Pokemon[]>(tmp);
            tmp = File.ReadAllText("all_pokemons.json");
            all_list = JsonSerializer.Deserialize<Pokemon[]>(tmp);
            lastid = all_list[all_list.Length-1].id;
        }
    }
    static class RixitConvert {
        public static string RCtoString (IList list) {
            string outs = "";
            string rixits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/";
            BitArray explist = new BitArray(Data.Lastid);
            explist.SetAll (false);
            for (int i=0; i<list.Contents.Length; i++)
            {
                explist[list.Contents[i].id]=list.Contents[i].wanted;
            }

            for (int i=0; i<explist.Length; i+=6)
            {
                int val = 0;
                for (int j=0; j<6; j++)
                {
                if(explist[i+j]) {val++;}
                val*=2;
                }
                string toAdd = rixits.Substring(val, 1);
                outs.Insert(outs.Length, toAdd);
            }
            return outs;
        }
    }
    interface IList {
        public string Name { get; set; }
        public Pokemon[] Contents { get; set; }
    }
    class NormalList : IList {
        public string Name { get; set; }
        public Pokemon[] Contents { get; set; }
        public NormalList (string n) {
            Name = n;
            Contents = Data.Normal_list;
        }
        public NormalList () {
            Name = "New list!";
            Contents = Data.Normal_list;
        }
    }
    class SpecialList : IList {
        public string Name { get; set; }
        public Pokemon[] Contents { get; set; }
        public SpecialList (string n) {
            Name = n;
            Contents = Data.Special_list;
        }
        public SpecialList () {
            Name = "New list!";
            Contents = Data.Special_list;
        }
    }
    class AllList : IList {
        public string Name { get; set; }
        public Pokemon[] Contents { get; set; }
        public AllList (string n) {
            Name = n;
            Contents = Data.All_list;
        }
        public AllList () {
            Name = "New list!";
            Contents = Data.All_list;
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