# Snippet for creating identity types

Create your identity type with ``newid.snippet``.

The snippet has two parameters:

- ``$classname$``: name of the model, e.g. User or Product. The struct type is the class name with the "Id" suffix e.g. ``UserId``, ``ProductId``
- ``$type$``: the type of the underlying identity value. ``int``, ``string``, ``Guid``, whatever.

## Features

- **Immutable** - as structs should be.
- **Operator overloading** - comparison operators ``==`` and ``!=`` are handled.
- **``IComparable``** - identities are compared using the underlying type.
- **``IEquatable``** - identities are equal if their underlying types are equal.
- **``null`` support**
  - **Nullable underyling type**  
  The generated struct handles cases where the underlying type is nullable.
  - **Support for nullable identities**  
  The generated struct of course can be nullable, this is handled by comparison operators (there's a catch though, see below).
  - **Null equality**  
  A nullable struct being ``null`` is considered equal to a struct with null

## Note on using ``null`` identities

Whether you want to use nullable identities depends on how you wish to work with them in your code.

Usually you'd have a business model like this:

```csharp
public class Product
{
    //ProductId is a struct type generated by the newid snippet.
    public ProductId Id { get; set; }

    // ...some other properties
}
```

If your data layer generates Ids automatically for you (e.g. some kind of auto increment functionality) then you may want to set the Id property to the ``default`` of the underlying type, e.g. ``new ProductId(default(int))`` since the property is ignored in your data layer anyway.

```csharp
public class Product
{
    //ProductId is a struct type generated by the newid snippet.
    public ProductId? Id { get; set; }

    // ...some other properties
}
```

In the example above, the ``Id`` property is nullable. I favor this approach more since it's clear that there are cases in the business layer where the model is valid with ``Id`` equal to null, e.g. during a repository insert.

## Handling of ``null`` struct and ``null`` underlying value

If you want the underlying type to be something nullable e.g. ``string``, ``int?`` etc. then you need to know that the snippet will generate a code which makes a null struct equal with a struct with the null value.

```csharp
ProductId? a = null;
ProductId b = new ProductId(null);

Console.WriteLine(a == b); // outputs true
```

This is a design decision. When using a nullable underlying type, I don't want to care whether the struct is null or whether the value of the struct is null, I consider both equal.

### There's a catch though

Always use comparison operators, do not use "Equals" method of a nullable struct, since it's a method ``Nullable<T>.Equals`` which follows a different logic and cannot be unfortunatelly overriden in any other way.

```csharp
ProductId? a = null;
ProductId b = new ProductId(null);

Console.WriteLine(a == b);      // outputs true
Console.WriteLine(a.Equals(b)); // outputs false !!!
```

Yes, this is not very nice but if you stick to using comparison operators or avoid using nullable underlying values, you should be fine.