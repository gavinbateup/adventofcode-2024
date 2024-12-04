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
            for(int dir = 0; dir < 8; dir++)
            {
                xmasCount += IsXmas(x, y, lines, (Direction)dir) ? 1 : 0;
            }
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
            var ne = GetCharInDirection(Direction.NorthEast, x, y, lines);
            var sw = GetCharInDirection(Direction.SouthWest, x, y, lines);

            if ((ne == 'M' && sw == 'S') || (ne == 'S' && sw == 'M'))
            {
                mas1 = true;
            }
            var nw = GetCharInDirection(Direction.NorthWest, x, y, lines);
            var se = GetCharInDirection(Direction.SouthEast, x, y, lines);

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
bool IsXmas(int x, int y, string[] lines, Direction direction)
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

char GetCharInDirection(Direction direction, int x, int y, string[] lines)
{
    var valid = true;
    (y, x, valid) = NextPos(x, y, direction, lines.Length, lines[0].Length);
    return valid ? lines[y][x] : '.';    
}

(int x, int y, bool valid) NextPos(int x, int y, Direction direction, int maxY, int maxX)
{
    switch (direction)
    {
        case Direction.North:
            return (y - 1, x, y > 0);
        case Direction.South:
            return (y + 1, x, y < maxY-1);
        case Direction.East:
            return (y, x + 1, x < maxX-1);
        case Direction.West:
            return (y, x - 1, x > 0);

        case Direction.NorthEast:
            return (y - 1, x + 1, y > 0 && x < maxX-1);
        case Direction.SouthEast:
            return (y + 1, x + 1, y < maxY-1 && x < maxX-1);

        case Direction.NorthWest:
            return (y - 1, x - 1, y > 0 && x > 0);
        case Direction.SouthWest:
            return (y + 1, x - 1, y < maxY-1 && x > 0);
    }
    return (x, y, false);
}

public enum Direction
{
    North = 0,
    NorthEast = 1,
    East = 2,
    SouthEast = 3,
    South = 4,
    SouthWest = 5,
    West = 6,
    NorthWest = 7

}