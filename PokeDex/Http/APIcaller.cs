using System;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http;


namespace PokeDex.Http
{
    class APIcaller
    {
        public JObject PokemonData { get; set; }

        public async void getData()
        {
            // Note: the URI constructor will throw an exception
            // if the string passed is not a valid URI
            var uri = new Uri("https://pokeapi.co/api/v2/pokemon/1");
            var httpClient = new HttpClient();

            // Always catch network exceptions for async methods
            try
            {
                //Change this so it captures all the needed data and creates a pokemon object
                var result = await httpClient.GetStringAsync(uri);
                PokemonData = JObject.Parse(result);
                // Grabbing the ability array
                var AbilitiesArray = PokemonData.Value<JArray>("abilities");
                
                // Getting the first object in array from above
                var AbilitiesOneObject = AbilitiesArray.Value<JObject>(0);
                
                // Getting the first object from the object above
                var AbilityOneObject = AbilitiesOneObject.Value<JObject>("ability");
                
                // Getting specific value
                string AbilityOne = AbilityOneObject.Value<string>("name").ToString();

                
                string AbilityTwo = PokemonData.Value<JArray>("abilities").Value<JObject>(1).Value<JObject>("ability").Value<string>("name").ToString();

                Debug.WriteLine(AbilityOne);
                Debug.WriteLine(AbilityTwo);
            }
            catch (Exception ex)
            {
                // Details in ex.Message and ex.HResult.       
            }

            // Once your app is done using the HttpClient object call dispose to 
            // free up system resources (the underlying socket and memory used for the object)
            httpClient.Dispose();
        }
    }
}
