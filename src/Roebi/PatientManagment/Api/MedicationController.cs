using Microsoft.AspNetCore.Mvc;
using Roebi.Common.UnitOfWork;
using Roebi.Auth;
using Roebi.PatientManagment.Domain;
using Roebi.UserManagment.Domain;
using AutoMapper;
using Roebi.PatientManagment.Application.Dto;

namespace Roebi.PatientManagment.Api
{
    [Authorize(Role.Admin, Role.User)]
    [ApiController]
    [Route("[controller]")]
    public class MedicationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;


        public MedicationController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
        public IActionResult Post([FromBody] MedicationDto medicationDto)
        {
            Medication medication = _mapper.Map<Medication>(medicationDto);
            medication.Patient = _unitOfWork.Patient.GetById(medicationDto.Patient);
            medication.Medicine = _unitOfWork.Medicine.GetById(medicationDto.Medicine);
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
