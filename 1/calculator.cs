using System;

class Calculator {

    private double?[] stack = new double?[2];
    private double? memory = null;

    public double? getMemory() {
        return memory;
    }

    public bool restoreMemory() {
        if (memory == null) {
            return false; 
        }

        push(memory);
        return true;
    }

    public bool storeMemory() {
        double? value = pop();
        if (value == null) {
            return false;
        }
        
        memory = value;
        return true;
    }

    public bool resetMemory() {
        if (memory == null) {
            return false;
        }

        memory = null;
        return true;
    }

    public bool ensureStack(int size) {
        int available = 0;
        foreach (double? element in stack) {
            if (element != null) {
                available++;
            }
        }
        return available >= size;
    }

    public void push(double? value) {
        if (stack[0] == null) {
            push(0, value);
        } else {
            push(1, value);
        }
    }

    public void push(int index, double? value) {
        stack[index] = value;
    }

    public bool canPush() {
        return stack[0] == null || stack[1] == null;
    }

    public double? pop() {
        double? value = take(1);
        if (value == null) {
            value = take(0);
        }

        return value;
    }

    public double? take(int index) {
        double? value = stack[index];
        if (value != null) {
            stack[index] = null;
        }

        return value;
    }

    public double? peek() {
        double? value = peek(1);
        if (value == null) {
            value = peek(0);
        }

        return value;
    }

    public double? peek(int index) {
        double? value = stack[index];
        
        return value;
    }

    public static void Main() {
        Console.WriteLine("Available operations: +, -, *, /, %, ^ (pow), ~(sqrt of 2), M+, M-, MR, R");
        Calculator calculator = new Calculator();
        
        while (true) {
            Console.WriteLine($"Current state: [{calculator.peek(0)}, {calculator.peek(1)}], memory={calculator.getMemory()}");
            Console.Write(calculator.canPush() ? "Enter operation or number: " : "Enter operation: ");
            string input = Console.ReadLine();

            if (calculator.canPush()) {
                try {
                    int numberToPush;
                    numberToPush = Convert.ToInt32(input);
                    calculator.push(numberToPush);
                    continue;
                } catch(Exception ex) {
                    
                }
            }

            switch(input) {
                case "R": {
                    calculator.push(0, null);
                    calculator.push(1, null);
                    break;
                }
                case "M+": {
                    if (!calculator.storeMemory()) {
                        Console.WriteLine("Error: No value to store memory");
                    }
                    break;
                }
                case "M-": {
                    if (!calculator.restoreMemory()) {
                        Console.WriteLine("Error: No value to restore from memory");
                    }
                    break;
                }
                case "MR": {
                    if (!calculator.resetMemory()) {
                        Console.WriteLine("Error: Memory already empty");
                    }
                    break;
                }
                case "+": {
                    if (ensureStack(calculator, 2)) {
                        calculator.push((calculator.pop() ?? 0) + (calculator.pop() ?? 0));
                    }
                    break;
                }
                case "-": {
                    if (ensureStack(calculator, 2)) {
                        calculator.push((calculator.take(0) ?? 0) - (calculator.take(1) ?? 0));
                    }
                    break;
                }
                case "*": {
                    if (ensureStack(calculator, 2)) {
                        calculator.push((calculator.pop() ?? 0) * (calculator.pop() ?? 0));
                    }
                    break;
                }
                case "/": {
                    if (ensureStack(calculator, 2)) {
                        if (calculator.peek(1) == 0) {
                            Console.WriteLine("Error: Division by zero");
                            continue;
                        }

                        calculator.push((calculator.take(0) ?? 0) / (calculator.take(1) ?? 1));
                    }
                    break;
                }
                case "%": {
                    if (ensureStack(calculator, 2)) {
                        calculator.push((calculator.take(0) ?? 0) * (calculator.take(1) ?? 0) / 100.0);
                    }
                    break;
                }
                case "^": {
                    if (ensureStack(calculator, 1)) {
                        double value = calculator.take(0) ?? 0;
                        double pow = calculator.take(1) ?? 2;

                        calculator.push(Math.Pow(value, pow));
                    }
                    break;
                }
                case "~": {
                     if (ensureStack(calculator, 1)) {
                        double value = calculator.pop() ?? 0;
                        if (value < 0) {
                            Console.WriteLine("Error: Sqrt value must be positive");
                            continue;
                        }

                        calculator.push(Math.Sqrt(value));
                    }
                    break;
                }
                default: {
                    Console.WriteLine($"Unknown operation or unsupported number: {input}");
                    break;
                }
            }
        }
    }

    public static bool ensureStack(Calculator calculator, int size) {
        if (calculator.ensureStack(size)) {
            return true;
        }

        Console.WriteLine("Error: Missing values for operation");
        return false;
    }

}