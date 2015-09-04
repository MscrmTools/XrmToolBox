using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MsCrmTools.MetadataDocumentGenerator
{
    /// <summary>
    /// Classe utilitaire permettant de réaliser des opérations de
    /// sérialisation et de déserialization
    /// </summary>
    public class XmlSerializerHelper
    {
        /// <summary>
        /// Deserialize un contenu xml de type chaine de caractère en
        /// une instance de la classe du type spécifié
        /// </summary>
        /// <param name="xmlContent">Contenu xml</param>
        /// <param name="objectType">Type de l'objet de destination</param>
        /// <returns>Objet déserialisé</returns>
        public static object Deserialize(string xmlContent, Type objectType)
        {
            var reader = new StringReader(xmlContent);
            var s = new XmlSerializer(objectType);
            return s.Deserialize(reader);
        }

        /// <summary>
        /// Sérialise un objet sous forme de chaine de caractère Xml
        /// </summary>
        /// <param name="o">Objet à sérialiser</param>
        /// <returns>Chaine résultante</returns>
        public static string Serialize(object o)
        {
            var s = new XmlSerializer(o.GetType());
            var builder = new StringBuilder();
            using (var writer = new StringWriter(builder))
            {
                s.Serialize(writer, o);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Sérialize un objet dans un fichier
        /// </summary>
        /// <param name="o">Objet à sérialiser</param>
        /// <param name="path">Chemin du fichier de destination</param>
        public static void SerializeToFile(object o, string path)
        {
            var s = new XmlSerializer(o.GetType());

            using (var writer = new StreamWriter(path, false))
            {
                s.Serialize(writer, o);
            }
        }
    }
}