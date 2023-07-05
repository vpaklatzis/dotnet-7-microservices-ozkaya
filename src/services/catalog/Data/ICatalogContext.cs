using MongoDB.Driver;

public interface ICatalogContext {
    
    IMongoCollection<Product> Products { get; }
}