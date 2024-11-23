using API.Clinic.Enterprise;
using Library.Clinic.DTO;
using Library.Clinic.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhysicianController : ControllerBase
    {
        private readonly ILogger<PhysicianController> _logger;

        public PhysicianController(ILogger<PhysicianController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<PhysicianDTO> Get()
        {
            return new PhysicianEC().Physicians;
        }

        [HttpGet("{id}")]
        public PhysicianDTO? GetById(int id)
        {
            return new PhysicianEC().GetById(id);
        }

        [HttpDelete("{id}")]
        public PhysicianDTO? Delete(int id)
        {
            return new PhysicianEC().Delete(id);
        }

        [HttpPost]
        public Physician? AddOrUpdate([FromBody] PhysicianDTO? physician)
        {
            return new PhysicianEC().AddOrUpdate(physician);
        }
    }
}
