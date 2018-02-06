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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace essaiVue
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            for (int i = 0; i < 5; i++)
            {

            }
        }

        private void btn_inscription_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Close();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            string userLogin = user_login.Text;
            String password = user_password.Password;

            //attendant login de mohamed
            //Users user = UserManager.signIn(userLogin, password);

            //a fixer 
            //Users user = UserManager.GetUserById(1);

            Picture picture = new Picture();
            picture.Src = @"E:\hatem-pc\isi\P55-c#\Ws_C#\c-sharpe\CsharpProject\essaiVue\Resources\no_photo.png";
            Users user = new Users(1, "hatem", "chaabane", "hatem", "123465", new Address(), picture, new List<Users>());

            if (user != null)
            {
                Contact contact = new Contact(user);
                contact.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("erreur de login");
            }

        }
    }
}
