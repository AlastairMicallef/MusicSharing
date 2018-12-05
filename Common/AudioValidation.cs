using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class AudioValidation
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please input name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please input price")]
        [Range(1, 1000000, ErrorMessage = "Prices should be greater than 0")]
        public decimal Price { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select category")]
        [Display(Name = "Category")]
        [Range(1, 5, ErrorMessage = "Please select category")]
        public int Category_fk { get; set; }



    }

    [MetadataType(typeof(AudioValidation))]
    public partial class Item
    { }
}
}
