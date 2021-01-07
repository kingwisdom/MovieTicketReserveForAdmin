using MovieTicketReserve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTicketReserve.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservationDetailPage : ContentPage
    {
        public ReservationDetailPage(int ID)
        {
            InitializeComponent();
            GetReservation(ID);
        }

        private async void GetReservation(int iD)
        {
            var res = await ApiServices.GetReservationById(iD);
            LblReservationId.Text = res.Id.ToString();
            LblReservationTime.Text = res.ReservationTime.ToString("MMM d, yyyy HH:mm");
            LblCustomer.Text = res.CustomerName;
            LblMovieName.Text = res.MovieName;
            LblEmail.Text = res.Email;
            LblPhone.Text = res.Phone;
            LblPrice.Text = res.Price + " NGN ";
            LblQty.Text = res.Qty.ToString();
            
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}