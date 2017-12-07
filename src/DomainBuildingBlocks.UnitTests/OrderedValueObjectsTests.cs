using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace DomainBuildingBlocks.UnitTests
{
    public abstract class OrderedValueObjectsTests<T> where T : ValueObject<T>
    {
        protected abstract IEquatable<OrderedValueObjects<SimpleValueObjectStub>> CreateValueObjects();
        
        protected abstract IEquatable<OrderedValueObjects<SimpleValueObjectStub>> CreateDifferentOrderValueObjects();
        
        protected abstract IEquatable<OrderedValueObjects<SimpleValueObjectStub>> CreateDifferentOrderAndPropertiesValueObjects();
        
        [Fact]
        public void Should_properly_report_equality()
        {
            var stub = CreateValueObjects();
            var stub2 = CreateValueObjects();

            stub.Should().Be(stub2);
            stub.Should().NotBeSameAs(stub2);
        }
        
        [Fact]
        public void Should_properly_report_equality_for_different_objects()
        {
            var stub = CreateValueObjects();
            var stub2 = CreateDifferentOrderValueObjects();

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
        
        [Fact]
        public void Should_properly_report_nonequality()
        {
            var stub = CreateValueObjects();
            var stub2 = CreateDifferentOrderAndPropertiesValueObjects();

            stub.Should().NotBe(stub2);
            stub.Should().NotBeSameAs(stub2);
        }
        
        [Fact]
        public void Should_properly_report_equality_with_operator()
        {
            var stub = (OrderedValueObjects<SimpleValueObjectStub>)CreateValueObjects();
            var stub2 = (OrderedValueObjects<SimpleValueObjectStub>)CreateValueObjects();

            (stub == stub2).Should().BeTrue();
        }
        
        [Fact]
        public void Should_properly_report_nonequality_with_operator()
        {
            var stub = (OrderedValueObjects<SimpleValueObjectStub>)CreateValueObjects();
            var stub2 = (OrderedValueObjects<SimpleValueObjectStub>)CreateDifferentOrderAndPropertiesValueObjects();

            (stub != stub2).Should().BeTrue();
        }
        
        [Fact]
        public void Should_report_get_hash_code_different_if_different_values()
        {
            var stub = CreateValueObjects();
            var stub2 = CreateDifferentOrderAndPropertiesValueObjects();

            stub.GetHashCode().Should().NotBe(stub2.GetHashCode());
        }
        
        [Fact]
        public void Should_report_get_hash_code_same_if_same_values()
        {
            var stub = CreateDifferentOrderValueObjects();
            var stub2 = CreateDifferentOrderValueObjects();

            stub.GetHashCode().Should().Be(stub2.GetHashCode());
        }
        
        [Fact]
        public void Should_report_get_hash_code_same_if_same_values_but_different_objects()
        {
            var stub = CreateValueObjects();
            var stub2 = CreateDifferentOrderValueObjects();

            stub.GetHashCode().Should().Be(stub2.GetHashCode());
        }
        
        [Fact]
        public void Should_report_not_equal_if_obj_is_null()
        {
            var stub = CreateValueObjects();
            stub.Equals(null).Should().BeFalse();
        }
        
        [Fact]
        public void Should_copy_one_object_to_another()
        {
            var initialObject = CreateValueObjects() as OrderedValueObjects<SimpleValueObjectStub>;

            var destinationObject = initialObject.Copy();

            initialObject.Equals(destinationObject).Should().BeTrue();
        }
    }

    public class OrderedValueObjectsTester : OrderedValueObjectsTests<SimpleValueObjectStub>
    {
        protected override IEquatable<OrderedValueObjects<SimpleValueObjectStub>> CreateValueObjects()
        {
            var simpleValueObjectStubs = new OrderedValueObjects<SimpleValueObjectStub>(new SimpleValueObjectStubComparer())
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
        
        protected override IEquatable<OrderedValueObjects<SimpleValueObjectStub>> CreateDifferentOrderValueObjects()
        {
            var simpleValueObjectStubs = new OrderedValueObjects<SimpleValueObjectStub>(new SimpleValueObjectStubComparer())
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

        protected override IEquatable<OrderedValueObjects<SimpleValueObjectStub>> CreateDifferentOrderAndPropertiesValueObjects()
        {
            var simpleValueObjectStubs = new OrderedValueObjects<SimpleValueObjectStub>(new SimpleValueObjectStubComparer())
            {
                new SimpleValueObjectStub
                {
                    Address = "2",
                    Age = 23,
                    Date = new DateTime(2017, 11, 30),
                    Name = "Nom1"
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