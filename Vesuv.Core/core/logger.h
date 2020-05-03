#pragma once

namespace Vesuv::Core {

	public ref class Logger
	{

	private:
		System::String^ context;


	private:
		Logger(System::String^& context);
		~Logger();


	public:
		static Logger^ GetFor();
		static Logger^ GetFor(System::String^ context);


	public:
		void Log();
		void Log(System::String^ message);
	};

}
