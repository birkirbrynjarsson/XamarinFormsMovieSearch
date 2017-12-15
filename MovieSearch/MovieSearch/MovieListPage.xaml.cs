using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MovieService.Models;
using MovieService.Services;

namespace MovieSearch
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieListPage : ContentPage
    {
        public MovieListViewModel _movieListViewModel;

        public MovieListPage(IMovieService movieService, List<Movie> movies, String searchString)
        {
            this._movieListViewModel = new MovieListViewModel(this.Navigation, movieService, movies, searchString);
            this.BindingContext = this._movieListViewModel;
            InitializeComponent();
        }
    }
}