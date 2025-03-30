using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApi.Application.DTOs.Response
{
    public class WalletResponseDTO
    {
        public int Id { get; set; }        
        public string? Name { get; set; }
        public decimal Balance { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
