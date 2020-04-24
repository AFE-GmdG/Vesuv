#pragma once

#define WIN32_LEAN_AND_MEAN             // Selten verwendete Komponenten aus Windows-Headern ausschließen
#include <SDKDDKVer.h>
#include <windows.h>

#include <algorithm>
#include <iomanip>
#include <iterator>
#include <map>
#include <numeric>
#include <optional>
#include <set>
#include <sstream>
#include <strsafe.h>
#include <vector>

#define GLM_FORCE_CUDA
#include <glm/glm.hpp>

#include "vulkan/vulkan.h"
#include "vulkan/vulkan_win32.h"

#define ASSERT_VK_SUCCESS(val)\
	if(val != VK_SUCCESS) {\
		__debugbreak();\
	}
