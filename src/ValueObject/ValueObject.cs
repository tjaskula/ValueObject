using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace ValueObject
{
    /// <summary>
	/// Base class for Value Objects. DDD concept.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
    public abstract class ValueObject<T> : IEquatable<T> where T : class
    {
        /// <summary>
        /// Compares two Value Objects according to the values of all fields in objects.
        /// </summary>
        /// <param name="obj">Object to compare to.</param>
        /// <returns>True if objects are considered equal.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var other = obj as T;

            return Equals(other);
        }

        /// <summary>
        /// Returns hashcode value calculated according to the values of all fields in objects.        
        /// </summary>
        /// <returns>Hashcode value.</returns>
        public override int GetHashCode()
        {
            IEnumerable<FieldInfo> fields = GetFields();

            const int startValue = 17;
            const int multiplier = 59;

            int hashCode = startValue;

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(this);

                if (value != null)
                    hashCode = hashCode * multiplier + value.GetHashCode();
            }

            return hashCode;
        }

        /// <summary>
        /// Compares two Value Objects according to the values of all fields in objects.
        /// </summary>
        /// <param name="other">Object to compare to.</param>
        /// <returns>True if objects are considered equal.</returns>
        public virtual bool Equals(T other)
        {
            if (other == null)
                return false;

            Type t = GetType();

            FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (FieldInfo field in fields)
            {
                object value1 = field.GetValue(other);
                object value2 = field.GetValue(this);

                if (value1 == null)
                {
                    if (value2 != null)
                        return false;
                }
                else if (value2 == null)
                {
                    return false;
                }
                else if (typeof(DateTime).IsAssignableFrom(field.FieldType) ||
                         typeof(DateTime?).IsAssignableFrom(field.FieldType))
                {
                    string dateString1 = ((DateTime)value1).ToLongDateString();
                    string dateString2 = ((DateTime)value2).ToLongDateString();

                    if (!dateString1.Equals(dateString2))
                    {
                        return false;
                    }
                }
                else if (typeof(string).IsAssignableFrom(field.FieldType))
                {
                    string string1 = value1.ToString();
                    string string2 = value2.ToString();

                    if (!string1.Equals(string2, StringComparison.OrdinalIgnoreCase))
                        return false;
                }
                else if (typeof(IEnumerable).IsAssignableFrom(field.FieldType))
                {
                    var col1 = value1 as IEnumerable;
                    var col2 = value2 as IEnumerable;
                    
                    if (col1 == null || col2 == null)
                        return false;

                    var e1 = col1.GetEnumerator();
                    var e2 = col2.GetEnumerator();
                    while (e1.MoveNext() && e2.MoveNext())
                    {
                        var item1 = e1.Current;
                        var item2 = e2.Current;

                        if (!item1.Equals(item2))
                            return false;
                    }
                }
                else if (!value1.Equals(value2))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Copies the content of public properties to the object passed by parameter.
        /// </summary>
        /// <param name="coptyTo">Object that will contain the copied data.</param>
        public virtual void CopyTo(T coptyTo)
        {
            Type t = GetType();
            PropertyInfo[] properties = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo property in properties)
            {
                var valueToCopy = property.GetValue(this, null);
                if (valueToCopy == null)
                    continue;
                if (property.CanWrite)
                    property.SetValue(coptyTo, valueToCopy, null);
            }
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            Type t = GetType();

            var fields = new List<FieldInfo>();

            while (t != typeof(object))
            {
                fields.AddRange(t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));

                t = t.BaseType;
            }

            return fields;
        }

        /// <summary>
        /// Compares two value objects for equality.
        /// </summary>
        /// <param name="x">The first value object.</param>
        /// <param name="y">The second value object.</param>
        /// <returns>Returns true if two object are the same.</returns>
        public static bool operator ==(ValueObject<T> x, ValueObject<T> y)
        {
            if (ReferenceEquals(x, null))
                return ReferenceEquals(y, null);

            return x.Equals(y);
        }

        /// <summary>
        /// Compares two value objects for inequlaity.
        /// </summary>
        /// <param name="x">The first value object.</param>
        /// <param name="y">The second value object.</param>
        /// <returns>Returns true if two object are not the same.</returns>
        public static bool operator !=(ValueObject<T> x, ValueObject<T> y)
        {
            return !(x == y);
        }
    }
}