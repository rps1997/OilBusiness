//using Microsoft.AspNetCore.Mvc.DataAnnotations

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OilBusiness.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100)]
        public int Displayorder { get; set; }

    }
}
