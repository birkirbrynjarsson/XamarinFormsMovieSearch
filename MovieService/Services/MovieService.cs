using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Genres;
using DM.MovieApi.MovieDb.Movies;
using MovieService.Settings;
using Movie = MovieService.Models.Movie;

namespace MovieService.Services
{
    public class MovieService : IMovieService
    {
        private IApiMovieRequest _movieDbApi;
        private List<Movie> _movies;

        public MovieService()
        {
            MovieDbFactory.RegisterSettings(new MovieDbSettings());
            _movieDbApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            _movies = new List<Movie>();
        }

        public async Task<List<Movie>> GetMoviesByTitle(string title)
        {
            ApiSearchResponse<MovieInfo> response = await _movieDbApi.SearchByTitleAsync(title);
            _movies = new List<Movie>();
            if (response.Results == null)
            {
                return _movies;
            }

            foreach (MovieInfo movie in response.Results)
            {
                _movies.Add(new Movie
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    ReleaseYear = movie.ReleaseDate.Year,
                    RemoteImageUrl = movie.PosterPath,
                    LocalImageUrl = "",
                    Actors = new List<String>(),
                    Rating = movie.VoteAverage
                });
            }
            return _movies;
        }

        public async Task GetCastInList(List<Movie> movies)
        {
            foreach (Movie movie in movies)
            {
                var credits = await _movieDbApi.GetCreditsAsync(movie.Id);
                if (credits.Item != null)
                {
                    if (movie.Actors == null)
                    {
                        movie.Actors = new List<String>();
                    }
                    else
                    {
                        movie.Actors.Clear();
                    }

                    var count = credits.Item.CastMembers.Count;
                    for (int i = 0; i < 3 && i < count; i++)
                    {
                        movie.Actors.Add(credits.Item.CastMembers[i].Name);
                    }
                }
            }
        }

        public async Task<List<String>> GetCastByMovieId(int id)
        {
            ApiQueryResponse<MovieCredit> response = await _movieDbApi.GetCreditsAsync(id);
            List<String> actors = new List<String>();
            if (response == null)
            {
                return actors;
            }
            foreach (MovieCastMember actor in response.Item.CastMembers)
            {
                actors.Add(actor.Name);
            }
            return actors;
        }

        public List<Movie> GetMovieList()
        {
            return _movies;
        }

        public async Task<Movie> GetMovieById(int id)
        {
            var response = await _movieDbApi.FindByIdAsync(id);
            if (response.Item == null)
            {
                return new Movie();
            }
            var movie = new Movie()
            {
                Id = response.Item.Id,
                Title = response.Item.Title,
                ReleaseDate = response.Item.ReleaseDate,
                ReleaseYear = response.Item.ReleaseDate.Year,
                RemoteImageUrl = response.Item.PosterPath,
                LocalImageUrl = "",
                Genres = new List<String>(),
                Description = response.Item.Overview,
                Rating = response.Item.VoteAverage,
                Runtime = response.Item.Runtime,
                Caption = response.Item.Tagline
            };
            movie.Actors = await GetCastByMovieId(movie.Id);
            foreach (Genre genre in response.Item.Genres)
            {
                movie.Genres.Add(genre.Name);
            }
            return movie;
        }

        public async Task<List<Movie>> GetTopRatedMovies()
        {
            var response = await _movieDbApi.GetTopRatedAsync();
            List<Movie> movies = new List<Movie>();

            if (response.Results == null)
            {
                return movies;
            }

            foreach (MovieInfo movie in response.Results)
            {
                movies.Add(new Movie
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    ReleaseYear = movie.ReleaseDate.Year,
                    RemoteImageUrl = movie.PosterPath,
                    LocalImageUrl = "",
                    Actors = new List<String>(),
                    Rating = movie.VoteAverage
                });
            }
            return movies;
        }

        public async Task GetTopRatedMovies(List<Movie> movieList)
        {
            ApiSearchResponse<MovieInfo> response = await _movieDbApi.GetTopRatedAsync();

            if (response.Results == null)
            {
                return;
            }

            foreach (MovieInfo movie in response.Results)
            {
                movieList.Add(new Movie
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    ReleaseYear = movie.ReleaseDate.Year,
                    RemoteImageUrl = movie.PosterPath,
                    LocalImageUrl = "",
                    Actors = new List<String>(),
                    Rating = movie.VoteAverage
                });
            }
            return;
        }

        public async Task GetMostPopularMovies(List<Movie> movieList)
        {
            ApiSearchResponse<MovieInfo> response = await _movieDbApi.GetPopularAsync();

            if (response.Results == null)
            {
                return;
            }

            foreach (MovieInfo movie in response.Results)
            {
                movieList.Add(new Movie
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    ReleaseYear = movie.ReleaseDate.Year,
                    RemoteImageUrl = movie.PosterPath,
                    LocalImageUrl = "",
                    Actors = new List<String>(),
                    Rating = movie.VoteAverage
                });
            }
            return;
        }
    }
}
