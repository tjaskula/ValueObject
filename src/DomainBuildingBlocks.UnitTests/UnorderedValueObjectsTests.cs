using System;
using FluentAssertions;
using Xunit;

namespace DomainBuildingBlocks.UnitTests
{
    public abstract class UnorderedValueObjectsTests<T> where T : ValueObject<T>
    {
        protected abstract IEquatable<UnorderedValueObjects<SimpleValueObjectStub>> CreateValueObjects();
        
        [Fact]
        public void Should_properly_report_equality()
        {
            var stub = CreateValueObjects();
            var stub2 = CreateValueObjects();

            stub.Should().Be(stub2);
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
    }
}