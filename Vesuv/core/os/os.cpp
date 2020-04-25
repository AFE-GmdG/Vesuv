#include "os.h"


OS* OS::singleton = nullptr;


OS::OS() {
	assert(singleton == nullptr);

	singleton = this;
}


OS::~OS() {
	singleton = nullptr;
}


OS* OS::getSingleton() {
	return singleton;
}


std::wstring OS::getSystemDir(SystemDir directory) const {
	UNREFERENCED_PARAMETER(directory);

	return L".";
}
