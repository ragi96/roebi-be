using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roebi.Auth;
using Roebi.Common.UnitOfWork;
using Roebi.RoboterManagment.Application.Dto;
using Roebi.RoboterManagment.Domain;
using Roebi.UserManagment.Domain;

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
            if (!medications.Any())
            {
                return NotFound();
            }
            else {
                foreach (var medication in medications) { 
                    medication.Job = job;
                    _unitOfWork.Medication.Update(medication);
                }
                _unitOfWork.Job.Add(job);
                _unitOfWork.Save();
                CreatedJob createdJob = _mapper.Map<CreatedJob>(job);
                createdJob.Medication = medications;
                return Ok(createdJob);
            }  
        }

        [Authorize(Role.Admin, Role.Roboter)]
        [HttpGet("{id:int}/state-started")]
        public IActionResult StateStarted(int id)
        {
            var job = _unitOfWork.Job.GetById(id);
            job.State = JobState.Started;
            _unitOfWork.Job.Update(job);
            return Ok();
        }

        [Authorize(Role.Admin, Role.Roboter)]
        [HttpGet("{id:int}/state-finished")]
        public IActionResult StateFinished(int id)
        {
            var job = _unitOfWork.Job.GetById(id);
            job.State = JobState.Finished;
            _unitOfWork.Job.Update(job);
            return Ok();
        }

        [Authorize(Role.Admin, Role.Roboter)]
        [HttpGet("{id:int}/state-failed")]
        public IActionResult StateFailed(int id)
        {
            var job = _unitOfWork.Job.GetById(id);
            job.State = JobState.Failed;
            _unitOfWork.Job.Update(job);
            return Ok();
        }
    }
}
