using LocalCommerce.Services.Stores.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LocalCommerce.Services.Stores.BusinessLogic.DataAccess;

public interface IStoresWriteStore
{
    Task UpdateStore(int id, StoreDetails details);
    Task<int> CreateRecord(StoreDetails details);
}

public class StoresWriteStore : DbContext, IStoresWriteStore
{
    private DbSet<StoreDetailEntity> Stores { get; set; } = null!;

    public StoresWriteStore(DbContextOptions<StoresWriteStore> options) : base(options)
    {}

    public async Task<int> CreateRecord(StoreDetails details)
    {
        StoreDetailEntity newStore = new StoreDetailEntity()
        {
            Description = details.Description,
            Name = details.Name
        };

        var result = await Stores.AddAsync(newStore);
        await SaveChangesAsync();

        return result.Entity.Id ?? throw new ApplicationException("the record has not been inserted in the db");
    }

    public async Task UpdateStore(int id, StoreDetails details)
    {
        var store = await Stores.SingleAsync(a => a.Id == id);
        store.Description = details.Description;
        store.Name = details.Name;

        await SaveChangesAsync();
    }

    private class StoreDetailEntity
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}