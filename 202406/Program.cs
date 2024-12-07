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
(int x, int y) startPos = (0, 0);
var currentDirection = Direction.North;

int positionCount = 0;

for (int y = 0; y < lines.Length; y++)
{
    var line = lines[y];
    for (int x = 0; x < line.Length; x++)
    {
        map[x, y] = new MapPoint { Block = line[x] == '#', x = x, y = y };
        if (line[x] != '#' && line[x] != '.')
        {
            currentPos = (x, y);
            startPos = (x, y);
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
        map[currentPos.x, currentPos.y].Directions.Add(currentDirection);
        break;
    }

    if (map[x, y].Block)
    {
        currentDirection = NextDirection(currentDirection);
    }
    else
    {
        map[currentPos.x, currentPos.y].Directions.Add(currentDirection);

        currentPos.x = x;
        currentPos.y = y;
    }

}

Console.WriteLine($"Day 6 Part 1: {positionCount}");

//for (int y = 0; y < lines.Length; y++)
//{
//    Console.WriteLine(lines[y]);
//}
//Console.WriteLine();
//for (int y = 0; y < lines.Length; y++)
//{
//    var lineString = string.Empty;
//    var line = lines[y];
//    for (int x = 0; x < line.Length; x++)
//    {
//        lineString += MapCharacter(map[x, y]);
//    }
//    Console.WriteLine(lineString);
//}

var pointList = new List<MapPoint>();
for (int y = 0; y < lines.Length; y++)
{
    var line = lines[y];
    for (int x = 0; x < line.Length; x++)
    {
        pointList.Add(map[x, y]);
    }
}

// part 2
var obstructionCount = 0;
// point that has been touched
// point has a block 90 degrees to the right that has been hit in the same direction
// point + 1 in direction is not already a block or invalid
var allTouchedSirections = pointList.Where(p => p.Touched).Select(p => new { p.x, p.y});
Console.WriteLine("Part2");
Console.WriteLine();

foreach (var point in allTouchedSirections) {
   var newmap = CopyMap(map, point.x, point.y);
    currentPos = startPos;
    currentDirection = Direction.North;
    while (true)
    {
        //positionCount += map[currentPos.x, currentPos.y].Touched ? 0 : 1;
        if (newmap[currentPos.x, currentPos.y].Touched && newmap[currentPos.x,currentPos.y].Directions.Contains(currentDirection))
        {
            obstructionCount++;
            //Console.WriteLine($"Option {obstructionCount}");
            //DrawMap(newmap, point.x, point.y);
            break;
        }

        newmap[currentPos.x, currentPos.y].Touched = true;
        var (y, x, valid) = NextPos(currentPos.x, currentPos.y, currentDirection, lines[0].Length, lines.Length);

        if (!valid)
        {
            newmap[currentPos.x, currentPos.y].Directions.Add(currentDirection);
            break;
        }

        if (newmap[x, y].Block)
        {
            currentDirection = NextDirection(currentDirection);
        }
        else
        {
            newmap[currentPos.x, currentPos.y].Directions.Add(currentDirection);

            currentPos.x = x;
            currentPos.y = y;
        }

    }
}

MapPoint[,] CopyMap(MapPoint[,] map, int newBlocX, int newBlockY)
{
    var newMap = new MapPoint[map.GetUpperBound(0)+1, map.GetUpperBound(1)+1];
    for (int x = 0;x < newMap.GetLength(0);x++)
    {
        for (int y = 0;y < newMap.GetLength(1); y++)
        {
            var p = map[x, y];
            newMap[x, y] = new MapPoint
            {
                x = x,
                y = y,
                Block = p.Block || (newBlocX == x && newBlockY == y)
            };            
        }    
    }
    return newMap;
}
//var touchedNextOK = allTouchedSirections.Where(p =>
//{
//    var next = NextPos(p.x, p.y, p.d, lines.Length, lines[0].Length);
//    return next.valid && !map[next.x,next.y].Block;
//})
//    .Select(p => new { p, position = NextPos(p.x, p.y, p.d, lines.Length, lines[0].Length) });


//foreach (var point in touchedNextOK)
//{
//    currentPos = (point.p.x, point.p.y);

//    currentDirection = NextDirection(point.p.d);
//    while (true)
//    {
//        var (y, x, valid) = NextPos(currentPos.x, currentPos.y, currentDirection, lines[0].Length, lines.Length);
//        if (!valid)
//        {

//            break;
//        }

//        if (map[x, y].Block && map[currentPos.x, currentPos.y].Directions.Contains(NextDirection(currentDirection)))
//        {
//            Console.WriteLine($"Seems OK: {point.position.x}, {point.position.y} - {point.p.d}");
//            obstructionCount++;
//            break;            
//        }
//        else
//        {
//            currentPos.x = x;
//            currentPos.y = y;
//        }
//    }
//}

Console.WriteLine($"Day 6 Part 2: {obstructionCount}");


void DrawMap(MapPoint[,] map, int xtraX = -1, int xtraY = -1)
{
    Console.WriteLine();
    for (int y = 0; y < lines.Length; y++)
    {
        var lineString = string.Empty;
        var line = lines[y];
        for (int x = 0; x < line.Length; x++)
        {
            lineString += x == xtraX && y == xtraY? 'O': MapCharacter(map[x, y]);
        }
        Console.WriteLine(lineString);
    }

}

char MapCharacter(MapPoint map)
{
    if (map.Block)
        return '#';

    if (map.Touched)
    {
        if (map.Directions.Count >= 2)
            return '+';
        if (map.Directions[0] == Direction.North || map.Directions[0] == Direction.South)
            return '|';

        return '-';
    }

    return '.';
}

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
public class MapPoint
{
    public bool Touched;
    public bool Block;
    public List<Direction> Directions = new List<Direction>();
    public int x;
    public int y;
}