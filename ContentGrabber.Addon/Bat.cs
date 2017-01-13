using System;
using System.Diagnostics;

namespace ContentGrabber.Addon
{
	public static class Bat
	{
		public static void Exec (string command)
		{
			var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
			processInfo.CreateNoWindow = true;
			processInfo.UseShellExecute = false;
			processInfo.RedirectStandardError = true;
			processInfo.RedirectStandardOutput = true;

			var process = Process.Start(processInfo);

			var stdout = "";
			var stderr = "";
			int rc = -1;

			process.OutputDataReceived += (object sender, DataReceivedEventArgs e) => {
				stdout += e.Data + System.Environment.NewLine;
			};

			process.BeginOutputReadLine();

			process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) => {
				stderr += e.Data + System.Environment.NewLine;
			};
			process.BeginErrorReadLine();

			process.WaitForExit();

			rc = process.ExitCode;
			process.Close();
		}
	}
}
