namespace SIS.Tests
{
    using SIS.MvcFramework.ViewEngine;
    using Xunit;
    public class SISViewEngineTests
    {
        [Theory]
        [InlineData("CleanHtml")]
        [InlineData("ForeachTest")]
        [InlineData("IfElseForEach")]
        [InlineData("ViewModel")]
        public void TestGetHtml(string fileName)
        {
            var viewModel = new TestViewModel
            {
                DateOfBirth = new DateTime(2019, 6, 1),
                Name = "Dogo Argentino",
                Price = 12345.67M
            };


            IViewEngine viewEngine = new SISViewEngine();
            var view = File.ReadAllText($"ViewTests/{fileName}.html");
            var result = viewEngine.GetHtml(view, viewModel,null);
            var expectedResult = File.ReadAllText($"ViewTests/{fileName}.Result.html");
            Assert.Equal(expectedResult, result);
            
        }
    }
}