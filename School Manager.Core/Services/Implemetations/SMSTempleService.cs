using AutoMapper;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace School_Manager.Core.Services.Implemetations
{
    public class SMSTempleService : ISMSTempleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SMSTempleService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public bool DeleteTemple(long Id)
        {
            var row = _unitOfWork.GetRepository<SMSTemple>().GetById(Id);
            if (row == null) { return false; }
            _unitOfWork.GetRepository<SMSTemple>().Remove(row);
            return _unitOfWork.SaveChanges() > 0;
        }

        public SMSTempleDto GetTemple(long Id)
        {
            var row = _unitOfWork.GetRepository<SMSTemple>().GetById(Id);
            if (row == null)
            {
                return null;
            }
            return _mapper.Map<SMSTempleDto>(row);
        }

        public SMSTempleDto GetTempleByName(string Name)
        {
            var row = _unitOfWork.GetRepository<SMSTemple>().Query(x=>x.Name == Name).FirstOrDefault();
            if (row == null)
            {
                return null;
            }
            return _mapper.Map<SMSTempleDto>(row);
        }

        public SMSTempleDto GetTempleByTempleID(long TempleId)
        {
            var row = _unitOfWork.GetRepository<SMSTemple>().Query(x => x.TempleId == TempleId).FirstOrDefault();
            if (row == null)
            {
                return null;
            }
            return _mapper.Map<SMSTempleDto>(row);
        }

        public async Task<List<SMSTempleDto>> GetTemples()
        {
            var row = await _unitOfWork.GetRepository<SMSTemple>().GetAllAsync();
            if (row == null)
            {
                return null;
            }
            return _mapper.Map<List<SMSTempleDto>>(row);
        }

        public bool UpdateTemple(SMSTempleUpdateDto updateDto)
        {
            var row = _unitOfWork.GetRepository<SMSTemple>().Query(x => x.Id == updateDto.Id).FirstOrDefault();
            if (row == null)
            {
                return false;
            }
            _mapper.Map(updateDto,row);
            _unitOfWork.GetRepository<SMSTemple>().Update(row);
            return _unitOfWork.SaveChanges() > 0;
        }
    }
}
