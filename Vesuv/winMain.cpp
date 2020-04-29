#include "winMain.h"
#include "shellapi.h"


std::tuple<std::wstring, std::vector<std::wstring>> preprocessCommandLine() {
	int argc;
	LPCWSTR cmdLine = GetCommandLineW();
	DWORD expandedCmdLineSize = ExpandEnvironmentStringsW(cmdLine, nullptr, 0);
	std::unique_ptr<WCHAR[]> expandedCmdLine = std::make_unique<WCHAR[]>(expandedCmdLineSize);
	assert(expandedCmdLineSize == ExpandEnvironmentStringsW(cmdLine, expandedCmdLine.get(), expandedCmdLineSize));
	std::unique_ptr<LPWSTR, decltype(&LocalFree)> argsPtr(CommandLineToArgvW(expandedCmdLine.get(), &argc), &LocalFree);
	LPWSTR* args = argsPtr.get();
	return {args[0], std::vector<std::wstring>(args + 1, args + argc)};
}


int APIENTRY wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR lpCmdLine, _In_ int nCmdShow) {
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	auto [executable, parameter] = preprocessCommandLine();
	OS_Windows os(executable, parameter, nCmdShow, hInstance);

	Error err = Main::setup();
	if (err != Error::OK) {
		return static_cast<int>(err);
	}

	if (Main::start()) {
		os.run();
	}

	Main::cleanup();

	return 0;
}
