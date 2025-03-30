using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApi.Application.DTOs.Response
{
    public class TransactionHistoryResponseDTO
    {
        public int Id { get; set; }
        public int WalletId { get; set; }
        public decimal Amount { get; set; } = 0;
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
