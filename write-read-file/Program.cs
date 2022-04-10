using System.Xml;

System.Console.WriteLine("Start App");
string[] branches = { "Volvo", "BMW", "Ford", "Mazda" };
string[] users = { "Petr", "Ondra", "Marek", "Tomas" };

writeToFile(branches, users);
readFromFile();

static void writeToFile(string[] branches, string[] users)
{
    Console.WriteLine("Writing, to file!");

    XmlWriterSettings set = new XmlWriterSettings();
    set.Indent = true; //nastaveni odrazeni

    using (XmlWriter xw = XmlWriter.Create(@"users.xml", set))
    {
        //Zalozeni dokumentu??
        xw.WriteStartDocument();
        xw.WriteStartElement("users");
        for (int i = 0; i < users.Length; i++)
        {

            //pridani hodnot do dokumentu
            xw.WriteStartElement("user");
            xw.WriteAttributeString("username", users[i]);
            xw.WriteEndElement();
            xw.WriteStartElement("branches");
            for (int j = 0; j < branches.Length; j++)
            {
                xw.WriteStartElement("branch");
                xw.WriteAttributeString("branchName", branches[j]);
                xw.WriteEndElement();
            }
            xw.WriteEndElement();

        }
        xw.WriteEndElement();
        xw.WriteEndDocument();
        xw.Flush();
    }
}

static void readFromFile()
{
    using (XmlReader xr = XmlReader.Create(@"users.xml"))
    {
        int body;
        while (xr.Read())
        {
            if (xr.NodeType == XmlNodeType.Element)
            {
                if (xr.GetAttribute("username") != null)
                {
                    Console.WriteLine();
                    Console.WriteLine(xr.GetAttribute("username"));
                    Console.WriteLine("Branches: ");
                }
                if (xr.GetAttribute("branchName") != null)
                {
                    Console.WriteLine(xr.GetAttribute("branchName"));
                }
            }
        }
    }
}