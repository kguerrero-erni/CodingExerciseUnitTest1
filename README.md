## What is Unit Testing?

Unit testing is a software testing technique where individual components or functions of an application are tested in isolation to ensure they work correctly. These tests are typically automated and help developers catch bugs early in the development cycle. A unit test should be small, fast, and reliable, focusing on verifying a single piece of functionality.

## Why is Unit Testing Important?

Unit testing offers several benefits, including:

- **Early Bug Detection**: By testing individual units early, developers can identify and fix issues before they propagate.
    
- **Code Quality Improvement**: Writing tests encourages modular, maintainable, and well-structured code.
    
- **Refactoring Confidence**: When modifying existing code, unit tests help ensure that new changes do not break existing functionality.
    
- **Facilitates Continuous Integration**: Automated unit tests can be integrated into CI/CD pipelines to detect issues before deployment.

## Key Principles of Unit Testing

1. **Isolation**: Each test should be independent and not rely on external dependencies like databases, APIs, or file systems.
    
2. **Determinism**: The same test should always produce the same result when run multiple times.
    
3. **Automation**: Tests should be automated so they can be run frequently and consistently.
    
4. **Readability & Maintainability**: Tests should be clear, concise, and easy to maintain as the code evolves.
## Writing a Basic Unit Test

A simple example using NUnit in C#:
```csharp
using NUnit.Framework;
using Shouldly;

public class Calculator
{
    public int Add(int a, int b) => a + b;
}

[TestFixture]
public class CalculatorTests
{
    private Calculator _calculator;

    [SetUp]
    public void Setup()
    {
        _calculator = new Calculator();
    }

    [Test]
    public void Add_ShouldReturnCorrectSum()
    {
        // Arrange
        int a = 2;
        int b = 3;
        
        // Act
        var result = _calculator.Add(a, b);
        
        // Assert
        result.ShouldBe(5);
    }
}
```

### Breakdown:

- `TestFixture`: Marks the test class.
    
- `SetUp`: Initializes test dependencies before each test runs.
    
- `Test`: Marks a test method.
    
- `AAA Pattern`: The test follows the **Arrange, Act, Assert** pattern:
    
    - **Arrange**: Set up necessary test data and objects.
        
    - **Act**: Perform the operation being tested.
        
    - **Assert**: Verify that the outcome matches the expected result.
        
- `Shouldly`: Provides more readable assertions (`ShouldBe` instead of `Assert.AreEqual`).
