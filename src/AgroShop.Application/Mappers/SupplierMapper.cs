using AgroShop.Application.Dto.SupplierDto;
using AgroShop.Core.Entities;

namespace AgroShop.Application.Mappers
{
    public static class SupplierMapper
    {
        public static SupplierDto ToDto(this Supplier supplier)
        {
            return new SupplierDto
            {
                Id = supplier.Id,
                Name = supplier.Name.Value
            };
        }

        public static IEnumerable<SupplierDto> ToDto(this IEnumerable<Supplier> suppliers)
        {
            return suppliers.Select(s => s.ToDto());
        }
    }
}
