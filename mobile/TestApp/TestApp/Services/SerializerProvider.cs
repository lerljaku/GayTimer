using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace GayTimer.Services
{
    public interface ISerializerProvider
    {
        void Serialize<TObject>(TObject obj, Stream path);

        TObject Deserialize<TObject>(Stream str);
    }

    public class SerializerProvider : ISerializerProvider
    {
        public TObject Deserialize<TObject>(Stream str)
        {
            var serializer = new XmlSerializer(typeof(TObject));

            var deserialized = (TObject)serializer.Deserialize(str);

            return deserialized;
        }

        public void Serialize<TObject>(TObject obj, Stream str)
        {
            var serializer = new XmlSerializer(typeof(TObject));

            serializer.Serialize(str, obj);
        }
    }
}