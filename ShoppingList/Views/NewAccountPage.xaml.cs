using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingList.Models;
using Newtonsoft.Json;

namespace ShoppingList.Views;

public partial class NewAccountPage : ContentPage
{
    public NewAccountPage()
    {
        InitializeComponent();
        Title = "Create New Account";
    }

    async void CreateAccount_OnClicked(object sender, EventArgs e)
    {
        
        //password match
        
        //valid email address
        
        
        //api
        var data = JsonConvert.SerializeObject(new UserAccount(txtUser.Text, txtPassword1.Text, txtEmail.Text));
        var client = new HttpClient();

        var response = await client.PostAsync(new Uri("https://joewetzel.com/fvtc/account/createuser"), 
            new StringContent(data, Encoding.UTF8, "application/json"));

        var AccountStatus = response.Content.ReadAsStringAsync().Result;
        
        //user exists?
        if (AccountStatus == "user exists")
        {
            await DisplayAlert("Error", "Username in use", "OK");
            return;
        }
        
        //is email in use?
        if (AccountStatus == "email exists")
        {
            await DisplayAlert("Error", "Email in use", "OK");
            return;
        }
        
        if (AccountStatus == "complete")
        {
            
            //login to account and get session key
            response = await client.PostAsync(new Uri("https://joewetzel.com/fvtc/account/login"), 
                new StringContent(data, Encoding.UTF8, "application/json"));

            var SKey = response.Content.ReadAsStringAsync().Result;
            
            if(!string.IsNullOrEmpty(SKey) && SKey.Length < 50)
            {
                App.SessionKey = SKey;
                Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "Sorry there was an issue logging you in", "OK");
                return;
            }
            
            

        }
        else
        {
            await DisplayAlert("Error", "Error occured creating account", "OK");
            return;
        }

    }
}