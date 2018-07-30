﻿using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SpecResults.Json
{
	public class JsonReporter : Reporter
	{
		public JsonReporter()
		{
			JsonSerializerSettings =
				new JsonSerializerSettings
				{
					Formatting = Formatting.Indented,
					ContractResolver = new ReportContractResolver(),
					NullValueHandling = NullValueHandling.Ignore,
					Converters = new JsonConverter[] {new StringEnumConverter()}.ToList()
				};
		}

		public JsonSerializerSettings JsonSerializerSettings { get; set; }

		public override void WriteToStream(Stream stream)
		{
			var json = JsonConvert.SerializeObject(
				Report,
				JsonSerializerSettings
				);
			var bytes = Encoding.UTF8.GetBytes(json);
			using (var ms = new MemoryStream(bytes))
			{
				ms.CopyTo(stream);
			}
		}
	}
}