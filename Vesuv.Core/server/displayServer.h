#pragma once
#include "../core/coreObject.h"

namespace Vesuv::Server {



	public ref class DisplayServer abstract :
		public Vesuv::Core::CoreObject
	{

	private:
		static DisplayServer^ singleton;


	public:
		DisplayServer();
		~DisplayServer();

	public:
		static DisplayServer^ getSingleton();

	};

}
