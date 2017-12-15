using System;
using System.Collections.Generic;
using MovieService.Services;
using Xamarin.Forms;

namespace MovieSearch
{
    public partial class TopRatedPage : ContentPage
    {
        IMovieService _movieService;
        public TopRatedPage(IMovieService movieService)
        {
            _movieService = movieService;
            var movies = movieService.GetTopRatedMovies();
            this.BindingContext = new MovieListViewModel(this.Navigation, _movieService,);
            InitializeComponent();
        }
    }
}
