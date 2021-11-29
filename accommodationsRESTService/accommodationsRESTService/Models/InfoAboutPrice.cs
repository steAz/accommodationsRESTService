using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace accommodationsRESTService.Models
{
    public class InfoAboutPrice
    {
        [JsonConverter(typeof(DateIntFormatToDateTimeConverter))]
        public DateTime Date { get; set; }

        public int Price { get; set; }

        /// <summary>
        /// Class which converts Date's int format from json file to DateTime format
        /// </summary>
        private class DateIntFormatToDateTimeConverter : JsonConverter<DateTime>
        {
            private static readonly string _format = "yyyyMMdd";


            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType != JsonTokenType.Number)
                    throw new Exception("Cannot convert to DateTime, wrong JsonTokenType");

                return DateTime.ParseExact(reader.GetInt32().ToString(), _format, System.Globalization.CultureInfo.InvariantCulture);
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(_format));
        }
    }
}
