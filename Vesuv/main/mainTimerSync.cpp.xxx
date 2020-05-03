#include "mainTimerSync.h"


void MainFrameTime::clampIdle(float minIdleStep, float maxIdleStep) {
	idleStep = std::min(std::max(idleStep, minIdleStep), maxIdleStep);
}


MainTimerSync::MainTimerSync()
	: lastCpuTicksUsec(),
	currentCpuTicksUsec(),
	timeAccum(0),
	timeDeficit(0),
	accumulatedPhysicsSteps(),
	typicalPhysicsSteps(),
	fixedFps(0) {

	for (int i = 0; i < CONTROL_STEPS; ++i) {
		typicalPhysicsSteps[i] = i;
		accumulatedPhysicsSteps[i] = i;
	}
}


void MainTimerSync::init(LARGE_INTEGER cpuTicksUsec) {
	currentCpuTicksUsec = lastCpuTicksUsec = cpuTicksUsec;
}


void MainTimerSync::setCpuTicksUsec(LARGE_INTEGER cpuTicksUsec) {
	currentCpuTicksUsec = cpuTicksUsec;
}

void MainTimerSync::setFixedFps(int fps) {
	fixedFps = fps;
}
