using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApi.Application.DTOs.Response
{
    public class TransactionResponseDTO
    {
        public bool isSuccess { get; set; }
        public TransactionHistoryResponseDTO transactionHistoryResponse { get; set; }
    }
}
