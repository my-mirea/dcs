using System;

class PetriDish {

    private int bacteries, antibiotics;
    private int antibioticStrength = 10;
    private int hours = 0;

    public PetriDish(int bacteries, int antibiotics) {
        this.bacteries = bacteries;
        this.antibiotics = antibiotics;
        
        if (bacteries < 0 || antibiotics < 0) {
            throw new Exception("Initial counts must be positive");
        }
    }

    public int leftHours() {
        return hours;
    }

    public int getBacteries() {
        return bacteries;
    }

    public bool tick() {
        if (bacteries <= 0 || antibioticStrength <= 0) {
            return false;
        }

        bacteries *= 2;
        bacteries -= antibiotics * antibioticStrength;
        if (antibioticStrength > 0) {
            antibioticStrength--;
        }

        hours++;

        return true;
    }

    public static void Main() {
        int bacteries, antibiotics;

        try {
            Console.Write("Enter initial bacteries count: ");
            bacteries = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter initial antibiotics count: ");
            antibiotics = Convert.ToInt32(Console.ReadLine());
        } catch(FormatException ex) {
            Console.WriteLine("Invalid input");
            return;
        }

        PetriDish dish = new PetriDish(bacteries, antibiotics);

        while (dish.tick()) {
            Console.WriteLine($"After {dish.leftHours()} hours bacteries count: {dish.getBacteries()}");
        }
    }

}