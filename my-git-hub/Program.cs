using System.Collections;

class Program
{
    public static Branch currentBranch;
    static void Main(string[] args)
    {
        //STARTING APP WITH FEW PRINTS AND INPUT
        Console.WriteLine("Welcome on GitHub");
        System.Threading.Thread.Sleep(500);
        Console.WriteLine("Enter your username:");
        string inputName = Console.ReadLine();
        TimeClass.loadingFunction("Creating Github account");
        User user;
        //FETCH DATA FROM FILE
        ArrayList fetched_users = Functions.fetchDataFromFile();
        foreach (User fetched_user in fetched_users)
        {
            // System.Console.WriteLine(fetched_user.name + "==" + inputName);
            if (fetched_user.name == inputName)
            {
                user = fetched_user;
                // System.Console.WriteLine(user.name);
                foreach (Branch branch in user.listOfBranch)
                {
                    // System.Console.WriteLine(branch.name);
                    foreach (Message msg in branch.messages)
                    {
                        // System.Console.WriteLine(msg.text);
                    }
                }
            } else {
                user = new User(inputName);
                //CREATE MASTER BRANCH FOR NEW USER
                user.addBranch("master");
            }
        }
        // //STARTING APP WITH FEW PRINTS AND INPUT
        // Console.WriteLine("Welcome on GitHub");
        // System.Threading.Thread.Sleep(500);
        // Console.WriteLine("Enter your username:");
        // string inputName = Console.ReadLine();
        // //TODO CHECK IF USER EXISTS AND LOAD DATA FROM XML FILE
        // User user = new User(inputName);
        // //CREATE MASTER BRANCH FOR NEW USER
        // user.addBranch("master");
        // TimeClass.loadingFunction("Creating Github account");
        // Boolean flag = true;
        // currentBranch = (Branch)user.listOfBranch[0];
        // //APP
        // while (flag)
        // {
        //     Console.WriteLine("USER: " + user.name + ", BRANCH: " + currentBranch.name);
        //     Functions.basicInfo();
        //     string input = Console.ReadLine();
        //     Console.Clear();
        //     currentBranch = Functions.signpost(input, user, currentBranch);
        //     Console.Clear();
        // }
        // Environment.Exit(0);
    }
}
