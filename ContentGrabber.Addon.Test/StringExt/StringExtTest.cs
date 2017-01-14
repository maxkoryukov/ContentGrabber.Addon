using System;
using NUnit.Framework;

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
}

