using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShoppingList.Models;

namespace ShoppingList.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        Title = "Login";
    }

    async void Login_OnClicked(object sender, EventArgs e)
    {
        //very user info input
        if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPassword.Text))
        {
            DisplayAlert("Error", "Please enter username and password", "OK");
        }
        //do the rest
        else
        {



            var data = JsonConvert.SerializeObject(new UserAccount(txtUser.Text, txtPassword.Text));
            var client = new HttpClient();

            //login to account and get session key
            var response = await client.PostAsync(new Uri("https://joewetzel.com/fvtc/account/login"),
                new StringContent(data, Encoding.UTF8, "application/json"));

            var SKey = response.Content.ReadAsStringAsync().Result;

            if (!string.IsNullOrEmpty(SKey) && SKey.Length < 50)
            {
                App.SessionKey = SKey;
                Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "Sorry Invalid Username or Password", "OK");
                return;
            }

        }
    }

    private void CreateAccount_OnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new NewAccountPage());
    }
}