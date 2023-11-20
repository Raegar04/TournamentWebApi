using KnightTournament.Models;

namespace KnightTournament.ViewModels
{
    public class LocationDetailsViewModel
    {
        public Guid Guid { get; set; }
        public string Country { get; set; }

        public string City { get; set; }

        public string Place { get; set; }

        public string ImgUri { get; set; }
    }
}
