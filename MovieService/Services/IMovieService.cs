using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieService.Models;

namespace MovieService.Services
{
    public interface IMovieService
    {
        Task<List<Movie>> GetMoviesByTitle(string title);
        List<Movie> GetMovieList();
        Task GetCastInList(List<Movie> movies);
        Task<List<String>> GetCastByMovieId(int id);
        Task<Movie> GetMovieById(int id);
        Task<List<Movie>> GetTopRatedMovies();
        Task GetTopRatedMovies(List<Movie> movieList);
        Task GetMostPopularMovies(List<Movie> movieList);
    }
}