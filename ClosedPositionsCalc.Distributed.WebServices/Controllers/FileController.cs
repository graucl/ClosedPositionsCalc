using ClosedPositionsCalc.Application.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ClosedPositionsCalc.Distributed.WebServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IIncomeAppService _incomeAppService;
        private string _excelFile;

        public FileController(IConfiguration config, IIncomeAppService incomeAppService)
        {
            this._config = config;
            this._incomeAppService = incomeAppService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            _excelFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _config.GetSection("FileSettings:Name").Value);
            await SaveFile(file);
            await _incomeAppService.Calculations(_excelFile);

            return Ok();
        }

        private async Task<bool> SaveFile(IFormFile file)
        {
            var success = false;

            try
            {
                var path = _excelFile;

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return success = true;
            }
            catch(Exception ex)
            {

            }

            return success;
        }
    }
}
