using CourseWorkDB.ViewModel.Product;
using GraphQL.Types;
using ProductModel = CourseWorkDB.Model.Product;

namespace CourseWorkDB.Graphql.Mutation.Product.ProductCrudTypes
{
    public class DetailsProductInfoGraphType : ObjectGraphType<DetailsProductInfo>
    {
        public DetailsProductInfoGraphType()
        {
            Field(el => el.Id);
            Field(el => el.Image);
            Field(el => el.CreatorId);
            Field(el => el.Name);
            Field(el => el.CategoryId);
            Field(el => el.Description);
            Field(el => el.Discount, nullable: true);
            Field(el => el.SpecificProductInfoId, nullable: true);

        }
    }

}