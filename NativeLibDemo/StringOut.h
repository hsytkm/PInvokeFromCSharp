#pragma once

/*----------------------------------------------------
	C++からC#に文字列を渡す
 ----------------------------------------------------*/
#include "Common.h"

// 固定文字を返す
DllExport const char* StringOut_GetConstMessagePtr() {
	return "This is const char*";
}
