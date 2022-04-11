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
        //FETCH DATA FROM FILE
        ArrayList fetched_users = Functions.fetchDataFromFile();
        //ASSIGN USER
        User user = Functions.assignUser(inputName, fetched_users);
        Boolean flag = true;
        currentBranch = (Branch)user.listOfBranch[0];
        //APP
        while (flag)
        {
            Console.WriteLine("USER: " + user.name + ", BRANCH: " + currentBranch.name);
            Functions.basicInfo();
            string input = Console.ReadLine();
            Console.Clear();
            currentBranch = Functions.signpost(input, user, currentBranch, fetched_users);
            Console.Clear();
        }
    }
}
