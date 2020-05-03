#include "os.h"

namespace Vesuv::Core::OS {

	using namespace System;
	using namespace Vesuv::Core;

	OS::OS(String^& executable, array<String^>^& parameter) :
		logger(Logger::GetFor()),
		executable(executable),
		parameter(parameter),
		exitCode(0) {

		OS::singleton = this;

		logger->Log("Initialize OS.");
	}


	OS::~OS() {
		OS::singleton = nullptr;
		if (logger) {
			logger->Log("Destroy OS.");
			delete logger;
		}
	}


	int OS::GetExitCode() {
		return exitCode;
	}
	void OS::SetExitCode(int exitCode) {
		this->exitCode = exitCode;
	}


	int OS::GetProcessorCount() {
		// The generic version returns only 1.
		// Override in system specific version with a correct implementation.
		return 1;
	}

	String^ Vesuv::Core::OS::OS::getSystemDir(SystemDir directory) {
		return L".";
	}


}
