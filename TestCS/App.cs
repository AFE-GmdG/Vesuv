using System;
using System.Diagnostics;

using Vesuv.Core;

#pragma warning disable IDE0059 // Unnötige Zuweisung eines Werts.
namespace TestCS
{

	class App
	{

		static int Main() {
			ProjectSettings settings = new ProjectSettings();
			var ll1 = settings.GetSetting<Int32>("Application/Config", "LogLevel");
			var width1 = settings.GetSetting<Int32>("Application/Config", "Width");
			var width2 = settings.GetSetting<Int64>("Application/Config", "Width");
			var width3 = settings.GetSetting<Int16>("Application/Config", "Width");
			var width4 = settings.GetSetting<SByte>("Application/Config", "Width");
			return 0;
		}

	}

}
#pragma warning restore IDE0059 // Unnötige Zuweisung eines Werts.
