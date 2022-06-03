using Microsoft.AspNetCore.Mvc;
using Roebi.Common.UnitOfWork;
using Roebi.Auth;
using Roebi.PatientManagment.Domain;
using Roebi.UserManagment.Domain;
using Roebi.PatientManagment.Application.Dto;
using AutoMapper;
using Roebi.LogManagment.Domain;
using System.Text.Json;

namespace Roebi.PatientManagment.Api
{
    [Authorize(Role.Admin, Role.User)]
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public PatientController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
        public IActionResult Post([FromBody] AddPatientDto patientDto)
        {
            User? user = HttpContext.Items["User"] as User;
            var patient = new Patient(patientDto.LastName, patientDto.Firstname, patientDto.EntryStamp, patientDto.CaseHistory);
            patient.Room = _unitOfWork.Room.GetById(patientDto.Room);
            _unitOfWork.Log.Add(new Log($"User: {user?.Username} created patient: {JsonSerializer.Serialize<Patient>(patient)}"));
            _unitOfWork.Patient.Add(patient);
            return Ok(_unitOfWork.Save());
        }

        [HttpPut]
        public IActionResult Put(UpdatePatientDto patientDto)
        {
            User? user = HttpContext.Items["User"] as User;
            var patient = new Patient(patientDto.Id, patientDto.LastName, patientDto.Firstname, patientDto.EntryStamp, patientDto.ExitStamp, patientDto.CaseHistory);
            patient.Room = _unitOfWork.Room.GetById(patientDto.Room);
            _unitOfWork.Log.Add(new Log($"User: {user?.Username} updated patient {patient.Id} to {JsonSerializer.Serialize<Patient>(patient)}"));
            _unitOfWork.Patient.Update(patient);
            return Ok(_unitOfWork.Save());
        }
    }
}
