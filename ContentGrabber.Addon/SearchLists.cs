using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ContentGrabber.Addon {
	public static class SearchLists {
		private static IEnumerable<string> _PatternsToEnum(string patterns) {
			return patterns
				.Split(';')
				.Select(x => x.Trim());
		}

		#region Real
		public static string _Pos(string text, IEnumerable<string> patterns) {
			var res = new List<string>();
			foreach (var p in patterns) {
				var pos = text.IndexOf(p);

				if (pos < 0) {
					// not found
					res.Add(string.Empty);
				} else {
					res.Add(string.Format("pos{0:0000#}", pos + 1));
				}
			}
			return string.Join(";", res);
		}

		public static int? _Ind(string text, int index, IEnumerable<string> patterns) {
			var p = patterns.ElementAt(index);
			var pos = text.IndexOf(p);

			if (pos < 0) {
				return null;
			};
			return pos;
		}

		public static bool _Ex(string text, int index, IEnumerable<string> patterns) {
			return _Ind(text, index, patterns).HasValue;
		}
		#endregion

		#region By Array
		public static string GetPositionsOfArray(string text, params string [] patterns) {
			return _Pos(text, patterns);
		}

		public static int? GetPositionOfArrayPattern(string text, int index, params string [] patterns) {
			return _Ind(text, index, patterns);
		}

		public static bool DoesExistArrayPattern(string text, int index, params string [] patterns) {
			return _Ex(text, index, patterns);
		}
		#endregion

		#region By String
		public static string GetPositionsOfString(string text, string patterns) {
			return _Pos(text, _PatternsToEnum(patterns));
		}
		public static int? GetPositionOfStringPattern(string text, int index, string patterns) {
			return _Ind(text, index,  _PatternsToEnum(patterns));
		}

		public static bool DoesExistStringPattern(string text, int index, string patterns) {
			return _Ex(text, index,  _PatternsToEnum(patterns));
		}
		#endregion
	}
}

