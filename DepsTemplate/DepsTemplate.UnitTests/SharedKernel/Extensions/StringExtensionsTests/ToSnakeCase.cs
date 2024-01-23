using DepsTemplate.SharedKernel.Extensions;
using Xunit;

namespace DepsTemplate.UnitTests.SharedKernel.Extensions.StringExtensionsTests
{
    public class ToSnakeCase
    {
        [Theory]
        [InlineData("Teste", "teste")]
        [InlineData("TesteOutro", "teste_outro")]
        [InlineData("TESTEOUTRO", "testeoutro")]
        [InlineData("TesteTesteTeste", "teste_teste_teste")]
        public void CanTransformStringToSnakeCase(string name, string expectedName)
        {
            var result = name.ToSnakeCase();

            Assert.Equal(expectedName, result);
        }
    }
}
