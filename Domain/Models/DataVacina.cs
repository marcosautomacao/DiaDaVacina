using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models
{
    [Table("DataVacina")]
    public class DataVacina
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int idade { get; set; }
        public string Estado { get; set; }
        public DateTime DataInicio { get; set; }
    }
}
