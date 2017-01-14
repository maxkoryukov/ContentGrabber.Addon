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
			return null;
		}
		public static int? _Ind(string text, int index, IEnumerable<string> patterns) {
			return null;
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
		public static int? GetPositionOfStringPattern(string text, int index, params string [] patterns) {
			return _Ind(text, index, patterns);
		}

		public static bool DoesExistStringPattern(string text, int index, params string [] patterns) {
			return _Ex(text, index, patterns);
		}
		#endregion
	}
}

