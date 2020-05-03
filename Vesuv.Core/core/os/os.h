#pragma once
#include "../logger.h"
#include "systemDir.h"

namespace Vesuv::Core::OS {

	public ref class OS abstract
	{

	private:
		static OS^ singleton;

		initonly Vesuv::Core::Logger^ logger;
		initonly System::String^ executable;
		initonly array<System::String^>^ parameter;

		int exitCode;


	public:
		OS(System::String^& executable, array<System::String^>^& parameter);
		virtual ~OS();


	protected:
		virtual void initialize() abstract;
		virtual System::UInt64 getTicksUsec() abstract;


	public:
		virtual int GetExitCode();
		virtual void SetExitCode(int exitCode);

		virtual int GetProcessorCount();

		virtual System::String^ getSystemDir(SystemDir directory);

	};

}
