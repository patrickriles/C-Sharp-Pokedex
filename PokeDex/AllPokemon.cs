using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PokeDex
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public class AllPokemon : ObservableCollection<string>,ISupportIncrementalLoading
    {
      

        public int lastItem = 1;

        public bool HasMoreItems
        {
            get
            {
                if(lastItem == 10000)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            ProgressBar progressBar = ((Window.Current.Content as Frame).Content
                as GetAll).ProgressBar;
            CoreDispatcher coreDispatcher = Window.Current.Dispatcher;

            return Task.Run<LoadMoreItemsResult>(async () =>
            {
                await coreDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    progressBar.IsIndeterminate = true;
                    progressBar.Visibility = Visibility.Visible;
                });

                List<string> items = new List<string>();
                for (int i = 0; i < count; i++)
                {
                    items.Add(string.Format("Item {0}", lastItem));
                    lastItem++;
                    if (lastItem == 10000)
                        break;
                }
                await coreDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    foreach (string item in items)
                    {
                        this.Add(item);
                        progressBar.Visibility = Visibility.Collapsed;
                        progressBar.IsIndeterminate = false;
                    }
                });
                return new LoadMoreItemsResult() { Count = count };
            }).AsAsyncOperation<LoadMoreItemsResult>();
        }

       
    }
}
