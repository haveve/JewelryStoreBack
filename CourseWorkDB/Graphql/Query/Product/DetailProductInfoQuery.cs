using CourseWorkDB.Graphql.Mutation.Product.MaterialInfoCrud;
using CourseWorkDB.Graphql.Mutation.Product.SizeInfoCrudTypes;
using CourseWorkDB.Graphql.Mutation.Product.SpecificProductInfo;
using CourseWorkDB.Graphql.Mutation.Product.StoneInfoCrud;
using CourseWorkDB.Repositories;
using GraphQL;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Query.Product
{
    public class DetailProductInfoQuery:ObjectGraphType
    {
        public DetailProductInfoQuery(IProductRepository productRepository) 
        {
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<SizeGraphType>>>>("get_sizes")
                .ResolveAsync(async context =>
                {
                    return await productRepository.GetSizesAsync();
                });

            Field<NonNullGraphType<ListGraphType<NonNullGraphType<SizeInfoGraphType>>>>("get_sizeinfos")
                .Argument<NonNullGraphType<IntGraphType>>("productId")
                .ResolveAsync(async context =>
                {
                    var productId = context.GetArgument<int>("productId");
                    return await productRepository.GetSizeInfosAsync(productId);
                });

            Field<NonNullGraphType<ListGraphType<NonNullGraphType<MaterialGraphType>>>>("get_materials")
                .ResolveAsync(async context =>
                {
                    return await productRepository.GetMaterialsAsync();
                });

            Field<NonNullGraphType<ListGraphType<NonNullGraphType<MaterialColorGraphType>>>>("get_material_colors")
                .ResolveAsync(async context =>
                {
                    return await productRepository.GetMaterialsColorsAsync();
                });


            Field<NonNullGraphType<ListGraphType<NonNullGraphType<MaterialInfoGraphType>>>>("get_material_infos")
                .Argument<NonNullGraphType<IntGraphType>>("productId")
                .ResolveAsync(async context =>
                {
                    var productId = context.GetArgument<int>("productId");
                    return await productRepository.GetMaterialInfosAsync(productId);
                });

            Field<NonNullGraphType<ListGraphType<NonNullGraphType<StoneColorGraphType>>>>("get_stone_colors")
                .ResolveAsync(async context =>
                {
                    return await productRepository.GetStoneColorsAsync();
                });


            Field<NonNullGraphType<ListGraphType<NonNullGraphType<StoneShapeGraphType>>>>("get_stone_shapes")
                .ResolveAsync(async context =>
                {
                    return await productRepository.GetStoneShapesAsync();
                });

            Field<NonNullGraphType<ListGraphType<NonNullGraphType<StoneTypeGraphType>>>>("get_stone_types")
                .ResolveAsync(async context =>
                {
                    return await productRepository.GetStoneTypesAsync();
                });

            Field<NonNullGraphType<ListGraphType<NonNullGraphType<StoneInfoGraphType>>>>("get_stone_infos")
                .Argument<NonNullGraphType<IntGraphType>>("productId")
                .ResolveAsync(async context =>
                {
                    var productId = context.GetArgument<int>("productId");
                    return await productRepository.GetStoneInfosAsync(productId);
                });




            Field<NonNullGraphType<ListGraphType<NonNullGraphType<LockTypeGraphType>>>>("get_spec_lock_types")
                .ResolveAsync(async context =>
                {
                    return await productRepository.GetLockTypesAsync();
                });

            Field<NonNullGraphType<ListGraphType<NonNullGraphType<StoneTypeGraphType>>>>("get_spec_shape_types")
                .ResolveAsync(async context =>
                {
                    return await productRepository.GetStoneTypesAsync();
                });

            Field<NonNullGraphType<ListGraphType<NonNullGraphType<SpecificProductInfoGraphType>>>>("get_spec_info")
                .Argument<NonNullGraphType<IntGraphType>>("productId")
                .ResolveAsync(async context =>
                {
                    var productId = context.GetArgument<int>("productId");
                    return await productRepository.GetSpecificProductInfoAsync(productId);
                });


        }
    }
}
