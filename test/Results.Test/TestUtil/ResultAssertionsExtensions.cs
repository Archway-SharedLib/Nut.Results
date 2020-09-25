namespace Archway.Results.Test.TestUtil
{
    public static class ResultAssertionsExtensions
    {
        public static ResultAssertions Should(this Result instance)
        {
            return new ResultAssertions(instance);
        }
        
        public static ResultAssertions<T> Should<T>(this Result<T> instance)
        {
            return new ResultAssertions<T>(instance);
        }
    }
}