using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kyc.Dtos;
using Kyc.Entities;

namespace Kyc.MappingProfiles
{
    public class Aadharprofile : Profile
    {
        public Aadharprofile()
        {
            CreateMap<Request,Aadhar>()
                .ForMember(
                    dest => dest.Number,
                    opt => opt.MapFrom(src => src.Rawtext)
                );
            
        }
        
    }
}