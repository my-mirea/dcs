using System;
 
public class LuckyTicket {
    public static void Main(string[] args) {
        Console.Write("Enter number: ");
        int input = 0;
        try {
            input = Convert.ToInt32(Console.ReadLine());
        } catch(FormatException ex) {
            Console.WriteLine("Invalid number");
            return;
        }
        
        if (!isValid(input)) {
            Console.WriteLine("Invalid input number (reuqired length 6)");
            return;
        }
 
        int first = splitInt(input, false);
        int second = splitInt(input, true);
 
        int firstSum = sumOfDecimals(first);
        int secondSum = sumOfDecimals(second);
 
        Console.WriteLine($"First: {first}, Second: {second}");
        Console.WriteLine($"First sum: {firstSum}, Second sum: {secondSum}");
        Console.WriteLine(firstSum == secondSum ? "Lucky ticket" : "Unluck bro");
    }
 
    public static bool isValid(int input) {
        return input >= 100_000 && input < 1_000_000;
    }
 
    public static int sumOfDecimals(int input) {
        int sum = 0;
        while (input > 0) {
            sum += input % 10;
            input /= 10;
        }
        return sum;
    }
 
    public static int splitInt(int origin, bool rem) {
        return rem ? origin % 1000 : origin / 1000;
    }
 
}