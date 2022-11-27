using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestfulWebApi.Services;

namespace RestfulWebApi.Controllers
{
	[ApiController]
    [Route("api/companies")]  // 保证url不变
    //[Route("api/[controller]")]
    public class CompaniesController:ControllerBase
	{
		private readonly ICompanyRepository _companyRepository;
		public CompaniesController(ICompanyRepository companyRepository)
		{
			_companyRepository = companyRepository ??
				throw new ArgumentNullException(nameof(companyRepository));
		}

		[HttpGet]
		public async Task<IActionResult> GetCompanies()
		{
			var companies = await _companyRepository.GetCompaniesAsync();
			//return new JsonResult(companies);
			return Ok(companies);
		}

		[HttpGet("{companyId}")]
		public async Task<IActionResult> GetCompany(Guid companyId)
		{
			// 并发不好
			//var exist = await _companyRepository.CompanyExistsAsync(companyId);
			//if (!exist)
			//	return NotFound();
			var company = await _companyRepository.GetCompanyAsync(companyId);
			if (company == null)
				return NotFound();
			return Ok(company);
		}

    }
}

