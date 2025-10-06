using EFModel;
using EFModel.Models;
using Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Server.Services
{
    public class MachineVirtuelleService
    {
        private readonly EClassRoomDbContext _db;

        public MachineVirtuelleService(EClassRoomDbContext db)
        {
            _db = db;
        }

        public async Task<List<MachineVirtuelleDto>> GetAllAsync()
        {
            return await _db.MachinesVirtuelles
                .Select(vm => new MachineVirtuelleDto
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    OwnerId = vm.OwnerId,
                    VmOsType = vm.VmOsType.ToString(),
                    VmType = vm.VmType.ToString(),
                    VmMachineId = vm.VmMachineId,
                    VmISO = vm.VmISO,
                    Sku = vm.Sku,
                    Offer = vm.Offer,
                    Version = vm.Version,
                    RdpInfo = vm.RdpInfo,
                    PublicIp = vm.PublicIp,
                    Status = vm.Status.ToString(),
                    NomMarketing = vm.NomMarketing
                })
                .ToListAsync();
        }

        public async Task<MachineVirtuelleDto> GetByIdAsync(int id)
        {
            var vm = await _db.MachinesVirtuelles.FindAsync(id);
            if (vm == null) return null;
            return new MachineVirtuelleDto
            {
                Id = vm.Id,
                Name = vm.Name,
                OwnerId = vm.OwnerId,
                VmOsType = vm.VmOsType.ToString(),
                VmType = vm.VmType.ToString(),
                VmMachineId = vm.VmMachineId,
                VmISO = vm.VmISO,
                Sku = vm.Sku,
                Offer = vm.Offer,
                Version = vm.Version,
                RdpInfo = vm.RdpInfo,
                PublicIp = vm.PublicIp,
                Status = vm.Status.ToString(),
                NomMarketing = vm.NomMarketing
            };
        }

        public async Task AddAsync(MachineVirtuelleDto dto)
        {
            var vm = new MachineVirtuelle
            {
                Name = dto.Name,
                OwnerId = dto.OwnerId,
                VmOsType = Enum.TryParse<VmOsType>(dto.VmOsType, out var osType) ? osType : VmOsType.Windows,
                VmType = Enum.TryParse<VmType>(dto.VmType, out var vmType) ? vmType : VmType.Standard_B2s,
                VmMachineId = dto.VmMachineId,
                VmISO = dto.VmISO,
                Sku = dto.Sku,
                Offer = dto.Offer,
                Version = dto.Version,
                RdpInfo = dto.RdpInfo,
                PublicIp = dto.PublicIp,
                Status = Enum.TryParse<VmStatus>(dto.Status, out var status) ? status : VmStatus.NotCreated,
                NomMarketing = dto.NomMarketing
            };
            _db.MachinesVirtuelles.Add(vm);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, MachineVirtuelleDto dto)
        {
            var vm = await _db.MachinesVirtuelles.FindAsync(id);
            if (vm == null) return;
            vm.Name = dto.Name;
            vm.OwnerId = dto.OwnerId;
            if (Enum.TryParse<VmOsType>(dto.VmOsType, out var osType))
                vm.VmOsType = osType;
            if (Enum.TryParse<VmType>(dto.VmType, out var vmType))
                vm.VmType = vmType;
            vm.VmMachineId = dto.VmMachineId;
            vm.VmISO = dto.VmISO;
            vm.Sku = dto.Sku;
            vm.Offer = dto.Offer;
            vm.Version = dto.Version;
            vm.RdpInfo = dto.RdpInfo;
            vm.PublicIp = dto.PublicIp;
            if (Enum.TryParse<VmStatus>(dto.Status, out var status))
                vm.Status = status;
            vm.NomMarketing = dto.NomMarketing;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var vm = await _db.MachinesVirtuelles.FindAsync(id);
            if (vm == null) return;
            _db.MachinesVirtuelles.Remove(vm);
            await _db.SaveChangesAsync();
        }
    }
}
