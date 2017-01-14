using System;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace ContentGrabber.Addon.Test {

	using ContentGrabber.Addon.Ext;

	[TestFixture]
	public class Samples {

		[Test]
		[Platform("Win")]
		public void Bat01() {

			var path = System.IO.Path.Combine("ContentGrabber.Addon.Test", "Bat", "echo.bat");
			ContentGrabber.Addon.Bat.Exec(path);
			Assert.True(true);
		}

		#region PrettyLanguage
		[Test]
		public void PrettyLanguage01() {

			// add to the top of file:
			// using ContentGrabber.Addon.Ext;

			var pretty = StringExt.PrettyLanguage("<tr><td>ITALIAN<");
			Assert.That(pretty, Is.EqualTo("Italian;"));
		}

		[Test]
		public void PrettyLanguage02() {
			// add to the top of file:
			// using ContentGrabber.Addon.Ext;
			var pretty ="<tr><td>ITALIAN<".PrettyLanguage();
			Assert.That(pretty, Is.EqualTo("Italian;"));
		}
		#endregion

		#region JoinNonEmpty
		[Test]
		public void JoinNonEmpty01() {
			var s001 = "a";
			var s002 = "";
			var s003 = "xxxxx";
			string s004 = null;
			var s005 = "123";

			var joined = StringExt.JoinNonEmpty(
				" | ", 
				s001, 
				s002, 
				s003, 
				s004, 
				s005 
				/* s006 .... as many as you wish */
			);
			Assert.That(joined, Is.EqualTo("a | xxxxx | 123"));
		}

		[Test]
		public void JoinNonEmpty02() {
			// add to the top of file:
			// using ContentGrabber.Addon.Ext;

			var s001 = "a";
			var s002 = "";
			var s003 = "xxxxx";
			string s004 = null;
			var s005 = "123";

			var joined = " | ".JoinNonEmpty(
				s001, 
				s002, 
				s003, 
				s004, 
				s005 
				/* s006 .... as many as you wish */
			);
			Assert.That(joined, Is.EqualTo("a | xxxxx | 123"));
		}
		#endregion

		[Test]
		public void ContainsAny02() {
			// add to the top of file:
			// using ContentGrabber.Addon.Ext;.Ext;

			var result = "one two three".ContainsAny("one", "two", "three");
			Assert.That(result, Is.True);
		}
	
		#region Regexes
		[Test]
		public void MatchFirstNotEmpty_01() {
			var result = "one two thress 4 5 6".MatchFirstNotEmpty(@"[a-z]+", @"\d+");
			Assert.That(result, Is.EqualTo("one"));
		}

		[Test]
		public void MatchFirstNotEmpty_02() {
			var result = "one two thress 4 5 6".MatchFirstNotEmpty(new Regex(@"\d+"), new Regex(@"[a-z]+"));
			Assert.That(result, Is.EqualTo("4"));
		}

		[Test]
		public void MatchNotEmpty_01() {
			var result = "one two thress 4 5 6".MatchNotEmpty(@"[a-z]+", @"\d+");
			Assert.That(result, Is.EqualTo(new string [] {"one", "two", "thress", "4", "5", "6"}));
		}

		[Test]
		public void MatchNotEmpty_02() {
			var result = "one two ba thress 4 5 6 ba".MatchNotEmpty(new Regex(@"\d+"), new Regex(@"ba"));
			Assert.That(result, Is.EqualTo(new string [] {"4", "5", "6", "ba", "ba"}));
		}
		#endregion


		#region Get Positions/Position/Existance using array
		[Test]
		public void SearchLists_GetPositionsOfArray() {

			// with open array 
			var result1 = SearchLists.GetPositionsOfArray("one two three", "one", "six", "two");
			// with explicit array
			var result2 = SearchLists.GetPositionsOfArray("one two three", new string [] {"one", "six", "two"});

			Assert.That(result1, Is.EqualTo(result2));

			Assert.That(result1, Is.EqualTo("pos00001;;pos00005"));
			Assert.That(result2, Is.EqualTo("pos00001;;pos00005"));
		}

		[Test]
		public void SearchLists_GetPositionOfArrayPattern() {

			// with open array 
			var result1 = SearchLists.GetPositionOfArrayPattern(
				"one two three", 1, "one", "six", "two"
			);
			// with explicit array
			var result2 = SearchLists.GetPositionOfArrayPattern(
				"one two three", 1, new string [] {"one", "six", "two"}
			);

			var result3 = SearchLists.GetPositionOfArrayPattern(
				"one two three", 2, new string [] {"one", "six", "two"} );
			//   012345678...    ^                                ^
			//       │           │                                │
			//       └─┐         └──────────────────┐             └────────┐
			// answer ─┘ , since we are looking for second pattern (it is here)
			Assert.That(result1, Is.EqualTo(result2));
			Assert.That(result1, Is.EqualTo(null));
			Assert.That(result2, Is.EqualTo(null));
			Assert.That(result3, Is.EqualTo(4));
		}

		[Test]
		public void SearchLists_DoesExistArrayPattern() {

			// with open array 
			var result1 = SearchLists.DoesExistArrayPattern(
				"one two three", 1, "one", "six", "two"
			);
			// with explicit array
			var result2 = SearchLists.DoesExistArrayPattern(
				"one two three", 1, new string [] {"one", "six", "two"}
			);

			var result3 = SearchLists.DoesExistArrayPattern(
				"one two three", 2, new string [] {"one", "six", "two"} );
			//   012345678...    ^                                ^
			//       │           │                                │
			//       └─┐         └──────────────────┐             └────────┐
			// answer ─┘ , since we are looking for second pattern (it is here)
			Assert.That(result1, Is.EqualTo(result2));
			Assert.That(result1, Is.EqualTo(false));
			Assert.That(result2, Is.EqualTo(false));
			Assert.That(result3, Is.EqualTo(true));
		}
		#endregion

		#region Get Positions/Position/Existance using array
		[Test]
		public void SearchLists_GetPositionsOfString() {

			var result = SearchLists.GetPositionsOfString("one two three", "one;six;two");
			Assert.That(result, Is.EqualTo("pos00001;;pos00005"));
		}

		[Test]
		public void SearchLists_GetPositionOfStringPattern() {

			var result = SearchLists.GetPositionOfStringPattern(
				"one two three", 2, "one;six;two"
			);
			Assert.That(result, Is.EqualTo(4));
		}

		[Test]
		public void SearchLists_DoesExistStringPattern() {

			var result = SearchLists.DoesExistStringPattern(
				"one two three", 2, "one;six;two"
			);
			Assert.That(result, Is.EqualTo(true));
		}
		#endregion
	}
}

