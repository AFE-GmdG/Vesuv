#pragma once
#include "../../globals.h"
#include "../../servers/displayServer.h"


class DisplayServerWindows
	: public DisplayServer
{

public:
	DisplayServerWindows();
	virtual ~DisplayServerWindows();

	static DisplayServer* createDisplayServer(WindowMode windowMode, uint32_t flags, Error& errorRef);
	static void registerWindowsDriver();

	virtual void processEvents();
};
