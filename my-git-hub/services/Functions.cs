using System.Collections;
using System.Text.RegularExpressions;
using System.Xml;

class Functions
{
    public static Branch currentBranch;
    //CREATING NEW BRANCH
    public static Branch addBranchToList(string branchName, User user)
    {
        return user.addBranch(branchName);
    }
    //MAIN FUNCTIONALITY OF APPLICATION
    public static Branch signpost(string input, User user, Branch cuurBranch)
    {
        currentBranch = cuurBranch;
        //EXIT APP
        if (Equals(input, "git exit") || Equals(input, "ge"))
        {
            Environment.Exit(0);
        }
        //CHANGE BRANCHE
        if (Equals(input, "git checkout") || Equals(input, "gch"))
        {
            changeBranch(user);
        }
        //CREATE NEW BRANCH
        if (Equals(input, "git branch") || Equals(input, "gb"))
        {
            createBranch(user);
        }
        //CREATE MESSAGE
        if (Equals(input, "create message") || Equals(input, "cm"))
        {
            createMessage();
        }
        //PUT MESSAGES TO STAGE AREA
        if (Equals(input, "git add") || Equals(input, "ga"))
        {
            addMessageToStageArea();
        }
        //COMMIT MESSAGE
        if (Equals(input, "git commit") || Equals(input, "gc"))
        {
            gitCommitMessage();
        }

        //PUSH (SAVE) ALL MESSAGES ON THE BRANCH
        if (Equals(input, "git push") || Equals(input, "gp"))
        {
            saveDataToBranch(user);
        }

        //GET DATA STORED ON THE BRANCH
        if (Equals(input, "git branch data") || Equals(input, "gbd"))
        {
            getDataFromBranch();
        }

        //GET MESSAGES WHICH ARE NOT IN STAGED AREA
        if (Equals(input, "get unstaged messages") || Equals(input, "gum"))
        {
            getUnstagedMessages();
        }

        return currentBranch;
    }

