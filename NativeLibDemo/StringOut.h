#pragma once

#include "Common.h"

bool SetStringToCharArray(const char* src, int srcLength, char* dest, int destLength) {
	if (destLength < 1) return true;

	bool err;
	int copyLength, nullIndex;

	if (destLength + 1 >= srcLength) {
		// 全部収まるパターン
		err = false;
		copyLength = srcLength;
		nullIndex = srcLength;
	}
	else {
		// 全部収まらないパターン
		err = true;
		copyLength = destLength - 1;
		nullIndex = destLength - 1;
	}

	for (int i = 0; i < copyLength; i++) {
		dest[i] = src[i];
	}
	dest[nullIndex] = '\0';
	return err;
}

bool SetStringToCharArray(const char* src, char* dest, int destLength) {
	return SetStringToCharArray(src, (int)strlen(src), dest, destLength);
}

//bool SetStringToCharArray(std::string str, char* dest, int destLength) {
//	return SetStringToCharArray(str.c_str(), (int)str.size(), dest, destLength);
//}


DllExport bool StringOut_GetMessageEn(char* dest, int destLength) {
	return SetStringToCharArray("Hello, I'm Library!", dest, destLength);
}

DllExport bool StringOut_GetMessageJp(char* dest, int destLength) {
	// ◆日本語を返せてない
	return SetStringToCharArray("こんにちわ！私はライブラリです！", dest, destLength);
}

DllExport bool StringOut_ToUpper(const char* src, char* dest, int destLength) {
	bool ret = SetStringToCharArray(src, dest, destLength);
	char* p = dest;
	while (*p != '\0')
	{
		*p = toupper(*p);
		p++;
	}
	return ret;
}
