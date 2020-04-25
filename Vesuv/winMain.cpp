#include "winMain.h"


int APIENTRY wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR lpCmdLine, _In_ int nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);
	UNREFERENCED_PARAMETER(nCmdShow);

	OS_Windows os(hInstance);

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
