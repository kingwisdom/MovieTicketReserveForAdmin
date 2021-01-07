using MovieTicketReserve.Models;
using MovieTicketReserve.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTicketReserve.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            
        }

        private void BtnMovies_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MovieListPage());
        }

        private void BtnReservations_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ReservationListPage());
        }
    }
}