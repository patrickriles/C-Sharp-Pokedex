using System;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http;
using PokeDex.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using PokeDex.ViewModel;

namespace PokeDex.Http
{
    public class APIcaller
    {
        public JObject PokemonData { get; set; }
        public Pokemon retrievedPokemon { get; set; }
        private PokemonViewModel pokeVM;
        string output;

        public APIcaller(PokemonViewModel vm)
        {
            this.pokeVM = vm;
        }

        public async Task<Pokemon> GetData(int i)
        {
            //retrievedPokemon = new Pokemon();
            Pokemon pokemon;

         

                // Note: the URI constructor will throw an exception
                // if the string passed is not a valid URI
                Uri baseURI = new Uri("https://pokeapi.co/api/v2/pokemon/");
                Uri uri = new Uri(baseURI.ToString() + i.ToString());
                var httpClient = new HttpClient();       
            
   
                try
                {
                    //Change this so it captures all the needed data and creates a pokemon object
                    var result = await httpClient.GetStringAsync(uri);
                    PokemonData = JObject.Parse(result);

                    string ID = "#" + i.ToString();
                    string Name = stringFormat(PokemonData.Value<string>("name"));
                    string Height = PokemonData.Value<string>("height");
                    string Weight = PokemonData.Value<string>("weight");

                    JArray AbilityArray = PokemonData.Value<JArray>("abilities");
                    List<string> Abilities = new List<string>();
                    
                    for (int j = 0; j < AbilityArray.Count; j++)
                    {
                        string currentAbility = AbilityArray.Value<JObject>(j).Value<JObject>("ability").Value<string>("name").ToString();
                        Abilities.Add(stringFormat(currentAbility));
                    }

                   
                    JArray MoveArray = PokemonData.Value<JArray>("moves");
                    List<string> Moves = new List<string>();

                    for (int j = 0; j < MoveArray.Count; j++)
                    {
                        if (MoveArray != null)
                        {
                            string currentMove = MoveArray.Value<JObject>(j).Value<JObject>("move").Value<string>("name");
                            Moves.Add(stringFormat(currentMove));
                        }
                    }
                   
                    JObject SpritesObject = PokemonData.Value<JObject>("sprites");
                    List<string> Sprites = new List<string>();

                    Sprites.Add(SpritesObject.Value<string>("front_default"));
                    Sprites.Add(SpritesObject.Value<string>("front_shiny"));                   

                    JArray TypeArray = PokemonData.Value<JArray>("types");
                    List<string> Types = new List<string>();

                    for (int j = 0; j < TypeArray.Count; j++)
                    {
                        if (TypeArray != null)
                        {
                            string currentType = TypeArray.Value<JObject>(j).Value<JObject>("type").Value<string>("name").ToString();
                            Types.Add(stringFormat(currentType));
                        }
                    }

                    Debug.WriteLine(ID);

                    pokemon = new Pokemon(ID, Name, Height, Weight, Abilities, Moves, Sprites, Types);

                return pokemon;
               
                }
                catch (Exception ex)
                {
                    // Details in ex.Message and ex.HResult.       
                }
                
                // Once your app is done using the HttpClient object call dispose to 
                // free up system resources (the underlying socket and memory used for the object)
                httpClient.Dispose();
                //return retrievedPokemon;

            
            // Debug.WriteLine(retrievedPokemon.Count);
            return retrievedPokemon;
        }

        private string stringFormat(string input)
        {

            //string output;
            int count = 0;
            output = "";

            foreach (char c in input)
            {
                if (count == 0)
                {
                    output += c.ToString().ToUpper();
                }

                else if (input[count - 1] == ' ' || input[count - 1] == '-')
                {
                    output += c.ToString().ToUpper();
                }

                else if (c == (char)'-')
                {
                    output += " ";
                }

                else
                {
                    output += c;
                }
                count++;
            }

            return output;
        }

      
    }
}
