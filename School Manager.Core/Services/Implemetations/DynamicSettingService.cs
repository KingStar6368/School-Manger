using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Implemetations
{
    public class DynamicSettingService : ISettingService
    {
        private readonly Dictionary<string, (string Value, string Type)> _settings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DynamicSettingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _settings = LoadSettingsFromDatabase();
        }

        private Dictionary<string, (string, string)> LoadSettingsFromDatabase()
        {
            return _unitOfWork.GetRepository<Setting>().GetAll().ToDictionary(x => x.Key, x => (x.Value, x.Type));
        }

        public string Get(string key)
        {
            if (_settings.TryGetValue(key, out var entry) && entry.Type == "Text")
                return entry.Value;

            return null;
        }

        public byte[] GetImage(string key)
        {
            if (_settings.TryGetValue(key, out var entry) && entry.Type == "Image")
            {
                return Convert.FromBase64String(entry.Value);
            }

            return null;
        }

        public bool SaveSetting(SettingDto dto)
        {
            var mainSetting = _unitOfWork.GetRepository<Setting>().Query(x => x.Key == dto.Key).FirstOrDefault();
            dto.Type = "Text";
            if (mainSetting == null)
            {
                var setting = _mapper.Map<Setting>(dto);
                _unitOfWork.GetRepository<Setting>().Add(setting);
            }
            else
            {
                _mapper.Map(dto, mainSetting);
                _unitOfWork.GetRepository<Setting>().Update(mainSetting);
            }
            return _unitOfWork.SaveChanges() > 0;
        }
        public async Task<bool> SaveSettingImage(SettingDto dto)
        {
            var valueBase64 = await ConvertImageToBase64Async(dto.File);
            dto.Value = valueBase64;
            dto.Type = "Image";
            var mainSetting = _unitOfWork.GetRepository<Setting>().Query(x => x.Key == dto.Key).FirstOrDefault();
            if (mainSetting == null)
            {
                var setting = _mapper.Map<Setting>(dto);
                _unitOfWork.GetRepository<Setting>().Add(setting);
            }
            else
            {
                _mapper.Map(dto, mainSetting);
                _unitOfWork.GetRepository<Setting>().Update(mainSetting);
            }
            return _unitOfWork.SaveChanges() > 0;
        }
        public async Task<string> ConvertImageToBase64Async(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return null;

            using (var ms = new MemoryStream())
            {
                await imageFile.CopyToAsync(ms);
                byte[] imageBytes = ms.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }
    }

}
