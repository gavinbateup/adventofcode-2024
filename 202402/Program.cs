using System.Runtime.CompilerServices;

var input = File.ReadAllText("./Input.txt");

//input = @"7 6 4 2 1
//1 2 7 8 9
//9 7 6 2 1
//1 3 2 4 5
//8 6 4 4 1
//1 3 6 7 9";


var lines = input.Split('\n');

var safeReportCount = 0;

foreach (var line in lines)
{
    var values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
    var safe = true;
    var increasing = false;
    for (int i = 0; i < values.Length; i++)
    {
        
        if (i > 0)
        {
            var diff = Math.Abs(values[i] - values[i - 1]);
            if (diff > 3 || diff <1)
            {
                safe = false;
                continue;
            }

            if (i == 1)
            {
                increasing = values[i] > values[i - 1];
            }
            else
            {
                if (increasing && values[i] < values[i-1]) {
                    safe = false;
                    continue;
                }
                if (!increasing && values[i] > values[i - 1])
                {
                    safe = false;
                    continue;
                }
            }
        }
    }

    if (safe)
        safeReportCount++;
}

Console.WriteLine($"Day 2 Part 1 : {safeReportCount}");

var isSafe = (int[] values) => {

    var safe = true;
    var increasing = false;
    for (int i = 0; i < values.Length; i++)
    {

        if (i > 0)
        {
            var diff = Math.Abs(values[i] - values[i - 1]);
            if (diff > 3 || diff < 1)
            {
                safe = false;
                continue;
            }

            if (i == 1)
            {
                increasing = values[i] > values[i - 1];
            }
            else
            {
                if (increasing && values[i] < values[i - 1])
                {
                    safe = false;
                    continue;
                }
                if (!increasing && values[i] > values[i - 1])
                {
                    safe = false;
                    continue;
                }
            }
        }
    }
    return safe;
};


safeReportCount = 0;

foreach (var line in lines)
{
    var values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();


    if (isSafe(values))
    {
        safeReportCount++;
    }
    else
    {
         for (int i = 0; i < values.Length; i++)
        {
            var newlist = values.Take(i).ToArray().Concat(values.Skip(i+1).ToArray()).ToArray();
            if (isSafe(newlist))
            {
                safeReportCount++;
                break;
            }
        }
    }
}

Console.WriteLine($"Day 2 Part 2 : {safeReportCount}");

