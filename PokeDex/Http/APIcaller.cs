using System;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http;
using PokeDex.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokeDex.Http
{
    class APIcaller
    {
        public JObject PokemonData { get; set; }
        public List<Pokemon> retrievedPokemon { get; set; }

        public async Task<List<Pokemon>> GetData()
        {
            retrievedPokemon = new List<Pokemon>();
            Pokemon pokemon;

            for (int i = 1; i <= 150; i++)
            {

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
                    string Name = PokemonData.Value<string>("name");
                    string Height = PokemonData.Value<string>("height");
                    string Weight = PokemonData.Value<string>("weight");

                    string AbilityOne = PokemonData.Value<JArray>("abilities").Value<JObject>(0).Value<JObject>("ability").Value<string>("name").ToString();
                    string AbilityTwo = PokemonData.Value<JArray>("abilities").Value<JObject>(1).Value<JObject>("ability").Value<string>("name").ToString();

                    List<string> Abilities = new List<string>();
                    Abilities.Add(AbilityOne);
                    if (AbilityTwo != null)
                    {
                        Abilities.Add(AbilityTwo);
                    }

                    List<string> Moves = new List<string>();
                    var MoveArray = PokemonData.Value<JArray>("moves");

                    for (int j = 0; j < MoveArray.Count; j++)
                    {
                        if (MoveArray != null)
                        {
                            string currentMove = MoveArray.Value<JObject>(j).Value<JObject>("move").Value<string>("name");
                            Moves.Add(currentMove);
                        }
                    }

                    List<string> Sprites = new List<string>();
                    var SpritesObject = PokemonData.Value<JObject>("sprites");


                    Sprites.Add(SpritesObject.Value<string>("front_default"));
                    Sprites.Add(SpritesObject.Value<string>("front_shiny"));
                    //Sprites.Add(SpritesObject.Value<string>("back_default"));
                    //Sprites.Add(SpritesObject.Value<string>("back_female"));

                    JArray TypeArray = PokemonData.Value<JArray>("types");
                    List<string> Types = new List<string>();

                    for (int j = 0; j < TypeArray.Count; j++)
                    {
                        if (TypeArray != null)
                        {
                            string currentType = TypeArray.Value<JObject>(j).Value<JObject>("type").Value<string>("name").ToString();
                            Types.Add(currentType);
                        }
                    }

                    Debug.WriteLine(ID);

                    pokemon = new Pokemon(ID, Name, Height, Weight, Abilities, Moves, Sprites, Types);

                    retrievedPokemon.Add(pokemon);
                   
                    
                }
                catch (Exception ex)
                {
                    // Details in ex.Message and ex.HResult.       
                }
                
                // Once your app is done using the HttpClient object call dispose to 
                // free up system resources (the underlying socket and memory used for the object)
                httpClient.Dispose();
                //return retrievedPokemon;

            }
            // Debug.WriteLine(retrievedPokemon.Count);
            return retrievedPokemon;
        }

      
    }
}