    public static Branch changeBranch(User user)
    {
        if (currentBranch.messages.Count > 0 || currentBranch.addList.Count > 0
            || currentBranch.commmitList.Count > 0)
        {
            Console.WriteLine("You didn`t pushed all changes. Do you really want to checkout? Your unpushed data will be deleted! y/n");
            Console.WriteLine("-----------------");
            Boolean first2 = true;
            while (true)
            {
                if (first2)
                {
                    first2 = false;
                }
                else
                {
                    Console.WriteLine("Make sure that your answer was correct, you have to put 'y' or 'n'!");
                    Console.WriteLine("-----------------");
                }
                string yesOrNo = Console.ReadLine();
                if (Equals(yesOrNo, "y"))
                {
                    break;
                }
                else if (Equals(yesOrNo, "n"))
                {
                    return currentBranch;
                }
            }
        }
        Console.Clear();
        Console.WriteLine("To what branch do you want to checkout?");
        Console.WriteLine("back", "-> Back to Menu");
        Console.WriteLine("-----------------");
        foreach (Branch branch in user.listOfBranch)
        {
            System.Console.WriteLine(branch.name);
        }
        Console.WriteLine("-----------------");

        string inputBranchName = Console.ReadLine();

        //back function
        if (Equals(inputBranchName, "back"))
        {
            return currentBranch;
        }

        foreach (Branch branch in user.listOfBranch)
        {
            if (Equals(branch.name, inputBranchName))
            {
                currentBranch.messages.Clear();
                currentBranch.addList.Clear();
                currentBranch.commmitList.Clear();
                currentBranch = branch;
                return currentBranch;
            }
        }

        Console.Clear();
        Console.WriteLine("This branch doesnot exits. Do you want to create branch with this name? y/n");
        Console.WriteLine("-----------------");
        Boolean first = true;
        while (true)
        {
            if (first)
            {
                first = false;
            }
            else
            {
                Console.WriteLine("Make sure that your answer was correct, you have to put 'y' or 'n'!");
                Console.WriteLine("-----------------");
            }
            string yesOrNo = Console.ReadLine();
            if (Equals(yesOrNo, "y"))
            {
                currentBranch.messages.Clear();
                currentBranch.addList.Clear();
                currentBranch.commmitList.Clear();
                currentBranch = addBranchToList(inputBranchName, user);
                break;
            }
            else if (Equals(yesOrNo, "n"))
            {
                break;
            }
        }
        return currentBranch;

    }
    public static Branch createBranch(User user)
    {
        while (true)
        {
            Console.WriteLine("Enter your branch name:");
            Console.WriteLine("back", "-> Back to Menu");
            Console.WriteLine("-----------------");
            string inputBranchName = Console.ReadLine();
            Boolean checkDuplicity = false;
            Console.Clear();

            //back function
            if (Equals(inputBranchName, "back"))
            {
                return currentBranch;
            }

            //CHECK IF BRANCH EXIST ALREADY
            foreach (Branch branch in user.listOfBranch)
            {
                if (Equals(branch.name, inputBranchName))
                {
                    Console.WriteLine("This branch already exists, make sure that name is not equal one of the branches!");
                    checkDuplicity = true;
                    break;
                }
            }

            //IF NOT THEN CREATE NEW BRANCH
            if (!checkDuplicity)
            {
                currentBranch = addBranchToList(inputBranchName, user);
                break;
            }

            //PRINT EXISTING BRANCHES
            Console.WriteLine("-----------------");
            foreach (Branch branch in user.listOfBranch)
            {
                System.Console.WriteLine(branch.name);
            }
            Console.WriteLine("-----------------");
        }
        return currentBranch;
    }
    public static Branch createMessage()
    {
        System.Console.WriteLine("Write one-line message:");
        Console.WriteLine("back", "-> Back to Menu");
        Console.WriteLine("-----------------");
        Boolean first = true;
        string inputMessage;
        while (true)
        {
            if (first)
            {
                first = false;
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("You have to put atleast one letter:");
                Console.WriteLine("back", "-> Back to Menu");
                Console.WriteLine("-----------------");
            }
            inputMessage = Console.ReadLine();
            if (!Equals(Regex.Replace(inputMessage, @"\s+", ""), ""))
            {
                break;
            }

        }

        //back function
        if (Equals(inputMessage, "back"))
        {
            return currentBranch;
        }

        currentBranch.messages.Add(new Message(inputMessage));
        return currentBranch;
    }

    public static Branch addMessageToStageArea()
    {
        if (currentBranch.messages.Count == 0)
        {
            System.Console.WriteLine("nothing to add");
            Thread.Sleep(1500);
            return currentBranch;
        }
        Console.WriteLine("Enter Index of message that you want to add to stage area:");
        Console.WriteLine("back", "-> Back to Menu");
        while (true)
        {
            //PRINT EXISTING notAdded Messages
            Console.WriteLine("-----------------");
            for (int i = 0; i < currentBranch.messages.Count; i++)
            {
                System.Console.WriteLine(i + " " + ((Message)currentBranch.messages[i]).text);
            }
            Console.WriteLine("-----------------");
            string inputIndexOfMessage = Console.ReadLine();

            //back function
            if (Equals(inputIndexOfMessage, "back"))
            {
                return currentBranch;
            }

            int option;
            bool success = int.TryParse(inputIndexOfMessage, out option);

            if (!success || option >= currentBranch.messages.Count || option < 0)
            {
                Console.Clear();
                Console.WriteLine("Are you sure that you entered right index of message?:");
                Console.WriteLine("Please Enter valid Index of message that you want to add to stage area:");
                Console.WriteLine("back", "-> Back to Menu");
            }
            else
            {
                currentBranch.addList.Add(currentBranch.messages[option]);
                currentBranch.messages.RemoveAt(option);
                break;
            }
        }

        return currentBranch;
    }

