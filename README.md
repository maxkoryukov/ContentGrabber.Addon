# ContentGrabber.Addon

[![Build status](https://ci.appveyor.com/api/projects/status/gdwfn2l56vvrq86g?svg=true)](https://ci.appveyor.com/project/maxkoryukov/contentgrabber-addon)
[![codecov](https://codecov.io/gh/maxkoryukov/ContentGrabber.Addon/branch/master/graph/badge.svg)](https://codecov.io/gh/maxkoryukov/ContentGrabber.Addon)

## Functions

### Run BAT

```csharp
		public void Bat01() {

			ContentGrabber.Addon.Bat.Exec(System.IO.Path.Combine("ContentGrabber.Addon.Test", "Bat", "echo.bat"));
			Assert.True(true);
		}
```

### Pretty Language

Use as static method:

```csharp
		public void PrettyLanguage01() {
			var pretty = ContentGrabber.Addon.StringExt.PrettyLanguage("<tr><td>ITALIAN<");
			Assert.That(pretty, Is.EqualTo("Italian;"));
		}
```

Or as string-extension:

```csharp
		public void PrettyLanguage02() {
			// add to the top of file:
			// using ContentGrabber.Addon;
			var pretty ="<tr><td>ITALIAN<".PrettyLanguage();
			Assert.That(pretty, Is.EqualTo("Italian;"));
		}
```

### Join multiple strings

Use as static method:

```csharp
		public void JoinNonEmpty01() {
			var s001 = "a";
			var s002 = "";
			var s003 = "xxxxx";
			string s004 = null;
			var s005 = "123";

			var joined = ContentGrabber.Addon.StringExt.JoinNonEmpty(" | ", s001, s002, s003, s004, s005 /* s006 .... as many as you wish */);
			Assert.That(joined, Is.EqualTo("a | xxxxx | 123"));
		}
```

Or as string-extension:

```csharp
		public void JoinNonEmpty02() {
			// add to the top of file:
			// using ContentGrabber.Addon;

			var s001 = "a";
			var s002 = "";
			var s003 = "xxxxx";
			string s004 = null;
			var s005 = "123";

			var joined = " | ".JoinNonEmpty(s001, s002, s003, s004, s005 /* s006 .... as many as you wish */);
			Assert.That(joined, Is.EqualTo("a | xxxxx | 123"));
		}
```
