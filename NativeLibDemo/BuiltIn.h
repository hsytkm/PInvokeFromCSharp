#pragma once

#include "Common.h"
#include <limits.h>

/*
 * int
 */
DllExport int BuiltIn_GetInt() { return 1234; }
DllExport int BuiltIn_GetIntMin() { return INT_MIN; }
DllExport int BuiltIn_GetIntMax() { return INT_MAX; }

/*
 * byte(unsigned char = BYTE)
 */
DllExport unsigned char BuiltIn_GetByteMax() { return UCHAR_MAX; }

/*
 * ulong(unsigned long long = ULONG64)
 */
DllExport unsigned long long BuiltIn_GetUInt64Max() { return ULLONG_MAX; }

/*
 * double
 */
DllExport double BuiltIn_GetDouble() {
	return 12.34;
}

/*
 * add
 */
DllExport double BuiltIn_AddIntDouble(int i, double d) {
	return static_cast<double>(i) + d;
}
