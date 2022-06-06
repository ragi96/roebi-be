using Microsoft.AspNetCore.Mvc;
using Roebi.Common.UnitOfWork;
using Roebi.Auth;
using Roebi.PatientManagment.Domain;
using Roebi.UserManagment.Domain;
using AutoMapper;
using Roebi.PatientManagment.Application.Dto;
using Roebi.LogManagment.Domain;
using System.Text.Json;

namespace Roebi.PatientManagment.Api
{
    [Authorize(Role.Admin, Role.User)]
    [ApiController]
    [Route("[controller]")]
    public class MedicationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        private static int TEN_MINUTES = 10 * 60 * 1000;

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

        [HttpGet("{id:int}/User")]
        public ActionResult<IEnumerable<Medication>> GetByUserId(int id)
        {
            return Ok(_unitOfWork.Medication.Find(medication => medication.Patient.Id == id).ToList());
        }

        [HttpGet("{id:int}")]
        public ActionResult<Medication> GetById(int id)
        {
            return Ok(_unitOfWork.Medication.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddMedicationDto medicationDto)
        {
            User? user = HttpContext.Items["User"] as User;
            Medication medication = _mapper.Map<Medication>(medicationDto);
            var MedicationsFuture = _unitOfWork.Medication.Find(med => med.TakingStamp > medication.TakingStamp && med.TakingStamp < medication.TakingStamp + TEN_MINUTES);
            var MedicationsPast = _unitOfWork.Medication.Find(med => med.TakingStamp < medication.TakingStamp && med.TakingStamp > medication.TakingStamp - TEN_MINUTES);

            if (MedicationsFuture.Count() <= 4 || MedicationsPast.Count() <= 4)
            {
                medication.Patient = _unitOfWork.Patient.GetById(medicationDto.Patient);
                medication.Medicine = _unitOfWork.Medicine.GetById(medicationDto.Medicine);
                _unitOfWork.Log.Add(new Log($"User: {user?.Username} created medication: {JsonSerializer.Serialize<Medication>(medication)}"));
                _unitOfWork.Medication.Add(medication);
                return Ok(_unitOfWork.Save());
            } else {
                return BadRequest(MedicationsFuture);
            }


        }

        [HttpPut]
        public IActionResult Put(UpdateMedicationDto medicationDto)
        {
            User? user = HttpContext.Items["User"] as User;
            Medication medication = _mapper.Map<Medication>(medicationDto);
            medication.Patient = _unitOfWork.Patient.GetById(medicationDto.Patient);
            medication.Medicine = _unitOfWork.Medicine.GetById(medicationDto.Medicine);
            _unitOfWork.Medication.Update(medication);
            _unitOfWork.Log.Add(new Log($"User: {user?.Username} updated medication {medication.Id} to {JsonSerializer.Serialize<Medication>(medication)}"));
            return Ok(_unitOfWork.Save());
        }
    }
}
