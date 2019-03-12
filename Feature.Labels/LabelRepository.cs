using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Constellation.Foundation.ModelMapping;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Globalization;

namespace Feature.Labels
{
	/// <summary>
	/// Centralized point for retrieving Label View Models from Sitecore.
	/// </summary>
	public class LabelRepository
	{
		#region Fields
		private static volatile IDictionary<string, Type> _labelTypes;

		private static readonly object LockObject = new object();
		#endregion

		#region Properties
		/// <summary>
		/// The complete set of Label class names organized by the ID of the Sitecore Item containing their runtime data.
		/// </summary>
		protected static IDictionary<string, Type> LabelTypes
		{
			get
			{
				if (_labelTypes == null)
				{
					lock (LockObject)
					{
						if (_labelTypes == null)
						{
							_labelTypes = Initialize();
						}
					}
				}

				return _labelTypes;
			}
		}
		#endregion


		#region Methods
		/// <summary>
		/// Creates a new instance of TLabel and retrieves data for TLabel's properties from Sitecore using the Context Database
		/// and the Context Language.
		/// </summary>
		/// <typeparam name="TLabel">The ViewModel to instantiate.</typeparam>
		/// <returns>An instance of TLabel with all values populated from Sitecore.</returns>
		public static TLabel GetLabelsForView<TLabel>()
			where TLabel : class, new()

		{
			return GetLabels<TLabel>(Sitecore.Context.Database, Sitecore.Context.Language);
		}


		/// <summary>
		/// Creates a new instance of TLabel and retrieves data for TLabel's properties from Sitecore using the Database and
		/// Language supplied.
		/// </summary>
		/// <param name="database">The database to retrieve Label values from.</param>
		/// <param name="language">The language content should be supplied in.</param>
		/// <typeparam name="TLabel">The Type of ViewModel to return.</typeparam>
		/// <returns>An instance of TLabel with all values populated from Sitecore.</returns>
		/// <exception cref="Exception"></exception>
		public static TLabel GetLabels<TLabel>(Database database, Language language)
			where TLabel : class, new()
		{
			if ((!(Attribute.GetCustomAttribute(typeof(TLabel), typeof(LabelAttribute)) is LabelAttribute labelAttribute)))
			{
				throw new Exception($"{typeof(TLabel).Name} does not have a LabelAttribute!");
			}

			if (string.IsNullOrEmpty(labelAttribute.DatasourceID))
			{
				throw new Exception($"LabelAttribute on {typeof(TLabel).Name} does not have a DatasourceID defined!");
			}

			try
			{
				var id = new ID(labelAttribute.DatasourceID);


				var item = database.GetItem(id, language);
				return MappingContext.Current.MapItemToNew<TLabel>(item);
			}
			catch (Exception ex)
			{
				throw new Exception($"LabelAttribute on {typeof(TLabel).Name} does not have a DatasourceID defined!", ex);
			}
		}

		/// <summary>
		/// Creates a new instance of a Label ViewModel based upon the supplied Label Item from Sitecore. The correct Type
		/// of Label to create will be based on the Label class that has a LabelAttribute with the DatasourceID matching the ID of the Item provided.
		/// This method is meant primarily for "blind" usage where the resulting object will be serialized or otherwise passed on but will not be operated
		/// upon in C#.
		/// </summary>
		/// <param name="item">The Item to create the Label ViewModel from.</param>
		/// <returns>An instance of a Label ViewModel</returns>
		/// <exception cref="Exception">If there is no Attributed class matching the provided Item, an Exception will be thrown.</exception>
		public static object GetLabels(Item item)
		{
			var type = LabelTypes[item.ID.ToString()];

			if (type == null)
			{
				throw new Exception(
					$"No matching Type for Label Item with ID of {item.ID}. Did you forget to add the LabelAttribute to the class?");
			}

			try
			{
				var model = Activator.CreateInstance(type);


				MappingContext.Current.MapTo(item, model);

				return model;
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed creating a Label object from type {type.Name}. Label ViewModels must be classes with parameterless constructors.", ex);
			}
		}
		#endregion

		private static Dictionary<string, Type> Initialize()
		{
			var output = new Dictionary<string, Type>();

			var assemblies = AppDomain.CurrentDomain.GetAssemblies();

			foreach (var assembly in assemblies)
			{
				if (assembly.IsDynamic)
				{
					continue;
				}

				if (assembly.FullName.StartsWith("Sitecore"))
				{
					continue;
				}

				var types = GetExportedTypes(assembly);

				foreach (var type in types)
				{
					if (type.IsAbstract)
					{
						continue;
					}

					if (type.IsGenericTypeDefinition)
					{
						continue;
					}

					try
					{
						if (type.GetConstructor(Type.EmptyTypes) == null)
						{
							Log.Warn($"Feature.Labels.LabelRepository: Type {type.Name} does not have a parameterless constructor, and cannot be used as a Label ViewModel.", typeof(LabelRepository));
							continue;
						}
					}
					catch (FileLoadException ex)
					{
						Log.Warn($"FileLoadException in LabelRepository. Exception: {ex.Message}", typeof(LabelRepository));
						continue;
					}


					if (!(Attribute.GetCustomAttribute(type, typeof(LabelAttribute)) is LabelAttribute labelAttribute))
					{
						continue;
					}

					if (output.ContainsKey(labelAttribute.DatasourceID))
					{
						continue;
					}

					output.Add(labelAttribute.DatasourceID, type);
				}
			}

			return output;
		}

		private static IEnumerable<Type> GetExportedTypes(Assembly assembly)
		{
			try
			{
				return assembly.GetExportedTypes();
			}
			catch (FileLoadException)
			{
				// Probably a type version mismatch, likely not custom code and can be skipped reliably
				return Type.EmptyTypes;
			}
			catch (TypeLoadException)
			{
				// Garbage, we can reliably skip this.
				return Type.EmptyTypes;
			}
			catch (NotSupportedException)
			{
				// A type load exception would typically happen on an Anonymously Hosted DynamicMethods
				// Assembly and it would be safe to skip this exception.
				return Type.EmptyTypes;
			}
			catch (ReflectionTypeLoadException ex)
			{
				// Return the types that could be loaded. Types can contain null values.
				return ex.Types.Where(type => type != null);
			}
			catch (Exception ex)
			{
				// Throw a more descriptive message containing the name of the assembly.
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
					"Unable to load types from assembly {0}. {1}", assembly.FullName, ex.Message), ex);
			}
		}

	}
}