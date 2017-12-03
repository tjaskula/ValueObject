using System;
using System.Collections;

namespace DomainBuildingBlocks.UnitTests
{
    public class SimpleValueObjectStub : ValueObject<SimpleValueObjectStub>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public DateTime? Date { get; set; }
    }
    
    public class ComplexValueObjectStub : ValueObject<ComplexValueObjectStub>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public DateTime? Date { get; set; }
        public IList Items { get; set; }
    }
}