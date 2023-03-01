using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JsonFx.Json
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class JsonClassAttribute : Attribute
	{
		#region Fields

		private string jsonName = null;

		#endregion Fields

		#region Init

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="jsonName"></param>
        public JsonClassAttribute(string jsonName)
		{
			this.jsonName = jsonName;
		}

		#endregion Init

		#region Properties

		/// <summary>
		/// Gets and sets the name to be used in JSON
		/// </summary>
		public string Name
		{
			get { return this.jsonName; }
			set { this.jsonName = value; }
		}

		#endregion Properties

		#region Methods

		/// <summary>
		/// Gets the name specified for use in Json serialization.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string GetClassName(Type value)
		{
			if (value == null)
			{
				return null;
			}
		    var attribs = value.GetCustomAttributes(typeof (JsonClassAttribute), false);
		    if (attribs.Length == 0)
		        return null;
            JsonClassAttribute attribute = attribs[0] as JsonClassAttribute;
		    if (attribute == null)
		        return null;
			return attribute.Name;
		}

		#endregion Methods
	}
}
