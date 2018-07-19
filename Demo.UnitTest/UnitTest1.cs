using Demo.Business;
using Demo.Common.Models;
using DemoProject.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Demo.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        CheckController controller;

        [TestInitialize]
        public void Intialize()
        {
            var checkBusiness = new CheckBusiness();
            controller = new CheckController(checkBusiness);
        }

        [TestMethod]

        public void TestMethodInputValidation()
        {
            var checkInput = new CheckInputModel() { Payee= string.Empty,Amount=1, CheckDate=DateTime.Now};
            var validationResult= ValidateModel(checkInput);
            validationResult.Count.Should().Be(1);
            validationResult.First().ErrorMessage.Should().ContainAny("Payee");
        }

        [TestMethod]

        public void TestMethodInputValidation2()
        {
            var checkInput = new CheckInputModel() { Payee = "Test", Amount = 0, CheckDate = DateTime.Now };
            var validationResult = ValidateModel(checkInput);
            validationResult.Count.Should().Be(1);
            validationResult.First().ErrorMessage.Should().ContainAny("Amount");
        }

        [TestMethod]
        public void TestMethodOutputValidation1()
        {
            var checkInput = new CheckInputModel() { Amount=1};
            CheckOutputModel output = ((ObjectResult)controller.Post(checkInput)).Value as CheckOutputModel;
            output.AmountInWords.Replace(" ","").ToUpper().Replace("ONLY","").Should().Be("ONEDOLLARS");
        }

        [TestMethod]
        public void TestMethodOutputValidation2()
        {
            var checkInput = new CheckInputModel() { Amount = 12 };
            CheckOutputModel output = ((ObjectResult)controller.Post(checkInput)).Value as CheckOutputModel;
            output.AmountInWords.Replace(" ", "").ToUpper().Replace("ONLY", "").Should().Be("TWELVEDOLLARS");
        }

        [TestMethod]
        public void TestMethodOutputValidation3()
        {
            var checkInput = new CheckInputModel() { Amount = 100.50M };
            CheckOutputModel output = ((ObjectResult)controller.Post(checkInput)).Value as CheckOutputModel;
            output.AmountInWords.Replace(" ", "").ToUpper().Replace("ONLY", "").Should().Be("ONEHUNDREDDOLLARSANDFIFTYCENTS");
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }


    }
}
