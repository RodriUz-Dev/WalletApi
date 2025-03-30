using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApi.Domain.Entities
{
    public class TransactionHistory
    {
        [Key]
        public int Id { get; set; }        
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; } = 0;
        [Required]
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }       
        public int WalletId { get; set; }
        public virtual Wallet Wallet { get; set; }

    }
}
