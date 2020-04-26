#pragma once
#include "../../globals.h"
#include "../../servers/displayServer.h"


class DisplayServerWindows
	: public DisplayServer
{

private:
	struct WindowData
	{
		HWND hWnd;
		bool minimized = false;
		bool maximized = false;
		bool fullscreen = false;
		bool borderless = false;
		bool resizable = true;
	};


private:
	bool consoleVisible = false;


public:
	DisplayServerWindows(WindowMode windowMode, uint32_t flags, Error& errorRef);
	virtual ~DisplayServerWindows();

	static DisplayServer* createDisplayServer(WindowMode windowMode, uint32_t flags, Error& errorRef);
	static void registerWindowsDriver();

	LRESULT wndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

	virtual void processEvents();
};
