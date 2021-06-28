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
	}
}
