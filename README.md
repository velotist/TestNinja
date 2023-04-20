# TestNinja

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
[^1]: Orginal code
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

You can then call the newly created method in your original code like this

```
public string ReadVideoTitle()
{
    var str = new FileReader().Read("video.txt");
```
- next step is to extract an interface from the FileReader class

```
public interface IFileReader
{
    string Read(string path);
}
```
So at the end we have something like this
```
public class FileReader : IFileReader
{
    public string Read(string path)
    {
        return File.ReadAllText(path);
    }
}
```
Now we can use this to create a fake implementation for our unit tests.
- in our unit testing project we add a new class for example FakeFileReader
> Hint: <p>older unit testing frameworks differentiate between mocks und stubs for faking. More modern frameworks don't differentiate and this is more practical. So instead of calling your class MockFileReader or StubFileReader we prefer to call it FakeFileReader.

So now we have something like this in our unit testing project which we use in our unit tests:
```
public class FakeFileReader : IFileReader
{
  public string Read(string path)
  {
    return "";
  }
}
```
Now there are three ways we can pass an instance of a class that implements the interface IFileReader.
- pass it as a parameter
- pass it as a property
- pass it in the constructor
