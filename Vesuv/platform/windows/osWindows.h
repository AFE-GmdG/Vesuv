#pragma once
#include <Windows.h>

namespace Vesuv::Platform::Windows {

	ref class OS_Windows :
		public Vesuv::Core::OS::OS
	{

	private:
		initonly HINSTANCE hInstance;
		initonly Vesuv::Core::Logger^ logger;
		System::UInt64 ticksPerSecond;
		System::UInt64 ticksStart;


	public:
		OS_Windows(HINSTANCE hInstance, System::String^& executable, array<System::String^>^& parameter);
		virtual ~OS_Windows();


	protected:
		virtual void initialize() override;
		virtual System::UInt64 getTicksUsec() override;


	public:
		virtual int GetProcessorCount() override;


	public:
		void run();

	};

}


//class OS_Windows
//	: public OS
//{
//
//private:
//	const int nCmdShow;
//	const HINSTANCE hInstance;
//	HWND hMainWindow;
//
//	LARGE_INTEGER ticksPerSecond;
//	LARGE_INTEGER ticksStart;
//
//	//MainLoop^ mainLoop;
//
//	bool forceQuit;
//
//public:
//	OS_Windows(const std::wstring& executable, const std::vector<std::wstring>& parameter, const int nCmdShow, const HINSTANCE hInstance);
//	virtual ~OS_Windows();
//
//	virtual void initialize();
//	virtual LARGE_INTEGER getTicksUsec() const;
//
//	virtual std::wstring getSystemDir(SystemDir directory) const;
//
//	void run();
//
//	const int getCmdShow() const;
//	HINSTANCE getHInstance() const;
//
//	void setMainWindow(HWND hMainWindow);
//};
