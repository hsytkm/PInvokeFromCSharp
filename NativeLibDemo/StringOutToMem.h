#pragma once

/*----------------------------------------------------
	C++からC#に文字列を渡す
	(C#が確保してくれたメモリに文字列を書き込む)
 ----------------------------------------------------*/
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

// アルファベットを返す
DllExport bool StringOutToMem_GetMessageEn(char* dest, int destLength) {
	bool err = SetStringToCharArray("Hello, I'm Library!", dest, destLength);
	
#ifndef _DEBUG
	// VisualStudio2019 16.6.2 の Releaseビルドだと、
	// Sleep入れないと戻り値が true(エラーあり)になる。 なぜ？最適化ミス？
	Sleep(1);
#endif

	return err;
}

// 日本語を返す
DllExport bool StringOutToMem_GetMessageJp(char* dest, int destLength) {
	bool err = SetStringToCharArray("こんにちわ！私はライブラリです！", dest, destLength);

#ifndef _DEBUG
	// VisualStudio2019 16.6.2 の Releaseビルドだと、
	// Sleep入れないと戻り値が true(エラーあり)になる。 なぜ？最適化ミス？
	Sleep(1);
#endif

	return err;
}

// アルファベットの大文字化
DllExport bool StringOutToMem_ToUpper(const char* src, char* dest, int destLength) {
	bool ret = SetStringToCharArray(src, dest, destLength);
	char* p = dest;
	while (*p != '\0')
	{
		*p = toupper(*p);
		p++;
	}
	return ret;
}
