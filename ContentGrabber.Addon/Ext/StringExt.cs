using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ContentGrabber.Addon.Ext {
	public static class StringExt {
		private static Regex DryPrettyLaguage = new Regex(@"<\w+>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public static string ToTitleCase(this string s, CultureInfo culture = null){
			if (string.IsNullOrWhiteSpace (s)) {
				return s;
			}

			if (null == culture) {
				culture = CultureInfo.InvariantCulture;
			}

			return culture.TextInfo.ToTitleCase(s.ToLower());
		}

		public static string PrettyLanguage (this string input) {
			if (string.IsNullOrWhiteSpace(input)) {
				return input;
			}

			var result = DryPrettyLaguage.Replace(input, "")
				.Trim()
				.ToTitleCase(System.Threading.Thread.CurrentThread.CurrentCulture)
			;

			if (result.EndsWith("<")) {
				result = result.Remove(result.Length-1);
			};
			result += ";";

			return result;
		}

		public static string JoinNonEmpty(this string sep, IEnumerable<string> str) {
			if (null == str) {
				return null;
			};

			var res = str
				.Where(x => !string.IsNullOrEmpty(x))	// filter empty and null
				.Aggregate<string, StringBuilder>(null, (agg, val) => {
				if (null == agg) {
					agg = new StringBuilder(val);
				} else {
					agg.Append(sep);
					agg.Append(val);
				}
				return agg;
			});

			if (null == res) {
				return null;
			}

			return res.ToString();
		}

		public static string JoinNonEmpty(this string sep, params string [] str) {
			return StringExt.JoinNonEmpty(sep, (IEnumerable<string>)str);
		}

		public static bool ContainsCustom(this string str, string sub) {
			return (str.Length - str.Replace(sub, String.Empty).Length) / sub.Length > 0;
		}

		public static bool ContainsLib(this string str, string sub) {
			return str.Contains(sub);
		}

		public static bool ContainsAny(this string str, params string [] substrings) {
			if (null == str) {
				return false;
			}
			foreach (var sub in substrings) {
				if (null == sub)
					continue;
				if (str.Contains(sub))
					return true;
			}
			return false;
		}

		#region Regexes
		public static string MatchFirstNotEmpty(this string text, params string [] patterns) {
			foreach (var p in patterns) {
				var r = new Regex(p);
				var m = r.Match(text);
				if (m.Success) {
					if (!string.IsNullOrEmpty(m.Value)) {
						return m.Value;
					}
				}
			}

			return null;
		}

		public static string MatchFirstNotEmpty(this string text, params Regex [] patterns) {
			foreach (var r in patterns) {
				var m = r.Match(text);
				if (m.Success) {
					if (!string.IsNullOrEmpty(m.Value))
						return m.Value;
				};
			}

			return null;
		}

		public static string [] MatchNotEmpty(this string text, params string [] patterns) {
			var r = patterns
				.Select(x => new Regex(x))
				.ToArray();
			return MatchNotEmpty(text, r);
		}

		public static string [] MatchNotEmpty(this string text, params Regex [] patterns) {
			var res = new List<string>();
			foreach (var r in patterns) {
				foreach (Match m in r.Matches(text)){
					res.Add(m.Value);
				}
			}
			return res
				.Where(x => !string.IsNullOrEmpty(x))
				.ToArray();
		}
		#endregion
	}
}

