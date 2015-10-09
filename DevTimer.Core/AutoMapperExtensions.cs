using AutoMapper;

namespace DevTimer.Core
{
	public static class AutoMapperExtensions
	{
		public static IMappingExpression<TSource, TDest> IgnoreAllUnmapped<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
		{
			expression.ForAllMembers(opt => opt.Ignore());
			return expression;
		}
		
		// Facilitates mapping of several objects to one object
		public static TDestination Map<TSource, TDestination>(this TDestination destination, TSource source)
		{
			return Mapper.Map(source, destination);
		}
	}
}
