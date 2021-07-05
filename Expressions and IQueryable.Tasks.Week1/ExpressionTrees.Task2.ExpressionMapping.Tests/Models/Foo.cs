namespace ExpressionTrees.Task2.ExpressionMapping.Tests.Models
{
    internal class Foo
    {
        public Foo(int propertyCopy1, int propertyNotCopyFoo1, string propertyCopy2, int propertyCopy3, string propertyNotCopy2)
        {
            PropertyCopy1 = propertyCopy1;
            PropertyNotCopyFoo1 = propertyNotCopyFoo1;
            PropertyCopy2 = propertyCopy2;
            PropertyCopy3 = propertyCopy3;
            PropertyNotCopy2 = propertyNotCopy2;
        }

        int PropertyCopy1 { get; set; }

        int PropertyNotCopyFoo1 { get; set; }

        string PropertyCopy2 { get; set; }

        int PropertyCopy3 { get; set; }

        string PropertyNotCopy2 { get; set; }

        public bool EqualsBar(Bar bar)
        {
            return PropertyCopy1 == bar.PropertyCopy1 &&
                   PropertyCopy2 == bar.PropertyCopy2 &&
                   PropertyCopy3.ToString() == bar.PropertyCopy3;
        }
    }
}