    public static Branch gitCommitMessage()
    {
        if (currentBranch.addList.Count == 0)
        {
            System.Console.WriteLine("nothing to commit, working directory clean");
            Thread.Sleep(1500);
            return currentBranch;
        }
        Console.WriteLine("Enter Index of message that you want to commit:");
        Console.WriteLine("back", "-> Back to Menu");
        while (true)
        {
            //PRINT EXISTING added Messages
            Console.WriteLine("-----------------");
            for (int i = 0; i < currentBranch.addList.Count; i++)
            {
                System.Console.WriteLine(i + " " + ((Message)currentBranch.addList[i]).text);
            }
            Console.WriteLine("-----------------");
            string inputIndexOfMessage = Console.ReadLine();

            //back function
            if (Equals(inputIndexOfMessage, "back"))
            {
                return currentBranch;
            }

            int option;
            bool success = int.TryParse(inputIndexOfMessage, out option);

            if (!success || option >= currentBranch.addList.Count || option < 0)
            {
                Console.Clear();
                Console.WriteLine("Are you sure that you entered valid index of message?:");
                Console.WriteLine("back", "-> Back to Menu");
            }
            else
            {
                currentBranch.commmitList.Add(currentBranch.addList[option]);
                currentBranch.addList.RemoveAt(option);
                break;
            }
        }
        return currentBranch;
    }
    public static Branch saveDataToBranch(User user)
    {
        if (currentBranch.commmitList.Count == 0)
        {
            System.Console.WriteLine("Everything up-to-date");
            Thread.Sleep(1500);
            return currentBranch;
        }

        TimeClass.loadingFunction("Checking token access");
        if (Equals(user.token, currentBranch.token))
        {
            System.Console.WriteLine("You have acces to this branch");
            Thread.Sleep(1500);
        }
        else
        {
            System.Console.WriteLine("Sorry you don`t have to change data on this branch");
            return currentBranch;
        }
        TimeClass.loadingFunction("Pushing to " + currentBranch);

        //SAVING DATA TO BRANCH
        foreach (Message m in currentBranch.commmitList)
        {
            currentBranch.branchData.Add(m);
        }
        currentBranch.commmitList.Clear();
        return currentBranch;
    }
    public static Branch getDataFromBranch()
    {
        if (currentBranch.branchData.Count == 0)
        {
            System.Console.WriteLine("Branch is clear");
            Thread.Sleep(1500);
            return currentBranch;
        }

        //PRINT DATA FROM BRANCH
        Console.WriteLine("-----------------");
        for (int i = 0; i < currentBranch.branchData.Count; i++)
        {
            System.Console.WriteLine(i + " " + ((Message)currentBranch.branchData[i]).text);
        }
        Console.WriteLine("-----------------");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        return currentBranch;
    }
    public static Branch getUnstagedMessages()
    {
        if (currentBranch.messages.Count == 0)
        {
            System.Console.WriteLine("There are no unstaged messages");
            Thread.Sleep(1500);
            return currentBranch;
        }

        //PRINT UNSTAGED MESSAGES
        Console.WriteLine("-----------------");
        for (int i = 0; i < currentBranch.messages.Count; i++)
        {
            System.Console.WriteLine(i + " " + ((Message)currentBranch.messages[i]).text);
        }
        Console.WriteLine("-----------------");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        return currentBranch;
    }

    //SHOW BASIC INFO ABOUT USER ACTIONS
    public static void basicInfo()
    {
        Console.WriteLine("-----------------");
        Console.WriteLine("CREATE --> ADD --> COMMIT --> PUSH");
        Console.WriteLine("{0,-25}{1,-6}{2,5}", "'get unstaged messages' ", "[gum]", "-> Get unstaged messages");
        Console.WriteLine("{0,-25}{1,-6}{2,5}", "'create message' ", "[cm]", "-> Create message");
        Console.WriteLine("{0,-25}{1,-6}{2,5}", "'git add' ", "[ga]", "-> Add message to stage area");
        Console.WriteLine("{0,-25}{1,-6}{2,5}", "'git commit' ", "[gc]", "-> Add message to branch");
        Console.WriteLine("{0,-25}{1,-6}{2,5}", "'git push' ", "[gp]", "-> Push data to branch");
        Console.WriteLine("{0,-25}{1,-6}{2,5}", "'git branch data' ", "[gbd]", "-> Get stored data on the branch");
        Console.WriteLine("{0,-25}{1,-6}{2,5}", "'git branch' ", "[gb]", "-> Create new branch");
        Console.WriteLine("{0,-25}{1,-6}{2,5}", "'git checkout' ", "[gch]", "-> Checkout to another branch");
        Console.WriteLine("{0,-25}{1,-6}{2,5}", "'git exit' ", "[ge]", "-> Quit GitHub");
        Console.WriteLine("-----------------");
    }

