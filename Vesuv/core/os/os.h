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

	const std::wstring executable;
	const std::vector<std::wstring> parameter;

public:
	OS(const std::wstring& executable, const std::vector<std::wstring>& parameter);
	virtual ~OS();

	static OS* getSingleton();

	virtual void initialize() = 0;
	virtual std::wstring getSystemDir(SystemDir directory) const;

};
