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
    /// Interaction logic for Contact.xaml
    /// </summary>
    public partial class Contact : Window
    {
        Users userConnecte;

        public Contact(Users user)
        {
            userConnecte = user;
            InitializeComponent();
            photo_user.Source = new BitmapImage(new Uri(user.MyPicture.Src));
            name_user.Content = user.FirstName;

            List<Users> AllContact = UserManager.getAllUserSaufCurrentUser(userConnecte.ID);

            foreach (Users usr in AllContact)
            {
                listViewAllContact.Items.Add(usr);
            }

        }

        private void AddUserToFriend_Click(object sender, RoutedEventArgs e)
        {
            Users friend = (Users)listViewAllContact.SelectedItem;

            UserManager.AddUserToFriend(userConnecte.ID, friend.ID);


            List<Users> AllFriends = UserManager.GetAllFriendByUser(userConnecte.ID);
            foreach (Users usr in AllFriends)
            {
                listViewFriend.Items.Add(usr);
            }

        }
    }
}
