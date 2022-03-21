using System.Diagnostics;

namespace ArrayOfArrays;

/// <summary>
/// Todo list
///
/// - object
/// - struct
/// - int ref, unsafe
/// - zuple
/// - record
///
/// - creation of 1 billion object ~ 4h
/// - parallelize
///
/// </summary>
public class Program
{
    private const int NumberOfEntities = 1_000_000;

    public static void Main()
    {
        Program p = new();

        Console.WriteLine($"{nameof(NumberOfEntities)}: {NumberOfEntities:N0}");

        TimeSpan timespan = MeasureTime(p.InitialVersion);
        Console.WriteLine($"InitialVersion:\t{timespan:hh\\:mm\\:ss\\:fff} hour:min:sec:ms");

        timespan = MeasureTime(p.WithObjects);
        Console.WriteLine($"InitialVersion:\t{timespan:hh\\:mm\\:ss\\:fff} hour:min:sec:ms");

        Console.WriteLine("--- Not yet implemented ---");

        timespan = MeasureTime(p.OneIntWithRefKeyword);
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
        int[] entitiesField = new int[NumberOfEntities];
        for (int i = 0; i < NumberOfEntities; i++)
        {
            entitiesField[i] = i;
        }
        ref int neighbour = ref entitiesField[2];
        Console.WriteLine(neighbour);
        entitiesField[2] = 734;
        Console.WriteLine(neighbour);

        int[] refarray = new int[NumberOfEntities];
        refarray[0] = neighbour;
        Console.WriteLine(refarray[0]);
        entitiesField[2] = 7;
        Console.WriteLine(refarray[0]);

        //output:
        //2
        //734
        //734
        //734
    }

    private void WithObjects()
    {
        // Create a list of entities
        Entity[] entities = new Entity[NumberOfEntities];

        // Initialize entity and it's value
        for (int i = 0; i < 31; i++)
        {
            entities[i] = new Entity();
            entities[i].Value = i;
        }

        // Set the neighbour for some of the entities
        entities[0].Neighbour = null;
        entities[1].Neighbour = entities[4];
        entities[2].Neighbour = null;
        entities[3].Neighbour = entities[0];
        entities[4].Neighbour = null;

        // Print all entity's value and it's neighbour's value
        foreach (Entity currentEntity in entities)
        {
            Debug.WriteLine(currentEntity);
        }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/ref-returns
    /// </summary>
    private void OneIntWithRefKeyword()
    {
        int x = 10;
        ref int y = ref x;

        Console.WriteLine($"{nameof(y)} = {y}");

        x = 11; // Changing x changes y
        Console.WriteLine($"\n{nameof(y)} = {y}");
    }
}