namespace NewidSnippetTest
{

	using System;
	using System.ComponentModel;

	[TypeConverter(typeof(SomeIdTypeConverter))]
	[Serializable]
	/// <summary>
	/// Value representing the identity of Some.
	/// </summary>
	public readonly struct SomeId : IComparable<SomeId>, IEquatable<SomeId>
	{
		/// <summary>
		/// Identity value of the SomeId.
		/// </summary>
		public string Id { get; }

		/// <summary>
		/// Creates a new value of SomeId
		/// </summary>
		/// <param name="value">Identity value</param>
		public SomeId(string id)
		{
			if (String.IsNullOrEmpty(id))
				throw new InvalidOperationException("The identity " + nameof(SomeId) + " cannot be empty or a null string.");

			Id = id;
		}

		/// <summary>
		/// Checks whether two identity values are equal.
		/// </summary>
		/// <param name="other">Other identity struct.</param>
		/// <returns>True if identities are equal. False otherwise.</returns>
		public bool Equals(SomeId other) => CompareTo(other) == 0;

		/// <summary>
		/// Checks whether two values of <see cref="SomeId"/> are equal.
		/// </summary>
		/// <param name="other">Other identity struct.</param>
		/// <returns>True if identities are equal. False otherwise.</returns>
		public override bool Equals(object obj)
		{
			if (obj is SomeId)
				return CompareTo((SomeId)obj) == 0;

			return false;
		}

		/// <summary>
		/// Compares values of two <see cref="SomeId"/> identities.
		/// </summary>
		/// <param name="other">Other identity struct.</param>
		/// <returns>0 if two <see cref="SomeId"/> are equal, 1 if this <see cref="SomeId"/> comes after the other <see cref="SomeId"/> and -1 if this <see cref="SomeId"/> comes before the other <see cref="SomeId"/>.</returns>
		public int CompareTo(SomeId other) => Id.CompareTo(other.Id);

		/// <summary>
		/// Returns the hashcode of the <see cref="SomeId"/>.
		/// </summary>
		/// <returns>Hash code of <see cref="SomeId"/></returns>
		public override int GetHashCode() => Id.GetHashCode();

		/// <summary>
		/// Returns the string representation of <see cref="SomeId"/>.
		/// </summary>
		/// <returns>String representation of <see cref="SomeId"/></returns>
		public override string ToString() => Id.ToString();

		/// <summary>
		/// Method for allowing explicit conversion from the primitive type to the <see cref="SomeId"/>.
		/// </summary>
		/// <param name="value">Value of the identity</param>
		public static explicit operator SomeId(string value) => new SomeId(value);

		/// <summary>
		/// Method for allowing explicit conversion from <see cref="SomeId"/> to the identity primitive type.
		/// </summary>
		/// <param name="localSomeId">Identity struct</param>
		public static explicit operator string(SomeId localSomeId) => localSomeId.Id;

		/// <summary>
		/// Overloads the equality operator for comparing the values of two <see cref="SomeId"/>.
		/// </summary>
		/// <param name="a">First <see cref="SomeId"/></param>
		/// <param name="b">Second <see cref="SomeId"/></param>
		/// <returns>True if two <see cref="SomeId"/> values are equal. False otherwise.</returns>
		public static bool operator ==(SomeId a, SomeId b) => a.CompareTo(b) == 0;

		/// <summary>
		/// Overloads the non-equality operator for comparing the values of two <see cref="SomeId"/>.
		/// </summary>
		/// <param name="a">First <see cref="SomeId"/></param>
		/// <param name="b">Second <see cref="SomeId"/></param>
		/// <returns>True if two <see cref="SomeId"/> values are not equal. False otherwise.</returns>
		public static bool operator !=(SomeId a, SomeId b) => !(a == b);

		class SomeIdTypeConverter : TypeConverter
		{
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) 
				=> sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

			public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
			{
				var stringValue = value as string;
				if (!String.IsNullOrEmpty(stringValue))
				{
					return new SomeId(stringValue);
				}
				return base.ConvertFrom(context, culture, value);
			}
		}
	}
}