// See husing System.Drawing;

using System.Reflection.Metadata.Ecma335;

var input = File.ReadAllText("./Input.txt");

//input = @"190: 10 19
//3267: 81 40 27
//83: 17 5
//156: 15 6
//7290: 6 8 6 15
//161011: 16 10 13
//192: 17 8 14
//21037: 9 7 18 13
//292: 11 6 16 20";

var lines = input.Split('\n').Select(s => s.Trim()).ToArray();

long count = 0;
long countPart2 = 0;
foreach (var line in lines)
{
    var eq = new Equation(line);
    if (eq.IsValid())
    {
        count += eq.Value;
    }

    if (eq.IsValidPart2())
    {
        countPart2 += eq.Value;
    }
}

Console.WriteLine($"Day 7 Part 1: {count}");
Console.WriteLine($"Day 7 Part 2: {countPart2}");

public class Equation
{
    public Equation(string line)
    {
        var split = line.Split(':', StringSplitOptions.TrimEntries);
        Value = long.Parse(split[0]);
        Numbers = split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)).ToArray();
    }
    public long Value { get; set; }
    public long[] Numbers { get; set; }

    public bool IsValid()
    {
        var tempVal = Numbers[0];
        var result = ApplyNext(tempVal, new Span<long>(Numbers, 1, Numbers.Length - 1).ToArray());

        return result == Value;
    }

    long ApplyNext(long value, long[] numbers)
    {
        if (value > Value)
            return value;

        var plus = numbers[0] + value;
        var multiply = numbers[0] * value;

        if (numbers.Length == 1)
        {
            if (plus == Value)
                return Value;

            if (multiply == Value)
                return Value;

            return -1;
        }
        else
        {
            var partA = ApplyNext(plus, new Span<long>(numbers, 1, numbers.Length - 1).ToArray());
            if (partA == Value)
                return partA;

            var partB = ApplyNext(multiply, new Span<long>(numbers, 1, numbers.Length - 1).ToArray());
            if (partB == Value)
                return partB;

            return -1;

        }
    }

    public bool IsValidPart2()
    {
        var tempVal = Numbers[0];
        var result = ApplyNextPart2(tempVal, new Span<long>(Numbers, 1, Numbers.Length - 1).ToArray());

        return result == Value;
    }
    long ApplyNextPart2(long value, long[] numbers)
    {
        if (value > Value)
            return value;

        var plus = numbers[0] + value;
        var multiply = numbers[0] * value;
        var concat = long.Parse(value.ToString() + numbers[0].ToString());


        if (numbers.Length == 1)
        {
            if (plus == Value)
                return Value;

            if (multiply == Value)
                return Value;

            if (concat == Value)
                return Value;

            return -1;
        }
        else
        {
            var partA = ApplyNextPart2(plus, new Span<long>(numbers, 1, numbers.Length - 1).ToArray());
            if (partA == Value)
                return partA;

            var partB = ApplyNextPart2(multiply, new Span<long>(numbers, 1, numbers.Length - 1).ToArray());
            if (partB == Value)
                return partB;

            var partC = ApplyNextPart2(concat, new Span<long>(numbers, 1, numbers.Length - 1).ToArray());
            if (partC == Value)
                return partC;

            return -1;

        }
    }
}