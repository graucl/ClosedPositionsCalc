using ClosedPositionsCalc.Application.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<FileController> _logger;
        private string _excelFile;

        public FileController(IConfiguration config, IIncomeAppService incomeAppService, ILogger<FileController> logger)
        {
            this._config = config;
            this._incomeAppService = incomeAppService;
            this._logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                _excelFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _config.GetSection("FileSettings:Name").Value);
                await SaveFile(file);
                await _incomeAppService.Calculations(_excelFile);

                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError("Error: " + ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogError("Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: " + ex.Message);
            }

            return Problem();
        }

        private async Task<bool> SaveFile(IFormFile file)
        {
            var success = false;
            _excelFile = null;

            try
            {
                if (_excelFile == null)
                {
                    throw new FileNotFoundException("File path not found");
                }

                var path = _excelFile;

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: " + ex.Message);
            }

            return success;
        }
    }
}
