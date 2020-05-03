#define WIN32_LEAN_AND_MEAN // Selten verwendete Komponenten aus Windows-Headern ausschlieﬂen
#include <SDKDDKVer.h>
#include <Windows.h>
#include <locale.h>
#include <shellapi.h>
#include <msclr/marshal.h>

#include <memory>

#include "resource.h"
#include "main/main.h"
#include "platform/windows/osWindows.h"

using namespace msclr::interop;
using namespace System;
using namespace System::Reflection;
using namespace Vesuv::Core;
using namespace Vesuv::Platform::Windows;

namespace Vesuv {

	value struct CommandLineArgs
	{
		String^ executable;
		array<String^>^ parameter;
	};

	CommandLineArgs PreprocessCommandLine() {
		int argc;
		LPCWSTR cmdLine = GetCommandLineW();
		DWORD expandedCmdLineSize = ExpandEnvironmentStringsW(cmdLine, nullptr, 0);
		std::unique_ptr<WCHAR[]> expandedCmdLine = std::make_unique<WCHAR[]>(expandedCmdLineSize);
		ExpandEnvironmentStringsW(cmdLine, expandedCmdLine.get(), expandedCmdLineSize);
		std::unique_ptr<LPWSTR, decltype(&LocalFree)> argsPtr(CommandLineToArgvW(expandedCmdLine.get(), &argc), &LocalFree);
		LPWSTR* args = argsPtr.get();
		String^ executable = marshal_as<String^>(args[0]);
		array<String^>^ parameter = gcnew array<String^>(argc - 1);
		for (int i = 0; i < argc - 1; ++i) {
			parameter[i] = marshal_as<String^>(args[i + 1]);
		}
		return {executable, parameter};
	}

	[STAThread]
	int main() {
		setlocale(LC_ALL, "en-US");
		auto [executable, parameter] = PreprocessCommandLine();

		Logger^ logger = Logger::GetFor("Main");
		try {
			HINSTANCE hInstance = GetModuleHandleW(nullptr);

			Assembly^ executingAssembly = Assembly::GetExecutingAssembly();
			AssemblyDescriptionAttribute^ description = dynamic_cast<AssemblyDescriptionAttribute ^>(Attribute::GetCustomAttribute(executingAssembly, AssemblyDescriptionAttribute::typeid));
			AssemblyFileVersionAttribute^ fileVersion = dynamic_cast<AssemblyFileVersionAttribute ^>(Attribute::GetCustomAttribute(executingAssembly, AssemblyFileVersionAttribute::typeid));

			logger->Log(String::Format("{0} Version {1}", description->Description, fileVersion->Version));

			OS_Windows os(hInstance, executable, parameter);

			Error error = Vesuv::Main::Main::setup();
			if (error != Error::Ok) {
				return static_cast<int>(error);
			}

			if (Vesuv::Main::Main::start()) {
				os.run();
			}

			//	Main::cleanup();

			return os.GetExitCode();
		} finally {
			delete logger;
		}
	}

}
