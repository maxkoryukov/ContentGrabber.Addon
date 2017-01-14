using System;
using NUnit.Framework;

namespace ContentGrabber.Addon.Test {

	using ContentGrabber.Addon;

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

			var pretty = ContentGrabber.Addon.StringExt.PrettyLanguage("<tr><td>ITALIAN<");
			Assert.That(pretty, Is.EqualTo("Italian;"));
		}

		[Test]
		public void PrettyLanguage02() {
			// add to the top of file:
			// using ContentGrabber.Addon;
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

			var joined = ContentGrabber.Addon.StringExt.JoinNonEmpty(
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
			// using ContentGrabber.Addon;

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
			// using ContentGrabber.Addon;

			var result = "one two three".ContainsAny("one", "two", "three");
			Assert.That(result, Is.True);
		}
	}
}

