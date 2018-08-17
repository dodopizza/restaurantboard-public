namespace Dodo.Core.Tests.DSL
{
    public static class Create
    {
        public static XElementBuilder XElement(string name)
        {
            return new XElementBuilder(name);
        }
    }
}