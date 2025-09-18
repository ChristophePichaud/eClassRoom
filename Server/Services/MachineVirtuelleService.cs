using EFModel;
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
                    TypeOS = vm.TypeOS,
                    TypeVM = vm.TypeVM,
                    Sku = vm.Sku,
                    Offer = vm.Offer,
                    Version = vm.Version,
                    DiskISO = vm.DiskISO,
                    NomMarketing = vm.NomMarketingVM
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
                TypeOS = vm.TypeOS,
                TypeVM = vm.TypeVM,
                Sku = vm.Sku,
                Offer = vm.Offer,
                Version = vm.Version,
                DiskISO = vm.DiskISO,
                NomMarketing = vm.NomMarketingVM
            };
        }

        public async Task AddAsync(MachineVirtuelleDto dto)
        {
            var vm = new MachineVirtuelle
            {
                Name = dto.Name,
                TypeOS = dto.TypeOS,
                TypeVM = dto.TypeVM,
                Sku = dto.Sku,
                Offer = dto.Offer,
                Version = dto.Version,
                DiskISO = dto.DiskISO,
                NomMarketingVM = dto.NomMarketing
            };
            _db.MachinesVirtuelles.Add(vm);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, MachineVirtuelleDto dto)
        {
            var vm = await _db.MachinesVirtuelles.FindAsync(id);
            if (vm == null) return;
            vm.Name = dto.Name;
            vm.TypeOS = dto.TypeOS;
            vm.TypeVM = dto.TypeVM;
            vm.Sku = dto.Sku;
            vm.Offer = dto.Offer;
            vm.Version = dto.Version;
            vm.DiskISO = dto.DiskISO;
            vm.NomMarketingVM = dto.NomMarketing;
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
