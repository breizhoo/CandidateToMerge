/////////////////////////////////////////////////////////////////////////////
//
// (c) 2007 BinaryComponents Ltd.  All Rights Reserved.
//
// http://www.binarycomponents.com/
//
/////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryComponents.SuperList
{
	public abstract class RowIdentifier
	{
		public abstract Column ColumnGroup { get; }
		public abstract object[] Items { get; }

		public static bool operator ==( RowIdentifier lhs, RowIdentifier rhs )
		{
			if( object.ReferenceEquals( lhs, rhs ) )
				return true;
			if( object.ReferenceEquals( lhs, null ) )
				return false;
			if( object.ReferenceEquals( rhs, null ) )
				return false;

			return lhs.Equals( rhs );
		}
		public static bool operator !=( RowIdentifier lhs, RowIdentifier rhs )
		{
			return !(lhs == rhs);
		}
		public override bool Equals( object obj )
		{
			throw new NotImplementedException();
		}
		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

	}
}
