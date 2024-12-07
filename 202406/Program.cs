using System.Drawing;

var input = File.ReadAllText("./Input.txt");

//input = @"....#.....
//.........#
//..........
//..#.......
//.......#..
//..........
//.#..^.....
//........#.
//#.........
//......#...";

var lines = input.Split('\n').Select(s => s.Trim()).ToArray();

var map = new MapPoint[lines[0].Length, lines.Length];

(int x, int y) currentPos = (0, 0);

var currentDirection = Direction.North;

int positionCount = 0;

for (int y = 0; y < lines.Length; y++)
{
    var line = lines[y];
    for (int x = 0; x < line.Length; x++)
    {
        map[x, y] = new MapPoint { Block = line[x] == '#' };
        if (line[x] != '#' && line[x] != '.')
        {
            currentPos = (x, y);
        }
    }
}

while (true)
{
    positionCount += map[currentPos.x, currentPos.y].Touched ? 0 : 1;

    map[currentPos.x, currentPos.y].Touched = true;
    var (y, x, valid) = NextPos(currentPos.x, currentPos.y, currentDirection, lines[0].Length, lines.Length);

    if (!valid)
    {
        break;
    }

    if (map[x, y].Block)
    {
        currentDirection = NextDirection(currentDirection);
    }
    else
    {
        currentPos.x = x;
        currentPos.y = y;
    }

}

Console.WriteLine($"Day 6 Part 1: {positionCount}");

Direction NextDirection(Direction dir)
{
    switch (dir)
    {
        case Direction.North:
            return Direction.East;
        case Direction.South:
            return Direction.West;
        case Direction.West:
            return Direction.North;
        case Direction.East: 
            return Direction.South;
    }
    throw new ArgumentOutOfRangeException();
}


(int y, int x, bool valid) NextPos(int x, int y, Direction direction, int maxY, int maxX)
{
    switch (direction)
    {
        case Direction.North:
            return (y - 1, x, y > 0);
        case Direction.South:
            return (y + 1, x, y < maxY - 1);
        case Direction.East:
            return (y, x + 1, x < maxX - 1);
        case Direction.West:
            return (y, x - 1, x > 0);

        case Direction.NorthEast:
            return (y - 1, x + 1, y > 0 && x < maxX - 1);
        case Direction.SouthEast:
            return (y + 1, x + 1, y < maxY - 1 && x < maxX - 1);

        case Direction.NorthWest:
            return (y - 1, x - 1, y > 0 && x > 0);
        case Direction.SouthWest:
            return (y + 1, x - 1, y < maxY - 1 && x > 0);
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
public struct MapPoint
{
    public bool Touched;
    public bool Block;
}