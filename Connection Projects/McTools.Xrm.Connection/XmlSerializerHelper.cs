using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace McTools.Xrm.Connection
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
            StringReader reader = new StringReader(xmlContent);
            XmlSerializer s = new XmlSerializer(objectType);
            return s.Deserialize(reader);
        }

        /// <summary>
        /// Sérialise un objet sous forme de chaine de caractère Xml
        /// </summary>
        /// <param name="o">Objet à sérialiser</param>
        /// <returns>Chaine résultante</returns>
        public static string Serialize(object o)
        {
            try
            {
                XmlSerializer s = new XmlSerializer(o.GetType());
                StringBuilder builder = new StringBuilder();
                using (StringWriter writer = new StringWriter(builder))
                {
                    s.Serialize(writer, o);
                }
                return builder.ToString();
            }
            catch (Exception error)
            {
                throw new Exception("Error while serializing: " + error.Message);
            }
        }

        /// <summary>
        /// Sérialize un objet dans un fichier
        /// </summary>
        /// <param name="o">Objet à sérialiser</param>
        /// <param name="path">Chemin du fichier de destination</param>
        public static void SerializeToFile(object o, string path)
        {
            try
            {
                XmlSerializer s = new XmlSerializer(o.GetType());

                using (var fStream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    fStream.SetLength(0);
                }

                using (var fStream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter writer = new StreamWriter(fStream))
                    {
                        s.Serialize(writer, o);
                    }
                }
            }
            catch (Exception error)
            {
                throw new Exception("Error while serializing: " + error.Message);
            }
        }
    }
}