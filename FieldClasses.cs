//
//  FieldClasses.cs
//
//  Author:
//       kazusa Okuda <kazusa@klamath.jp>
//
//  Copyright (c) 2015 kazusa Okuda
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace CellularAutomatonTest1
{

	public class CA4Field : IEnumerable<FourthDInt>
	{

		public static readonly int FieldMax = 50;

		public static long FieldSize{
			get{return FieldMax * FieldMax * FieldMax;}
		}

		int[,,,] _cellField = new int[FieldMax,FieldMax,FieldMax,FieldMax];

		#region IEnumerable implementation

		public int this[int w, int x, int y, int z]{
			get{
				if (x < 0)
					x = FieldMax + x;
				else if (x >= FieldMax)
					x = x - FieldMax;
				if (y < 0)
					y = FieldMax + y;
				else if (y >= FieldMax)
					y = y - FieldMax;
				if (z < 0)
					z = FieldMax + z;
				else if (z >= FieldMax)
					z = z - FieldMax;
				if (w < 0)
					w = FieldMax + w;
				else if (w >= FieldMax)
					w = w - FieldMax;
				return this._cellField[w,x,y,z];}

			set{this._cellField [w, x, y, z] = value;}
		}

		public IEnumerator<FourthDInt> GetEnumerator ()
		{
			for(var i = 0;_cellField.GetLength(0)>i;i++){
				for(var j = 0;_cellField.GetLength(1)>j;j++){
					for(var k = 0;_cellField.GetLength(2)>k;k++){
						for(var l = 0;_cellField.GetLength(3)>l;l++){
							yield return new FourthDInt(i,j,k,l,_cellField[i,j,k,l]);
						}
					}
				}
			}
		}

		#endregion
		#region IEnumerable implementation
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return this.GetEnumerator ();
		}
		#endregion

		public CA4Field(){
			for(var i = 0;_cellField.GetLength(0)>i;i++){
				for(var j = 0;_cellField.GetLength(1)>j;j++){
					for(var k = 0;_cellField.GetLength(2)>k;k++){
						for (var l = 0; _cellField.GetLength (3) > l; l++) {
							_cellField [i, j, k, l] = 0;
						}
					}
				}
			}
		}

		public void Initialize ()
		{
			for(var i = 0;_cellField.GetLength(0)>i;i++){
				for(var j = 0;_cellField.GetLength(1)>j;j++){
					for(var k = 0;_cellField.GetLength(2)>k;k++){
						for (var l = 0; _cellField.GetLength (3) > l; l++) {
							_cellField [i, j, k,l] = 0;
						}
					}
				}
			}
		}
		public void Initialize (int val)
		{
			for(var i = 0;_cellField.GetLength(0)>i;i++){
				for(var j = 0;_cellField.GetLength(1)>j;j++){
					for(var k = 0;_cellField.GetLength(2)>k;k++){
						for (var l = 0; _cellField.GetLength (3) > l; l++) {
							_cellField [i, j, k, l] = val;
						}
					}
				}
			}
		}

		public void InitializeWithRamdom ()
		{
			var rnd = new System.Random ();
			for(var i = 0;_cellField.GetLength(0)>i;i++){
				for(var j = 0;_cellField.GetLength(1)>j;j++){
					for(var k = 0;_cellField.GetLength(2)>k;k++){
						for (var l = 0; _cellField.GetLength (3) > l; l++) {
							_cellField [i, j, k, l] = rnd.Next () % 2;
						}
					}
				}
			}
		}

		public CA4Field Copy(){
			var res = new CA4Field();
			for(var i = 0;_cellField.GetLength(0)>i;i++){
				for(var j = 0;_cellField.GetLength(1)>j;j++){
					for(var k = 0;_cellField.GetLength(2)>k;k++){
						for (var l = 0; _cellField.GetLength (3) > l; l++) {
							res [i, j, k,l] = _cellField [i, j, k,l];
						}
					}
				}
			}
			return res;
		}
	}

	public class CAField : IEnumerable<ThirdDInt>
	{

		public static readonly int FieldMax = 50;

		public static long FieldSize{
			get{return FieldMax * FieldMax * FieldMax;}
		}

		int[,,] _cellField = new int[FieldMax,FieldMax,FieldMax];

		#region IEnumerable implementation

		public int this[int x, int y, int z]{
			get{
				if (x < 0)
					x = FieldMax + x;
				else if (x >= FieldMax)
					x = x - FieldMax;
				if (y < 0)
					y = FieldMax + y;
				else if (y >= FieldMax)
					y = y - FieldMax;
				if (z < 0)
					z = FieldMax + z;
				else if (z >= FieldMax)
					z = z - FieldMax;
				return this._cellField[x,y,z];}

			set{this._cellField [x, y, z] = value;}
		}
		//
		//		public int this[Tuple<int,int,int> t]{
		//			get{return this._cellField[t.Item1,t.Item2,t.Item3];}
		//			set{this._cellField [t.Item1, t.Item2, t.Item3] = value;}
		//		}

		public IEnumerator<ThirdDInt> GetEnumerator ()
		{
			for(var i = 0;_cellField.GetLength(0)>i;i++){
				for(var j = 0;_cellField.GetLength(1)>j;j++){
					for(var k = 0;_cellField.GetLength(2)>k;k++){
						yield return new ThirdDInt(i,j,k,_cellField[i,j,k]);
					}
				}
			}
		}

		#endregion
		#region IEnumerable implementation
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return this.GetEnumerator ();
		}
		#endregion

		public CAField(){
			for(var i = 0;_cellField.GetLength(0)>i;i++){
				for(var j = 0;_cellField.GetLength(1)>j;j++){
					for(var k = 0;_cellField.GetLength(2)>k;k++){
						_cellField [i, j, k] = 0;
					}
				}
			}
		}

		public void Initialize ()
		{
			for(var i = 0;_cellField.GetLength(0)>i;i++){
				for(var j = 0;_cellField.GetLength(1)>j;j++){
					for(var k = 0;_cellField.GetLength(2)>k;k++){
						_cellField [i, j, k] = 0;
					}
				}
			}
		}
		public void Initialize (int val)
		{
			for(var i = 0;_cellField.GetLength(0)>i;i++){
				for(var j = 0;_cellField.GetLength(1)>j;j++){
					for(var k = 0;_cellField.GetLength(2)>k;k++){
						_cellField [i, j, k] = val;
					}
				}
			}
		}

		public void InitializeWithRamdom ()
		{
			var rnd = new System.Random ();
			for(var i = 0;_cellField.GetLength(0)>i;i++){
				for(var j = 0;_cellField.GetLength(1)>j;j++){
					for(var k = 0;_cellField.GetLength(2)>k;k++){
						_cellField [i, j, k] = rnd.Next() % 2;

					}
				}
			}
		}

		public CAField Copy(){
			var res = new CAField();
			for(var i = 0;_cellField.GetLength(0)>i;i++){
				for(var j = 0;_cellField.GetLength(1)>j;j++){
					for(var k = 0;_cellField.GetLength(2)>k;k++){
						res[i,j,k] =  _cellField [i, j, k];
					}
				}
			}
			return res;
		}
	}

	public class FourthDInt:IComparable
	{
		#region IComparable implementation

		public int CompareTo (object obj)
		{
			if (obj == null)
			{
				throw new NullReferenceException ();
			}

			if (this.GetType() != obj.GetType())
			{
				throw new ArgumentException("Type is not ThirdDInt", "obj");
			}

			return this.Value.CompareTo(((ThirdDInt)obj).Value);
		}

		#endregion

		public int wAxis{ get; set;}
		public int xAxis{get;set;}
		public int yAxis{get;set;}
		public int zAxis{get;set;}
		public int Value{get;set;}

		public FourthDInt(){
			wAxis = 0;
			xAxis = 0;
			yAxis = 0;
			zAxis = 0;
			Value = 0;
		}

		public FourthDInt(int w, int x, int y, int z, int val){
			wAxis = w;
			xAxis = x;
			yAxis = y;
			zAxis = z;
			Value = val;
		}

		public override string ToString(){
			string s = $"{this.wAxis} , {this.xAxis} , {this.yAxis} , {this.zAxis} : {this.Value}";
			return s;
		}

		public static int operator+ (FourthDInt a, FourthDInt b){
			return a.Value + b.Value;
		} 

		public static int operator- (FourthDInt a, FourthDInt b){
			return a.Value - b.Value;
		} 
	}



	public class ThirdDInt:IComparable
	{
		#region IComparable implementation

		public int CompareTo (object obj)
		{
			if (obj == null)
			{
				throw new NullReferenceException ();
			}

			if (this.GetType() != obj.GetType())
			{
				throw new ArgumentException("Type is not ThirdDInt", "obj");
			}

			return this.Value.CompareTo(((ThirdDInt)obj).Value);
		}

		#endregion

		public int xAxis{get;set;}
		public int yAxis{get;set;}
		public int zAxis{get;set;}
		public int Value{get;set;}

		public ThirdDInt(){
			xAxis = 0;
			yAxis = 0;
			zAxis = 0;
			Value = 0;
		}



		public ThirdDInt(int x, int y, int z, int val){
			xAxis = x;
			yAxis = y;
			zAxis = z;
			Value = val;
		}

		public override string ToString(){
			string s = $"{this.xAxis} , {this.yAxis} , {this.zAxis} : {this.Value}";
			return s;
		}

		public static int operator+ (ThirdDInt a, ThirdDInt b){
			return a.Value + b.Value;
		} 

		public static int operator- (ThirdDInt a, ThirdDInt b){
			return a.Value - b.Value;
		} 
	}

	public static class StringExtention{
		public static void COut(this string value){
			Console.WriteLine(value);
		}
	}

}

