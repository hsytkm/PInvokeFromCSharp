#pragma once

#include "Common.h"

DllExport bool Bool_GetTrue() {
	return true;
}

DllExport bool Bool_Not(bool b) {
	if (b == true)
		return false;
	else
		return true;
}

DllExport bool Bool_And(bool b0, bool b1) {
	return b0 && b1;
}
