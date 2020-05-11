#include "osWindows.h"
#include "../../globals.h"


namespace Vesuv::Platform::Windows {

	using namespace System;
	using namespace System::IO;
	using namespace Vesuv::Core;
	using namespace Vesuv::Core::OS;


	UInt64 InitTicksPerSecond() {
		LARGE_INTEGER ticksPerSecond;
		QueryPerformanceFrequency(&ticksPerSecond);
		return static_cast<UInt64>(ticksPerSecond.QuadPart);
	}


	UInt64 InitTicksStart(UInt64 ticksPerSecond) {
		LARGE_INTEGER ticks;
		QueryPerformanceCounter(&ticks);
		return ticks.QuadPart * 1000000L / ticksPerSecond;
	}


	OS_Windows::OS_Windows(HINSTANCE hInstance, String^& executable, array<String^>^& parameter) :
		OS(executable, parameter),
		hInstance(hInstance),
		logger(Logger::GetFor()),
		ticksPerSecond(InitTicksPerSecond()),
		ticksStart(InitTicksStart(ticksPerSecond)),
		mainLoop(nullptr) {

		logger->Log("Initialize OS_Windows (ctor)");
	}


	OS_Windows::~OS_Windows() {
		if (logger) {
			logger->Log("Destroy OS_Windows");
			delete logger;
		}
	}


	void OS_Windows::Initialize() {
		logger->Log("Initialize OS_Windows (Initialize)");
		// Init CrashHandler
		// Init FileAccess / DirAccess???
		// Init NetSocket

		// Init a process map <= Make usage of the .net Threading Modell

		mainLoop = nullptr;
	}


	UInt64 OS_Windows::GetTicksUsec() {
		LARGE_INTEGER ticks;
		QueryPerformanceCounter(&ticks);
		return static_cast<UInt64>(ticks.QuadPart * 1000000L / ticksPerSecond) - ticksStart;
	}


	Error OS_Windows::SetCwd(String^ cwd) {
		try {
			Directory::SetCurrentDirectory(cwd);
			return Error::Ok;
		} catch(Exception^) {
			return Error::CantOpen;
		}
	}


	void OS_Windows::Run() {}

}



//#include "../../main/main.h"
//#include "displayServerWindows.h"
//
//
//OS_Windows::OS_Windows(const std::wstring& executable, const std::vector<std::wstring>& parameter, const int nCmdShow, const HINSTANCE hInstance)
//	: OS(executable, parameter),
//	nCmdShow(nCmdShow),
//	hInstance(hInstance),
//	hMainWindow(nullptr),
//	ticksPerSecond(),
//	ticksStart(),
//	//mainLoop(nullptr),
//	forceQuit(false) {
//
//	DisplayServerWindows::registerWindowsDriver();
//}
//
//
//OS_Windows::~OS_Windows() {}
//
//
//void OS_Windows::initialize() {
//	QueryPerformanceFrequency(&ticksPerSecond);
//	LARGE_INTEGER ticks;
//	QueryPerformanceCounter(&ticks);
//	ticksStart.QuadPart = ticks.QuadPart * 1000000L / ticksPerSecond.QuadPart;
//}
//
//
//LARGE_INTEGER OS_Windows::getTicksUsec() const {
//	LARGE_INTEGER ticks;
//	LARGE_INTEGER time;
//	QueryPerformanceCounter(&ticks);
//	time.QuadPart = ticks.QuadPart * 1000000L / ticksPerSecond.QuadPart;
//	time.QuadPart -= ticksStart.QuadPart;
//	return time;
//}
//
//
//std::wstring OS_Windows::getSystemDir(SystemDir directory) const {
//	KNOWNFOLDERID id = FOLDERID_Public;
//
//	switch (directory) {
//	case OS::SystemDir::PUBLIC:
//		break;
//	case OS::SystemDir::DESKTOP:
//		id = FOLDERID_Desktop;
//		break;
//	case OS::SystemDir::DOCUMENTS:
//		id = FOLDERID_Documents;
//		break;
//	case OS::SystemDir::DOWNLOADS:
//		id = FOLDERID_Downloads;
//		break;
//	}
//
//	PWSTR buffer = nullptr;
//	assert(S_OK == SHGetKnownFolderPath(id, KF_FLAG_DEFAULT, NULL, &buffer));
//	const std::wstring path(buffer);
//	CoTaskMemFree(buffer);
//
//	return path;
//}
//
//
//void OS_Windows::run() {
//	//if (!mainLoop) {
//	//	return;
//	//}
//
//	//mainLoop->init();
//
//	DisplayServer* displayServer = DisplayServer::getSingleton();
//
//	while (!forceQuit) {
//		displayServer->processEvents();
//		if (Vesuv::Main::Main::interaction()) {
//			break;
//		}
//	};
//
//	//mainLoop->finish();
//}
//
//
//const int OS_Windows::getCmdShow() const {
//	return nCmdShow;
//}
//
//
//HINSTANCE OS_Windows::getHInstance() const {
//	return hInstance;
//}
//
//
//void OS_Windows::setMainWindow(HWND hWnd) {
//	hMainWindow = hWnd;
//}
