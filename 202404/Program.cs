using _202404;

var input = File.ReadAllText("./Input.txt");

//input = @"MMMSXXMASM
//MSAMXMSMSA
//AMXSXMAAMM
//MSAMASMSMX
//XMASAMXAMM
//XXAMMXXAMA
//SMSMSASXSS
//SAXAMASAAA
//MAMMMXMMMM
//MXMXAXMASX";

var lines = input.Split('\n').Select(s => s.Trim()).ToArray();

int xmasCount = 0;
for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[y].Length; x++)
    {
        var ch = lines[y][x];
        if (ch == 'X')
        {

            xmasCount += IsXmas(x, y, lines, direction.East) ? 1 : 0;
            xmasCount += IsXmas(x, y, lines, direction.West) ? 1 : 0;
            xmasCount += IsXmas(x, y, lines, direction.North) ? 1 : 0;
            xmasCount += IsXmas(x, y, lines, direction.South) ? 1 : 0;
            xmasCount += IsXmas(x, y, lines, direction.NorthEast) ? 1 : 0;
            xmasCount += IsXmas(x, y, lines, direction.NorthWest) ? 1 : 0;
            xmasCount += IsXmas(x, y, lines, direction.SouthEast) ? 1 : 0;
            xmasCount += IsXmas(x, y, lines, direction.SouthWest) ? 1 : 0;


        }
    }
}

Console.WriteLine($"Day 4 Part 1: {xmasCount}");

// Part 2
var masXCount = 0;
for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[y].Length; x++)
    {
        var ch = lines[y][x];
        if (ch == 'A')
        {
            var mas1 = false; var mas2 = false;
            // north east and southWest
            var ne = GetCharInDirection(direction.NorthEast, x, y, lines);
            var sw = GetCharInDirection(direction.SouthWest, x, y, lines);

            if ((ne == 'M' && sw == 'S') || (ne == 'S' && sw == 'M'))
            {
                mas1 = true;
            }
            var nw = GetCharInDirection(direction.NorthWest, x, y, lines);
            var se = GetCharInDirection(direction.SouthEast, x, y, lines);

            if ((nw == 'M' && se == 'S') || (nw == 'S' && se == 'M'))
            {
                mas2 = true;
            }

            masXCount += mas1 && mas2 ? 1 : 0;

        }
    }
}

Console.WriteLine($"Day 4 Part 2: {masXCount}");

char NextChar(char ch)
{
    switch (ch)
    {
        case 'X':
            return 'M';
        case 'M':
            return 'A';
        case 'A':
            return 'S';
    }
    throw new NotImplementedException();
}
bool IsXmas(int x, int y, string[] lines, direction direction)
{
    var currentChar = 'X';
    var valid = true;
    for (int i = 0; i < 3; i++)
    {
        (y, x, valid) = NextPos(x, y, direction, lines.Length, lines[0].Length);
        currentChar = NextChar(currentChar);
        if (!valid || currentChar != lines[y][x])
            return false;
    }
    return true;
}

char GetCharInDirection(direction direction, int x, int y, string[] lines)
{
    var valid = true;
    (y, x, valid) = NextPos(x, y, direction, lines.Length, lines[0].Length);
    return valid ? lines[y][x] : '.';    
}

(int x, int y, bool valid) NextPos(int x, int y, direction direction, int maxY, int maxX)
{
    switch (direction)
    {
        case direction.North:
            return (y - 1, x, y > 0);
        case direction.South:
            return (y + 1, x, y < maxY-1);
        case direction.East:
            return (y, x + 1, x < maxX-1);
        case direction.West:
            return (y, x - 1, x > 0);

        case direction.NorthEast:
            return (y - 1, x + 1, y > 0 && x < maxX-1);
        case direction.SouthEast:
            return (y + 1, x + 1, y < maxY-1 && x < maxX-1);

        case direction.NorthWest:
            return (y - 1, x - 1, y > 0 && x > 0);
        case direction.SouthWest:
            return (y + 1, x - 1, y < maxY-1 && x > 0);
    }
    return (x, y, false);
}