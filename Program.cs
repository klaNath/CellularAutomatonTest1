//
//  Program.cs
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
	class MainClass
	{

		public static void Main (string[] args)
		{
			"WakeUp".COut ();
			var MainTask = Task.Run ((Func<Task>)CAMain);
			System.Threading.Thread.Yield ();
			MainTask.Wait ();
			"Bye".COut ();
		}

		public static async Task CAMain(){
			var CellField = new CAField();
			CellField.InitializeWithRamdom ();
			while(true){
				foreach(var s in CellField){
					int Num = CheckBlock (CellField, s);
					CellField [s.xAxis, s.yAxis, s.zAxis] = Num;
				}
			}


		}

		static int CheckBlock (CAField cellField, ThirdDInt s)
		{
			var x = s.xAxis;
			var y = s.yAxis;
			var z = s.zAxis;
			var State = new int[27];
			foreach(var elm in Enumerable.Range(0,27)){
				int _x = x + elm / 9 -1 ;
				int _y = y + (elm / 3) % 3 -1;
				int _z = z + elm % 3 -1;
				State [elm] = cellField [_x, _y, _z];
			}

			return CheckRule (State);
		}

		static int CheckRule (int[] state)
		{
			var sum = state.Sum ();
			if (sum > 5 & sum < 15)
				return 1;
			else
				return 0;
		}
	}

	public class Rule{
		public static readonly int Death = 0;
	}


	public class CAField : IEnumerable<ThirdDInt>
	{

		private static readonly int FieldMax = 500;

		private int[,,] _cellField = new int[FieldMax,FieldMax,FieldMax];

		#region IEnumerable implementation

		public int this[int x, int y, int z]{
			get{
				if (x < 0)
					x = FieldMax + x;
				if (y < 0)
					y = FieldMax + y;
				if (z < 0)
					z = FieldMax + z;
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
						_cellField [i, j, k] = rnd.Next(1);
					}
				}
			}
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
