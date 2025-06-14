using Microsoft.AspNetCore.Identity;

namespace GerenciamentoDePessoas.Data
{
    public class Usuario : IdentityUser
    {
        public string CPF { get; set; }
    }
}
