using System;
using System.Collections.Generic;
using System.Text;
using Demo.Common;

namespace Demo.Business
{
    public class CheckBusiness : ICheckBusiness
    {
        public string ConvertAmountToWords(decimal amount)
        {
            return amount.ToWords();
        }
    }
}
