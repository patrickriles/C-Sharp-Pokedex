using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeDex.Model
{
    public class Pokemon
    {
        string Name { get; }
        string Height { get; }
        string Weight { get; }
        List<string> Abilities { get; }
        List<string> Moves { get; }
        // If uri doesnt work make this string
        List<string> Sprites { get; }
        List<string> Types { get; }

        public Pokemon(string name, string height, string weight, List<string> abilities, List<string>moves
            , List<string> sprites, List<string>types)
        {
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
