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

	typedef DisplayServer* (*CreateFunction)(WindowMode, uint32_t, Error& errorRef);


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
	static DisplayServer* create(WindowMode windowMode, uint32_t flags, Error& errorRef);

	virtual bool isConsoleVisible() const;
	virtual void setConsoleVisible(const bool visible);

	virtual void processEvents() = 0;
};
