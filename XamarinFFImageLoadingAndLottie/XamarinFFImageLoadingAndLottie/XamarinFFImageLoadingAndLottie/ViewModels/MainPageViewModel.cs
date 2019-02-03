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
        private Random _random;
        private IEnumerable<Monkey> _monkeys;
        private Monkey _selectedMonkeys;

        public Monkey SelectedMonkey
        {
            get => _selectedMonkeys;
            set => SetProperty(ref _selectedMonkeys, value);
        }

        public ICommand ReloadMonkeysCommand
        {
            get { return new Command(() => { LoadMonkeys(); }); }
        }
        
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            _random = new Random();
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            LoadMonkeys();
        }

        private void LoadMonkeys()
        {
            if (IsBusy) return;

            IsBusy = true;

            if(_monkeys is null)
            {
                using (var reader = new StreamReader(Assembly.GetAssembly(typeof(App))
                                                               .GetManifestResourceStream(Assembly.GetAssembly(typeof(App)).GetManifestResourceNames().First()) ?? throw new InvalidOperationException()))
                {
                    _monkeys = JsonConvert.DeserializeObject<IEnumerable<Monkey>>(reader.ReadToEnd());
                }
            }

            int index = _random.Next(0, _monkeys.Count());

            SelectedMonkey = _monkeys.ElementAt(index);

            IsBusy = false;
        }
    }
}
