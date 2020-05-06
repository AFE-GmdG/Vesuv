#pragma once

namespace Vesuv::Server {


	public ref class DisplayServer abstract :
		public Vesuv::Core::CoreObject
	{

	private:
		static DisplayServer^ singleton;


	public:
		DisplayServer();
		virtual ~DisplayServer();

	public:
		static DisplayServer^ getSingleton();

	};

}
