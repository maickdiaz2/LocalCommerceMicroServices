namespace LocalCommerce.Services.Stores.Dtos;

public record CreateStoreRequest(StoreDetails Details);

public record StoreDetails(string? Name, string? Description);

public record FullStoreResponse(int? Id, StoreDetails? Details);

public record StoreUpdated(int StoreId, StoreDetails Details);

public record StoreCreated(int Id, CreateStoreRequest StoreRequest);