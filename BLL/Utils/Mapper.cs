using AutoMapper;
using BOL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Utils
{
    public class Mapper : Profile
    {
        public Mapper() 
        { 
            CreateMap<CustomerResquestDTO, Customer>().ReverseMap();
        }
    }
}
