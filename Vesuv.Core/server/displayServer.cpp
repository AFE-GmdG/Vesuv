#include "displayServer.h"

namespace Vesuv::Server {

	using namespace System;
	using namespace Vesuv::Core;


	DisplayServer::DisplayServer() {
		DisplayServer::singleton = this;
	}


	DisplayServer::~DisplayServer() {
		DisplayServer::singleton = nullptr;
	}


	DisplayServer^ DisplayServer::getSingleton() {
		return DisplayServer::singleton;
	}

}
