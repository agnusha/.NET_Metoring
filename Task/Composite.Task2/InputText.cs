namespace Composite.Task2
{
    public class InputText : IComponent
    {
        readonly string name;
        readonly string value;

        public InputText(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public string ConvertToString(int depth = 0)
        {
            return $"{SpaceGeneration.Space(depth)}<inputText name=\'{name}\' value=\'{value}\'/>{SpaceGeneration.EndLine(depth)}";
        }
    }
}
