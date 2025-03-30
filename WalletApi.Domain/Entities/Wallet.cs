using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApi.Domain.Entities
{
    public class Wallet
    {
        [Key]        
        public int Id { get; set; }
        public string? DocumentId { get; set; }
        [Required(ErrorMessage = "The owner name  {0} is required")]
        [MinLength(1, ErrorMessage = "The field {0} must have at least {1} character")]        
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<TransactionHistory> TransactionsHistory { get; set; } = new List<TransactionHistory>();
    }

}
