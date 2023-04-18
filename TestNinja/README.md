# TestNinjaStarter

*Based on*
[Mosh Hamedani - Unit Testing for C# Developers](https://www.udemy.com/course/unit-testing-csharp/)

## Test Driven Development
- Write a failing test **first**.
- **Test** only the **outcome** of a method.
- Write the simplest code to make the test pass.
- Refactor your code.

## Takeaways
When you write your tests after production code,
- Run your test
    - if it passes then go in the production code and -><p>
    - Make a small change in the line that is supposed to make the test pass
- Create a bug (return a different value or comment out that line)
    - if your test still passes <p>
-> that means that your test is not running the right thing

- Do not test
    - language features
    - third party code

