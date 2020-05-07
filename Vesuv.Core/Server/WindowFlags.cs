using System;

namespace Vesuv.Server
{

	[Flags]
	public enum WindowFlags
	{
		NoFlags = 0,
		ResizeDisabled = 1,
		Borderless = 2,
		AlwaysOnTop = 4,
		Transparent = 8,
		NoFocus = 16
	}

}
