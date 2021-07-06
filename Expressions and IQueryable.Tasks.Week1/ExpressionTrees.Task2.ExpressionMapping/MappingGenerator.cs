using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionTrees.Task2.ExpressionMapping
{
    public class MappingGenerator
    {
        public Mapper<TSource, TDestination> Generate<TSource, TDestination>()
        {
            var sourceParam = Expression.Parameter(typeof(TSource));

            var newExpression = Expression.New(typeof(TDestination));
            var memberInitExpression = Expression.MemberInit(newExpression, MemberBindings(typeof(TDestination), sourceParam));
           
            var mapFunction =
                Expression.Lambda<Func<TSource, TDestination>>(
                    memberInitExpression,
                    sourceParam
                );

            return new Mapper<TSource, TDestination>(mapFunction.Compile());
        }

        private static IEnumerable<MemberBinding> MemberBindings(Type type, ParameterExpression sourceParam)
        {
            var result = new List<MemberBinding>();
            var propertiesBar = type.GetProperties();
            var propertiesFoo = sourceParam.Type.GetProperties().Where(prop => propertiesBar.Any(x => x.Name == prop.Name));

            foreach (var fooPropertyInfo in propertiesFoo)
            {
                var barPropertyInfo = propertiesBar.First(x => x.Name == fooPropertyInfo.Name);
                var getFooProperty = Expression.MakeMemberAccess(sourceParam, fooPropertyInfo);
                var memberAssignment = Expression.Bind(barPropertyInfo, getFooProperty);
                result.Add(memberAssignment);
            }
            return result;
        }
    }
}
