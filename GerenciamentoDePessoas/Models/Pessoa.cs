using GerenciamentoDePessoas.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace GerenciamentoDePessoas.Models
{
    public class Pessoa
    {
        public Pessoa() {}
        public Pessoa(int id, string nome, string sobrenome, DateTime DataNascimento)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            this.DataNascimento = DataNascimento;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "O Nome deve ser maior que 2 caracteres e menor que 10")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Sobrenome é obrigatório.")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "O Sobrenome deve ser maior que 2 caracteres e menor que 10")]
        public string Sobrenome { get; set; }

        [CustomValidation(typeof(Pessoa), "ValidarDataNascimento")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório")]
        [StringLength(11,MinimumLength = 11, ErrorMessage = "O CPF deve conter no máximo 11 digitos")]        
        public string CPF { get; set; }

        [Required(ErrorMessage = "O Tipo sanguineo é obrigatório!")]
        public ETipoSanguineo TipoSanguineo { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        // Adicione outros campos conforme necessário

        public static ValidationResult ValidarDataNascimento(DateTime dataNascimento)
        {
            if (dataNascimento > DateTime.Now)
            {
                return new ValidationResult("A data de nascimento não pode ser futura.");
            }

            return ValidationResult.Success;
        }
    }
}
