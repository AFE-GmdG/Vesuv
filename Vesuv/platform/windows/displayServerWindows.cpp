#include "displayServerWindows.h"
#include "osWindows.h"
#include "../../resource.h"

#define MAX_LOADSTRING 100


WCHAR szTitle[MAX_LOADSTRING];                  // Titelleistentext
WCHAR szWindowClass[MAX_LOADSTRING];            // Der Klassenname des Hauptfensters.


LRESULT CALLBACK WndProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam) {
	DisplayServerWindows* displayServer = static_cast<DisplayServerWindows*>(DisplayServer::getSingleton());
	if (displayServer) {
		return displayServer->wndProc(hWnd, msg, wParam, lParam);
	} else {
		return DefWindowProcW(hWnd, msg, wParam, lParam);
	}
}


DisplayServerWindows::DisplayServerWindows(WindowMode windowMode, WindowFlags flags, Error& errorRef)
	: logger(Logger::getLoggerFor(__FILE__, true)),
	hInstance(static_cast<OS_Windows*>(OS::getSingleton())->getHInstance()),
	windowIdCounter(DisplayServer::MAIN_WINDOW_ID),
	windows() {

	UNREFERENCED_PARAMETER(windowMode);
	UNREFERENCED_PARAMETER(flags);

	errorRef = Error::OK;

	LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadStringW(hInstance, IDS_APP_CLASS, szWindowClass, MAX_LOADSTRING);
	HICON hIcon = LoadIconW(hInstance, MAKEINTRESOURCEW(IDI_VESUV));

	const WNDCLASSEXW wcexw = {
		sizeof(WNDCLASSEXW),
		CS_OWNDC | CS_DBLCLKS,
		WndProc,
		0,
		0,
		hInstance,
		hIcon,
		nullptr, // hCursor
		nullptr, // hbrBackground
		nullptr, // lpszMenuName
		szWindowClass,
		hIcon
	};

	if (!RegisterClassExW(&wcexw)) {
		logger.error(L"Failed to register the window class.");
		errorRef = Error::ERR_UNAVAILABLE;
		return;
	}

	// Todo: Create Vulkan Context

	// Create Main Window
	OS_Windows* osWindows = static_cast<OS_Windows*>(OS::getSingleton());
	DisplayServer::WindowID mainWindowId = createWindow(windowMode, flags);
	HWND hMainWindow = windows[mainWindowId].hWnd;
	ShowWindow(hMainWindow, osWindows->getCmdShow());
	SetForegroundWindow(hMainWindow);
	SetFocus(hMainWindow);

	// Todo: Create Vulkan Rendering Device

	osWindows->setMainWindow(windows[DisplayServer::MAIN_WINDOW_ID].hWnd);

	//errorRef = Error::ERR_NYI;
}


DisplayServerWindows::~DisplayServerWindows() {}


DisplayServer* DisplayServerWindows::createDisplayServer(WindowMode windowMode, WindowFlags flags, Error& errorRef) {
	return new DisplayServerWindows(windowMode, flags, errorRef);
}


void DisplayServerWindows::registerWindowsDriver() {
	registerCreateFunction(createDisplayServer);
}


DisplayServer::WindowID DisplayServerWindows::createWindow(WindowMode windowMode, WindowFlags flags) {
	DWORD dwStyle;
	DWORD dwExStyle;

	getWindowStyle(windowIdCounter == DisplayServer::MAIN_WINDOW_ID,
		windowMode == WindowMode::FULLSCREEN,
		(flags & WindowFlags::BORDERLESS) == WindowFlags::BORDERLESS,
		(flags & WindowFlags::RESIZE_DISABLED) != WindowFlags::RESIZE_DISABLED,
		windowMode == WindowMode::MAXIMIZED,
		(flags & WindowFlags::NO_FOCUS) == WindowFlags::NO_FOCUS,
		dwStyle,
		dwExStyle);


	WindowID id = windowIdCounter;
	WindowData wd;
	wd.hWnd = CreateWindowExW(
		dwExStyle,
		szWindowClass,
		szTitle,
		dwStyle,
		CW_USEDEFAULT,
		CW_USEDEFAULT,
		CW_USEDEFAULT,
		CW_USEDEFAULT,
		nullptr,
		nullptr,
		hInstance,
		nullptr);

	if (!wd.hWnd) {
		logger.error(L"");
		return DisplayServer::INVALID_WINDOW_ID;
	}

	// Todo: connect Vulkan

	windows[id] = wd;
	windowIdCounter++;

	return id;
}


void DisplayServerWindows::getWindowStyle(bool isMainWindow, bool isFullscreen, bool isBorderless, bool isResizable, bool isMaximized, bool noActivateFocus, DWORD& dwStyleRef, DWORD& dwExStyleRef) {
	dwStyleRef = WS_OVERLAPPED;
	dwExStyleRef = WS_EX_WINDOWEDGE;

	if (isMainWindow) {
		dwExStyleRef |= WS_EX_APPWINDOW;
	}

	if (isFullscreen || isBorderless) {
		dwStyleRef = WS_POPUP;
	} else {
		if (isResizable) {
			dwStyleRef |= WS_OVERLAPPEDWINDOW;
			if (isMaximized) {
				dwStyleRef |= WS_MAXIMIZE;
			}
		} else {
			dwStyleRef |= WS_CAPTION | WS_SYSMENU;
		}
	}

	if (noActivateFocus) {
		dwExStyleRef |= WS_EX_TOPMOST | WS_EX_NOACTIVATE;
	}

	dwStyleRef |= WS_CLIPCHILDREN | WS_CLIPSIBLINGS;
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
