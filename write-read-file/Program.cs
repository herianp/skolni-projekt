using System.Collections;
using System.Xml;

System.Console.WriteLine("Start App");
string[] messages = { "Volvo", "BMW", "Ford", "Mazda" };
string[] users = { "Petr", "Ondra", "Marek", "Tomas" };
string[] branches = { "master", "development" };

writeToFile(branches, users, messages);

// readFromFile();
readFromFile2();

static void writeToFile(string[] branches, string[] users, string[] messages)
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

            xw.WriteStartElement("branches");
            for (int j = 0; j < branches.Length; j++)
            {
                xw.WriteStartElement("branch");
                xw.WriteAttributeString("branchName", branches[j]);
                xw.WriteStartElement("messages");
                for (int k = 0; k < messages.Length; k++)
                {
                    xw.WriteStartElement("message");
                    xw.WriteAttributeString("bodyMessage", messages[k]);
                    xw.WriteEndElement();
                }
                xw.WriteEndElement();
                xw.WriteEndElement();
            }
            xw.WriteEndElement();
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
                if (xr.GetAttribute("bodyMessage") != null)
                {
                    Console.WriteLine(xr.GetAttribute("bodyMessage"));
                }
            }
        }
    }
}

static void readFromFile2()
{
    int sum = 0;
    using (XmlReader reader = XmlReader.Create(@"users.xml"))
    {
        while (reader.Read())
        {
            if (reader.IsStartElement())
            {
                if (reader.Name == "user")
                {
                    XmlReader subreader = reader.ReadSubtree();
                    while (subreader.Read())
                    {
                        if (reader.GetAttribute("username") != null)
                        {
                            Console.WriteLine(reader.GetAttribute("username") + ":");
                        }
                        if (reader.GetAttribute("branchName") != null)
                        {
                            Console.WriteLine(reader.GetAttribute("branchName") + ":");
                            XmlReader branch_reader = reader.ReadSubtree();
                            while (branch_reader.Read())
                            {
                                if (reader.GetAttribute("bodyMessage") != null)
                                {
                                    Console.WriteLine(reader.GetAttribute("bodyMessage"));
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}