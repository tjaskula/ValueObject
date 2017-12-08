using System;
using FluentAssertions;
using Xunit;

namespace DddBuildingBlocks.ValueObject.UnitTests
{
    public abstract class ValueObjectTests<T> where T : ValueObject<T>
    {
        protected abstract ValueObject<T> CreateValueObject();

        protected abstract ValueObject<T> CreateDifferentValueObject();

        [Fact]
        public void Should_properly_report_equality_with_default()
        {
            T stub = default(T);
            T stub2 = default(T);

            stub.Should().Be(stub2);
        }

        [Fact]
        public void Should_properly_report_equality_with_hydrated_props()
        {
            ValueObject<T> stub = CreateValueObject();
            ValueObject<T> stub2 = CreateValueObject();

            stub.Should().Be(stub2);
            stub.Should().NotBeSameAs(stub2);
        }

        [Fact]
        public void Should_properly_report_equality_when_same_reference()
        {
            ValueObject<T> stub = CreateValueObject();

            stub.Should().Be(stub);
            stub.Should().BeSameAs(stub);
        }

        [Fact]
        public void Should_properly_report_nonequality()
        {
            ValueObject<T> stub = CreateValueObject();
            ValueObject<T> stub2 = CreateDifferentValueObject();

            stub.Should().NotBe(stub2);
            stub.Should().NotBeSameAs(stub2);
        }

        [Fact]
        public void Should_properly_report_nonequality_when_not_same_type()
        {
            var stub = new Stub();
            ValueObject<T> stub2 = CreateValueObject();

            stub2.Equals(stub).Should().BeFalse();
            stub.Equals(stub2).Should().BeFalse();
        }
        
        [Fact]
        public void Should_properly_report_equality_with_operator()
        {
            ValueObject<T> stub = CreateValueObject();
            ValueObject<T> stub2 = CreateValueObject();

            (stub == stub2).Should().BeTrue();
        }
        
        [Fact]
        public void Should_properly_report_nonequality_with_operator()
        {
            ValueObject<T> stub = CreateValueObject();
            ValueObject<T> stub2 = CreateDifferentValueObject();

            (stub != stub2).Should().BeTrue();
        }

        [Fact]
        public void Should_report_get_hash_code_different_if_different_values()
        {
            ValueObject<T> stub = CreateValueObject();
            ValueObject<T> stub2 = CreateDifferentValueObject();

            stub.GetHashCode().Should().NotBe(stub2.GetHashCode());
        }

        [Fact]
        public void Should_report_get_hash_code_same_if_same_values_with_list_of_list()
        {
            ValueObject<T> stub = CreateValueObject();
            ValueObject<T> stub2 = CreateValueObject();

            stub.GetHashCode().Should().Be(stub2.GetHashCode());
        }
        
        [Fact]
        public void Should_report_get_hash_code_same_if_same_values_with_list()
        {
            ValueObject<T> stub = CreateDifferentValueObject();
            ValueObject<T> stub2 = CreateDifferentValueObject();

            stub.GetHashCode().Should().Be(stub2.GetHashCode());
        }

        [Fact]
        public void Should_report_not_equal_if_obj_is_null()
        {
            ValueObject<T> stub = CreateValueObject();
            stub.Equals(null).Should().BeFalse();
        }

        [Fact]
        public void Should_compare_null_attributes_to_non_null_attributes()
        {
            var stub1 = new ComplexValueObjectStub();
            var stub2 = new ComplexValueObjectStub();

            stub1.Address = "123 Main";

            stub1.Should().NotBe(stub2);
            stub2.Should().NotBe(stub1);
        }

        [Fact]
        public void Should_compare_null_dates_to_non_null_attributes()
        {
            var stub1 = new ComplexValueObjectStub();
            var stub2 = new ComplexValueObjectStub();

            stub1.Date = DateTime.Now;

            stub1.Should().NotBe(stub2);
            stub2.Should().NotBe(stub1);
        }

        [Fact]
        public void Should_copy_one_object_to_another()
        {
            var initialObject = new ComplexValueObjectStub
                                {
                                    Address = "Address",
                                    Age = 18,
                                    Name= "Name",
                                    Date = DateTime.Now
                                };

            var destinationObject = new ComplexValueObjectStub();

            initialObject.CopyTo(destinationObject);

            initialObject.Equals(destinationObject).Should().BeTrue();
        }

        protected class Stub { }
    }

    public class ValueObjectStubTester : ValueObjectTests<ComplexValueObjectStub>
    {
        protected override ValueObject<ComplexValueObjectStub> CreateValueObject()
        {
            
            return new ComplexValueObjectStub { Address = "4", Age = 2, Name = null, Items = GetOrderedValueObjects() };
        }

        protected override ValueObject<ComplexValueObjectStub> CreateDifferentValueObject()
        {
            return new ComplexValueObjectStub { Address = "1", Age = 2, Name = null, Items = GetOrderedValueObjects() };
        }

        private OrderedValueObjects<SimpleValueObjectStub> GetOrderedValueObjects()
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
    }
}