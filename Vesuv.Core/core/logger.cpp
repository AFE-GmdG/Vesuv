#include "logger.h"

namespace Vesuv::Core {

	using namespace System;
	using namespace System::Diagnostics;


	Logger::Logger(String^& context) :
		context(context) {}


	Logger::~Logger() {}


	Logger^ Logger::GetFor() {
		StackTrace stackTrace(1, true);
		int i = 0;
		StackFrame^ stackFrame = stackTrace.GetFrame(i);
		String^ context = stackFrame->GetMethod()->DeclaringType->FullName;
		return GetFor(context);
	}
	Logger^ Logger::GetFor(String^ context) {
		return gcnew Logger(context);
	}


	void Logger::Log() {
		Debug::WriteLine("");
	}
	void Logger::Log(String^ message) {
		Debug::WriteLine(message, context);
	}

}
