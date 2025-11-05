using System;

class Program
{
    static void Main(string[] args)
    {
        Person person = new Person();
        person._givenName = "Raphael";
        person._familyName = "Daveal";

        person.ShowEasternName();
        person.ShowWesternName();
    }
}
