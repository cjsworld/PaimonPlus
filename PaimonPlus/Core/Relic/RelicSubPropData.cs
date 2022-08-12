using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PaimonPlus.Core {
	public class RelicSubPropData {
		public readonly PropType PropType;
		public readonly List<double> Values = new();
		private double BaseValue;
		private double DiffAvg;

		public RelicSubPropData(PropType propType) {
			PropType = propType;
		}

		public void AddValue(double value) {
			Values.Add(value);
			BaseValue = Values[0];
			if (Values.Count == 0) {
				DiffAvg = 0.0;
			} else {
				var last = Values[Values.Count - 1];
				DiffAvg = (last - BaseValue) / (Values.Count - 1);
			}
		}

		public double CalcPreciseValue(double value) {
			double result = 0;
			int hitCount = 0;
			int totalRank = 0;
			if (CalcPreciseValue(value, ref result, ref hitCount, ref totalRank)) {
				return result;
			} else {
				return value;
			}
		}

		public bool CalcPreciseValue(double value, ref double result, ref int hitCount, ref int totalRank) {
			for (var n = 1; n <= 6; n++) {
				var x = (value - n * BaseValue);
				x /= DiffAvg;

				//Trace.WriteLine($">>>n={n},x={x}");
				var k = (int)Math.Round(x);
				if (Math.Abs(x - k) < 0.25) {
					if (k < 0 || k > 3 * n) {
						continue;
					}
					hitCount = n;
					totalRank = k;
					result = BaseValue * n + DiffAvg * k;
					return true;
				}
			}
			Trace.WriteLine($"Unable to calc percise value for {PropType} with {value}");
			return false;
		}
	}
}
