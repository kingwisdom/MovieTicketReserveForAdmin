using MovieTicketReserve.Models;
using MovieTicketReserve.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTicketReserve.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservationListPage : ContentPage
    {
        public ObservableCollection<Reservation> Reservations;
        public ReservationListPage()
        {
            InitializeComponent();
            Reservations = new ObservableCollection<Reservation>();
            GetReservationList();
        }

        private async void GetReservationList()
        {
            var reservations = await ApiServices.GetAllReservation();
            foreach (var item in reservations)
            {
                Reservations.Add(item);
            }
            CvReservations.ItemsSource = Reservations;
        }

        private void back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void CvReservations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var current = e.CurrentSelection.FirstOrDefault() as Reservation;
            if (current == null) return;
            Navigation.PushModalAsync(new ReservationDetailPage(current.Id));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}