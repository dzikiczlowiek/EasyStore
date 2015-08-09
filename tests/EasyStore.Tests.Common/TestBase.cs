namespace EasyStore.Tests.Common
{
    public abstract class TestBase
    {
        private static readonly ValuesGenerator ValuesGenerator = new ValuesGenerator();

        public static ValuesGenerator A
        {
            get
            {
                return ValuesGenerator;
            }
        }
    }
}
