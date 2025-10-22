using System;

class Calendar {

    public static void Main() {
        Console.Write("Select kind of first week day (1 - mon, ..., 1 - sun): ");
        int firstDay;
        try {
            firstDay = Convert.ToInt32(Console.ReadLine()) - 1;
            if (firstDay < 0 || firstDay >= 7) {
                throw new Exception("Unknown kind of week day");
            }

        } catch(Exception e) {
            Console.WriteLine("Invalid week day number");
            return;
        }


        Console.Write("Enter day of month: ");
        int dayOfMonth;
        try {
            dayOfMonth = Convert.ToInt32(Console.ReadLine()) - 1;
            if (firstDay < 0 || firstDay >= 31) {
                throw new Exception("Illegal number of month day");
            }

        } catch(Exception e) {
            Console.WriteLine("Invalid month day number");
            return;
        }

        Console.WriteLine(isWeekend(firstDay, dayOfMonth) ? "Weeeeeeekend!" : "Oh.. Regular working day");
    }

    public static bool isWeekend(int firstDayType, int dayOfMonth) {
        if (dayOfMonth > 0 && dayOfMonth < 5 || dayOfMonth > 7 && dayOfMonth < 10) {
            return true;
        }

        return (firstDayType + dayOfMonth) % 7 >= 5;
    }

}