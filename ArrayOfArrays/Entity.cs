namespace ArrayOfArrays;

public class Entity
{
    public int Value { get; set; }
    public Entity? Neighbour { get; set; }

    public override string ToString()
    {
        return $"This entity's value = {Value}. This entity's neighbour's value = {Neighbour?.Value}"; // https://stackoverflow.com/questions/28352072/what-does-question-mark-and-dot-operator-mean-in-c-sharp-6-0
    }
}