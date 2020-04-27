#pragma once
#include "../globals.h"


class DisplayServer
{

public:
	enum class WindowMode
	{
		WINDOWED,
		MINIMIZED,
		MAXIMIZED,
		FULLSCREEN,
	};

	enum class WindowFlags
	{
		NO_FLAG = 0,
		RESIZE_DISABLED = 1,
		BORDERLESS = 2,
		ALWAYS_ON_TOP = 4,
		IS_TRANSPARENT = 8,
		NO_FOCUS = 16,
	};

	typedef DisplayServer* (*CreateFunction)(WindowMode, WindowFlags, Error& errorRef);
	typedef int WindowID;

	const WindowID MAIN_WINDOW_ID = 0;
	const WindowID INVALID_WINDOW_ID = -1;

private:
	static DisplayServer* singleton;
	const BaseLogger logger;


protected:
	static CreateFunction createFunction;


public:
	DisplayServer();
	virtual ~DisplayServer();

	static DisplayServer* getSingleton();

	static void registerCreateFunction(CreateFunction createFunction);
	static DisplayServer* create(WindowMode windowMode, WindowFlags flags, Error& errorRef);

	virtual void processEvents() = 0;
};

template<> struct enable_bitmask_operators<DisplayServer::WindowFlags>
{
	static const bool enable = true;
};
