﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RetryLogic.Tests
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	public partial class TestDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public TestDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TestDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TestDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TestDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<TestObject> TestObjects
		{
			get
			{
				return this.GetTable<TestObject>();
			}
		}
	}
	
	[Table(Name="")]
	public partial class TestObject
	{
		
		private string _String;
		
		private int _DateTime;
		
		private bool _Bool;
		
		public TestObject()
		{
		}
		
		[Column(Storage="_String", CanBeNull=false)]
		public string String
		{
			get
			{
				return this._String;
			}
			set
			{
				if ((this._String != value))
				{
					this._String = value;
				}
			}
		}
		
		[Column(Name="DateTime", Storage="_DateTime")]
		public int Number
		{
			get
			{
				return this._DateTime;
			}
			set
			{
				if ((this._DateTime != value))
				{
					this._DateTime = value;
				}
			}
		}
		
		[Column(Storage="_Bool")]
		public bool Bool
		{
			get
			{
				return this._Bool;
			}
			set
			{
				if ((this._Bool != value))
				{
					this._Bool = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
