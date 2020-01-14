#pragma once

/*--------------------------------
	C#からC++に文字列を渡す
 --------------------------------*/
#include "Common.h"
#include <string>
//#include <Windows.h>	//LPCWSTR

// 日本語化けない（けど、文字数が2倍になる)
DllExport int StringIn_AnsiCountChar(const char* c) {
	size_t size = strlen(c);
	return static_cast<int>(size);
}

// 日本語化ける
DllExport int StringIn_UnicodeCountChar(const char *c) {
	size_t size = strlen(c);
	return static_cast<int>(size);
}

// 日本語ダメ
DllExport int StringIn_UnicodeCountStdString(const char* c) {
	std::string str(c);
	size_t size = str.size();	// size() は length() と同じっぽい
	return static_cast<int>(size);
}

// 日本語OK
DllExport int StringIn_UnicodeCountWChar(const wchar_t* str) { // const wchar_t* = LPCWSTR
	size_t size = wcslen(str);
	return static_cast<int>(size);
}
