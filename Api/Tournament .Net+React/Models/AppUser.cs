using KnightTournament.Attributes;
using KnightTournament.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace KnightTournament.Models
{
    public class AppUser: IdentityUser<Guid>
    {
        public override Guid Id { get; set; }
        public Rank Rank { get; set; }

        [DisallowSimilar]
        public override string? UserName { get; set; }

        public double Rating { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        public virtual ICollection<Trophy>? Trophies { get; set; } = new List<Trophy>();

        public virtual ICollection<Combat>? Combats { get; set; } = new List<Combat>();

        public virtual ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>(); 

        public virtual ICollection<Tournament> HoldedTournaments { get; set; } = new List<Tournament>();
    }
}
