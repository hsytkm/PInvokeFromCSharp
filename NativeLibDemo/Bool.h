#pragma once

#include "Common.h"

DllExport bool Bool_GetFalse() {
	return false;
}

DllExport bool Bool_GetTrue() {
	return true;
}

DllExport bool Bool_Not(bool b) {
	return !b;
}

DllExport bool Bool_And(bool b0, bool b1) {
	return b0 && b1;
}

DllExport bool Bool_Or(bool b0, bool b1) {
	return b0 || b1;
}
