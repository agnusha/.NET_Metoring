namespace ExpressionTrees.Task2.ExpressionMapping.Tests.Models
{
    internal class Foo
    {
        public Foo()
        {
        }
        
        public Foo(int propertyCopy1, int propertyNotCopyFoo1, string propertyCopy2, int propertyCopy3, string propertyNotCopyFoo2)
        {
            PropertyCopy1 = propertyCopy1;
            PropertyNotCopyFoo1 = propertyNotCopyFoo1;
            PropertyCopy2 = propertyCopy2;
            PropertyCopy3 = propertyCopy3;
            PropertyNotCopyFoo2 = propertyNotCopyFoo2;
        }

        public int PropertyCopy1 { get; set; }

        public int PropertyNotCopyFoo1 { get; set; }

        public string PropertyCopy2 { get; set; }

        public int PropertyCopy3 { get; set; }

        public string PropertyNotCopyFoo2 { get; set; }

        public bool EqualsBar(Bar bar)
        {
            return PropertyCopy1 == bar.PropertyCopy1 &&
                   PropertyCopy2 == bar.PropertyCopy2 &&
                   PropertyCopy3.ToString() == bar.PropertyCopy3;
        }
    }
}
