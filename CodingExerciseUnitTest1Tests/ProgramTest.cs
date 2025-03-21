using CodingExerciseUnitTest1;
using Moq;
using Shouldly;

namespace CodingExerciseUnitTest1Tests
{
    public interface IInputReader
    {
        string ReadLine();
    }

    public class ConsoleInputReader : IInputReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
    public class ProgramTest
    {


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Choice1_ShouldPromptForUsername_PromptsUserName()
        {

            // Arrange
            var data = String.Join("\n", new[]
            {
                "1",
                "Joachim",
                "2",
                "1",
                "Elijah",
                "2",
                "5"
            });
            Console.SetIn(new StringReader(data));

            // Act
            Program.Main(new string[] { });

            // Assert
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            TestContext.Out.WriteLine(stringWriter.ToString());
        }
    }
}