#include "displayServerWindows.h"


LRESULT CALLBACK WndProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam) {
	DisplayServerWindows* displayServer = static_cast<DisplayServerWindows*>(DisplayServer::getSingleton());
	if (displayServer) {
		return displayServer->wndProc(hWnd, msg, wParam, lParam);
	} else {
		return DefWindowProcW(hWnd, msg, wParam, lParam);
	}
}


DisplayServerWindows::DisplayServerWindows(WindowMode windowMode, uint32_t flags, Error& errorRef) {
	UNREFERENCED_PARAMETER(windowMode);
	UNREFERENCED_PARAMETER(flags);

	consoleVisible = IsWindowVisible(GetConsoleWindow());
	errorRef = Error::OK;
}


DisplayServerWindows::~DisplayServerWindows() {
}


DisplayServer* DisplayServerWindows::createDisplayServer(WindowMode windowMode, uint32_t flags, Error& errorRef) {
	return new DisplayServerWindows(windowMode, flags, errorRef);
}


void DisplayServerWindows::registerWindowsDriver() {
	registerCreateFunction(createDisplayServer);
}


LRESULT DisplayServerWindows::wndProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam) {


	return DefWindowProcW(hWnd, msg, wParam, lParam);
}


void DisplayServerWindows::processEvents() {
	MSG msg;

	while (PeekMessageW(&msg, nullptr, 0, 0, PM_REMOVE)) {
		TranslateMessage(&msg);
		DispatchMessageW(&msg);
	}
}
