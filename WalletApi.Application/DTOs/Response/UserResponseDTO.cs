using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApi.Application.DTOs.Response
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }       
        public string? Token { get; set; }
    }
}
