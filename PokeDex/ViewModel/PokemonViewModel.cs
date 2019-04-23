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
using Windows.UI.Xaml.Media.Imaging;

namespace PokeDex.ViewModel
{
    
        
    public class PokemonViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Pokemon> pokemon { get; set; }
        APIcaller apicaller;
        public List<Pokemon> _allPokemon = new List<Pokemon>();
        public string PokeName;
        public BitmapImage PokeImageUrl;
        public BitmapImage PokeImageUrl2;
        public string PokeMove1;
        public string PokeMove2;
        public string PokeMove3;
        public string PokeMove4;
        public string movesTitle;
        public string pokeWeight;
        public string pokeHeight;
        public string pokeType1;
        public string pokeType2;
        public string StatTitle;

        private string _filter;

        private Pokemon _selectedPokemon;


        public PokemonViewModel()
        {
            pokemon = new ObservableCollection<Pokemon>();
            apicaller = new APIcaller(this);
            this.GetPokemon();
        }

        public async void GetPokemon()
        {
            for (int i = 1; i <= 151; i++)
            {
                this._allPokemon.Add(await apicaller.GetData(i));
                PerformFiltering();
            }
         
        }



        public Pokemon SelectedPokemon
        {

            get { return _selectedPokemon; }

            set
            {
                _selectedPokemon = value;

                if (value == null)
                {
                    PokeName = "";
                  
                }
                else
                {
                    PokeName = value.Name;
                    PokeImageUrl = new BitmapImage(new Uri(@value.Sprites[1], UriKind.RelativeOrAbsolute));
                    PokeImageUrl2 = new BitmapImage(new Uri(@value.Sprites[0], UriKind.RelativeOrAbsolute));

                    StatTitle = "Stats";
                    movesTitle = "Moves";
                    PokeMove1 = value.Moves[0];
                    if (value.Moves.Count > 1)
                    {
                        PokeMove2 = value.Moves[1];
                        PokeMove3 = value.Moves[2];
                        PokeMove4 = value.Moves[3];
                    }
                    pokeWeight = "Weight: " + value.Weight;
                    pokeHeight = "Height: " + value.Height;
                    pokeType1 = value.Types[0];
                    if(value.Types.Count > 1)
                    {
                        pokeType2 = value.Types[1];
                    }
                  
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PokeName"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PokeImageUrl"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PokeImageUrl2"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PokeMove1"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PokeMove2"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PokeMove3"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PokeMove4"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("movesTitle"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StatTitle"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("pokeHeight"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("pokeWeight"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("pokeType1"));
                if (value.Types.Count > 1 || value != null)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("pokeType2"));
                }




            }
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
