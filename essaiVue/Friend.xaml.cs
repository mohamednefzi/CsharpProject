using BLLListContact;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace essaiVue
{
    /// <summary>
    /// Interaction logic for Friend.xaml
    /// </summary>
    public partial class Friend : Window
    {
        Users friend;
        Users CurrentUser;
        public Friend(Users friend, Users currentUser)
        {
            this.CurrentUser = currentUser;
            this.friend = friend;
            InitializeComponent();

            this.friend_image.Source = new BitmapImage(new Uri(friend.MyPicture.Src));
            firstName_friend.Content = friend.FirstName;
            LastName_friend.Content = friend.LastName;
            Number_friend.Content = friend.MyAddress.Number;
            Adress_friend.Content = friend.MyAddress.Street + friend.MyAddress.City + friend.MyAddress.Country;

            //confimation_friend.IsEnabled = false;
        }




        private void closeFriendPage_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void confimation_friend_Click(object sender, RoutedEventArgs e)
        {
            UserManager.ConfirmNewFriend(CurrentUser.ID, friend.ID);
        }
    }
}
