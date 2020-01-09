#pragma once

#include "Common.h"
#include <string>
//#include <Windows.h>	//LPCWSTR

// 日本語ダメ
DllExport int StringIn_CountChar(const char *c) {
	size_t size = strlen(c);
	return static_cast<int>(size);
}

// 日本語ダメ
DllExport int StringIn_CountStdString(const char* c) {
	std::string str(c);
	size_t size = str.size();	// .length() と同じ
	return static_cast<int>(size);
}

// 日本語OK
DllExport int StringIn_CountWChar(const wchar_t* str) {
	// const wchar_t* = LPCWSTR
	size_t size = wcslen(str);
	return static_cast<int>(size);
}
