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
        private string excelFile;

        public FileController(IConfiguration config, IIncomeAppService incomeAppService)
        {
            this._config = config;
            this._incomeAppService = incomeAppService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {

            excelFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _config.GetSection("FileSettings:Name").Value);
            await SaveFile(file);

            _incomeAppService.Calculations(excelFile);

            return Ok();
        }

        private async Task<bool> SaveFile(IFormFile file)
        {
            var success = false;

            try
            {
                //var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                var filename = _config.GetSection("FileSettings:Name").Value;
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);

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

        //private async string GetFilePath(string path)
    }
}
