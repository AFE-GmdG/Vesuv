#pragma once

namespace Vesuv::Main {

	public ref class Main
	{

	private:
		// Singletons
		// Initialized in setup()
		//static Vesuv::Server::Engine^ engine = nullptr;
		static Vesuv::Core::ProjectSettings^ projectSettings = nullptr;


		// Initialized in setup2()
		//static Vesuv::Server::AudioServer^ audio_server = nullptr;
		static Vesuv::Server::DisplayServer^ displayServer = nullptr;
		//static Vesuv::Server::RenderingServer^ rendering_server = nullptr;
		//static Vesuv::Server::CameraServer^ camera_server = nullptr;
		//static Vesuv::Server::XRServer^ xr_server = nullptr;
		//static Vesuv::Server::PhysicsServer3D^ physics_server = nullptr;
		//static Vesuv::Server::PhysicsServer2D^ physics_2d_server = nullptr;
		//static Vesuv::Server::NavigationServer3D^ navigation_server = nullptr;
		//static Vesuv::Server::NavigationServer2D^ navigation_2d_server = nullptr;


		// Initialized in start()
		// Everything the main loop needs to know about frame timings


		// Drivers


		// Engine config/tools
		static bool editor = false;
		static bool project_manager = false;


		// Display
		static Vesuv::Server::WindowMode windowMode = Vesuv::Server::WindowMode::Windowed;
		static Vesuv::Server::WindowFlags windowFlags = Vesuv::Server::WindowFlags::NoFlags;


		// Timing / State
		static System::UInt64 lastTicks;
		static System::UInt64 targetTicks;
		static System::UInt32 frames;
		static System::UInt32 frame;
		static bool forceRedrawRequested;
		static int iterating;


	private:
		static Vesuv::Core::Error SetupPlatformSpecific();


	public:
		static Vesuv::Core::Error Setup();
		static bool Start();

		static void Cleanup();

	};

}
