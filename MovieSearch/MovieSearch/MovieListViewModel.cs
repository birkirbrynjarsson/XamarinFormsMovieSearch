using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MovieService.Models;
using MovieService.Services;

namespace MovieSearch
{
    public class MovieListViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        private IMovieService _movieService;
        private Movie _selectedMovie;
        private List<Movie> _movieList;

        public MovieListViewModel(INavigation navigation, IMovieService movieService, List<Movie> movieList, String searchString)
        {
            this._navigation = navigation;
            this._movieService = movieService;
            if (movieList == null || movieList.Count == 0)
            {
                this._movieList = new List<Movie>();
            }
            else
            {
                this._movieList = movieList;
            }
        }

        public List<Movie> Movies
        {
            get => this._movieList;

            set
            {
                this._movieList = value; 
                OnPropertyChanged();
            }
        }

        public Movie SelectedMovie
        {
            get => this._selectedMovie;

            set {
                if (value != null)
                {
                    this._selectedMovie = value;
                    this._navigation.PushAsync(new MoviePage(this._selectedMovie), true);
                } 
            }
        }

        public async void GetTopRatedMovies()
        {
            await _movieService.GetTopRatedMovies(this._movieList);
            await _movieService.GetCastInList(this._movieList);
        }

        public async void GetMostPopularMovies()
        {
            await _movieService.GetMostPopularMovies(this._movieList);
            await _movieService.GetCastInList(this._movieList);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
