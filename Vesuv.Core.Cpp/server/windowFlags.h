#pragma once

namespace Vesuv::Server {

	[System::Flags]
	public enum class WindowFlags
	{
		NoFlags = 0,
		ResizeDisabled = 1,
		Borderless = 2,
		AlwaysOnTop = 4,
		Transparent = 8,
		NoFocus = 16
	};

}
