using System.Diagnostics;

namespace ArrayOfArrays;

public class Program
{
    public static void Main()
    {
        Program p = new();

        TimeSpan timespan = MeasureTime(p.InitialVersion);
        Console.WriteLine($"InitialVersion:\t{timespan:hh\\:mm\\:ss\\:fff} hour:min:sec:ms");
    }

    private static TimeSpan MeasureTime(Action algorithm)
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();
        algorithm();
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }

    private void InitialVersion()
    {
        int[] entities_field = new int[31];
        for (int i = 0; i < 31; i++)
        {
            entities_field[i] = i;
        }
        ref int neighbour = ref entities_field[2];
        Console.WriteLine(neighbour);
        entities_field[2] = 734;
        Console.WriteLine(neighbour);

        int[] refarray = new int[31];
        refarray[0] = neighbour;
        Console.WriteLine(refarray[0]);
        entities_field[2] = 7;
        Console.WriteLine(refarray[0]);

        //output:
        //2
        //734
        //734
        //734
    }
}