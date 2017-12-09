# Domain Driven Design Value Object

[![Build Status](https://ci.appveyor.com/api/projects/status/github/tjaskula/valueobject?branch=master&svg=true)](https://ci.appveyor.com/project/tjaskula/valueobject) 
[![NuGet](https://img.shields.io/nuget/dt/DddBuildingBlocks.ValueObject.svg)](https://www.nuget.org/packages/DddBuildingBlocks.ValueObject) 
[![NuGet](https://img.shields.io/nuget/vpre/DddBuildingBlocks.ValueObject.svg)](https://www.nuget.org/packages/DddBuildingBlocks.ValueObject)

Yet another implementation of the Domain Driven Design Value Object building block.

> Value Object : An object that contains attributes but has no conceptual identity.

In Domain Driven Design when a concept is modeled as a value it should have most of these characteristcs:

- Measures, describes, quantifies a concept in the domain.  
- Is immutable.  
- It models a conceptual whole.  
- It completly remplacable by another value object when a one or more attributes changes.  
- It can be compared with other value objects using equlaity.  
- It supplies side effect free behavior.  

## Installing ValueObject

You should install [ValueObject with NuGet](https://www.nuget.org/packages/DddBuildingBlocks.ValueObject):

    Install-Package DddBuildingBlocks.ValueObject
    
Or via the .NET Core command line interface:

    dotnet add package DddBuildingBlocks.ValueObject

Either commands, from Package Manager Console or .NET Core CLI, will download and install DddBuildingBlocks.ValueObject and all required dependencies.

## Usage

You must derive from `ValueObject<T>` class in order to get equality, `==` and `!=` operators for free.
However it's up to you to ensure that the object is immutable and side effect free.

```csharp
public class PersonInfo : ValueObject<PersonInfo>
{
    public PersonInfo(int age, string nickname)
    {
        Age = age;
        Nickname = nickname;
    }
    
    public int Age { get; }
    public string Nickname { get; }
}
```

You can use it like this:

```csharp
var personInfo1 = new PersonInfo(23, "Tomasz");
var personInfo2 = new PersonInfo(23, "Tomasz");
var personInfo3 = new PersonInfo(45, "John");  
  
personInfo1 == personInfo2; // reports true;
  
personInfo1 == personInfo3; // reports false;
```

### Collections of value objects

From time to time it might be helpful to model collections of value objects with the same equality semantics as the regular value object.
For that purpose there are two classes for unordered collections `UnorderedValueObjects<T>` and ordered ones `OrderedValueObjects<T>`.
The `OrderedValueObjects<T>` allows you to provide a custom `Comparer<T>`in order to keep your value object sorted in the collection.
Wheter you use unordered or ordered collection it depends on the domain and business requirements.


This is how you can use it:

```csharp
public class PersonInfo : ValueObject<PersonInfo>
{
    private OrderedValueObjects<Claim> _claims = new OrderedValueObjects<Claim>(new ClaimsComparer());
 
    public PersonInfo(int age, string nickname)
    {
        Age = age;
        Nickname = nickname;
    }
    
    public int Age { get; }
    public string Nickname { get; }
    public OrderedValueObjects<Claim> Claims => _claims;
    
    public void AttachClaim(Claim claim)
    {
        _claims.Add(claim);
    }
}
  
// for brievety I don't show the Claim class which is really simple.
  
var personInfo1 = new PersonInfo(23, "Tomasz");
personInfo1.AttachClaim(new Claim(1));
personInfo1.AttachClaim(new Claim(2));
  
var personInfo1 = new PersonInfo(23, "Tomasz");
// note that claims are added in different order
personInfo1.AttachClaim(new Claim(2));
personInfo1.AttachClaim(new Claim(1));
  
personInfo1 == personInfo2; // reports true;

```

I hope this will be usefull to you. Enjoy !

## Maintainers

- [@tjaskula](http://twitter.com/tjaskula)