var input = File.ReadAllText("./Input.txt");

//input = @"47|53
//97|13
//97|61
//97|47
//75|29
//61|13
//75|53
//29|13
//97|29
//53|29
//61|53
//97|53
//61|29
//47|13
//75|47
//97|75
//47|61
//75|61
//47|29
//75|13
//53|13

//75,47,61,53,29
//97,61,53,29,13
//75,29,13
//75,97,47,61,53
//61,13,29
//97,13,75,29,47";

var lines = input.Split('\n').Select(s => s.Trim()).ToArray();

var rules = new List<int[]>();
var pageLists = new List<int[]>();

var currentList = rules;
var separator = '|';
foreach (var line in lines)
{

    if (string.IsNullOrWhiteSpace(line))
    {
        currentList = pageLists;
        separator = ',';
        continue;
    }
    currentList.Add(line.Split(separator).Select(s => int.Parse(s)).ToArray());
}

Console.WriteLine($"Rules count:{rules.Count}");
Console.WriteLine($"pagelists count:{pageLists.Count}");

int outputSum = 0;
foreach (var pagelist in pageLists)
{
    var valid = true;
    for(int i = 0; i< pagelist.Length; i++)
    {
        var applicableRules = rules.Where(r => r[1] == pagelist[i]).ToArray();
        if (applicableRules.Length > 0)
        {
            foreach (var rule in applicableRules) {
                for (int r = i; r < pagelist.Length; r++) { 
                    if (rule[0] == pagelist[r])
                    {
                        valid = false;
                        break;
                    }
                }
                if (!valid)
                {
                    break;
                }
            }
            if (!valid)
            {
                break;
            }
        }            
    }

    if (valid)
    {
        Console.WriteLine(string.Join(',', pagelist));
        var middle = (int)Math.Floor((double)pagelist.Length / 2);
        Console.WriteLine($"middle value: {pagelist[middle]}");
        outputSum += pagelist[middle];
    }

}

Console.WriteLine($"Day 5 Part 1: {outputSum}");