    public static ArrayList fetchDataFromFile()
    {
        ArrayList user_list = new ArrayList();
        using (XmlReader reader = XmlReader.Create(@"database/users.xml"))
        {
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    if (reader.Name == "user")
                    {
                        int count_of_branches = 0;
                        User user = new User();
                        XmlReader subreader = reader.ReadSubtree();
                        while (subreader.Read())
                        {
                            if (reader.GetAttribute("username") != null)
                            {
                                user.name = reader.GetAttribute("username");
                            }
                            if (reader.GetAttribute("branchName") != null)
                            {
                                user.addBranch(reader.GetAttribute("branchName"));
                                currentBranch = (Branch)user.listOfBranch[count_of_branches];
                                XmlReader branch_reader = reader.ReadSubtree();
                                while (branch_reader.Read())
                                {
                                    if (reader.GetAttribute("bodyMessage") != null)
                                    {
                                        currentBranch.branchData.Add(new Message(reader.GetAttribute("bodyMessage")));
                                    }
                                }
                                count_of_branches += 1;
                            }
                        }
                        user_list.Add(user);
                    }
                }
            }
        }
        return user_list;
    }

    public static User assignUser(string inputName, ArrayList user_list)
    {
        User user = null;
        foreach (User fetched_user in user_list)
        {
            System.Console.WriteLine(fetched_user.name + "==" + inputName);
            if (fetched_user.name == inputName)
            {
                user = fetched_user;
                System.Console.WriteLine(user.name);
                foreach (Branch branch in user.listOfBranch)
                {
                    System.Console.WriteLine(branch.name);
                    foreach (Message msg in branch.messages)
                    {
                        System.Console.WriteLine(msg.text);
                    }
                }
                return user;
            }
            else
            {
                user = new User(inputName);
                //CREATE MASTER BRANCH FOR NEW USER
                user.addBranch("master");
            }
        }
        return user;
    }

    // public static void saveDataToFile()
    // { //branches, users, message
    //     Console.WriteLine("Writing, to file!");

    //     XmlWriterSettings set = new XmlWriterSettings();
    //     set.Indent = true; //nastaveni odrazeni

    //     using (XmlWriter xw = XmlWriter.Create(@"users.xml", set))
    //     {
    //         //Zalozeni dokumentu??
    //         xw.WriteStartDocument();
    //         xw.WriteStartElement("users");
    //         for (int i = 0; i < users.Length; i++)
    //         {

    //             //pridani hodnot do dokumentu
    //             xw.WriteStartElement("user");
    //             xw.WriteAttributeString("username", users[i]);

    //             xw.WriteStartElement("branches");
    //             for (int j = 0; j < branches.Length; j++)
    //             {
    //                 xw.WriteStartElement("branch");
    //                 xw.WriteAttributeString("branchName", branches[j]);
    //                 xw.WriteStartElement("messages");
    //                 for (int k = 0; k < messages.Length; k++)
    //                 {
    //                     xw.WriteStartElement("message");
    //                     xw.WriteAttributeString("bodyMessage", messages[k]);
    //                     xw.WriteEndElement();
    //                 }
    //                 xw.WriteEndElement();
    //                 xw.WriteEndElement();
    //             }
    //             xw.WriteEndElement();
    //             xw.WriteEndElement();

    //         }
    //         xw.WriteEndElement();
    //         xw.WriteEndDocument();
    //         xw.Flush();
    //     }
    // }
}