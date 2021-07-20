namespace Composite.Task2
{
    static class SpaceGeneration
    {
        public static string Space(int depth)
        {
            return new string(' ', depth);
        }
        public static string EndLine(int depth)
        {
            return depth > 0 ?"\r\n":string.Empty;
        }
    }
}
