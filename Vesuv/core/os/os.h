#pragma once
#include "../../globals.h"
#include "mainLoop.h"


class OS
{

public:
	enum class SystemDir
	{
		PUBLIC,
		DESKTOP,
		DOCUMENTS,
		DOWNLOADS,
	};


private:
	static OS* singleton;


public:
	OS();
	virtual ~OS();

	static OS* getSingleton();

	virtual void initialize() = 0;
	virtual std::wstring getSystemDir(SystemDir directory) const;

};
