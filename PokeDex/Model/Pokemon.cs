using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeDex.Model
{
    public class Pokemon
    {
        public string ID { get; }
        public string Name { get; }
        public string Height { get; }
        public string Weight { get; }
        public List<string> Abilities { get; }
        public List<string> Moves { get; }
        // If uri doesnt work make this string
        public List<string> Sprites { get; }
        public List<string> Types { get; }

        public Pokemon(string id, string name, string height, string weight, List<string> abilities, List<string>moves
            , List<string> sprites, List<string>types)
        {
            this.ID = id;
            this.Name = name;
            this.Height = height;
            this.Weight = weight;
            this.Abilities = abilities;
            this.Moves = moves;
            this.Sprites = sprites;
            this.Types = types;
        }
        //Just used as a place holder for  searching
        public string PokemonAsString => string.Join(", ", Name);
    }
}
