﻿using KnightTournament.Models.Enums;
using KnightTournament.Models;

namespace KnightTournament.ViewModels
{
    public class CombatDetailsViewModel
    {
        public Guid Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public CombatType Type { get; set; }
    }
}
