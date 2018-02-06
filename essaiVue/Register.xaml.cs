using BLLListContact;
using Entities;
using Microsoft.Win32;
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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        Users users;
        public Register()
        {
            InitializeComponent();
        }

        public Register(Users user)
        {
            this.users = user;
            InitializeComponent();
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            String lastName = lastName_user.Text;
            String firstName = fistName_user.Text;
            String login = login_user.Text;
            String password = password_user.Password;
            String streetAndCity = adrress.Text;
            int phone = int.Parse(phone_user.Text);
            String country = province_user.Text;

            Address addressFinal = new Address();
            addressFinal.Number = phone;
            addressFinal.Province = "";
            addressFinal.Country = country;
            addressFinal.Street = streetAndCity;
            addressFinal.City = "";

            Picture picture = new Picture();
            picture.Src = photo_user.Source.ToString();
            picture.ID = 1;

            Users user = new Users();
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Login = login;
            user.pwd = password;
            user.MyAddress = addressFinal;
            user.MyPicture = picture;

            if (UserManager.UsernameExist(user.Login))
            {
                MessageBox.Show("login exsite deja ...");
            }
            else
            {
                UserManager.insertUser(user);
                MessageBox.Show("Bien enregistré");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }



        }

        private void btnLoadPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                photo_user.Source = new BitmapImage(new Uri(op.FileName));  //ajoute le chemin de l'image
                //string photoPath = photo_user.Source.ToString();
            }
        }
    }




}
