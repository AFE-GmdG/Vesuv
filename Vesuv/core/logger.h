#pragma once
#include <sstream>

class BaseLogger
{

public:
	BaseLogger(std::wstring context);


protected:
	std::wstring context;


public:
	virtual void debug() const;
	virtual void debug(std::wstring message) const;
	virtual void warn() const;
	virtual void warn(std::wstring message) const;
	virtual void error() const;
	virtual void error(std::wstring message) const;

};


class VerboseLogger
	: public BaseLogger
{

public:
	VerboseLogger(std::wstring context);


	virtual void debug() const;
	virtual void debug(std::wstring message) const;

};

namespace Logger {
	BaseLogger getLogger();
	BaseLogger getLogger(bool enforceVerbose);
	BaseLogger getLoggerFor(std::string context);
	BaseLogger getLoggerFor(std::string context, bool enforceVerbose);
	BaseLogger getLoggerFor(std::wstring context);
	BaseLogger getLoggerFor(std::wstring context, bool enforceVerbose);

};
