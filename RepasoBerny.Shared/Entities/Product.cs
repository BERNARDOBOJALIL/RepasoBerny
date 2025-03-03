using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepasoBerny.Shared.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractere ")]
        [Display(Name = "Producto")]

        public string? Name { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Display(Name = "Precio")]
        public decimal? Price { get; set; }

        [Display(Name = "Imagen")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [Display(Name = "Inventario")]
        public double Stock {  get; set; }
        public User User { get; set; }

    }
}
