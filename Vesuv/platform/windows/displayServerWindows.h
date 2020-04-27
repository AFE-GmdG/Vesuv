#pragma once
#include "../../globals.h"
#include "../../servers/displayServer.h"


class DisplayServerWindows
	: public DisplayServer
{

private:
	struct WindowData
	{
		HWND hWnd = nullptr;
		bool minimized = false;
		bool maximized = false;
		bool fullscreen = false;
		bool borderless = false;
		bool resizable = true;
	};


private:
	const BaseLogger logger;
	HINSTANCE hInstance;

	WindowID windowIdCounter;
	std::map<WindowID, WindowData> windows;

public:
	DisplayServerWindows(WindowMode windowMode, WindowFlags flags, Error& errorRef);
	virtual ~DisplayServerWindows();

	static DisplayServer* createDisplayServer(WindowMode windowMode, WindowFlags flags, Error& errorRef);
	static void registerWindowsDriver();


private:
	WindowID createWindow(WindowMode windowMode, WindowFlags flags);
	void getWindowStyle(bool isMainWindow, bool isFullscreen, bool isBorderless, bool isResizable, bool isMaximized, bool noActivateFocus, DWORD& dwStyleRef, DWORD& dwExStyleRef);

public:
	LRESULT wndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
	virtual void processEvents();
};
