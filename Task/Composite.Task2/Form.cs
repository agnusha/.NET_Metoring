using System;
using System.Collections.Generic;

namespace Composite.Task2
{
    public class Form : IComponent
    {
        readonly string name;
        readonly List<IComponent> items = new List<IComponent>();

        public Form(String name)
        {
            this.name = name;
        }

        public void AddComponent(IComponent component)
        {
            items.Add(component);
        }

        public string ConvertToString(int depth = 0)
        {
            var res = string.Empty;
            items.ForEach(i => res += i.ConvertToString(depth+1));
            return $@"{SpaceGeneration.Space(depth)}<form name='{name}'>
{res}{SpaceGeneration.Space(depth)}</form>{SpaceGeneration.EndLine(depth)}";
        }
    }
}