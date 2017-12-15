using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using MovieService.Models;
using MovieService.Services;

namespace MovieSearch
{
    public partial class App : Application
    {
        IMovieService _movieService;
        public App()
        {
            InitializeComponent();
            _movieService = new MovieService.Services.MovieService();

            var searchPage = new MainPage(_movieService);
            var topRatedPage = new MovieListPage(_movieService, null, "top rated");
            var mostPopularPage = new MovieListPage(_movieService, null, "most popular");

            var tabPage = new TabPage(searchPage, topRatedPage, mostPopularPage);

            MainPage = tabPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
