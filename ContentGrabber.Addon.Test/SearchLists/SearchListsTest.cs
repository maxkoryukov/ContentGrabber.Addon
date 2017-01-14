using System;
using NUnit.Framework;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace ContentGrabber.Addon.Test.SearchListsTest {

	using ContentGrabber.Addon;

	[TestFixture]
	public class Common {

		#region Infrastructure
		private class CaseData{
			public string text;
			public List<string> patterns;
			public List<int?> positions;
		}

		private static List<CaseData> RawCases;
		#endregion

		/// <summary>
		/// Initializes raw test data
		/// </summary>
		static Common() {
			RawCases = new List<CaseData>();
			RawCases.Add(new CaseData() {
				text = "one apple two women three cats",
				patterns = new List<string>() { "one", "three", "mocha" },
				positions = new List<int?>() { 0, 20, null }
			});
		}

		public static IEnumerable TestCases_GetPositionsOfString() {
			var d = RawCases[0];
			var ans = d.positions
				.Select(x => {
					return x.HasValue ? (int?)x + 1 : (int?)null;
				})
				.Select(x => x.ToString())
				.ToArray();
			yield return new object[] {d.text, string.Join(";", d.patterns), string.Join(";", ans)};
		}
		[Test]
		[TestCaseSource("TestCases_GetPositionsOfString")]
		public void GetPositionsOfString(string input, string patterns, string res) {
			var act = SearchLists.GetPositionsOfString(input, patterns);
			Assert.That(act, Is.EqualTo(res));
		}


		public static IEnumerable TestCases_GetPositionsOfArray() {
			var d = RawCases[0];
			var ans = d.positions
				.Select(x => {
					return x.HasValue ? (int?)x + 1 : (int?)null;
				})
				.Select(x => x.ToString())
				.ToArray();
			yield return new object[] {d.text, d.patterns.ToArray(), string.Join(";", ans)};
		}
		[Test]
		[TestCaseSource("TestCases_GetPositionsOfArray")]
		public void GetPositionsOfArray(string input, string [] patterns, string res) {
			var act = SearchLists.GetPositionsOfArray(input, patterns);
			Assert.That(act, Is.EqualTo(res));
		}

	}
}

