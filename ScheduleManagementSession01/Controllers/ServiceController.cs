using Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Core;

namespace ScheduleManagementSession01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceservice;
        private readonly IServiceService _serviceservice1;
        public ServiceController(IServiceService serviceService, IServiceService serviceService1)
        {
            _serviceservice = serviceService;
            _serviceservice1 = serviceService1;
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public IActionResult Post([FromBody] ServiceCreateModel model)
        {
            var result = _serviceservice.Add(model);
            if (result.Succeed) return Ok(result.Data);
            return BadRequest(result.ErrorMessage);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public IActionResult Get()
        {
            var result = _serviceservice.GetAll();
            if (result.Succeed) return Ok(result.Data);
            return BadRequest(result.ErrorMessage);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _serviceservice.Get(id);
            if (result.Succeed) return Ok(result.Data);
            return BadRequest(result.ErrorMessage);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteById(Guid id)
        {
            var result = _serviceservice.Delete(id);
            if (result.Succeed) return Ok(result.Data);
            return BadRequest(result.ErrorMessage);
        }
        [HttpPut]
        public IActionResult Update(ServiceUpdateModel model)
        {
            var result = _serviceservice.Update(model);
            if (result.Succeed) return Ok(result.Data);
            return BadRequest(result.ErrorMessage);
        }
        [HttpGet("TestDI")]
        public IActionResult TestDI()
        {
            var result = "First Instance " + _serviceservice.TestDI() + "\n";
            var result2 = "Second Instance " + _serviceservice1.TestDI() + "\n";
            var data = result + result2;
            return Ok(data);

        }
    }
}
