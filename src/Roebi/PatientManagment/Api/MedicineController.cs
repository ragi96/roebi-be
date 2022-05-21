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
    public class MedicineController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicineController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Medicine>> GetAll()
        {
            return Ok(_unitOfWork.Medicine.GetAll());
        }

        [HttpGet("{id:int}")]
        public ActionResult<Medicine> GetById(int id)
        {
            return Ok(_unitOfWork.Medicine.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Medicine medicine)
        {
            _unitOfWork.Medicine.Add(medicine);
            return Ok(_unitOfWork.Save());
        }

        [HttpPut]
        public IActionResult Put(Medicine medicine)
        {
            _unitOfWork.Medicine.Update(medicine);
            return Ok(_unitOfWork.Save());
        }
    }
}
