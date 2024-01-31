using System;
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
    static class Data {
 /*       private int normallistsize;
        public int Normallistsize {
            get { return normallistsize; }
        }
        private int speciallistsize;
        public int Speciallistsize {
            get { return speciallistsize; }
        }*/
        private Pokemon[] normal_list;
        public Pokemon[] Normal_list {
           get { return normal_list; }
        }
        private Pokemon[] special_list;
        public Pokemon[] Special_list {
           get { return special_list; }
        }
        private Pokemon[] all_list;
        public Pokemon[] All_list {
           get { return all_list; }
        }
        public void LoadData () {
            string tmp = File.ReadAllText("normal_pokemons.json");
            normal_list = JsonSerializer.Deserialize<Pokemon[]>(tmp);
            tmp = File.ReadAllText("special_pokemons.json");
            special_list = JsonSerializer.Deserialize<Pokemon[]>(tmp);
            tmp = File.ReadAllText("all_pokemons.json");
            all_list = JsonSerializer.Deserialize<Pokemon[]>(tmp);
        }
    }
    public interface IList {
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
}