﻿//
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
	partial class MainClass
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
			var NextField = new CAField ();

			CellField.InitializeWithRamdom ();
			var swatch = new System.Diagnostics.Stopwatch ();
			while(true){
				swatch.Start ();
				await Task.Run(() => {
					Parallel.ForEach (CellField, s => {
						NextField [s.xAxis, s.yAxis, s.zAxis] = CheckBlock (CellField, s);
					});
				});
				CellField = NextField.Copy();
				swatch.Stop ();
				Console.Write (swatch.ElapsedMilliseconds + " : ");
				Console.WriteLine (CellField.Sum(x => x.Value) / (double)CAField.FieldSize * 100);
				swatch.Reset ();
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
			if (sum > 5 & sum < 16)
				return 1;
			else
				return 0;
		}
	}
}
