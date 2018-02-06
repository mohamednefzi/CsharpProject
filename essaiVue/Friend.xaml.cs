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

            if (UserManager.GetIdRelation(CurrentUser.ID, friend.ID) == -1)
            {
                confimation_friend.IsEnabled = true;
            }
            else
            {
                confimation_friend.IsEnabled = false;



                listVeiewEvent.Items.Clear();
                List<Events> listeEvenement = EventsManager.GetAllFriendsEvents(CurrentUser.ID, friend.ID);
                foreach (Events evt in listeEvenement)
                {
                    evt.Type = "envoyé";
                    listVeiewEvent.Items.Add(evt);
                }
                listeEvenement = EventsManager.GetAllFriendsEvents(friend.ID, CurrentUser.ID);
                foreach (Events evt in listeEvenement)
                {
                    evt.Type = "Récu";
                    listVeiewEvent.Items.Add(evt);
                }
            }
        }




        private void closeFriendPage_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void confimation_friend_Click(object sender, RoutedEventArgs e)
        {
            UserManager.ConfirmNewFriend(CurrentUser.ID, friend.ID);
        }



        private void add_event_Click(object sender, RoutedEventArgs e)
        {
            int idEventGenerated = EventsManager.AddEvent(CurrentUser.ID, friend.ID, "premiere Combat");
            EventsManager.ConfirmEvents(idEventGenerated);

            listVeiewEvent.Items.Clear();
            List<Events> listeEvenement = EventsManager.GetAllFriendsEvents(CurrentUser.ID, friend.ID);
            foreach (Events evt in listeEvenement)
            {
                evt.Type = "envoyé";
                listVeiewEvent.Items.Add(evt);
            }
            listeEvenement = EventsManager.GetAllFriendsEvents(friend.ID, CurrentUser.ID);
            foreach (Events evt in listeEvenement)
            {
                evt.Type = "Récu";
                listVeiewEvent.Items.Add(evt);
            }






        }



        private void delete_event_Click(object sender, RoutedEventArgs e)
        {
            if (listVeiewEvent.SelectedItem != null)
            {
                Events events = (Events)listVeiewEvent.SelectedItem;
                EventsManager.DeleteEvents(events.ID);

                List<Events> listeEvenement = EventsManager.GetAllFriendsEvents(CurrentUser.ID, friend.ID);
                listVeiewEvent.Items.Clear();
                foreach (Events evt in listeEvenement)
                {
                    listVeiewEvent.Items.Add(evt);
                }

            }
        }
    }
}
