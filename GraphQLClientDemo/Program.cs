using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQLClientDemo;

//HttpClientHandler clientHandler = new HttpClientHandler();
//clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


//var graphQLOptions = new GraphQLHttpClientOptions
//{
//    EndPoint = new Uri("http://localhost:5160/graphql", UriKind.Absolute),
//};
//var graphQLClient = new GraphQLHttpClient(graphQLOptions, new NewtonsoftJsonSerializer());

//var msg = new GraphQLRequest
//{
//    Query = @"
//    {
//        products {
//                id,
//                name,
//                quantity
//         }
//    }"
//};
//var graphQLResponse = await graphQLClient.SendQueryAsync<ProductsResponse>(msg).ConfigureAwait(false);
//foreach (var product in graphQLResponse.Data.products)
//{
//    Console.WriteLine($"{product.Name}, {product.Quantity}");
//}




//creating a graphQL client which wil be used to send http request to the GraphQL API endpoint  
var client = new GraphQLHttpClient("http://localhost:5160/graphql", new NewtonsoftJsonSerializer());

var query = new GraphQLRequest
{
    Query = @"
    {
        productsList{
                id, 
                name,
                quantity
              }
    }"

};



//sending the request to the graphql api to return list of products
var repsonse = await client.SendQueryAsync<ProductsResponse>(query);

foreach (var product in repsonse.Data.productsList)
{
    Console.WriteLine($"{product.Name}, {product.Quantity}");
}

//---------------------------------Query to return Product by ID-------------------------------------------------------
query = new GraphQLRequest
{
    Query = @"
        query firstProduct($id: Int!) {
        productById(id:$id) {
        id,
        name,
        quantity
      }
    }",
    OperationName = "firstProduct",
    Variables = new {id = 2}

};

var singleResponse = await client.SendQueryAsync<ProductResponse>(query);
Console.WriteLine(singleResponse.Data.productById);


//------------------------------------Mutation-------------------------------------------------------------------

query = new GraphQLRequest
{
    Query = @"
        mutation AddProduct($prod: ProductInput){
          createProduct(product: $prod){
            id,
            name,
            quantity
          }
    }",
    OperationName = "AddProduct",
    Variables = new { prod = new { id = 5, Name = "Burst Speaker", Quantity = 7 } }
};

var mutationResponse = await client.SendMutationAsync<CreateProductResponse>(query);
Console.WriteLine(mutationResponse.Data.createProduct);