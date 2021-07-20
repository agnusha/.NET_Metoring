namespace Composite.Task2
{
    public class LabelText : IComponent
    {
        readonly string value;

        public LabelText(string value)
        {
            this.value = value;
        }

        public string ConvertToString(int depth = 0)
        {
            return $"{SpaceGeneration.Space(depth)}<label value=\'{value}\'/>{SpaceGeneration.EndLine(depth)}";
        }
    }
}
