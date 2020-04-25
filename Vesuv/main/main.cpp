#include "main.h"

#include "../servers/displayServer.h"


// Initialized in setup()


// Initialized in setup2()
static DisplayServer* displayServer;


// Drivers


// Display
static DisplayServer::WindowMode windowMode = DisplayServer::WindowMode::WINDOWED;
static uint32_t windowFlags = 0;


/* Engine initialization
 *
 * Consists of several methods that are called by each platform's specific main function.
 *
 * setup is responsible for the initialization of all low level singletons and core types,
 * parsing the command line arguments to configure things accordingly.
 */
Error Main::setup() {
	OS::getSingleton()->initialize();

	return setup2();
}


/* Engine initialization (Part 2)
 *
 * setup2 registers high level servers and singletons, display the boot splash,
 * register higher level types (Scene, Editor, ...)
 */
Error Main::setup2() {
	// Initialize DisplayServer
	Error err;
	displayServer = DisplayServer::create(windowMode, windowFlags, err);

	return err;
}


/* Engine startup
 *
 * start is the last step and that's where command line tools can run or the main loop
 * can be created eventually and the project settings put into action.
 */
bool Main::start() {
	return true;
}


bool Main::interaction() {
  bool exit = true;

  return exit;
}


void Main::cleanup() {

}
