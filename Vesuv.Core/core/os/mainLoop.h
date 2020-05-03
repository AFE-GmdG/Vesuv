#pragma once

namespace Vesuv::Core::OS {

	public ref class MainLoop
	{

	public:
		MainLoop();
		virtual ~MainLoop();

		void init();
		bool iteration(float time);
		bool idle(float time);
		void finish();

	};

}
