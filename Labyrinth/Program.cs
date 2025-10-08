namespace Labyrinth;

internal class Program
{
    private static void Main(string[] args)
    {
        const string chemin = """
            +--+--------+
            |  /        |
            |  +--+--+  |
            |     |k    |
            +--+  |  +--+
               |k       |
            +  +-------/|
            |           |
            +-----------+
            """;

        Labyrinth labyrinth = new Labyrinth(chemin);
    }
}