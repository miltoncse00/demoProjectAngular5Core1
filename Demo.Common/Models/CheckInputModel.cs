using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Common.Models
{
    public class CheckInputModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Payee { get; set; }
        [Required]
        [Range(.01, 99999999999999.99, ErrorMessage = "Amount must be numeric, greater than 0, and smaller than 10000000000000")]
        public decimal Amount { get; set; }
        [Required]
        public DateTime CheckDate { get; set; }
    }
}
