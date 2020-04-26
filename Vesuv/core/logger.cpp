#include "logger.h"
#include "os/os.h"

#include <iostream>


BaseLogger::BaseLogger(std::wstring context)
	: context(context) {}


void BaseLogger::debug() const {}
void BaseLogger::debug(std::wstring message) const {}


void BaseLogger::warn() const {
	std::wclog << std::endl;
}
void BaseLogger::warn(std::wstring message) const {
	if (context.empty()) {
		std::wclog << L"[WARN]: " << message << std::endl;
	} else {
		std::wclog << L"[WARN] " << context << L": " << message << std::endl;
	}
}


void BaseLogger::error() const {
	std::wcerr << std::endl;
}
void BaseLogger::error(std::wstring message) const {
	if (context.empty()) {
		std::wcerr << L"[ERROR]: " << message << std::endl;
	} else {
		std::wcerr << L"[ERROR] " << context << L": " << message << std::endl;
	}
}


VerboseLogger::VerboseLogger(std::wstring context)
	: BaseLogger(context) {}


void VerboseLogger::debug() const {
	std::wcout << std::endl;
}
void VerboseLogger::debug(std::wstring message) const {
	if (context.empty()) {
		std::wcout << L"[DEBUG]: " << message << std::endl;
	} else {
		std::wcout << L"[DEBUG] " << context << L": " << message << std::endl;
	}
}


BaseLogger Logger::getLogger() {
	return BaseLogger(L"");
}
BaseLogger Logger::getLogger(bool enforceVerbose) {
	if (enforceVerbose /*|| OS::isVerbose*/) {
		return VerboseLogger(L"");
	}
	return BaseLogger(L"");
}


BaseLogger Logger::getLoggerFor(std::string context)
{
	return Logger::getLoggerFor(context, false);
}
BaseLogger Logger::getLoggerFor(std::string context, bool enforceVerbose) {
	const int size = MultiByteToWideChar(CP_UTF8, 0, context.c_str(), -1, nullptr, 0);
	LPWSTR lpBuffer = new WCHAR[size];
	MultiByteToWideChar(CP_UTF8, 0, context.c_str(), -1, lpBuffer, size);
	std::wstring wContext(lpBuffer);
	delete[] lpBuffer;
	return Logger::getLoggerFor(wContext, enforceVerbose);
}
BaseLogger Logger::getLoggerFor(std::wstring context) {
	return BaseLogger(context);
}
BaseLogger Logger::getLoggerFor(std::wstring context, bool enforceVerbose) {
	if (enforceVerbose /*|| OS::isVerbose*/) {
		return VerboseLogger(context);
	}
	return BaseLogger(context);
}
