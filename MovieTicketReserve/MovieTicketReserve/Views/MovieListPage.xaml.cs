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
    public partial class MovieListPage : ContentPage
    {
        public ObservableCollection<MovieList> MovieLists;
        private int pageNumber = 0;
        public MovieListPage()
        {
            InitializeComponent();
            MovieLists = new ObservableCollection<MovieList>();
            GetMovies();
        }

        private async void GetMovies()
        {
            pageNumber++;
            var movies = await ApiServices.GetAllMovies(pageNumber, 6);
            foreach (var item in movies)
            {
                MovieLists.Add(item);
            }
            CvMovies.ItemsSource = MovieLists;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void add_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddMoviePage());
        }

        private void CvMovies_RemainingItemsThresholdReached(object sender, EventArgs e)
        {
            GetMovies();
        }

        private async void CvMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var current = e.CurrentSelection.FirstOrDefault() as MovieList;
            if (current == null) return;
            var result = await DisplayAlert("Confirm", "Are you sure you want to delete this?", "Yes", "No");
            if (result)
            {
                var response = await ApiServices.DeleteMovies(current.Id);
                if (response == false) return;
                MovieLists.Clear();
                pageNumber = 0;
                GetMovies();
            }

            ((CollectionView)sender).SelectedItem = null;
        }
    }
}