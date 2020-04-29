#pragma once
#include "../../globals.h"
#include "../../core/os/os.h"
#include "../../core/os/mainLoop.h"

class OS_Windows
	: public OS
{

private:
	const int nCmdShow;
	const HINSTANCE hInstance;
	HWND hMainWindow;

	LARGE_INTEGER ticksPerSecond;
	LARGE_INTEGER ticksStart;

	MainLoop* mainLoop;

	bool forceQuit;

public:
	OS_Windows(const std::wstring& executable, const std::vector<std::wstring>& parameter, const int nCmdShow, const HINSTANCE hInstance);
	virtual ~OS_Windows();

	virtual void initialize();
	virtual LARGE_INTEGER getTicksUsec() const;

	virtual std::wstring getSystemDir(SystemDir directory) const;

	void run();

	const int getCmdShow() const;
	HINSTANCE getHInstance() const;

	void setMainWindow(HWND hMainWindow);
};
