#pragma once
#include <Windows.h>

namespace Vesuv::Platform::Windows {

	ref class OS_Windows :
		public Vesuv::Core::OS::OS
	{

#pragma region Fields
	private:
		initonly HINSTANCE hInstance;
		initonly Vesuv::Core::Logger^ logger;
		initonly System::UInt64 ticksPerSecond;
		initonly System::UInt64 ticksStart;

		Vesuv::Core::OS::MainLoop^ mainLoop;
#pragma endregion

#pragma region Properties
	public:
		virtual property int ProcessorCount {
			int get() override {
				SYSTEM_INFO systemInfo;
				GetSystemInfo(&systemInfo);
				return systemInfo.dwNumberOfProcessors;
			}
		};
#pragma endregion

#pragma region ctor/dtor/Dispose
	public:
		OS_Windows(HINSTANCE hInstance, System::String^& executable, array<System::String^>^& parameter);
		virtual ~OS_Windows();
#pragma endregion

#pragma region Overwritten Methods
	public:
		virtual void Initialize() override;
		virtual System::UInt64 GetTicksUsec() override;

		virtual Vesuv::Core::Error SetCwd(System::String^ cwd) override;
#pragma endregion

#pragma region Methods
	public:
		void Run();
#pragma endregion

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
