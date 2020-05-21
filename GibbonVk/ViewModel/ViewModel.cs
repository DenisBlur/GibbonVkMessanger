using Jupiter.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;

namespace GibbonVk.ViewModel
{
    class ViewModel : ViewModelBase
    {

        public RelayCommand GoToFriendCommand { get; private set; }


        public ViewModel()
        {
        }

        protected override void InitializeCommands()
        {

            GoToFriendCommand = new RelayCommand(friend =>
            {
                NavigationService.Navigate(typeof(MainPage), new Dictionary<string, object>
                {
                    ["friend"] = friend
                });
            });
        }

    }
}
