using Demo.Business;
using Demo.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoProject.Api
{
    [Route("api/Checks")]
    public class CheckController : Controller
    {
        private readonly ICheckBusiness checkBusiness;

        public CheckController(ICheckBusiness checkBusiness)
        {
            this.checkBusiness = checkBusiness;
        }

        [HttpPost]
        public ActionResult Post([FromBody] CheckInputModel checkInputModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var amountInWords = checkBusiness.ConvertAmountToWords(checkInputModel.Amount);

            var checkOutputModel = new CheckOutputModel();
            checkOutputModel.Amount = checkInputModel.Amount;
            checkOutputModel.CheckDate = checkInputModel.CheckDate;
            checkOutputModel.Payee = checkInputModel.Payee;
            checkOutputModel.Id = checkInputModel.Id;
            checkOutputModel.AmountInWords = amountInWords;


            return Ok(checkOutputModel);
        }
      
    }
}
