#pragma once


enum class Error
{
	OK = 0,
	FAILED,
	ERR_UNAVAILABLE = 100,
	ERR_UNCONFIGURED,
	ERR_UNAUTHORIZED,
	ERR_NYI = 99999
};
