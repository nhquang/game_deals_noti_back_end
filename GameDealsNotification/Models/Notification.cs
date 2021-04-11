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
        public int game_id { get; set; }
        [StringLength(50)]
        [Required]
        public string email { get; set; }
        [StringLength(20)]
        [Required]
        public string name { get; set; }
        [Required]
        public double price { get; set; }
    }
}
