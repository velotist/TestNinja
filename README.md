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

## Breaking External Dependencies
- Refactor towards a loosley-coupled design

Example (extraction of a method)[^1]

```
public string ReadVideoTitle()
{
    var str = File.ReadAllText("video.txt");
```
Here we are touching an external resource, the System.IO.File resource.

To Do:<p>
decouple this code from the System.IO.File resource with
- moving all the code that touches an external resource into a separate class
So now you have a new class for example FileReader in which you have a method
```
public class FileReader
{
    public string Read(string path)
    {
        return File.ReadAllText(path);
    }
```
<p>
You can then call it in the original code

[^1]: Orginal code

```
public string ReadVideoTitle()
{
    var str = new FileReader().Read("video.txt");
```


