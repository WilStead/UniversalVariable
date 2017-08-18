# UniversalVariable
Introduces the `let` class, which is capable of storing arbitrary values and properties.

## What is UniversalVariable?
A .NET library which provides the `let` class, which can be used to store any value, and can be assigned any properties.

It is intended as a way to introduce JavaScript-style loosely-typed variables. Although `let` is a class, and instances can be constructed with `let()`, you can also assign almost any value directly with statements like the following:

```c#
let i = 42;
let j = "Hello, World!";
let k = true;
let l = new object[] { 42, "Hello", true };
```

It is also possible to read and write properties of `let` objects with indexing syntax. For example:

```c#
let x = "Hello, ";
x["newProp"] = "World!";
let y = x + x["newProp"];
```

Please see the [project wiki](https://github.com/WilStead/UniversalVariable/wiki) for additional detail.

## How do I get this?
Available as a [NuGet package](https://www.nuget.org/packages/UniversalVariable).

## Can I contribute?
Yes! [Pull requests](https://help.github.com/articles/about-pull-requests/) are welcome for bug fixes and/or new features.

Please add unit tests for any new feature.

## Help! It's not working!
Feel free to submit an [Issue](https://help.github.com/articles/about-issues/) if something isn't working right, but make sure you have read the [wiki](https://github.com/WilStead/UniversalVariable/wiki) to verify that your trouble is due to an actual bug, and not simply a design limitation of the library.
