#pragma once

/*--------------------------------
	C#����C++�ɕ������n��
 --------------------------------*/
#include "Common.h"
#include <string>
//#include <Windows.h>	//LPCWSTR

// ���{��_��
DllExport int StringIn_CountChar(const char *c) {
	size_t size = strlen(c);
	return static_cast<int>(size);
}

// ���{��_��
DllExport int StringIn_CountStdString(const char* c) {
	std::string str(c);
	size_t size = str.size();	// size() �� length() �Ɠ������ۂ�
	return static_cast<int>(size);
}

// ���{��OK
DllExport int StringIn_CountWChar(const wchar_t* str) { // const wchar_t* = LPCWSTR
	size_t size = wcslen(str);
	return static_cast<int>(size);
}