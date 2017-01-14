using System;
using System.Text;
using System.Collections.Generic;

namespace TestApplication
{

	public static class ContainsApproaches {
		public static bool Custom(this string str, string sub) {
			return (str.Length - str.Replace(sub, String.Empty).Length) / sub.Length > 0;
		}

		public static bool Contains(this string str, string sub) {
			return str.Contains(sub);
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			DateTime end;
			DateTime start = DateTime.Now;

			Console.WriteLine("### Overall Start Time: " + start.ToLongTimeString());
			Console.WriteLine();

			//TestFastestWayToSeeIfAStringOccursInAString(5000, 1);
			TestFastestWayToSeeIfAStringOccursInAString(25000, 1);
//			TestFastestWayToSeeIfAStringOccursInAString(100000, 1);
//			TestFastestWayToSeeIfAStringOccursInAString(1000000, 1);
//			TestFastestWayToSeeIfAStringOccursInAString(5000, 100);
			//TestFastestWayToSeeIfAStringOccursInAString(25000, 50);
			TestFastestWayToSeeIfAStringOccursInAString(100000, 100);

			//TestFastestWayToSeeIfAStringOccursInAString(1000000, 100);
			//TestFastestWayToSeeIfAStringOccursInAString(5000, 1000);

//			TestFastestWayToSeeIfAStringOccursInAString(25000, 1000);
			TestFastestWayToSeeIfAStringOccursInAString(100000, 1000);
//			TestFastestWayToSeeIfAStringOccursInAString(1000000, 1000);

			end = DateTime.Now;
			Console.WriteLine();
			Console.WriteLine("### Overall End Time: " + end.ToLongTimeString());
			Console.WriteLine("### Overall Run Time: {0}", end - start);
		}

		static void TestFastestWayToSeeIfAStringOccursInAString(int NumberOfStringsToGenerate, int NumberOfSearchCharsToGenerate)
		{
			Console.WriteLine("######## " + System.Reflection.MethodBase.GetCurrentMethod().Name);
			Console.WriteLine("Number of Random Strings that will be generated: {0}", NumberOfStringsToGenerate);
			Console.WriteLine("Number of Search Strings that will be generated: {0}", NumberOfSearchCharsToGenerate);
			Console.WriteLine();

//			object lockObject = new object();
			string[] strings = GetArrayOfRandomStrings(NumberOfStringsToGenerate, 100, 150);
			Console.WriteLine("###########################################################");
			string[] substrings = GetArrayOfRandomStrings(NumberOfSearchCharsToGenerate, 4,10);

			Console.WriteLine("###########################################################");
			Console.WriteLine();


			Console.WriteLine("T3: Substring()");
			MeasureWithForeach(ContainsApproaches.Contains, strings, substrings);


			Console.WriteLine("T1: Custom Substring()");
			MeasureWithForeach(ContainsApproaches.Custom, strings, substrings);


			GC.Collect();

		}

		private static void MeasureWithForeach(System.Func<string, string, bool> method, IEnumerable<string> strings, IEnumerable<string> subs) {

			int matches = 0;
			var start = DateTime.Now;
			foreach (var str in strings) {
				foreach (var sub in subs) {
					if (method(str, sub))
						matches++;
				};
			};
	
			var end = DateTime.Now;
			Console.WriteLine("Result : {0}", end - start);
			Console.WriteLine("Matches: {0}", matches);
			Console.WriteLine();
		}

		private static string [] GetArrayOfRandomStrings(int count, int min_len, int max_len) {
			var res = new string[count];
			var d = max_len - min_len;
			if (d <= 0)
				d = 10;
			for (int x = 0; x < count; x++)
			{
				// 30 <= len <= 49
				int len = Randomizer.Next(min_len, max_len);
				res[x] = AppendRandom("", len, "abcdefghijklmnopqrstuvwzyz 1234567890");// ABCDEFGHIJKLMNOPQRSTUVWXYZ !@#$%^&*()<>,./?;:'-+=/*       ");
			}
		
			return res;
		}

		// copypaste from 
		// https://github.com/maxkoryukov/MK.Lib/blob/master/src/MK.Lib/Ext/StringExt.cs
		private static Random Randomizer = new Random();
		public static string AppendRandom(string src, int min_length, string char_domain = null)
		{
			int src_len = (string.IsNullOrEmpty(src)) ? 0 : src.Length;

			if (src_len >= min_length)
				return src;

			if ("" == char_domain && min_length > src_len)
				throw new ArgumentException("Can not append chars from empty set. Pass at least one char to char_domain", "char_domain");

			if (null == char_domain)
				char_domain = "absadfsadfsadfasdf";

			int len = char_domain.Length;

			if (null == src)
				src = "";

			while (src_len < min_length)
			{
				src += char_domain[Randomizer.Next(len)];
				src_len++;
			}

			return src;
		}
	}

}