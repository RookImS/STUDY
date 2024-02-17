using System;
using System.IO;
using System.Windows;
using System.Xml.Serialization; 

namespace Project1_NotepadClone
{
    public class NotepadCloneSettings
    {
        public WindowState WindowState = WindowState.Normal;
        public Rect RestoreBounds = Rect.Empty;
        public TextWrapping TextWrapping = TextWrapping.NoWrap;
        public string FontFamily = "";
        public string FontStyle =
            new FontStyleConverter().ConvertToString(FontStyles.Normal);
        public string FontWeight = 
            new FontWeightConverter().ConvertToString(FontWeights.Normal);
        public string FontStretch =
            new FontStretchConverter().ConvertToString(FontStretches.Normal);
        public double FontSize = 11;

        // 설정을 파일에 저장
        public virtual bool Save(string strAppData)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(strAppData));
                StreamWriter write = new StreamWriter(strAppData);
                XmlSerializer xml = new XmlSerializer(GetType());
                xml.Serialize(write, this);
                write.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }

        // 설정을 파일에서 불러들임
        public static object Load(Type type, string strAppData)
        {
            StreamReader reader;
            object settings;
            XmlSerializer xml = new XmlSerializer(type);

            try
            {
                reader = new StreamReader(strAppData);
                settings = xml.Deserialize(reader);
                reader.Close();
            }
            catch
            {
                settings = type.GetConstructor(System.Type.EmptyTypes).Invoke(null);
            }
            return settings;
        }
    }
}
