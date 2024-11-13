using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters")]
        public string? Symbol { get; set; } = null;
        
        [MaxLength(10, ErrorMessage = "Company name cannot be over 10 characters")]
        public string? CompanyName { get; set; } = null;
        
        [Range(1, 1000000000)]
        public decimal? Purchase { get; set; } = null;
        
        [Range(0.001, 100)]
        public decimal? LastDiv { get; set; } = null;
        
        [MaxLength(10, ErrorMessage = "Industry cannot be over 10 characters")]
        public string? Industry { get; set; } = null;
        
        [Range(1, 5000000000)]
        public long? MarketCap { get; set; } = null;
    }
}