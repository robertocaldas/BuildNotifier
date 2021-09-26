using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BuildNotifier
{
	[InitializeOnLoad]
	internal class BuildHandler : IPostprocessBuildWithReport
	{
		public int callbackOrder => 0;

		// This is necessary because of a bug in Unity where
		// Unity won't trigger OnPostprocessBuild when a build fails.
		static BuildHandler()
		{
			BuildPlayerWindow.RegisterBuildPlayerHandler(Build);
		}

		public void OnPostprocessBuild(BuildReport report)
		{
			var notifier = new BuildNotifier();

			var summary = report.summary;
			var result = summary.result.ToString();
			if (summary.result == BuildResult.Unknown)
			{
				// another Unity bug (https://issuetracker.unity3d.com/issues/ipostprocessbuildwithreport-always-return-unknown-even-when-the-actual-build-has-succeeded)
				result += " (probably Success)";
			}
			var message = $"{summary.platform} finished building at {report.summary.buildEndedAt.ToLocalTime()}\n" +
			              $"Result: {result}\n" +
			              $"Errors: {summary.totalErrors}, Warnings: {summary.totalWarnings}";
			notifier.SendPushNotification(message);
		}

		private static void Build(BuildPlayerOptions buildOptions)
		{
			try
			{
				BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(buildOptions);
			}
			catch (BuildPlayerWindow.BuildMethodException e)
			{
				// Build failed:
				Debug.LogException(e);
				var message = e.InnerException == null ? e.ToString() : e.InnerException.ToString();
				var notifier = new BuildNotifier();
				notifier.SendPushNotification("Build failed: " + message);
			}
		}
	}
}