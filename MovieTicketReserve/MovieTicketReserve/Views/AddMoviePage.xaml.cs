using ImageToArray;
using MovieTicketReserve.Models;
using MovieTicketReserve.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTicketReserve.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMoviePage : ContentPage
    {
        MediaFile file;
        public AddMoviePage()
        {
            InitializeComponent();
        }

        private void back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void uploadImg_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Error", ":(you can not uuse this feature", "ok");
                return;
            }
            file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null) return;
            uploadImg.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            var imageArr = FromFile.ToArray(file.GetStream());
            var movie = new Movie()
            {
                Name = EntName.Text,
                Description = EntDescription.Text,
                Language = EntLanguage.Text,
                Duration = EntDuration.Text,
                PlayingDate = EntPlayingDate.Text,
                PlayingTime = EntPlayingTime.Text,
                TicketPrice = EntTicketPrice.Text,
                Rating = Convert.ToDouble(EntRating.Text),
                Genre = EntGenre.Text,

                TrailorUrl = EntTrailorUrl.Text,
                ImageArray = imageArr
            };
            var response = await ApiServices.AddMovie(file, movie);
            if (!response)
            {
                await DisplayAlert("Error occured", "Something went wrong, Could not be added", "Cancel");
            }
            else
            {
                await DisplayAlert("Success", "Movie added successfully", "Ok");
                await Navigation.PopModalAsync();
            }
        }
    }
}