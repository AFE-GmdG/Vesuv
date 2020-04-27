#include "osWindows.h"

#include "../../main/main.h"
#include "displayServerWindows.h"


OS_Windows::OS_Windows(const std::wstring& executable, const std::vector<std::wstring>& parameter, const int nCmdShow, const HINSTANCE hInstance)
	: OS(executable, parameter),
	nCmdShow(nCmdShow),
	hInstance(hInstance),
	hMainWindow(nullptr),
	ticksPerSecond(),
	ticksStart(),
	mainLoop(nullptr),
	forceQuit(false) {

	DisplayServerWindows::registerWindowsDriver();
}


OS_Windows::~OS_Windows() {}


void OS_Windows::initialize() {
	QueryPerformanceFrequency(&ticksPerSecond);
	LARGE_INTEGER ticks;
	QueryPerformanceCounter(&ticks);
	ticksStart.QuadPart = ticks.QuadPart * 1000000L / ticksPerSecond.QuadPart;
}


std::wstring OS_Windows::getSystemDir(SystemDir directory) const {
	KNOWNFOLDERID id = FOLDERID_Public;

	switch (directory) {
	case OS::SystemDir::PUBLIC:
		break;
	case OS::SystemDir::DESKTOP:
		id = FOLDERID_Desktop;
		break;
	case OS::SystemDir::DOCUMENTS:
		id = FOLDERID_Documents;
		break;
	case OS::SystemDir::DOWNLOADS:
		id = FOLDERID_Downloads;
		break;
	}

	PWSTR buffer;
	assert(S_OK == SHGetKnownFolderPath(id, KF_FLAG_DEFAULT, NULL, &buffer));
	const std::wstring path(buffer);
	CoTaskMemFree(buffer);

	return path;
}


void OS_Windows::run() {
	if (!mainLoop) {
		return;
	}

	mainLoop->init();

	DisplayServer* displayServer = DisplayServer::getSingleton();

	while (!forceQuit) {
		displayServer->processEvents();
		if (Main::interaction()) {
			break;
		}
	};

	mainLoop->finish();
}


const int OS_Windows::getCmdShow() const {
	return nCmdShow;
}


HINSTANCE OS_Windows::getHInstance() const {
	return hInstance;
}
