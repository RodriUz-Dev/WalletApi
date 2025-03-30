using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApi.Application.DTOs.Request
{
    public class WalletRequestDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }        
        public decimal Balance { get; set; } = 0;
        
    }
}
