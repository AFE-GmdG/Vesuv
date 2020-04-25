#include "displayServer.h"


DisplayServer* DisplayServer::singleton = nullptr;
DisplayServer::CreateFunction DisplayServer::createFunction = nullptr;

DisplayServer::DisplayServer() {
	assert(singleton == nullptr);

	singleton = this;
}


DisplayServer::~DisplayServer() {
	singleton = nullptr;
}


DisplayServer* DisplayServer::getSingleton() {
	return singleton;
}


void DisplayServer::registerCreateFunction(CreateFunction createDisplayServer) {
	DisplayServer::createFunction = createDisplayServer;
}

DisplayServer* DisplayServer::create(WindowMode windowMode, uint32_t flags, Error& errorRef) {
	return createFunction(windowMode, flags, errorRef);
}
