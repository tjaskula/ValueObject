using System;
using System.Collections;
using FluentAssertions;
using Xunit;

namespace DomainBuildingBlocks.UnitTests
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

        [Fact(Skip = "Failing test while featue in progress")]
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
        public void Should_report_get_hash_code_different_if_different_values()
        {
            ValueObject<T> stub = CreateValueObject();
            ValueObject<T> stub2 = CreateDifferentValueObject();

            stub.GetHashCode().Should().NotBe(stub2.GetHashCode());
        }

        [Fact]
        public void Should_report_get_hash_code_same_if_same_values()
        {
            ValueObject<T> stub = CreateValueObject();
            ValueObject<T> stub2 = CreateValueObject();

            stub.GetHashCode().Should().Be(stub2.GetHashCode());
        }

        [Fact]
        public void Should_report_not_equal_if_obj_is_null()
        {
            ValueObject<T> stub = CreateValueObject();
            (stub.Equals(null)).Should().BeFalse();
        }

        [Fact]
        public void Should_compare_null_attributes_to_non_null_attributes()
        {
            var stub1 = new ValueObjectStub();
            var stub2 = new ValueObjectStub();

            stub1.Address = "123 Main";

            stub1.Should().NotBe(stub2);
            stub2.Should().NotBe(stub1);
        }

        [Fact]
        public void Should_compare_null_dates_to_non_null_attributes()
        {
            var stub1 = new ValueObjectStub();
            var stub2 = new ValueObjectStub();

            stub1.Date = DateTime.Now;

            stub1.Should().NotBe(stub2);
            stub2.Should().NotBe(stub1);
        }

        [Fact]
        public void Should_copy_one_object_to_another()
        {
            var initialObject = new ValueObjectStub
                                {
                                    Address = "Address",
                                    Age = 18,
                                    Name= "Name",
                                    Date = DateTime.Now
                                };

            var destinationObject = new ValueObjectStub();

            initialObject.CopyTo(destinationObject);

            initialObject.Equals(destinationObject).Should().BeTrue();
        }

        protected class Stub { }
    }

    public class ValueObjectStub : ValueObject<ValueObjectStub>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public DateTime? Date { get; set; }
        public IList Items { get; set; }
    }

    public class ValueObjectStubTester : ValueObjectTests<ValueObjectStub>
    {
        protected override ValueObject<ValueObjectStub> CreateValueObject()
        {
            return new ValueObjectStub { Address = "4", Age = 2, Name = null, Items = new [] {new [] {1, 2, 3} } };
        }

        protected override ValueObject<ValueObjectStub> CreateDifferentValueObject()
        {
            return new ValueObjectStub { Address = "1", Age = 2, Name = null, Items = new[] { 1, 2, 5 } };
        }
    }
}