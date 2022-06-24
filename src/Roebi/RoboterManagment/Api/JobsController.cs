using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roebi.Auth;
using Roebi.Common.UnitOfWork;
using Roebi.LogManagment.Domain;
using Roebi.PatientManagment.Domain;
using Roebi.RoboterManagment.Application.Dto;
using Roebi.RoboterManagment.Domain;
using Roebi.UserManagment.Domain;
using System.Text.Json;

namespace Roebi.RoboterManagment.Api
{
    [Route("[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public JobsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize(Role.Admin, Role.Roboter)]
        [HttpGet]
        public ActionResult<CreatedJob> CreateJob()
        {
            var job = new Job();
            var medications = _unitOfWork.Medication.FindForJob(job.Id);
            User? user = HttpContext.Items["User"] as User;
            if (!medications.Any())
            {
                return NotFound();
            }
            else {
                _unitOfWork.Job.Add(job);
                foreach (var medication in medications) {
                    medication.Job = job;
                    _unitOfWork.Log.Add(new Log($"User: {user?.Username} update medicaton {medication.Id} to {JsonSerializer.Serialize<Medication>(medication)}"));
                    _unitOfWork.Medication.Update(medication);
                }
                CreatedJob createdJob = _mapper.Map<CreatedJob>(job);
                createdJob.Medication = medications.ToList();
                _unitOfWork.Save();
                return Ok(createdJob);
            }  
        }

        [Authorize(Role.Admin, Role.Roboter)]
        [HttpGet("{id:int}/state-started")]
        public IActionResult StateStarted(int id)
        {
            User? user = HttpContext.Items["User"] as User;
            var job = _unitOfWork.Job.GetById(id);
            job.State = JobState.Started;
            _unitOfWork.Job.Update(job);
            _unitOfWork.Log.Add(new Log($"User: {user?.Username} updated Job {id} to {JsonSerializer.Serialize<Job>(job)}"));
            _unitOfWork.Save();
            return Ok();
        }

        [Authorize(Role.Admin, Role.Roboter)]
        [HttpGet("{id:int}/state-finished")]
        public IActionResult StateFinished(int id)
        {
            User? user = HttpContext.Items["User"] as User;
            var job = _unitOfWork.Job.GetById(id);
            job.State = JobState.Finished;
            _unitOfWork.Job.Update(job);
            _unitOfWork.Log.Add(new Log($"User: {user?.Username} updated Job {id} to {JsonSerializer.Serialize<Job>(job)}"));
            _unitOfWork.Save();
            return Ok();
        }

        [Authorize(Role.Admin, Role.Roboter)]
        [HttpGet("{id:int}/state-failed")]
        public IActionResult StateFailed(int id)
        {
            User? user = HttpContext.Items["User"] as User;
            var job = _unitOfWork.Job.GetById(id);
            job.State = JobState.Failed;
            _unitOfWork.Job.Update(job);
            _unitOfWork.Log.Add(new Log($"User: {user?.Username} updated Job {id} to {JsonSerializer.Serialize<Job>(job)}"));
            _unitOfWork.Save();
            return Ok();
        }
    }
}
