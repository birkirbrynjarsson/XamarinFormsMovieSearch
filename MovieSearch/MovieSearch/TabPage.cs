using System;

using Xamarin.Forms;

namespace MovieSearch
{
    public class TabPage : TabbedPage
    {
        private MainPage _searchPage;
        private MovieListPage _topRatedPage;
        private MovieListPage _mostPopularPage;

        public TabPage(MainPage searchPage, MovieListPage topRatedPage, MovieListPage mostPopularPage)
        {
            _searchPage = searchPage;
            _topRatedPage = topRatedPage;
            _mostPopularPage = mostPopularPage;

            this.Children.Add(createSearchPage());
            this.Children.Add(createTopRatedPage());
            this.Children.Add(createMostPopularPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _topRatedPage._movieListViewModel.GetTopRatedMovies();
            _mostPopularPage._movieListViewModel.GetMostPopularMovies();
        }

        private NavigationPage createSearchPage()
        {
            var page = new NavigationPage(this._searchPage);
            page.Title = "Search";
            return page;
        }

        private NavigationPage createTopRatedPage()
        {
            var page = new NavigationPage(this._topRatedPage);
            page.Title = "Top Rated";
            return page;
        }

        private NavigationPage createMostPopularPage()
        {
            var page = new NavigationPage(this._mostPopularPage);
            page.Title = "Most Popular";
            return page;
        }
    }
}

