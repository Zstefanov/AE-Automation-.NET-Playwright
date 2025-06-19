using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_extensive_project.Models
{
    public class Brand
    {
        //making sure identity column is created(autoatically incremented)
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!; //avoid warning
    }
}
