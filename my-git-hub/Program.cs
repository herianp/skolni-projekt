using System.Collections;

class Program
{
    public static Branch currentBranch;
    static void Main(string[] args)
    {
        Console.WriteLine("Ahoj");
        XMLWrite.WriteXML(new User("Petr"));
        // //STARTING APP WITH FEW PRINTS AND INPUT
        // Console.WriteLine("Welcome on GitHub");
        // System.Threading.Thread.Sleep(500);
        // Console.WriteLine("Enter your name:");
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
