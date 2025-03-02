﻿
using Homeworkfour.API.Filters;
using Homeworkfour.Business.DTOs;
using Homeworkfour.Bussiness.Abstract;
using Homeworkfour.Domain.Entities;
using Homeworkfour.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Homeworkfour.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService companyService;

        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        //[HttpGet]
        //[ServiceFilter(typeof(ActionFilterExample))]
        //public IActionResult GetData()
        //{
        //    return Ok(new { data = "Veriler Yüklendi"});
        //}

        [HttpGet]
        [Log]
        public IActionResult Log()
        {
            return NoContent();
        }

        /// <summary>
        /// Tüm şirket bilgilerini getirir.
        /// </summary>
        /// <returns></returns>
        [Route("Compaines")]
        [HttpGet]
        public IActionResult Get()
        {
            var companies = companyService.GetAllCompany().Select(x=> new CompanyDto
            {
                Name = x.Name,
                Address = x.Address,
                City = x.City,
                Country = x.Country,
                Description = x.Description,
                Location = x.Location,
                Phone = x.Phone               
            });
            return Ok(new CompanyResponse { Data = companies, Success = true });
        }

        /// <summary>
        /// Şirket ekleme işlemi yapar
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("AddCompany")]
        [HttpPost]
        public IActionResult Post([FromBody] CompanyDto model)
        {
            companyService.AddCompany(new Company
            {
                Address = model.Address,
                City = model.City,
                Description = model.Description,
                CreatedBy = "SAMET",
                CreatedAt = System.DateTime.Now,
                IsDeleted = false,
                Name = model.Name.ToUpper(),
                Country = model.Country,
                Phone = model.Phone,
                Location = model.Location,
            });
            return Ok(
                new CompanyResponse
                {
                    Data = "İşleminiz Başarıyla Tamamlandı",
                    Success = true
                });
        }
        [Route("DeleteCompany")]
        [HttpPost]
        public IActionResult Delete([FromBody] Company model)
        {
            companyService.DeleteCompany(model);

            return Ok(
                new CompanyResponse
                {
                    Data = "İşleminiz Başarıyla Tamamlandı",
                    Success = true
                });
        }

        [Route("UpdateCompany")]
        [HttpPost]
        public IActionResult Update([FromBody] Company model)
        {
            companyService.UpdateCompany(model);

            return Ok(
                new CompanyResponse
                {
                    Data = "İşleminiz Başarıyla Tamamlandı",
                    Success = true
                });
        }
    }
}
