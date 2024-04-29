using Newtonsoft.Json;
using System.ComponentModel;
using System.Data;

namespace monitoring_postgre_database
{
    internal class JsonDataSerializer
    {
        public static string Serialize(DataTable table)
        {
            return JsonConvert.ToString(table);
        }

        public static string Serialize(int value)
        {
            return JsonConvert.ToString(value);
        }

        public static string DeserializeString(byte[] message)
        {
            ByteConverter byteConverter = new ByteConverter();

            return byteConverter.ConvertToString(message);
        }

        public DataTable DeserializeTable(string table)
        {
            return JsonConvert.DeserializeObject<DataTable>(table);
        }
    }
}
