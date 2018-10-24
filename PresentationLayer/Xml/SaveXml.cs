using System.IO;
using System.Xml.Serialization;

namespace LanguageProcessor.Xml
{
    class SaveXml
    {
        public static void SaveData(object obj, string fileName)
        {
            var serializer = new XmlSerializer(obj.GetType());
            var writer = new StreamWriter(fileName);
            serializer.Serialize(writer, obj);
            writer.Close();
        }
    }
}
