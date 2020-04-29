using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GibbonVk.Models
{
    class UserModel : INotifyPropertyChanged
    {
        int UserId { set; get; }
        string FirstName { set; get; }
        string LastName { set; get; }
        string DBorn { set; get; }
        string Status { set; get; }
        string City { set; get; }
        List<PhotoModel> PhotosUser { set; get; }
        int isClosed { set; get; }
        int canPost { set; get; }
        int online { set; get; }
        int canSendFriendRequest { set; get; }
        int canWritePrivateMessage { set; get; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
