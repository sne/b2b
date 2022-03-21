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
    private const int NumberOfEntities = 100_000;
    private const bool IsConsoleWriteLineEnabled = false;

    public static void Main()
    {
        Console.WriteLine("Started...");
        Program p = new();

        Console.WriteLine($"{nameof(NumberOfEntities)}: {NumberOfEntities:N0}");
        Console.WriteLine($"{nameof(IsConsoleWriteLineEnabled)}: {IsConsoleWriteLineEnabled}");

        TimeSpan timespan = MeasureTime(p.InitialVersion);
        Console.WriteLine($"InitialVersion:\t{timespan:hh\\:mm\\:ss\\:fff} hour:min:sec:ms\t... still running...");

        timespan = MeasureTime(p.WithObjects);
        Console.WriteLine($"WithObjects:\t{timespan:hh\\:mm\\:ss\\:fff} hour:min:sec:ms\t... still running...");

        Console.WriteLine("--- Not yet implemented ---");

        timespan = MeasureTime(p.OneIntWithRefKeyword);
        Console.WriteLine($"OneIntWithRefKeyword:\t{timespan:hh\\:mm\\:ss\\:fff} hour:min:sec:ms\t... still running...");

        timespan = MeasureTime(p.ArrayOfIntsWithRefKeyword);
        Console.WriteLine($"ArrayOfIntsWithRefKeyword:\t{timespan:hh\\:mm\\:ss\\:fff} hour:min:sec:ms\t... still running...");

        timespan = MeasureTime(p.OneIntWithPointer);
        Console.WriteLine($"OneIntWithPointer:\t{timespan:hh\\:mm\\:ss\\:fff} hour:min:sec:ms\t... still running...");

        timespan = MeasureTime(p.ArrayOfIntWithPointer);
        Console.WriteLine($"ArrayOfIntWithPointer:\t{timespan:hh\\:mm\\:ss\\:fff} hour:min:sec:ms\t... still running...");

        Console.WriteLine("Ended.");
    }

    private static TimeSpan MeasureTime(Action algorithm)
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();
        algorithm();
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }

    private void Output(object message)
    {
        if (IsConsoleWriteLineEnabled)
        {
            Console.WriteLine(message);
        }
    }

    private void InitialVersion()
    {
        int[] entitiesField = new int[NumberOfEntities];
        for (int i = 0; i < NumberOfEntities; i++)
        {
            entitiesField[i] = i;
        }
        ref int neighbour = ref entitiesField[2];
        Output(neighbour);
        entitiesField[2] = 734;
        Output(neighbour);

        int[] refarray = new int[NumberOfEntities];
        refarray[0] = neighbour;
        Output(refarray[0]);
        entitiesField[2] = 7;
        Output(refarray[0]);

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
        for (int i = 0; i < NumberOfEntities; i++)
        {
            entities[i] = new Entity { Value = i };
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
            Output(currentEntity);
        }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/ref-returns
    /// </summary>
    private void OneIntWithRefKeyword()
    {
        int x = 10;
        ref int y = ref x;

        Output($"{nameof(y)} = {y}");

        x = 11; // Changing x changes y
        Output($"\n{nameof(y)} = {y}");
    }

    private void ArrayOfIntsWithRefKeyword()
    {
        int[] firstArray = new int[NumberOfEntities];
        firstArray[0] = 0;
        firstArray[1] = 1;
        firstArray[2] = 2;
        int x = 10;

        int[] secondArray = new int[NumberOfEntities];
        //secondArray[0] = ref firstArray[1];               // Doesn't work yet

        Output($"{nameof(secondArray)}[0] = {secondArray[0]}");

        firstArray[1] = 11;
        Output($"\n{nameof(secondArray)}[0] = {secondArray[0]}");

    }

    /// <summary>
    /// Source: https://www.geeksforgeeks.org/unsafe-code-in-c-sharp/
    /// Allow to have unsafe blocks by enabling the feature in the project properties
    ///          Project → Context menu: Properties → Tab: Build → Node: Genera
    ///          → Check: Unsafe code (Allow code that uses the 'unsafe' keyword to compile
    /// </summary>
    private void OneIntWithPointer()
    {
        unsafe
        {
            int x = 10;     // Declare a regular int
            int* pointer;   // Declare a pointer that can point to a int
            pointer = &x;   // Have the pointer point to the value of x

            Output($"{nameof(x)} = {x}");
            Output($"{nameof(pointer)} = {*pointer}");

            x = 11;
            Output($"\n{nameof(x)} = {x}");
            Output($"{nameof(pointer)} = {*pointer}");
        }
    }

    private void ArrayOfIntWithPointer()
    {
        unsafe
        {
            int[] firstArray = new int[NumberOfEntities];
            firstArray[0] = 0;
            firstArray[1] = 1;
            firstArray[2] = 2;
            firstArray[3] = 3;
            firstArray[4] = 4;

            int*[] secondArray = new int*[3]; // An int array that contains pointers to other ints
                                              // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/unsafe-code
            secondArray[0] = (int*)firstArray[2];
            secondArray[1] = (int*)firstArray[0];
            secondArray[2] = (int*)firstArray[4];

            int test = *secondArray[0];
            Output($"{nameof(secondArray)}[0] = {*secondArray[0]}");
            Output($"{nameof(secondArray)}[1] = {*secondArray[1]}");
            Output($"{nameof(secondArray)}[2] = {*secondArray[2]}");

            firstArray[2] = 7;
            firstArray[0] = 8;
            firstArray[4] = 9;

            Output($"\n{nameof(secondArray)}[0] = {*secondArray[0]}");
            Output($"{nameof(secondArray)}[1] = {*secondArray[1]}");
            Output($"{nameof(secondArray)}[2] = {*secondArray[2]}");
        }
    }
}