using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;

namespace TestApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			DateTime end;
			DateTime start = DateTime.Now;

			Console.WriteLine("### Overall Start Time: " + start.ToLongTimeString());
			Console.WriteLine();

			TestFastestWayToSeeIfAStringOccursInAString(5000, 1);
			TestFastestWayToSeeIfAStringOccursInAString(25000, 1);
			TestFastestWayToSeeIfAStringOccursInAString(100000, 1);
			TestFastestWayToSeeIfAStringOccursInAString(1000000, 1);
			TestFastestWayToSeeIfAStringOccursInAString(5000, 100);
			TestFastestWayToSeeIfAStringOccursInAString(25000, 100);
			TestFastestWayToSeeIfAStringOccursInAString(100000, 100);
			TestFastestWayToSeeIfAStringOccursInAString(1000000, 100);
			TestFastestWayToSeeIfAStringOccursInAString(5000, 1000);
			TestFastestWayToSeeIfAStringOccursInAString(25000, 1000);
			TestFastestWayToSeeIfAStringOccursInAString(100000, 1000);
			TestFastestWayToSeeIfAStringOccursInAString(1000000, 1000);

			end = DateTime.Now;
			Console.WriteLine();
			Console.WriteLine("### Overall End Time: " + end.ToLongTimeString());
			Console.WriteLine("### Overall Run Time: " + (end - start));

			Console.WriteLine();
			Console.WriteLine("Hit Enter to Exit");
			Console.ReadLine();

		}

		//###############################################################

		//what is the fastest way to see if a string occurs in a string?

		static void TestFastestWayToSeeIfAStringOccursInAString(int NumberOfStringsToGenerate, int NumberOfSearchCharsToGenerate)
		{
			Console.WriteLine("######## " + System.Reflection.MethodBase.GetCurrentMethod().Name);
			Console.WriteLine("Number of Random Strings that will be generated: " + NumberOfStringsToGenerate.ToString("#,##0"));
			Console.WriteLine("Number of Search Strings that will be generated: " + NumberOfSearchCharsToGenerate.ToString("#,##0"));
			Console.WriteLine();

			object lockObject = new object();
			int total = 0;
			DateTime end = DateTime.Now;
			DateTime start = DateTime.Now;
			//the strings to search
			string[] ss = new string[NumberOfStringsToGenerate];
			//the chars/strings to look for. We use both because we're testing some string methods too.
			string[] sf = new string[NumberOfSearchCharsToGenerate];
			//the count of each substring finding
			int[] c = new int[sf.Length];

			//Generate the string arrays
			int z = 10;

			//yes I realize that most real world applications people are going to search strings
			//that are "human readable" and not random like I generate below. But this is a test
			//and I'm not about to type of millions of different strings for testing. :-)
			//strings to be searched. Completely random. Using generate password method to come up with all sorts of mixtures.
			Console.WriteLine("Generating strings to search.");
			for (int x = 0; x < ss.Length; x++)
			{
				ss[x] = System.Web.Security.Membership.GeneratePassword(z, x % 5);
				z += 1;
				if (z > 25)
					z = 10;
			}

			//strings to search for
			Console.WriteLine("Generating strings to search for.");
			z = 2;
			for (int x = 0; x < sf.Length; x++)
			{
				sf[x] = System.Web.Security.Membership.GeneratePassword(z, x % 2);
				z += 1;
				if (z > 8)
					z = 2;
			}
			Console.WriteLine("###########################################################");
			Console.WriteLine();

			//return (strToSearch.Length - strToSearch.Replace(strKeyToLookFor, String.Empty).Length) / strKeyToLookFor.Length;
			Console.WriteLine("Starting method: 'custom string method' ");
			start = DateTime.Now;
			for (int x = 0; x < ss.Length; x++)
			{
				for (int y = 0; y < sf.Length; y++)
				{
					c[y] += ((ss[x].Length - ss[x].Replace(sf[y], String.Empty).Length) / sf[y].Length > 0 ? 1 : 0);
				}
			}
			end = DateTime.Now;
			Console.WriteLine("Finished at: " + end.ToLongTimeString());
			Console.WriteLine("Time: " + (end - start));
			total = 0;
			for (int x = 0; x < c.Length; x++)
			{
				total += c[x];
			}
			Console.WriteLine("Total finds: " + total + Environment.NewLine);
			Console.WriteLine();
			Console.WriteLine("###########################################################");
			Console.WriteLine();

			Array.Clear(c, 0, c.Length);
			Console.WriteLine("Starting method: 'count split string on string' ");
			start = DateTime.Now;
			for (int x = 0; x < ss.Length; x++)
			{
				for (int y = 0; y < sf.Length; y++)
				{
					c[y] += (ss[x].Split(new string[] { sf[y] }, StringSplitOptions.None).Count() - 1 > 0 ? 1 : 0);
				}
			}
			end = DateTime.Now;
			Console.WriteLine("Finished at: " + end.ToLongTimeString());
			Console.WriteLine("Time: " + (end - start));
			total = 0;
			for (int x = 0; x < c.Length; x++)
			{
				total += c[x];
			}
			Console.WriteLine("Total finds: " + total + Environment.NewLine);
			Console.WriteLine();
			Console.WriteLine("###########################################################");
			Console.WriteLine();

			Array.Clear(c, 0, c.Length);
			Console.WriteLine("Starting method: 'String.Contains' ");
			start = DateTime.Now;
			for (int x = 0; x < ss.Length; x++)
			{
				for (int y = 0; y < sf.Length; y++)
				{
					c[y] += (ss[x].Contains(sf[y]) == true ? 1 : 0);
				}
			}
			end = DateTime.Now;
			Console.WriteLine("Finished at: " + end.ToLongTimeString());
			Console.WriteLine("Time: " + (end - start));
			total = 0;
			for (int x = 0; x < c.Length; x++)
			{
				total += c[x];
			}
			Console.WriteLine("Total finds: " + total + Environment.NewLine);
			Console.WriteLine();
			Console.WriteLine("###########################################################");
			Console.WriteLine();

			Array.Clear(c, 0, c.Length);
			Console.WriteLine("Starting method: 'String.indexOf' ");
			start = DateTime.Now;
			for (int x = 0; x < ss.Length; x++)
			{
				for (int y = 0; y < sf.Length; y++)
				{
					c[y] += (ss[x].IndexOf(sf[y]) >= 0 ? 1 : 0);
				}
			}
			end = DateTime.Now;
			Console.WriteLine("Finished at: " + end.ToLongTimeString());
			Console.WriteLine("Time: " + (end - start));
			total = 0;
			for (int x = 0; x < c.Length; x++)
			{
				total += c[x];
			}
			Console.WriteLine("Total finds: " + total + Environment.NewLine);
			Console.WriteLine();
			Console.WriteLine("###########################################################");
			Console.WriteLine();

			Array.Clear(c, 0, c.Length);
			Console.WriteLine("Starting method: 'linq contains usage' ");
			start = DateTime.Now;
			for (int y = 0; y < sf.Length; y++)
			{
				c[y] += ss.Where(o => o.Contains(sf[y])).Count();
			}
			end = DateTime.Now;
			Console.WriteLine("Finished at: " + end.ToLongTimeString());
			Console.WriteLine("Time: " + (end - start));
			total = 0;
			for (int x = 0; x < c.Length; x++)
			{
				total += c[x];
			}
			Console.WriteLine("Total finds: " + total + Environment.NewLine);
			Console.WriteLine();
			Console.WriteLine("###########################################################");
			Console.WriteLine();

			Array.Clear(c, 0, c.Length);
			Console.WriteLine("Starting method: 'linq with IndexOf usage' ");
			start = DateTime.Now;
			for (int y = 0; y < sf.Length; y++)
			{
				c[y] += ss.Where(o => o.IndexOf(sf[y]) > -1).Count();
			}
			end = DateTime.Now;
			Console.WriteLine("Finished at: " + end.ToLongTimeString());
			Console.WriteLine("Time: " + (end - start));
			total = 0;
			for (int x = 0; x < c.Length; x++)
			{
				total += c[x];
			}
			Console.WriteLine("Total finds: " + total + Environment.NewLine);
			Console.WriteLine();
			Console.WriteLine("###########################################################");
			Console.WriteLine();

			Array.Clear(c, 0, c.Length);
			Console.WriteLine("Starting method: 'Regex IsMatch uncompiled' ");
			start = DateTime.Now;
			for (int x = 0; x < ss.Length; x++)
			{
				for (int y = 0; y < sf.Length; y++)
				{
					c[y] += Regex.IsMatch(ss[x], Regex.Escape(sf[y])) == true ? 1 : 0;
				}
			}
			end = DateTime.Now;
			Console.WriteLine("Finished at: " + end.ToLongTimeString());
			Console.WriteLine("Time: " + (end - start));
			total = 0;
			for (int x = 0; x < c.Length; x++)
			{
				total += c[x];
			}
			Console.WriteLine("Total finds: " + total + Environment.NewLine);
			Console.WriteLine();
			Console.WriteLine("###########################################################");
			Console.WriteLine();

			total = 0;
			Console.WriteLine("Starting method: 'Single AsParallel() Regex' ");
			start = DateTime.Now;
			for (int x = 0; x < ss.Length; x++)
			{
				total += sf.AsParallel().Sum(s => Regex.IsMatch(ss[x], Regex.Escape(s)) ? 1 : 0);
			}
			end = DateTime.Now;
			Console.WriteLine("Finished at: " + end.ToLongTimeString());
			Console.WriteLine("Time: " + (end - start));
			Console.WriteLine("Total finds: " + total + Environment.NewLine);
			Console.WriteLine();
			Console.WriteLine("###########################################################");
			Console.WriteLine();

			total = 0;
			Console.WriteLine("Starting method: 'Parallel For Custom Counting' ");
			start = DateTime.Now;
			Parallel.For(0, ss.Length,
				() => 0,
				(x, loopState, subtotal) =>
				{
					for (int y = 0; y < sf.Length; y++)
					{
						subtotal += ((ss[x].Length - ss[x].Replace(sf[y], String.Empty).Length) / sf[y].Length > 0 ? 1 : 0);
					}
					return subtotal;
				},
				(s) =>
				{
					lock (lockObject)
					{
						total += s;
					}
				}
			);
			end = DateTime.Now;
			Console.WriteLine("Finished at: " + end.ToLongTimeString());
			Console.WriteLine("Time: " + (end - start));
			Console.WriteLine("Total finds: " + total + Environment.NewLine);
			Console.WriteLine();
			Console.WriteLine("###########################################################");
			Console.WriteLine();

			total = 0;
			Console.WriteLine("Starting method: 'Parallel For Split String' ");
			start = DateTime.Now;
			Parallel.For(0, ss.Length,
				() => 0,
				(x, loopState, subtotal) =>
				{
					for (int y = 0; y < sf.Length; y++)
					{
						subtotal += (ss[x].Split(new string[] { sf[y] }, StringSplitOptions.None).Count() - 1 > 0 ? 1 : 0);
					}
					return subtotal;
				},
				(s) =>
				{
					lock (lockObject)
					{
						total += s;
					}
				}
			);
			end = DateTime.Now;
			Console.WriteLine("Finished at: " + end.ToLongTimeString());
			Console.WriteLine("Time: " + (end - start));
			Console.WriteLine("Total finds: " + total + Environment.NewLine);
			Console.WriteLine();
			Console.WriteLine("###########################################################");
			Console.WriteLine();

			total = 0;
			Console.WriteLine("Starting method: 'Parallel For String.Contains()' ");
			start = DateTime.Now;
			Parallel.For(0, ss.Length,
				() => 0,
				(x, loopState, subtotal) =>
				{
					for (int y = 0; y < sf.Length; y++)
					{
						subtotal += (ss[x].Contains(sf[y]) == true ? 1 : 0);
					}
					return subtotal;
				},
				(s) =>
				{
					lock (lockObject)
					{
						total += s;
					}
				}
			);
			end = DateTime.Now;
			Console.WriteLine("Finished at: " + end.ToLongTimeString());
			Console.WriteLine("Time: " + (end - start));
			Console.WriteLine("Total finds: " + total + Environment.NewLine);
			Console.WriteLine();
			Console.WriteLine("###########################################################");
			Console.WriteLine();

			total = 0;
			Console.WriteLine("Starting method: 'Parallel For String.IndexOf()' ");
			start = DateTime.Now;
			Parallel.For(0, ss.Length,
				() => 0,
				(x, loopState, subtotal) =>
				{
					for (int y = 0; y < sf.Length; y++)
					{
						subtotal += (ss[x].IndexOf(sf[y]) >= 0 ? 1 : 0);
					}
					return subtotal;
				},
				(s) =>
				{
					lock (lockObject)
					{
						total += s;
					}
				}
			);
			end = DateTime.Now;
			Console.WriteLine("Finished at: " + end.ToLongTimeString());
			Console.WriteLine("Time: " + (end - start));
			Console.WriteLine("Total finds: " + total + Environment.NewLine);
			Console.WriteLine();
			Console.WriteLine("###########################################################");
			Console.WriteLine();

			total = 0;
			Console.WriteLine("Starting method: 'Parallel For Linq.Contains()' ");
			start = DateTime.Now;
			Parallel.For(0, sf.Length,
				() => 0,
				(x, loopState, subtotal) =>
				{
					subtotal += ss.Where(o => o.Contains(sf[x])).Count();
					return subtotal;
				},
				(s) =>
				{
					lock (lockObject)
					{
						total += s;
					}
				}
			);
			end = DateTime.Now;
			Console.WriteLine("Finished at: " + end.ToLongTimeString());
			Console.WriteLine("Time: " + (end - start));
			Console.WriteLine("Total finds: " + total + Environment.NewLine);
			Console.WriteLine();
			Console.WriteLine("###########################################################");
			Console.WriteLine();

			total = 0;
			Console.WriteLine("Starting method: 'Parallel For Linq.IndexOf()' ");
			start = DateTime.Now;
			Parallel.For(0, sf.Length,
				() => 0,
				(x, loopState, subtotal) =>
				{
					subtotal += ss.Where(o => o.IndexOf(sf[x]) > -1).Count();
					return subtotal;
				},
				(s) =>
				{
					lock (lockObject)
					{
						total += s;
					}
				}
			);
			end = DateTime.Now;
			Console.WriteLine("Finished at: " + end.ToLongTimeString());
			Console.WriteLine("Time: " + (end - start));
			Console.WriteLine("Total finds: " + total + Environment.NewLine);
			Console.WriteLine();
			Console.WriteLine("###########################################################");
			Console.WriteLine();

			total = 0;
			Console.WriteLine("Starting method: 'Parallel For Regex.IsMatch()' ");
			start = DateTime.Now;
			Parallel.For(0, ss.Length,
				() => 0,
				(x, loopState, subtotal) =>
				{
					for (int y = 0; y < sf.Length; y++)
					{
						subtotal += Regex.IsMatch(ss[x], Regex.Escape(sf[y])) == true ? 1 : 0;
					}
					return subtotal;
				},
				(s) =>
				{
					lock (lockObject)
					{
						total += s;
					}
				}
			);
			end = DateTime.Now;
			Console.WriteLine("Finished at: " + end.ToLongTimeString());
			Console.WriteLine("Time: " + (end - start));
			Console.WriteLine("Total finds: " + total + Environment.NewLine);
			Console.WriteLine();
			Console.WriteLine("###########################################################");
			Console.WriteLine();

			total = 0;
			Console.WriteLine("Starting method: 'Parallel For AsParallel() Regex' ");
			start = DateTime.Now;
			Parallel.For(0, ss.Length,
				() => 0,
				(x, loopState, subtotal) =>
				{
					subtotal += sf.AsParallel().Sum(s => Regex.IsMatch(ss[x], Regex.Escape(s)) ? 1 : 0);
					return subtotal;
				},
				(s) =>
				{
					lock (lockObject)
					{
						total += s;
					}
				}
			);
			end = DateTime.Now;
			Console.WriteLine("Finished at: " + end.ToLongTimeString());
			Console.WriteLine("Time: " + (end - start));
			Console.WriteLine("Total finds: " + total + Environment.NewLine);
			Console.WriteLine();
			Console.WriteLine("###########################################################");
			Console.WriteLine();

			Array.Clear(ss, 0, ss.Length);
			ss = null;
			Array.Clear(sf, 0, sf.Length);
			sf = null;
			Array.Clear(c, 0, c.Length);
			c = null;
			GC.Collect();

		}
	}

}