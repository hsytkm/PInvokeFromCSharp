#pragma once

#include "Common.h"

DllExport int BuiltIn_GetInt() {
	return 1234;
}

DllExport double BuiltIn_GetDouble() {
	return 12.34;
}

DllExport double BuiltIn_AddIntDouble(int i, double d) {
	return static_cast<double>(i) + d;
}
