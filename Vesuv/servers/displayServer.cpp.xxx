#include "displayServer.h"


DisplayServer* DisplayServer::singleton = nullptr;
DisplayServer::CreateFunction DisplayServer::createFunction = nullptr;

DisplayServer::DisplayServer()
	: logger(Logger::getLoggerFor(__FILE__, true))
{
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


DisplayServer* DisplayServer::create(WindowMode windowMode, WindowFlags flags, Error& errorRef) {
	return createFunction(windowMode, flags, errorRef);
}
