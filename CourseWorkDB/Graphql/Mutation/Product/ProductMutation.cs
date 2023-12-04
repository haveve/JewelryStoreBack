using CourseWorkDB.Graphql.Mutation.Product.ProductCrudTypes;
using CourseWorkDB.Graphql.Mutation.Product.StoneInfoCrud;
using CourseWorkDB.Model;
using CourseWorkDB.Repositories;
using CourseWorkDB.ViewModel.Product;
using FileUploadSample;
using GraphQL;
using GraphQL.Types;
using GraphQL.Upload.AspNetCore;
using Microsoft.Extensions.FileProviders;
using ProductModel = CourseWorkDB.Model.Product;

namespace CourseWorkDB.Graphql.Mutation.Product
{
    public class ProductMutation:ObjectGraphType
    {
        public ProductMutation(IUploadRepository uploadRepository, IProductRepository productRepository)
        {
            Field<NonNullGraphType<ProductGraphType>>("add_product")
                .Argument<NonNullGraphType<AddProductInputGraphType>>("product")
                .Argument<NonNullGraphType<UploadGraphType>>("image")
                .ResolveAsync(async context =>
                {
                    var productData = context.GetArgument<AddProduct>("product");
                    var img = context.GetArgument<IFormFile>("image");
                    productData.Image = await uploadRepository.SaveImgAsync(img,productData.CategoryId);

                    try
                    {
                        return await productRepository.AddProductAsync(productData);
                    }
                    catch
                    {
                        uploadRepository.DeleteFile(Path.Combine(productData.CategoryId.ToString(),productData.Image));
                        throw;
                    }
                });
            Field<NonNullGraphType<ProductGraphType>>("update_product")
                .Argument<NonNullGraphType<ProductInputGraphType>>("product")
                .Argument<UploadGraphType>("image")
                .ResolveAsync(async context =>
                {
                    var productData = context.GetArgument<ProductModel>("product");
                    var img = context.GetArgument<IFormFile?>("image");
                    
                    var newImg = string.Empty;

                    if (img is not null)
                    {
                        productData.Image = await uploadRepository.SaveImgAsync(img, productData.CategoryId);
                        newImg = productData.Image;
                    }

                    try
                    {
                        var result = await productRepository.UpdateProductAsync(productData);

                        if (img is not null)
                        {
                            uploadRepository.DeleteFile(Path.Combine(result.CategoryId.ToString(), result.Image));
                            result.Image = newImg;
                        }
                        return result;
                    }
                    catch
                    {
                        if (img is not null)
                        {
                            uploadRepository.DeleteFile(Path.Combine(productData.CategoryId.ToString(), productData.Image));
                        }
                        throw;
                    }
                });

            Field<NonNullGraphType<StatusGraphType>>("change_product_state")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .Argument<NonNullGraphType<BooleanGraphType>>("disabled")
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<int>("id");
                    var disabled = context.GetArgument<bool>("disabled");

                    return await productRepository.ChangeProductStateAsync(id,disabled);
                });

            Field<IntGraphType>("delete_product")
                .Argument<IntGraphType>("productId")
                .ResolveAsync(async context =>
                {
                    var productId = context.GetArgument<int>("productId");
                    return await productRepository.RemoveProductAsync(productId);
                });

            Field<NonNullGraphType<CategoryGraphType>>("add_category")
                .Argument<NonNullGraphType<StringGraphType>>("name")
                .ResolveAsync(async context =>
                {
                    var name = context.GetArgument<string>("name");

                    return await productRepository.AddCategoryAsync(name);

                });

            Field<NonNullGraphType<CategoryGraphType>>("update_category")
                .Argument<NonNullGraphType<CategoryInputGraphType>>("category")
                .ResolveAsync(async context =>
                {
                    var category = context.GetArgument<Category>("category");

                    return await productRepository.UpdateCategoryAsync(category);

                });

            Field<NonNullGraphType<IntGraphType>>("delete_category")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<int>("id");
                    return await productRepository.RemoveCategoryAsync(id);
                });

            Field<NonNullGraphType<CreatorGraphType>>("add_creator")
               .Argument<NonNullGraphType<StringGraphType>>("name")
               .ResolveAsync(async context =>
               {
                   var name = context.GetArgument<string>("name");

                   return await productRepository.AddCreatorAsync(name);

               });

            Field<NonNullGraphType<CreatorGraphType>>("update_creator")
                .Argument<NonNullGraphType<CreatorInputGraphType>>("creator")
                .ResolveAsync(async context =>
                {
                    var category = context.GetArgument<Creator>("creator");

                    return await productRepository.UpdateCreatorAsync(category);

                });

            Field<NonNullGraphType<StatusGraphType>>("change_creator_state")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .Argument<NonNullGraphType<BooleanGraphType>>("disabled")
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<int>("id");
                    var disabled = context.GetArgument<bool>("disabled");

                    return await productRepository.ChangeCreatorStateAsync(id,disabled);
                });


            Field<IntGraphType>("delete_creator")
                .Argument<IntGraphType>("creatorId")
                .ResolveAsync(async context =>
                {
                    var creatorId = context.GetArgument<int>("creatorId");
                    return await productRepository.RemoveCreatorAsync(creatorId);
                });

        }
    }
}
