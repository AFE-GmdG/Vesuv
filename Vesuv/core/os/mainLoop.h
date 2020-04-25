#pragma once
#include "../../globals.h"


class MainLoop
{

public:
	MainLoop();
	virtual ~MainLoop();

	void init();
	bool iteration(float time);
	bool idle(float time);
	void finish();

};
