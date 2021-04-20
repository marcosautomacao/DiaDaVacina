using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Pessoa")]
    public class Pessoa
    {
        [Key]
        public string Telefone { get; set; }
        public string Estado { get; set; }
        public DateTime DataNascimento;
    }
}
