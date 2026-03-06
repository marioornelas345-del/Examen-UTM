# Prompt 8: Presentation Layer (Console Menu)

Context:
The main entry point for FoodCampus API (Console).
Constraints:
- C# 14, .NET 10.
- Interactive menu: Register restaurant, list restaurants, register order, list orders by user.
- Use Dependency Injection.
- Reference ONLY the Application project.
- Robust input: Use TryParse for all numbers.
- Exception handling: Catch all errors and print in Red (ConsoleColor.Red).
- Resiliency: Don't crash on connection timeouts or format errors.
- Use unbound generics syntax (nameof(IRepository<>)) in DI configuration if possible.
Output:
Program.cs with full menu logic and DI setup.
