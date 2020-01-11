#pragma once

/*----------------------------------------------------
	C++����C#�ɕ������n��
	(C#���m�ۂ��Ă��ꂽ�������ɕ��������������)
 ----------------------------------------------------*/
#include "Common.h"

bool SetStringToCharArray(const char* src, int srcLength, char* dest, int destLength) {
	if (destLength < 1) return true;

	bool err;
	int copyLength, nullIndex;

	if (destLength + 1 >= srcLength) {
		// �S�����܂�p�^�[��
		err = false;
		copyLength = srcLength;
		nullIndex = srcLength;
	}
	else {
		// �S�����܂�Ȃ��p�^�[��
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

// �A���t�@�x�b�g��Ԃ�
DllExport bool StringOutToMem_GetMessageEn(char* dest, int destLength) {
	return SetStringToCharArray("Hello, I'm Library!", dest, destLength);
}

// ���{���Ԃ�
DllExport bool StringOutToMem_GetMessageJp(char* dest, int destLength) {
	// �����{���Ԃ��ĂȂ�
	return SetStringToCharArray("����ɂ���I���̓��C�u�����ł��I", dest, destLength);
}

// �A���t�@�x�b�g�̑啶����
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