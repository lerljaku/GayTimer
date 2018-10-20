using Newtonsoft.Json;

namespace GayTimer.Services
{
    public interface ISerializerProvider
    {
        string Serialize<TObject>(TObject obj);

        TObject Deserialize<TObject>(string str);
    }

    public class SerializerProvider : ISerializerProvider
    {
        public TObject Deserialize<TObject>(string value)
        {
            return JsonConvert.DeserializeObject<TObject>(value);
        }

        public string Serialize<TObject>(TObject obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}