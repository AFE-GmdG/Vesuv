#pragma once

namespace Vesuv::Core {

	public enum class Error
	{
		Ok = 0,
		Failed,

		Unavailable = 100,
		Unconfigured,
		Unauthorized,

		// Not yet implemented.
		NYI = 99999,
	};

}
