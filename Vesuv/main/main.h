#pragma once
#include "../globals.h"
#include "../core/os/os.h"


namespace Main {

	Error setup();
	Error setup2();
	bool start();

	bool interaction();

	void cleanup();

};
