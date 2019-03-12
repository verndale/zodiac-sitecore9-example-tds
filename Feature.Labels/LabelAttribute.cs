using System;
// ReSharper disable InconsistentNaming

namespace Feature.Labels
{
	/// <summary>
	/// 
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class LabelAttribute : Attribute
	{
		/// <summary>
		/// Indicates that the annotated class is a Label ViewModel.
		/// </summary>
		/// <param name="datasourceID">The ID of the Item that can be mapped to the annotated class.</param>
		public LabelAttribute(string datasourceID)
		{
			DatasourceID = datasourceID;
		}

		/// <summary>
		/// Gets or sets the ID of the Item that can be mapped to the annotated class.
		/// </summary>
		public string DatasourceID { get; set; }
	}
}
