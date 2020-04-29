#pragma once
#include "../globals.h"

struct MainFrameTime
{
	// Time to advance idles for. (argument to process())
	float idleStep;

	// Number of times to iterate the physics engine.
	unsigned int physicsSteps;

	// Fraction through the current physics tick.
	float interpolationFraction;

	void clampIdle(float minIdleStep, float maxIdleStep);
};

class MainTimerSync
{

private:
	// Wall clock time measured on the main thread.
	LARGE_INTEGER lastCpuTicksUsec;
	LARGE_INTEGER currentCpuTicksUsec;

	// Logical game time since last physics timestep.
	float timeAccum;

	// Current difference between wall clock time and reported sum of idleSteps.
	float timeDeficit;

	// Number of frames back for keeping accumulated physics steps roughly constant.
	// Value of 12 chosen because that is what is required to make 144 Hz monitors
	// behave well with 60 Hz physics updates. The only worse commonly available refresh
	// would be 85, requiring CONTROL_STEPS = 17.
	static const int CONTROL_STEPS = 12;

	// Sum of physics steps done over the last (i+1) frames.
	int accumulatedPhysicsSteps[CONTROL_STEPS];

	// Typical value for accumulatedPhysicsSteps[i] is either this or this plus one
	int typicalPhysicsSteps[CONTROL_STEPS];

	int fixedFps;


protected:
	// Todo


public:
	MainTimerSync();

	// Start the clock
	void init(LARGE_INTEGER cpuTicksUsec);
	// Set measured wall clock time
	void setCpuTicksUsec(LARGE_INTEGER cpuTicksUsec);
	// Set fixed fps
	void setFixedFps(int fps);

};
