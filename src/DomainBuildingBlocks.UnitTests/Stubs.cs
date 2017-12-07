using System;
using System.Collections.Generic;

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
        public OrderedValueObjects<SimpleValueObjectStub> Items { get; set; }
    }
    
    public class SimpleValueObjectStubComparer : Comparer<SimpleValueObjectStub>
    {
        public override int Compare(SimpleValueObjectStub x, SimpleValueObjectStub y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }

                // If x is null and y is not null, y
                // is greater. 
                return -1;
            }

            // If x is not null...
            //
            if (y == null)
                // ...and y is null, x is greater.
            {
                return 1;
            }

            return x.Name.CompareTo(y.Name);
        }
    }
}