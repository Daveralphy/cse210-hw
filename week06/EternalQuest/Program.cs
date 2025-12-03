// To exceed the requirements for this assignment:
// I added levels to help users feel progress.
// I added XP to reward users for completing goals.
// I added a daily streak system to encourage users to keep coming back every day.
// I also added automatic saving and loading so progress stays after closing the app.

using System;

class Program
{

    static void Main(string[] args)
    {
        GoalManager gm = new GoalManager();

        gm.AutoLoad();    
        gm.Start();
        gm.AutoSave();
    }
}
