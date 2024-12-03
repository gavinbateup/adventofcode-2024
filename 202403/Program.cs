using System.Text.RegularExpressions;

var input = File.ReadAllText("./Input.txt");

//input = @"xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

var mulstatements = Regex.Matches(input, "mul\\(\\d{1,3},\\d{1,3}\\)");
var doStatements = Regex.Matches(input, "do\\(\\)");
var dontStatements = Regex.Matches(input, "don't\\(\\)");

var total = 0;

foreach (var mulstatement in mulstatements)
{


    var values = mulstatement.ToString().Replace("mul(", "").Replace(")", "").Split(',');

    total += int.Parse(values[0]) * int.Parse(values[1]);

}

Console.WriteLine($"Day 3 Part 1: {total}");

// part 2
//input = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

mulstatements = Regex.Matches(input, "mul\\(\\d{1,3},\\d{1,3}\\)");
doStatements = Regex.Matches(input, "do\\(\\)");
dontStatements = Regex.Matches(input, "don't\\(\\)");
total = 0;

var letsDoIt = true;

var statements = mulstatements.Select(s => new { position = s.Index, value = s.Value })
    .Union(doStatements.Select(s => new { position = s.Index, value = s.Value }))
    .Union(dontStatements.Select(s => new { position = s.Index, value = s.Value }))
    .OrderBy(s => s.position);

foreach (var statement in statements)
{

    switch (statement.value)
    {
        case "do()":
            letsDoIt = true;
            break;
        case "don't()":
            letsDoIt = false;
            break;
        default:
            if (letsDoIt)
            {
                var values = statement.value.Replace("mul(", "").Replace(")", "").Split(',');

                total += int.Parse(values[0]) * int.Parse(values[1]);
            }
            break;
    }

}

Console.WriteLine($"Day 3 Part 2: {total}");