# ContentGrabber.Addon

[![Build status](https://ci.appveyor.com/api/projects/status/gdwfn2l56vvrq86g?svg=true)](https://ci.appveyor.com/project/maxkoryukov/contentgrabber-addon)
[![codecov](https://codecov.io/gh/maxkoryukov/ContentGrabber.Addon/branch/master/graph/badge.svg)](https://codecov.io/gh/maxkoryukov/ContentGrabber.Addon)

## Functions

All **really actual and working** samples are in [tests](ContentGrabber.Addon.Test/Samples.cs)

### Run BAT

```csharp
		public void Bat01() {

			var path = System.IO.Path.Combine("ContentGrabber.Addon.Test", "Bat", "echo.bat");
			ContentGrabber.Addon.Bat.Exec(path);
			Assert.True(true);
		}
```

### Pretty Language

Use as static method:

```csharp
		public void PrettyLanguage01() {

			// add to the top of file:
			// using ContentGrabber.Addon.Ext;

			var pretty = StringExt.PrettyLanguage("<tr><td>ITALIAN<");
			Assert.That(pretty, Is.EqualTo("Italian;"));
		}
```

Or as string-extension:

```csharp
		public void PrettyLanguage02() {
			// add to the top of file:
			// using ContentGrabber.Addon.Ext;
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
```

Or as string-extension:

```csharp
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
```

### String contains substring

Test shows, that default `string.Contains` works fast enough.

Here is a test run `CustomContains` vs `LibContains`: https://ci.appveyor.com/project/maxkoryukov/contentgrabber-addon/build/0.2.1.18-0.2.1#L78

And here: http://disq.us/p/1f8wxjc

Meanwhile, there are two extension methods for string:

1. `ContainsCustom` , implementing T1 from [strange article](http://cc.davelozinski.com/c-sharp/fastest-way-to-check-if-a-string-occurs-within-a-string)
2. `ContainsLib`, just a wrapper around `string.Contains`

which you could use for your purpopses.

> But it is strongly recommended to use default `string.Contains` method.

### String contains one of three(any number) strings

Use as string-extension:

```csharp
		public void ContainsAny02() {
			// add to the top of file:
			// using ContentGrabber.Addon.Ext;.Ext;

			var result = "one two three".ContainsAny("one", "two", "three");
			Assert.That(result, Is.True);
		}
```
