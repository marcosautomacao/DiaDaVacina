using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models
{
    [Table("Pessoa")]
    [Index(nameof(DataNascimento))]
    public class Pessoa
    {
        [Key]
        public Int64 Telefone { get; set; }
        public string Estado { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
    }
}
