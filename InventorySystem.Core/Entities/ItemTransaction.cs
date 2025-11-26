using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.Entities
{
    public class ItemTransaction
    {
        public int TransactionId { get; set; }
        public int ItemId { get; set; }
        public int Total { get; set; }
        public DateTime TransactionDate { get; set; }
        public int UserId { get; set; }
        public bool IsCompleted { get; set; }
        public Item? Item { get; set; }
}
}
