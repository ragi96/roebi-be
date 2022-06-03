using AutoMapper;

using Roebi.PatientManagment.Application.Dto;
using Roebi.PatientManagment.Domain;
using Roebi.RoboterManagment.Application.Dto;
using Roebi.RoboterManagment.Domain;

namespace Roebi.Common.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Medication, UpdateMedicationDto>().ReverseMap();
            CreateMap<Medication, AddMedicationDto>().ReverseMap();
            CreateMap<Patient, int>().ConstructUsing(source => source.Id).ReverseMap();
            CreateMap<Medicine, int>().ConstructUsing(source => source.Id).ReverseMap();
            CreateMap<Job, CreatedJob>().ReverseMap();
            CreateMap<Patient, UpdateMedicationDto>().ReverseMap();
            CreateMap<AddPatientDto, Patient>().ReverseMap();

        }
    }
}
