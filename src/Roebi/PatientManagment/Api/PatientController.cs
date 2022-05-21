using Microsoft.AspNetCore.Mvc;
using Roebi.Common.UnitOfWork;
using Roebi.Auth;
using Roebi.PatientManagment.Domain;
using Roebi.UserManagment.Domain;

namespace Roebi.PatientManagment.Api
{
    [Authorize(Role.Admin, Role.User)]
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetAll()
        {
            return Ok(_unitOfWork.Patient.GetAll());
        }

        [HttpGet("{id:int}")]
        public ActionResult<Patient> GetById(int id)
        {
            return Ok(_unitOfWork.Patient.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Patient patient)
        {
            _unitOfWork.Patient.Add(patient);
            return Ok(_unitOfWork.Save());
        }

        [HttpPut]
        public IActionResult Put(Patient patient)
        {
            _unitOfWork.Patient.Update(patient);
            return Ok(_unitOfWork.Save());
        }
    }
}
