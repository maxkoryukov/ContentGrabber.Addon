using System;
using NUnit.Framework;

using ContentGrabber.Addon;

namespace ContentGrabber.Addon.Test.StringExt {
	[TestFixture]
	public class PrettyLanguage {
	
		[Test]
		[TestCase(null, ExpectedResult=null)]
		[TestCase("",   ExpectedResult="")]
		[TestCase("  ", ExpectedResult="  ")]
		[TestCase("<tr><td>ENGLISH<", ExpectedResult="English;")]
		[TestCase("<tr><td>FRENCH<", ExpectedResult="French;")]
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
}

