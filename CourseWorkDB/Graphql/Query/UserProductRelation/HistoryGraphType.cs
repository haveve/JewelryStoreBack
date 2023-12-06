﻿using CourseWorkDB.Model;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Query.UserProductRelation
{
    public class HistoryGraphType:ObjectGraphType<History>
    {
        public HistoryGraphType()
        {
            Field(el => el.Disabled);
            Field(el => el.TotalCost);
            Field(el => el.Name);
            Field(el => el.CategoryId);
            Field(el => el.Image);
            Field(el => el.Count);
        }
    }
}