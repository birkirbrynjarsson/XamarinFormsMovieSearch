using Xamarin.Forms;

namespace MovieSearch.Model
{
    public class Person
    {
        public string Name { get; set; }
        public int BirthYear { get; set; }
        public string ImageName { get; set; }
        public ImageSource ImageSource => ImageSource.FromResource("MovieSearch.Images." + this.ImageName + ".png");
    }
}
