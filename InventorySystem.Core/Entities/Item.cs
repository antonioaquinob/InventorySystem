using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.Entities
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string ItemBrand { get; set; } = string.Empty;
        public string ItemDescription { get; set; } = string.Empty;
        public decimal ItemPrice { get; set; }
        public int QuantityAvailable { get; set; }
        public int CriticalLevel { get; set; }
    }
}
