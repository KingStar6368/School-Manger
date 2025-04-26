using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Manager.Core.ViewModels.RawMaterial;
using School_Manager.Domain.Entities.Catalog.Operation;

namespace School_Manager.Core.Services.Interfaces
{
    public interface IRawMaterialService
    {
        Task<List<RawMaterialCombo>> GetRawMaterialCombosAsync();
        Task<List<RawMaterialGrid>> GetRawMaterialGridAsync();
        RawMaterialDetail GetOutgoingMaterialDetailAsync(int materialId,int warehouseId,int transferId);
        RawMaterialDetail GetIncomingMaterialDetailAsync(int materialId,int warehouseId,int IncomingId);
        bool GetIncomingMaterialsAsync(int IncomingId);
        RawMaterialCombo GetRawMaterialComboRow(int Id);
        RawMaterialDTO GetRawMaterialDTO(int Id);
        double CheckRemain(int materialId, int warehouseId,int TransferId);
        RawMaterial GetSingle(int MaterialId);
        bool Save(RawMaterialDTO rawMaterialDTO);
        bool Update(RawMaterialDTO rawMaterialDTO);
        bool HaveData(int Id);
        bool Delete(int rawMaterialId);
    }
}
