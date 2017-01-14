using System;
using NUnit.Framework;
using System.Text.RegularExpressions;
using System.Collections;

namespace ContentGrabber.Addon.Test.StringExtTest {

	using ContentGrabber.Addon.Ext;

	[TestFixture]
	public class ToTitleCase {

		[Test]
		[TestCase(null,   ExpectedResult=null)]
		[TestCase("",     ExpectedResult="")]
		[TestCase("  ",   ExpectedResult="  ")]
		[TestCase("asdf", ExpectedResult="Asdf")]
		[TestCase("mAn in The MiDdlE", ExpectedResult="Man In The Middle")]
		public string Normal(string input) {
			return input.ToTitleCase();
		}

		[Test]
		[TestCase("asdf", ExpectedResult="Asdf")]
		public string WithExplicitCulture(string input) {
			var ci = System.Globalization.CultureInfo.InvariantCulture;
			return input.ToTitleCase(ci);
		}
	}

	[TestFixture]
	public class PrettyLanguage {
	
		[Test]
		[TestCase(null, ExpectedResult=null)]
		[TestCase("",   ExpectedResult="")]
		[TestCase("  ", ExpectedResult="  ")]
		[TestCase("<tr><td>ENGLISH<", ExpectedResult="English;")]
		[TestCase("<tr><td>FRENCH<", ExpectedResult="French;")]
		[TestCase("<tr><td>ITALIAN", ExpectedResult="Italian;")]
		public string Normal(string input) {
			return input.PrettyLanguage();
		}
	}

	[TestFixture]
	public class JoinNonEmpty{
		[Test]
		[TestCase(null, null, ExpectedResult=null)]
		[TestCase("-|-", new string [] {null}, ExpectedResult=null)]
		[TestCase("-|-", new string [] {""}, ExpectedResult=null)]
		[TestCase("-|-", new string [] {"a", "", "b", null, "c"}, ExpectedResult="a-|-b-|-c")]
		[TestCase("-|-", new string [] {"A", "", "B", null, "C"}, ExpectedResult="A-|-B-|-C")]
		public string Normal(string sep, string [] input) {
			return sep.JoinNonEmpty(input);
		}
	}

	[TestFixture]
	public class Contains {
		[Test]
		[TestCase("a two four five", "four", ExpectedResult=true)]
		public bool Normal(string str, string sub) {
			var res1 = str.ContainsCustom(sub);
			var res2 = str.ContainsLib(sub);

			Assert.That(res1, Is.EqualTo(res2));
			return res1;
		}
	}

	[TestFixture]
	public class ContainsAny {
		[Test]
		[TestCase(null, new string [] { null }, ExpectedResult=false)]
		[TestCase("a two four five", new string [] { null }, ExpectedResult=false)]
		[TestCase("a two four five", new string [] { "two" }, ExpectedResult=true)]
		[TestCase("a two four five", new string [] { "ten" }, ExpectedResult=false)]
		public bool Normal(string str, string [] subs) {
			return str.ContainsAny(subs);
		}
	}

	[TestFixture]
	public class MatchFirstNotEmpty {
		[Test]
		[TestCase("a two four five", new string [] { @"\d", @"\w{3}" }, ExpectedResult="two")]
		[TestCase("a two four five", new string [] { @"[+%<>]", @"\w+" }, ExpectedResult="a")]
		[TestCase("a two four five", new string [] { @"\d" }, ExpectedResult=null)]
		public string MatchFirstNotEmpty_Strings(string str, string [] string_patterns) {
			return str.MatchFirstNotEmpty(string_patterns);
		}

		public static IEnumerable TestCases_MatchFirstNotEmpty_Regex() {
			yield return new TestCaseData("a two four five", new Regex [] { new Regex(@"\d"), new Regex(@"\w{3}") }).Returns("two");
			yield return new TestCaseData("a two four five", new Regex [] { new Regex(@"[+%<>]"), new Regex(@"\w+") }).Returns("a");
			yield return new TestCaseData("a two four five", new Regex [] { new Regex(@"\d") }).Returns(null);
		}

		[Test]
		[TestCaseSource("TestCases_MatchFirstNotEmpty_Regex")]
		public string MatchFirstNotEmpty_Regex(string str, Regex [] regex_patterns) {
			return str.MatchFirstNotEmpty(regex_patterns);
		}
	}

	[TestFixture]
	public class MatchNotEmpty {
		[Test]
		[TestCase("a two four five", new string [] { @"\w{3}" }, ExpectedResult=new string[] {"two", "fou", "fiv"})]
		[TestCase("a two four five", new string [] { @"\d" }, ExpectedResult=new string[0])]
		public string[] MatchNotEmpty_Strings(string str, string [] string_patterns) {
			return str.MatchNotEmpty(string_patterns);
		}

		public static IEnumerable TestCases_MatchNotEmpty_Regex() {
			yield return new TestCaseData("a two four five", new Regex [] { new Regex(@"\w{3}") }).Returns(new string[] {"two", "fou", "fiv"});
			yield return new TestCaseData("a two four five", new Regex [] { new Regex(@"\d") }).Returns(new string[0]);
		}

		[Test]
		[TestCaseSource("TestCases_MatchNotEmpty_Regex")]
		public string[] MatchNotEmpty_Regex(string str, Regex [] regex_patterns) {
			return str.MatchNotEmpty(regex_patterns);
		}
	}
}

