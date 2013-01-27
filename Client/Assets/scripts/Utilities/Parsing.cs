using System;
using System.Collections.Generic;
using System.Text;
using Utilities.Json;

namespace Utilities
{
	public class Parsing
	{
		public Parsing()
		{}

		public static T json_decode<T>(string json)
		{
			json = json.Replace("[]", "null");
			return Converter.Deserialize<T>(json);
		}

		public static string json_encode(object obj)
		{
			return Converter.Serialize(obj);
		}
	}
}
