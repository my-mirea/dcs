using System;

class Reduction {
    public static void Main() {
        int nominator, denominator;
        
        try {
            Console.Write("Enter nominator: ");
            nominator = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter denominator: ");
            denominator = Convert.ToInt32(Console.ReadLine());
        } catch(FormatException ex) {
            Console.WriteLine("Invalid nominator or denominator number");
            return;
        }

        if (denominator == 0) {
            Console.WriteLine("Invalid denominator (requires non-zero)");
            return;
        }

        int divider = calcBiggestDivider(Math.Abs(nominator), Math.Abs(denominator));

        nominator /= divider;
        denominator /= divider;

        if (denominator < 0) {
            // If denominator
            nominator = -nominator;
            denominator = -denominator;
        }

        Console.WriteLine($"Result: {nominator} / {denominator}");
    }

    public static int calcBiggestDivider(int a, int b){
        while (b != 0) {
            int temp = b;
            b = a % b;
            a = temp;
        }
        
        return a;
    }
    
}
