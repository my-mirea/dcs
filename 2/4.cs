using System;

class Guessor {
    public static void Main() {
        int number = guessNumber(0, 64);
        Console.WriteLine($"Guessed number: {number}");
    }

    public static int guessNumber(int start, int end) {
        if (Math.Abs(start - end) == 1) {
            return end;
        }
        
        int half = (end - start) / 2;
        
        Console.Write($"Is number bigger then {start + half} ({start}..{end} with {half}): ");
        string result = Console.ReadLine();
        if (result == "yes" || result == "1") {
            return guessNumber(start + half, end);
        }
        
        return guessNumber(start, end - half);
    }
    
}
