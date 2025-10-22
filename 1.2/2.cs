using System;

class ATM {

    public static void Main() {
        Console.Write("Enter amount to withdraw: ");
        int amount;
        try {
            amount = Convert.ToInt32(Console.ReadLine());
            if (amount <= 0 || amount >= 150_000 || amount % 100 != 0) {
                throw new Exception("Illegal withdraw amount");
            }

        } catch(Exception e) {
            Console.WriteLine("Illegal withdraw amount");
            return;
        }

        int[] nominalos = {5000, 2000, 1000, 500, 200, 100};
        foreach (int nominalo in nominalos) {
            amount = withdraw(amount, nominalo);
        }

        if (amount > 0) {
            Console.WriteLine($"Something went wrong... Remain {amount}");
        }
    }

    public static int withdraw(int amount, int nominalo) {
        int taken = 0;
        while (amount >= nominalo) {
            taken++;
            amount -= nominalo;
        }

        if (taken > 0) {
            Console.WriteLine($"x{taken} {nominalo}");
        }

        return amount;
    }

}