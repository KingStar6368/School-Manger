using AutoMapper;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Identity;
using School_Manager.Domain.Entities.Catalog.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Implemetations
{
    public class SMSLogService : ISMSLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SMSLogService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public bool CreateSMSLog(SMSLogDto smsLog)
        {
            var sl = _mapper.Map<SMSLog>(smsLog);
            _unitOfWork.GetRepository<SMSLog>().Add(sl);
            var res = _unitOfWork.SaveChanges();
            return res > 0;
        }

        public int GetBillCount(long UserId)
        {
            var ds = _unitOfWork.GetRepository<SMSLog>()
                .Query(x => x.UserId == UserId && x.type == Domain.Entities.Catalog.Enums.SMSType.BillInfo).Count();
            return ds;
        }

        public SMSLogDto GetDto(int id)
        {
            var ds = _unitOfWork.GetRepository<SMSLog>().GetById(id);
            return _mapper.Map<SMSLogDto>(ds);
        }

        public int GetInfoCount(long UserId)
        {
            var ds = _unitOfWork.GetRepository<SMSLog>()
               .Query(x => x.UserId == UserId && x.type == Domain.Entities.Catalog.Enums.SMSType.Info).Count();
            return ds;
        }

        public SMSLogDto GetLastWarning(long UserId)
        {
            var ds = _unitOfWork.GetRepository<SMSLog>().Query(x => x.UserId == UserId).OrderBy(x=>x.SMSTime).LastOrDefault();
            return _mapper.Map<SMSLogDto>(ds);
        }

        public async Task<List<SMSLogDto>> GetSMSLog()
        {
            var ds = await _unitOfWork.GetRepository<SMSLog>().GetAllAsync();
            return _mapper.Map<List<SMSLogDto>>(ds);
        }

        public int GetWarningCount(long UserId)
        {
            var ds = _unitOfWork.GetRepository<SMSLog>()
               .Query(x => x.UserId == UserId && x.type == Domain.Entities.Catalog.Enums.SMSType.Warnning).Count();
            return ds;
        }
    }
}
