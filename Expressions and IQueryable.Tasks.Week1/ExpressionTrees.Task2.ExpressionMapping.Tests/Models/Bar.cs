namespace ExpressionTrees.Task2.ExpressionMapping.Tests.Models
{
    internal class Bar
    {
        public Bar()
        {
        }

        public Bar(int propertyCopy1, int propertyNotCopyBar1, string propertyCopy2, string propertyNotCopyBar2)
        {
            PropertyCopy1 = propertyCopy1;
            PropertyNotCopyBar1 = propertyNotCopyBar1;
            PropertyCopy2 = propertyCopy2;
            PropertyNotCopyBar2 = propertyNotCopyBar2;
        }

        public int PropertyCopy1 { get; set; }

        public int PropertyNotCopyBar1 { get; set; }

        public string PropertyCopy2 { get; set; }

        public string PropertyNotCopyBar2 { get; set; }

    }
}
