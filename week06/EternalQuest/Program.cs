// To exceed the requirements for this assignment:
// I added levels to help users feel progress.
// I added XP to reward users for completing goals.
// I added a daily streak system to encourage users to keep coming back every day.
// I also added automatic saving and loading so progress stays after closing the app.

using System;

using System;

class Program
{
    // Exceeded requirements:
    // I added levels. This helps the user feel progress.
    // I added XP. This rewards the user for completing goals.
    // I added a daily streak system. This encourages the user to keep coming back every day.
    // I added automatic loading and saving so progress does not reset.

    static void Main(string[] args)
    {
        GoalManager gm = new GoalManager();

        gm.AutoLoad();
        gm.Start();
        gm.AutoSave();
    }
}
