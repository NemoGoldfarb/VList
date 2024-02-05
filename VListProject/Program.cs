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
            Data.LoadData();
            NormalList test = (NormalList)RixitConvert.StringtoList("u00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000","normal");
            Console.WriteLine (test.Contents[0].wanted);
            Console.WriteLine (test.Contents[1].wanted);
            Console.WriteLine (test.Contents[2].wanted);
            Console.WriteLine (test.Contents[3].wanted);
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
            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
            };
            string tmp = File.ReadAllText("normal_pokemons.json");
            normal_list = JsonSerializer.Deserialize<Pokemon[]>(tmp,options);
            tmp = File.ReadAllText("special_pokemons.json");
            special_list = JsonSerializer.Deserialize<Pokemon[]>(tmp,options);
            tmp = File.ReadAllText("all_pokemons.json");
            all_list = JsonSerializer.Deserialize<Pokemon[]>(tmp,options);
            lastid = all_list[all_list.Length-1].id;
        }
    }
    static class RixitConvert {
        public static string ListtoString (IList list) {
            string outs = "";
            string rixits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/";
            int expsize = Data.Lastid+6-(Data.Lastid%6);
            BitArray explist = new BitArray(expsize);
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
                outs=outs.Insert(outs.Length, toAdd);
            }
            return outs;
        }
        public static IList StringtoList (string inputstring, string type) {
            string rixits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/";
            int expsize = Data.Lastid+6-(Data.Lastid%6);
            BitArray explist = new BitArray(expsize);
            explist.SetAll (false);
            for (int i=0; i<inputstring.Length; i++)
            {
                int val = rixits.IndexOf(inputstring[i]);
                for (int j=5; j>=0; j--)
                {
                    if (val%2==1) {explist[i+j]=true;}
                    val/=2;
                }
            }
            if (type=="normal")
            {
            NormalList outlist = new NormalList ();
            foreach (Pokemon p in outlist.Contents) {
                if (explist[p.id-1]) {p.flip();}
            }
            return outlist;
            }
            else if (type=="special")
            {
            SpecialList outlist = new SpecialList ();
            foreach (Pokemon p in outlist.Contents) {
                if (explist[p.id-1]) {p.flip();}
            }
            return outlist;
            }
            else
            {
            AllList outlist = new AllList ();
            foreach (Pokemon p in outlist.Contents) {
                if (explist[p.id-1]) {p.flip();}
            }
            return outlist;
            }
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
        public Pokemon (int ID, string NAME)
        {
            id=ID;
            name=NAME;
            wanted=false;
        }
      /*  public Pokemon (int tid, string tname, bool twanted)
        {
            id=tid;
            name=tname;
            wanted=twanted;
        }*/
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
}