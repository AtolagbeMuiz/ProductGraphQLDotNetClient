namespace GraphQLClientDemo;

public record Product(int Id, string Name, int Quantity);

public record CreateProductResponse(Product createProduct);
public record ProductResponse(Product productById);
public record ProductsResponse(Product[] productsList);