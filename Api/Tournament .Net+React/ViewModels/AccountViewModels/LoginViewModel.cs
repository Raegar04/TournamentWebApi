using System.ComponentModel.DataAnnotations;

namespace KnightTournament.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
