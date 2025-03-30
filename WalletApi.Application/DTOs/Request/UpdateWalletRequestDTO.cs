using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApi.Application.DTOs.Request
{
    public class UpdateWalletRequestDTO
    {
        public int Id { get; set; }
        public string? DocumentId { get; set; }
        public string? Name { get; set; }       
        public decimal Balance { get; set; } = 0;
    }
}
