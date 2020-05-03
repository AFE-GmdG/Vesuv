using System;
using System.Diagnostics;

namespace TestCS
{

	class App
	{

		static int Main() {
			Debug.AutoFlush = true;
			Debug.Indent();
			Debug.WriteLine("Entering Main");
			Console.WriteLine("Hello, World!");
			Debug.WriteLine("Exiting Main");
			Debug.Unindent();
			return 0;
		}

	}

}
