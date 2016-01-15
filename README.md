# FluentAssertions
A lightweight library for creating fluent, extensible assertions in unit tests.

## Basic usage
As opposed to the standard Assert.AreEqual(expected, actual), FluentAssertions works 
in the following manner.

```csharp
//Act
var actual = GetComplexObject();

//Assert
AssertChain.GetFor(actual)
    .AreEqual("Expected name", m => m.Name)
    .IsTrue(m => m.IsAComplexObject)
    .IsNull(m => m.SomeNullValue);
```

## Extensibility
The real power of the library comes from the extensible framework. This allows code reuse
and improves readability.

```csharp
//Address.cs in code project
public class Address
{
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }   
}

//FluentAssertionsExtensions.cs in Test project
public static class FluentAssertions
{
    
    public static IAssertChain<Address> IsInCity(this IAssertChain<Address> chain, string expectedCity) 
    {
        return chain.AreEqual(expectedCity, m => m.City);
    }
    
    public static IAssertChain<Address> HasAddress(this IAssertChain<Address> chain, string expectedAddressLine1, string expectedAddressLine2 = null) 
    {
        var chain = chain
            .AreEqual(expectedAddressLine1, 
                m => m.AddressLine1, 
                "AddressLine1 is incorrect");
                
        return expectedAddressLine2 == null
            ? chain
            : chain.AreEqual(expectedAddressLine2, 
                m => m.AddressLine2,
                "AddressLine2 is incorrect");
    }
}

//SomeUnitTestFile.cs
//Act
var actual = _addressRepository.GetAddress(3);

//Assert
AssertChain.GetFor(actual)
    .IsInCity("New York City")
    .HasAddress("123 Fake St", "Apartment 13");
```
Just don't forget to return IAssertChain<T> from your extension methods, otherwise you'll break the 
fluent syntax!