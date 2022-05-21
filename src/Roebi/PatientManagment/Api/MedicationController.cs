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
    public class MedicationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Medication>> GetAll()
        {
            return Ok(_unitOfWork.Medication.GetAll());
        }

        [HttpGet("{id:int}")]
        public ActionResult<Medication> GetById(int id)
        {
            return Ok(_unitOfWork.Medication.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Medication medication)
        {
            _unitOfWork.Medication.Add(medication);
            return Ok(_unitOfWork.Save());
        }

        [HttpPut]
        public IActionResult Put(Medication medication)
        {
            _unitOfWork.Medication.Update(medication);
            return Ok(_unitOfWork.Save());
        }
    }
}
