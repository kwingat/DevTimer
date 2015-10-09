using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace DevTimer.Core
{
	public static class AutoMapperConfig
	{
		public static void Register(IConfiguration configuration)
		{
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				// Skip 3rd-party assemblies
				if (assembly.FullName.StartsWith("DevTimer"))
				{
					Type[] types = assembly.GetExportedTypes();

					LoadStandardMappings(configuration, types);
					LoadCustomMappings(configuration, types);
				}
			}

#if DEBUG
			try
			{
				Mapper.AssertConfigurationIsValid();
			}
			catch (AutoMapperConfigurationException ex)
			{
				Debug.WriteLine("Auto mapper configuration is invalid.");
				Debug.Indent();

				Debug.WriteLine(ex.Message);
				Debug.Unindent();

				Debugger.Break();
			}
#endif
		}

		private static void LoadStandardMappings(IConfiguration configuration, IEnumerable<Type> types)
		{
			var maps = (from t in types
						from i in t.GetInterfaces()
						where i.IsGenericType &&
							i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
							!t.IsAbstract &&
							!t.IsInterface
						select new
						{
							Source = i.GetGenericArguments()[0],
							Destination = t
						}).ToArray();

			foreach (var map in maps)
			{
				configuration.CreateMap(map.Source, map.Destination);
			}
		}

		private static void LoadCustomMappings(IConfiguration configuration, IEnumerable<Type> types)
		{
			var maps = (from t in types
						from i in t.GetInterfaces()
						where typeof(IHasCustomMapping).IsAssignableFrom(t) &&
							!t.IsAbstract &&
							!t.IsInterface &&
							!t.ContainsGenericParameters
						select (IHasCustomMapping)Activator.CreateInstance(t));

			foreach (var map in maps)
			{
				map.CreateMapping(configuration);
			}
		}
	}
}
