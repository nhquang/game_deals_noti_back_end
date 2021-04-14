using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GameDealsNotification.Models
{
    public class Notification
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int game_id { get; set; }
        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string email { get; set; }
        [StringLength(20)]
        [Required(AllowEmptyStrings = false)]
        public string name { get; set; }
        [Required]
        [Range(0,double.MaxValue)]
        public double price { get; set; }
    }
}
