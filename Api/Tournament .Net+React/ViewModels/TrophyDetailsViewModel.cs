using KnightTournament.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnightTournament.ViewModels
{
    public class TrophyDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
    }
}
