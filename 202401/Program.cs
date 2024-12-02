var input = File.ReadAllText("./Input.txt");

var lines = input.Split('\n');
var left = new List<int>();
var right = new List<int>();
foreach (var line in lines)
{
    var values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries );
    left.Add(int.Parse(values[0]));
    right.Add(int.Parse(values[1]));
}

left = left.OrderBy(x => x).ToList();
right = right.OrderBy(x => x).ToList();

// part 1
long total = 0;
for (int i = 0; i < left.Count; i++)
{
    total += Math.Abs(right[i] - left[i]);
}

Console.WriteLine($"Part 1: {total}");


// part 2


long total2 = 0;
for (int i = 0; i < left.Count; i++)
{
    total2 += left[i] * right.Count(x => x == left[i]) ;
}

Console.WriteLine($"Part 2: {total2}");