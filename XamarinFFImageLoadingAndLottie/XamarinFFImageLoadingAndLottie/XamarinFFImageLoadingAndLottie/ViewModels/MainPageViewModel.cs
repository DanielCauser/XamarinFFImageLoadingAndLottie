using System;
using Prism.Navigation;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Newtonsoft.Json;
using Xamarin.Essentials;
using XamarinFFImageLoadingAndLottie.Models;

namespace XamarinFFImageLoadingAndLottie.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private IEnumerable<Monkey> _monkeys;

        public IEnumerable<Monkey> Monkeys
        {
            get => _monkeys;
            set => SetProperty(ref _monkeys, value);
        }

        public ICommand ReloadMonkeysCommand
        {
            get { return new Command(async () => { await LoadMonkeys(); }); }
        }
        
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
        }

        public override async void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            await LoadMonkeys();
        }

        private async Task LoadMonkeys()
        {
            if (IsBusy) return;

            IsBusy = true;
            await Task.Run(() =>
            {
                using (var reader = new StreamReader(Assembly.GetAssembly(typeof(App))
                                                                   .GetManifestResourceStream(Assembly.GetAssembly(typeof(App)).GetManifestResourceNames().First()) ?? throw new InvalidOperationException()))
                {
                    Monkeys = JsonConvert.DeserializeObject<IEnumerable<Monkey>>(reader.ReadToEnd());
                }
            });
            IsBusy = false;
        }
    }
}
