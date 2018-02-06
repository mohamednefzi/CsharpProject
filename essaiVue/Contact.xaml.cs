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

            List<Users> AllFriends = UserManager.GetAllFriendByUser(userConnecte.ID);

            foreach (Users usr in AllFriends)
            {
                listViewFriend.Items.Add(usr);
            }

        }

        private void AddUserToFriend_Click(object sender, RoutedEventArgs e)
        {
            Users friend = (Users)listViewAllContact.SelectedItem;

            UserManager.AddUserToFriend(userConnecte.ID, friend.ID);


            List<Users> AllFriends = UserManager.GetAllFriendByUser(userConnecte.ID);
            listViewFriend.Items.Clear();
            foreach (Users usr in AllFriends)
            {
                listViewFriend.Items.Add(usr);
            }

        }

        private void NotConfirmedFriend_Click(object sender, RoutedEventArgs e)
        {
            List<Users> AllFriends = UserManager.GetAllNotConfirmedFriend(userConnecte.ID);
            listViewFriend.Items.Clear();
            foreach (Users usr in AllFriends)
            {
                listViewFriend.Items.Add(usr);
            }
        }

        private void TrueFriend_Click(object sender, RoutedEventArgs e)
        {
            List<Users> AllFriends = UserManager.GetAllFriendByUser(userConnecte.ID);
            listViewFriend.Items.Clear();
            foreach (Users usr in AllFriends)
            {
                listViewFriend.Items.Add(usr);
            }
        }

        private void infosFriend_Click(object sender, RoutedEventArgs e)
        {
            Users friendUsers = (Users)listViewFriend.SelectedItem;
            Friend friendPage = new Friend(UserManager.GetUserById(friendUsers.ID), userConnecte);
            friendPage.Show();
        }

        private void delete_friend_Click(object sender, RoutedEventArgs e)
        {
            Users friendUsers = (Users)listViewFriend.SelectedItem;

            UserManager.DeleteFriend(userConnecte.ID, friendUsers.ID);

            this.TrueFriend_Click(new object(), new RoutedEventArgs());

        }

        private void deconnexion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void delete_contact_Click(object sender, RoutedEventArgs e)
        {
            UserManager.deleteUser(userConnecte.ID);
            MessageBox.Show("Contact bien supprimé");
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void UserNotFriend_Click(object sender, RoutedEventArgs e)
        {
            List<Users> AllContact = UserManager.GetAllUserNotFriendByIdUser(userConnecte.ID);
            listViewAllContact.Items.Clear();
            foreach (Users usr in AllContact)
            {
                listViewAllContact.Items.Add(usr);
            }
        }

        private void AllUser_Click(object sender, RoutedEventArgs e)
        {
            List<Users> AllContact = UserManager.getAllUserSaufCurrentUser(userConnecte.ID);
            listViewAllContact.Items.Clear();
            foreach (Users usr in AllContact)
            {
                listViewAllContact.Items.Add(usr);
            }
        }

        private void friend_notification_Click(object sender, RoutedEventArgs e)
        {
            List<Users> listeDesNotifications = UserManager.GetFriendNotification(userConnecte.ID);

            listViewFriend.Items.Clear();
            foreach (Users usr in listeDesNotifications)
            {
                listViewFriend.Items.Add(usr);
            }


        }
    }
}
