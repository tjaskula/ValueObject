using System;
using FluentAssertions;
using Xunit;

namespace DomainBuildingBlocks.UnitTests
{
    public abstract class UnorderedValueObjectsTests<T> where T : ValueObject<T>
    {
        protected abstract IEquatable<UnorderedValueObjects<SimpleValueObjectStub>> CreateValueObjects();
        
        protected abstract IEquatable<UnorderedValueObjects<SimpleValueObjectStub>> CreateDifferentValueObjects();
        
        [Fact]
        public void Should_properly_report_equality()
        {
            var stub = CreateValueObjects();
            var stub2 = CreateValueObjects();

            stub.Should().Be(stub2);
            stub.Should().NotBeSameAs(stub2);
        }
        
        [Fact]
        public void Should_properly_report_equality_when_same_reference()
        {
            var stub = CreateValueObjects();

            stub.Should().Be(stub);
            stub.Should().BeSameAs(stub);
        }
    }

    public class UnorderedValueObjectsTester : UnorderedValueObjectsTests<SimpleValueObjectStub>
    {
        protected override IEquatable<UnorderedValueObjects<SimpleValueObjectStub>> CreateValueObjects()
        {
            var simpleValueObjectStubs = new UnorderedValueObjects<SimpleValueObjectStub>
            {
                new SimpleValueObjectStub
                {
                    Address = "2",
                    Age = 23,
                    Date = new DateTime(2017, 11, 30),
                    Name = "Nom"
                },
                new SimpleValueObjectStub
                {
                    Address = "3",
                    Age = 70,
                    Date = new DateTime(2017, 12, 30),
                    Name = "Vom"
                },
                new SimpleValueObjectStub
                {
                    Address = "1",
                    Age = 18,
                    Date = new DateTime(2017, 10, 30),
                    Name = "Bom"
                }
            };
            return simpleValueObjectStubs;
        }
        
        protected override IEquatable<UnorderedValueObjects<SimpleValueObjectStub>> CreateDifferentValueObjects()
        {
            var simpleValueObjectStubs = new UnorderedValueObjects<SimpleValueObjectStub>
            {
                new SimpleValueObjectStub
                {
                    Address = "2",
                    Age = 23,
                    Date = new DateTime(2017, 11, 30),
                    Name = "Nom"
                },
                new SimpleValueObjectStub
                {
                    Address = "1",
                    Age = 18,
                    Date = new DateTime(2017, 10, 30),
                    Name = "Bom"
                },
                new SimpleValueObjectStub
                {
                    Address = "3",
                    Age = 70,
                    Date = new DateTime(2017, 12, 30),
                    Name = "Vom"
                }
            };
            return simpleValueObjectStubs;
        }
    }
}