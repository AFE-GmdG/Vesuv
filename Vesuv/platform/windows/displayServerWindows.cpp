#include "displayServerWindows.h"


DisplayServerWindows::DisplayServerWindows() {
}


DisplayServerWindows::~DisplayServerWindows() {
}


DisplayServer* DisplayServerWindows::createDisplayServer(WindowMode windowMode, uint32_t flags, Error& errorRef) {
	UNREFERENCED_PARAMETER(windowMode);
	UNREFERENCED_PARAMETER(flags);
	UNREFERENCED_PARAMETER(errorRef);

	return nullptr;
}


void DisplayServerWindows::registerWindowsDriver() {
	registerCreateFunction(createDisplayServer);
}


void DisplayServerWindows::processEvents() {
	MSG msg;

	while (PeekMessageW(&msg, nullptr, 0, 0, PM_REMOVE)) {
		TranslateMessage(&msg);
		DispatchMessageW(&msg);
	}
}
