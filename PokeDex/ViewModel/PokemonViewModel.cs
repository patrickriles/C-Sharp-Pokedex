using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokeDex.Model;
using PokeDex.Http;
using System.Diagnostics;

namespace PokeDex.ViewModel
{
    
        
    public class PokemonViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Pokemon> pokemon { get; set; }
        APIcaller apicaller;
        public List<Pokemon> _allPokemon = new List<Pokemon>();

        private string _filter;
       

        public PokemonViewModel()
        {
            pokemon = new ObservableCollection<Pokemon>();
            apicaller = new APIcaller();
           // apicaller.GetData();
           // this.GetPokemon();
           
        }

        public void GetPokemon()
        {
            
            this._allPokemon = apicaller.GetPokemon();
            Debug.WriteLine(_allPokemon[0]);
        }

        public string Filter
        {
            get { return _filter; }
            set
            {
                if (value == _filter) { return; }
                _filter = value;
                PerformFiltering();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Filter)));
            }
        }

        

        /// <summary>
        /// Filters all values and puts them back into the list
        /// </summary>
        public void PerformFiltering()
        {
            if (_filter == null)
            {
                _filter = "";
            }

            var lowerCaseFilter = Filter.ToLowerInvariant().Trim();

            var result =
                _allPokemon.Where(d => d.PokemonAsString.ToLowerInvariant().
                Contains(lowerCaseFilter))
                .ToList();

            var toRemove = pokemon.Except(result).ToList();

            foreach (var x in toRemove)
            {
                pokemon.Remove(x);
            }

            var resultCount = result.Count();

            for (int i = 0; i < resultCount; i++)
            {

                var resultItem = result[i];
                if (i + 1 > pokemon.Count || !pokemon[i].Equals(resultItem))
                {
                    pokemon.Insert(i, resultItem);
                }
            }
        }


    }
}
