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

namespace BinaryComponents.Utility.Assemblies
{
	public sealed class ManifestResources
	{
		public ManifestResources( string baseNamespace )
			: this( baseNamespace, System.Reflection.Assembly.GetCallingAssembly() )
		{
		}

		public string[] ResourceNames
		{
			get
			{
				return _assembly.GetManifestResourceNames();
			}
		}

		public ManifestResources( string baseNamespace, System.Reflection.Assembly assembly )
		{
			if( baseNamespace == null )
			{
				throw new ArgumentNullException( "baseNamespace" );
			}
			if( assembly == null )
			{
				throw new ArgumentNullException( "assembly" );
			}

			_baseNamespace = baseNamespace;
			_assembly = assembly;
		}

		public System.Xml.XmlDocument GetXmlDocument( string path )
		{
			System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();

			using( System.IO.Stream stream = GetStream( path ) )
			{
				if( stream == null )
				{
					throw new ArgumentException( string.Format( "Resource '{0}' not found.", path ), "path" );
				}
				xmlDoc.Load( stream );
			}

			return xmlDoc;
		}

		public string GetString( string path )
		{
			using( System.IO.Stream stream = GetStream( path ) )
			using( System.IO.StreamReader sr = new System.IO.StreamReader( stream ) )
			{
				return sr.ReadToEnd();
			}
		}

		public System.IO.Stream GetStream( string path )
		{
			return _assembly.GetManifestResourceStream( _baseNamespace + "." + path );
		}

		public System.Drawing.Icon GetIcon( string path )
		{
			using( System.IO.Stream stream = GetStream( path ) )
			{
				return new System.Drawing.Icon( stream );
			}
		}

		public System.Drawing.Image GetImage( string path )
		{
			using( System.IO.Stream stream = GetStream( path ) )
			{
				return System.Drawing.Image.FromStream( stream );
			}
		}

		private string _baseNamespace;
		private System.Reflection.Assembly _assembly;
	}
}
