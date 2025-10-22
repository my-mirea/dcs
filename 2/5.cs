using System;

class CaffeeMachine {


    private int water, milk;
    private int makedLatte, makedAmericano;
    private int earned;

    public CaffeeMachine(int water, int milk) {
        if (water < 0 || milk < 0) {
            throw new Exception("Initial capacities must be positive");
        }

        this.water = water;
        this.milk = milk;
    }

    public void dump() {
        Console.WriteLine("--------------------------------");
        Console.WriteLine($"Remaining water: {water}");
        Console.WriteLine($"Remaining milk: {milk}");
        Console.WriteLine($"Maked latte: {makedLatte}");
        Console.WriteLine($"Maked americano: {makedAmericano}");
        Console.WriteLine($"Earned: {earned}");
        Console.WriteLine("--------------------------------");
    }

    private void make(int water, int milk, int price) {
        if (this.water < water) {
            throw new Exception("Not enough water");
        }
        if (this.milk < milk) {
            throw new Exception("Not enough milk");
        }
        
        this.water -= water;
        this.milk -= milk;
        this.earned += price;
    }

    public void makeLatte() {
        make(300, 0, 150);
        makedLatte++;
    }
    
    public void makeAmericano() {
        make(270, 30, 170);
        makedAmericano++;
    }

    public bool shouldWork() {
        return water >= 300 || water >= 270 && milk >= 30;
    }

    public static void Main() {
        int water, milk;

        try {
            Console.Write("Enter initial water capacity: ");
            water = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter initial milk capacity: ");
            milk = Convert.ToInt32(Console.ReadLine());
        } catch(FormatException ex) {
            Console.WriteLine("Invalid initiali capacities input");
            return;
        }

        CaffeeMachine machine = new CaffeeMachine(water, milk);
        while (machine.shouldWork()) {
            Console.Write("Enter caffee type (latte - 1, americano - 2): ");
            int type = Convert.ToInt32(Console.ReadLine());
            try {
                if (type == 1) {
                    machine.makeLatte();
                } else if (type == 2) {
                    machine.makeAmericano();
                } else {
                    throw new Exception("Invalid caffee type");
                }
                Console.WriteLine("Caffee made successfully");
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        machine.dump();
    }

}