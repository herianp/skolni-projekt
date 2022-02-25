using System.Xml.Serialization;
class XMLWrite
{
    public static void WriteXML(User user)
    {
        Console.WriteLine(user.name);
        XmlSerializer serializer =
            new XmlSerializer(typeof(User));
    
        TextWriter textWriter = new StreamWriter("database/users.xml");
        serializer.Serialize(textWriter, user);
        textWriter.Close();
    }
}