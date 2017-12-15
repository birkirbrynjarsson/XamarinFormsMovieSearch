using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MovieService.Models;
using MovieService.Services;

namespace MovieSearch
{
    public partial class MainPage : ContentPage
    {
        private IMovieService _movieService;

        public MainPage(IMovieService movieService)
        {
            this._movieService = movieService;
            InitializeComponent();
        }

        private async void MovieListButton_OnClicked(object sender, EventArgs e)
        {
            var movies = await this._movieService.GetMoviesByTitle(this.SearchInput.Text);
            await this.Navigation.PushAsync(new MovieListPage(this._movieService, movies, this.SearchInput.Text));
        }
    }
}
