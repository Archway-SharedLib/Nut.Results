namespace Nut.Results.FluentAssertions
{
    public static class ResultAssertionsExtensions
    {
        public static ResultAssertions Should(this in Result instance)
        {
            return new ResultAssertions(instance);
        }
        
        public static ResultAssertions<T> Should<T>(this in Result<T> instance)
        {
            return new ResultAssertions<T>(instance);
        }
    }
}