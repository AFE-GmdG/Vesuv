#include "main.h"

namespace Vesuv::Main {

	using namespace System;
	using namespace Vesuv::Core;
	using namespace Vesuv::Core::OS;
	using namespace Vesuv::Server;


	Error Main::setupPlatformSpecific() {
		return Error::NYI;
	}


	Error Main::setup() {
		return Error::Ok;
	}


	bool Main::start() {
		return false;
	}


}
