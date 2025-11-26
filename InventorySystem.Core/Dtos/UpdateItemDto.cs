using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.Dtos
{
    public class UpdateItemDto
    {
        [Required]
        [MaxLength(100)]
        public string ItemName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string ItemBrand { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string ItemDescription { get; set; } = string.Empty;

        [Required]
        public decimal ItemPrice { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int QuantityAvailable { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int CriticalLevel { get; set; }
    }
}
