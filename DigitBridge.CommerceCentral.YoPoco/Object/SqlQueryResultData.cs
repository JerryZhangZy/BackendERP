using DigitBridge.Base.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitBridge.CommerceCentral.YoPoco
{
	public class SqlQueryResultData
	{
		public List<string> heading;
		public List<object[]> data;

		public SqlQueryResultData()
		{
			heading = new List<string>();
			data = new List<object[]>();
		}
		public bool HasData => data != null && data.Count > 0;
		public int GetIndex(string name) =>	heading != null && heading.Count > 0
				? heading.FindIndex(x => name.EqualsIgnoreSpace(x))
				: -1;

		public object GetData(string name, int rowNum = 0)
		{ 
			var index = GetIndex(name);
			return index < 0 ? null : data[rowNum][index];
		}
	}
}
