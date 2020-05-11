#include "main.h"
#include "../globals.h"
#include <msclr/marshal.h>

namespace Vesuv::Main {

	using namespace System;
	using namespace Vesuv::Core;
	using namespace Vesuv::Core::OS;
	using namespace Vesuv::Server;


	void Main::ShowHelp() {
		MessageBoxW(nullptr, L"-h  --help\tShow this help.\r\n\
-e  --editor\tStart direct with the editor\r\n\
-p  --project-manager\tStart with the project manager (default)\r\n", L"Command Line Parameter", MB_ICONINFORMATION | MB_OK);
	}

	Error Main::SetupPlatformSpecific() {
		return Error::NYI;
	}


	Error Main::Setup() {
		auto const os = Vesuv::Core::OS::OS::Singleton;
		os->Initialize();
		engine = gcnew Engine();
		projectSettings = gcnew ProjectSettings();
		auto icic = StringComparison::InvariantCultureIgnoreCase;
		for (int i = 0; i < os->Parameter->Length; ++i) {
			auto const parameter = os->Parameter[i];

			if (parameter->Equals("-e", icic) || parameter->Equals("--editor")) {
				editor = true;
				continue;
			} else if (parameter->Equals("-p", icic) || parameter->Equals("--project-manager", icic)) {
				projectManager = true;
				continue;
			} else if (parameter->EndsWith("project.vesuv", icic)) {
				String^ path = ".";
				int sep = Math::Max(parameter->LastIndexOf(L'/'), parameter->LastIndexOf(L'\\'));
				if (sep > 0) {
					path = parameter->Substring(0, sep);
				}
				if (os->SetCwd(path) != Error::Ok) {
					projectPath = path;
				}
#ifndef RUNTIME_ONLY
				editor = true;
#endif // !RUNTIME_ONLY

			} else if (parameter->Equals("-h", icic) || parameter->Equals("--help", icic)) {
				ShowHelp();
			}

			return Error::InvalidParameter;
		}

#ifndef RUNTIME_ONLY
		if (editor && projectManager) {
			MessageBoxW(nullptr, L"Error: Command line arguments implied opening both editor and project manager, which is not possible.", L"Error: Invalid Parameter", MB_ICONERROR | MB_OK);
			return Error::InvalidParameter;
		}
#endif // !RUNTIME_ONLY

		// TODO
		// Network File System needs to be configured before global project settings,
		// since the global project settings are based on the 'project.vesuv' file
		// which will only be available through the network if this is enabled
		// ...

		if (projectSettings->Setup(projectPath, mainPack, upwards) == Error::Ok) {
#ifndef RUNTIME_ONLY
			foundProject = true;
#endif
		} else {
#ifdef RUNTIME_ONLY
			pin_ptr<const wchar_t> message = PtrToStringChars(String::Format(L"Error: Couldn't load project data at path \"{0}\".", projectPath));
			MessageBoxW(nullptr, message, L"Error: Invalid Parameter", MB_ICONERROR | MB_OK);
#else
			pin_ptr<const wchar_t> message = PtrToStringChars(String::Format(L"Error: Couldn't found \"project.vesuv\" at path \"{0}\".", projectPath));
			MessageBoxW(nullptr, message, L"Error: Invalid Parameter", MB_ICONERROR | MB_OK);
			editor = false;
#endif
			return Error::NotFound;
		}

		return Error::Ok;
	}


	bool Main::Start() {
		return false;
	}


	void Main::Cleanup() {
		if (projectSettings) {
			delete projectSettings;
		}
	}
}
