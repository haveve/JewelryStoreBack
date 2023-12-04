﻿using CourseWorkDB.Graphql.Mutation.Product.MaterialInfoCrud;
using CourseWorkDB.Graphql.Mutation.Product.SizeInfoCrudTypes;
using CourseWorkDB.Graphql.Mutation.Product.SpecificProductInfo;
using CourseWorkDB.Graphql.Mutation.Product.StoneInfoCrud;
using CourseWorkDB.Model;
using CourseWorkDB.Repositories;
using CourseWorkDB.ViewModel.Material;
using CourseWorkDB.ViewModel.Product;
using CourseWorkDB.ViewModel.Size;
using GraphQL;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.Product
{
    public class DetailProductInfoMutation:ObjectGraphType
    {
        public DetailProductInfoMutation(IProductRepository productRepository)
        {
            Field<SizeGraphType>("add_size")
                .Argument<StringGraphType>("name")
                .ResolveAsync(async context =>
                {
                    var name = context.GetArgument<string>("name");

                    return await productRepository.AddSizeAsync(name);
                });

           Field<SizeGraphType>("update_size")
                .Argument<SizeInputGraphType>("size")
                .ResolveAsync(async context =>
                {
                    var size = context.GetArgument<Size>("size");
                    return await productRepository.UpdateSizeAsync(size);
                });

            Field<IntGraphType>("delete_size")
                .Argument<IntGraphType>("sizeId")
                .ResolveAsync(async context =>
                {
                    var sizeId = context.GetArgument<int>("sizeId");
                    return await productRepository.RemoveSizeAsync(sizeId);
                });

            Field<NonNullGraphType<ListGraphType<NonNullGraphType<AddSizeInfoGraphType>>>>("add_size_infos")
                .Argument<NonNullGraphType<ListGraphType<NonNullGraphType<AddSizeInfoInputGraphType>>>>("sizeInfos")
                .ResolveAsync(async context =>
                {
                    var sizeInfos = context.GetArgument<IEnumerable<AddSizeInfo>>("sizeInfos");
                    return await productRepository.AddSizeInfosAsync(sizeInfos);
                });

            Field<AddSizeInfoGraphType>("update_size_info")
                .Argument<AddSizeInfoInputGraphType>("sizeInfo")
                .ResolveAsync(async context =>
                {
                    var sizeInfo = context.GetArgument<AddSizeInfo>("sizeInfo");
                    return await productRepository.UpdateSizeInfoAsync(sizeInfo);
                });

            Field<IntGraphType>("delete_size_info")
                .Argument<IntGraphType>("sizeInfoId")
                .ResolveAsync(async context =>
                {
                    var sizeInfoId = context.GetArgument<int>("sizeInfoId");
                    return await productRepository.RemoveSizeInfoAsync(sizeInfoId);
                });


            Field<MaterialColorGraphType>("add_material_color")
                .Argument<StringGraphType>("name")
                .ResolveAsync(async context =>
                {
                    var name = context.GetArgument<string>("name");
                    return await productRepository.AddMaterialColorAsync(name);
                });

            Field<MaterialColorGraphType>("update_material_color")
                 .Argument<MaterialColorInputGraphType>("materialColor")
                 .ResolveAsync(async context =>
                 {
                     var material = context.GetArgument<MaterialColor>("materialColor");
                     return await productRepository.UpdateMaterialColorAsync(material);
                 });

            Field<IntGraphType>("delete_material_color")
                .Argument<IntGraphType>("materialColorId")
                .ResolveAsync(async context =>
                {
                    var materialColorId = context.GetArgument<int>("materialColorId");
                    return await productRepository.RemoveMaterialColorAsync(materialColorId);
                });

            Field<MaterialGraphType>("add_material")
                .Argument<StringGraphType>("name")
                .ResolveAsync(async context =>
                {
                    var name = context.GetArgument<string>("name");
                    return await productRepository.AddMaterialAsync(name);
                });

            Field<MaterialGraphType>("update_material")
                 .Argument<MaterialInputGraphType>("material")
                 .ResolveAsync(async context =>
                 {
                     var material = context.GetArgument<Material>("material");
                     return await productRepository.UpdateMaterialAsync(material);
                 });

            Field<IntGraphType>("delete_material")
                .Argument<IntGraphType>("materialId")
                .ResolveAsync(async context =>
                {
                    var materialId = context.GetArgument<int>("materialId");
                    return await productRepository.RemoveMaterialAsync(materialId);
                });

            Field<NonNullGraphType<ListGraphType<NonNullGraphType<AddMaterialInfoGraphType>>>>("add_materialInfos")
                .Argument<NonNullGraphType<ListGraphType<NonNullGraphType<AddMaterialInfoInputGraphType>>>>("materialInfos")
                .ResolveAsync(async context =>
                {
                    var materialInfos = context.GetArgument<IEnumerable<AddMaterialInfo>>("materialInfos");
                    return await productRepository.AddMaterialInfosAsync(materialInfos);
                });

            Field<AddMaterialInfoGraphType>("update_material_info")
                .Argument<AddMaterialInfoInputGraphType>("materialInfo")
                .ResolveAsync(async context =>
                {
                    var materialInfo = context.GetArgument<AddMaterialInfo>("materialInfo");
                    return await productRepository.UpdateMaterialInfoAsync(materialInfo);
                });

             Field<IntGraphType>("delete_material_info")
                .Argument<IntGraphType>("materialInfoId")
                .ResolveAsync(async context =>
                {
                    var materialInfoId = context.GetArgument<int>("materialInfoId");
                    return await productRepository.RemoveMaterialInfoAsync(materialInfoId);
                });



            Field<NonNullGraphType<StoneColorGraphType>>("add_stone_color")
                .Argument<NonNullGraphType<StringGraphType>>("name")
                .ResolveAsync(async context =>
                {
                    var name = context.GetArgument<string>("name");
                    return await productRepository.AddStoneColorAsync(name);

                });

            Field<NonNullGraphType<StoneColorGraphType>>("update_stone_color")
                .Argument<NonNullGraphType<StoneColorInputGraphType>>("stoneColor")
                .ResolveAsync(async context =>
                {
                    var stoneColor = context.GetArgument<StoneColor>("stoneColor");

                    return await productRepository.UpdateStoneColorAsync(stoneColor);

                });

            Field<NonNullGraphType<IntGraphType>>("delete_stone_color")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<int>("id");
                    return await productRepository.RemoveStoneColorAsync(id);
                });



            Field<NonNullGraphType<StoneShapeGraphType>>("add_stone_shape")
                .Argument<NonNullGraphType<StringGraphType>>("name")
                .ResolveAsync(async context =>
                {
                    var name = context.GetArgument<string>("name");
                    return await productRepository.AddStoneShapeAsync(name);

                });

            Field<NonNullGraphType<StoneShapeGraphType>>("update_stone_shape")
                .Argument<NonNullGraphType<StoneShapeInputGraphType>>("stoneShape")
                .ResolveAsync(async context =>
                {
                    var stoneShape = context.GetArgument<StoneShape>("stoneShape");

                    return await productRepository.UpdateStoneShapeAsync(stoneShape);

                });

            Field<NonNullGraphType<IntGraphType>>("delete_stone_shape")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<int>("id");
                    return await productRepository.RemoveStoneShapeAsync(id);
                });


             Field<NonNullGraphType<StoneTypeGraphType>>("add_stone_type")
                .Argument<NonNullGraphType<StringGraphType>>("name")
                .ResolveAsync(async context =>
                {
                    var name = context.GetArgument<string>("name");
                    return await productRepository.AddStoneTypeAsync(name);

                });

            Field<NonNullGraphType<StoneTypeGraphType>>("update_stone_type")
                .Argument<NonNullGraphType<StoneTypeInputGraphType>>("stoneType")
                .ResolveAsync(async context =>
                {
                    var stoneType = context.GetArgument<StoneType>("stoneType");
                    return await productRepository.UpdateStoneTypeAsync(stoneType);

                });

            Field<NonNullGraphType<IntGraphType>>("delete_stone_type")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<int>("id");
                    return await productRepository.RemoveStoneTypeAsync(id);
                });
            
            Field< NonNullGraphType < ListGraphType<NonNullGraphType<AddStoneInfoGraphType>>>>("add_stone_infos")
               .Argument<NonNullGraphType<ListGraphType<NonNullGraphType<AddStoneInfoInputGraphType>>>>("stoneInfo")
               .ResolveAsync(async context =>
               {
                   var stoneInfos = context.GetArgument<IEnumerable<AddStoneInfo>>("stoneInfo");
                   return await productRepository.AddStoneInfosAsync(stoneInfos);

               });

            Field<NonNullGraphType<AddStoneInfoGraphType>>("update_stone_info")
                .Argument<NonNullGraphType<AddStoneInfoInputGraphType>>("stoneInfo")
                .ResolveAsync(async context =>
                {
                    var stoneInfo = context.GetArgument<AddStoneInfo>("stoneInfo");
                    return await productRepository.UpdateStoneInfoAsync(stoneInfo);
                });

            Field<NonNullGraphType<IntGraphType>>("delete_stone_info")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<int>("id");
                    return await productRepository.RemoveStoneInfoAsync(id);
                });



            Field<NonNullGraphType<ShapeTypeGraphType>>("add_spec_shape_type")
               .Argument<NonNullGraphType<StringGraphType>>("name")
               .ResolveAsync(async context =>
               {
                   var name = context.GetArgument<string>("name");
                   return await productRepository.AddShapeTypeAsync(name);

               });

            Field<NonNullGraphType<ShapeTypeGraphType>>("update_spec_shape_type")
                .Argument<NonNullGraphType<ShapeTypeInputGraphType>>("shapeType")
                .ResolveAsync(async context =>
                {
                    var shapeType = context.GetArgument<ShapeType>("shapeType");
                    return await productRepository.UpdateShapeTypeAsync(shapeType);

                });

            Field<NonNullGraphType<IntGraphType>>("delete_spec_shape_type")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<int>("id");
                    return await productRepository.RemoveShapeTypeAsync(id);
                });




            Field<NonNullGraphType<LockTypeGraphType>>("add_spec_lock_type")
              .Argument<NonNullGraphType<StringGraphType>>("name")
              .ResolveAsync(async context =>
              {
                  var name = context.GetArgument<string>("name");
                  return await productRepository.AddLockTypeAsync(name);

              });

            Field<NonNullGraphType<LockTypeGraphType>>("update_spec_lock_type")
                .Argument<NonNullGraphType<LockTypeInputGraphType>>("lockType")
                .ResolveAsync(async context =>
                {
                    var lockType = context.GetArgument<LockType>("lockType");
                    return await productRepository.UpdateLockTypeAsync(lockType);

                });

            Field<NonNullGraphType<IntGraphType>>("delete_spec_lock_type")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<int>("id");
                    return await productRepository.RemoveLockTypeAsync(id);
                });

            Field<NonNullGraphType<AddSpecificProductInfoGraphType>>("add_spec_info")
              .Argument<NonNullGraphType<AddSpecificProductInfoInputGraphType>>("specInfo")
              .ResolveAsync(async context =>
              {
                  var specInfo = context.GetArgument<AddSpecificProductInfo>("specInfo");
                  return await productRepository.AddSpecificProductInfoAsync(specInfo);

              });

            Field<NonNullGraphType<AddSpecificProductInfoGraphType>>("update_spec_info")
                .Argument<NonNullGraphType<UpdateSpecificProductInfoInputGraphType>>("specInfo")
                .ResolveAsync(async context =>
                {
                    var specInfo = context.GetArgument<AddSpecificProductInfo>("specInfo");
                    return await productRepository.UpdateSpecificProductInfoAsync(specInfo);
                });

            Field<NonNullGraphType<IntGraphType>>("delete_spec_info")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<int>("id");
                    return await productRepository.RemoveSpecificProductInfoAsync(id);
                });
        }
    }
}
