using CourseWorkDB.Graphql.Mutation.Product.ProductCrudTypes;
using CourseWorkDB.Repositories;
using CourseWorkDB.ViewModel.Product;
using GraphQL;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Query.Product
{
    public class ProductQuery:ObjectGraphType
    {
        public ProductQuery(IProductRepository productRepository) 
        {
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<CategoryGraphType>>>>("get_categories")
                .ResolveAsync(async context =>
                {
                    return await productRepository.GetCategoriesAsync();
                });
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<CreatorGraphType>>>>("get_creators")
                .ResolveAsync(async context =>
                {
                    return await productRepository.GetCreatorsAsync();
                });

            Field<NonNullGraphType<ProductPaginationGraphType>>("get_products")
                .Argument<NonNullGraphType<ProductSearchInputGraphType>>("searchProduct")
                .ResolveAsync(async context =>
                {
                    var searchProduct = context.GetArgument<ProductSort>("searchProduct");
                    return await productRepository.GetProductAsync(searchProduct);
                });
        }
    }
}
