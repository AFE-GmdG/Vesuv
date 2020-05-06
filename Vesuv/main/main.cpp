#include "main.h"

namespace Vesuv::Main {

	using namespace System;
	using namespace Vesuv::Core;
	using namespace Vesuv::Core::OS;
	using namespace Vesuv::Server;


	Error Main::SetupPlatformSpecific() {
		return Error::NYI;
	}


	Error Main::Setup() {
		projectSettings = gcnew ProjectSettings();

		return Error::Ok;
	}


	bool Main::Start() {
		return false;
	}


	void Main::Cleanup() {
		if (projectSettings) {
			delete projectSettings;
		}
	}
}
