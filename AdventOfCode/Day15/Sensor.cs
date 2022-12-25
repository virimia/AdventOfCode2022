using AdventOfCode.Helpers;

namespace AdventOfCode.Day15;

internal class Sensor
{
    public CoordinateLong Position { get; set; } = new(0, 0);
    public Beacon ClosestBeacon { get; set; }
}
