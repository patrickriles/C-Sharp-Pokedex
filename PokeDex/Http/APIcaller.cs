using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Web.Http.Headers;


namespace PokeDex.Http
{
    class APIcaller
    {
        public async void getData()
        {
            // Note: the URI constructor will throw an exception
            // if the string passed is not a valid URI
            var uri = new Uri("https://pokeapi.co/api/v2/pokemon/1");
            var httpClient = new HttpClient();

            // Always catch network exceptions for async methods
            try
            {
                var result = await httpClient.GetStringAsync(uri);
                Debug.WriteLine(result);
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
