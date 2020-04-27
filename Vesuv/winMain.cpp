#include "winMain.h"
#include "shellapi.h"


int APIENTRY wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR lpCmdLine, _In_ int nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	// Preprocess commandline
	LPCWSTR cmdLine = GetCommandLineW();
	DWORD expandedCmdLineSize = ExpandEnvironmentStringsW(cmdLine, nullptr, 0);
	LPWSTR expandedCmdLine = new WCHAR[expandedCmdLineSize];
	assert(expandedCmdLineSize == ExpandEnvironmentStringsW(cmdLine, expandedCmdLine, expandedCmdLineSize));
	int argc;
	LPWSTR* args;
	args = CommandLineToArgvW(expandedCmdLine, &argc);
	delete[] expandedCmdLine;

	OS_Windows os(args[0], std::vector<std::wstring>(args + 1, args + argc), nCmdShow, hInstance);

	LocalFree(args);

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
