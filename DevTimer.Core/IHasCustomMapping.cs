using AutoMapper;

namespace DevTimer.Core
{
	//
	// See: http://www.pluralsight.com/courses/build-application-framework-aspdotnet-mvc-5 and
	//		https://www.youtube.com/watch?v=AQGk8WnMpAY for more details
	//
	public interface IHasCustomMapping
	{
		void CreateMapping(IConfiguration configuration);
	}
}
