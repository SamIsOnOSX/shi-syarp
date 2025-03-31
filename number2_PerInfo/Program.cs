using System;

namespace PersonalInfoApp
{
    class Program
    {
        static void Main()
        {
            Console.Write("Ibutang imong pangalan:");
            string userName = Console.ReadLine();
            
            Console.Write($"Bossing, {userName}! sunod, please tell me your age:");
            
            string userAge = Console.ReadLine();
            Console.Write($"Grabe {userAge} na diay ka, karon unsa imong favorite color?");
            
            string favoriteColor = Console.ReadLine();
            Console.WriteLine($"Sae diay ta boss {favoriteColor}.");
        }
    }
}
