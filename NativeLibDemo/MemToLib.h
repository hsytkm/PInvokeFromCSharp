#pragma once

/*---------------------------------------------------------
	C#����n���ꂽ�A���}�l�[�W��������C++�œǂݏ�������
 ---------------------------------------------------------*/
#include "Common.h"

// Align C++ and C#
struct MemoryDataToLib {
	BYTE* ptr;
	int size;
};

/*
 * �n���ꂽ�������̓ǂݎ��(���v�l)
 */
DllExport int MemToLib_GetBufferDataSum(const MemoryDataToLib& data) {
	int sum = 0;
	for (int i = 0; i < data.size; i++) {
		sum += *(data.ptr + i);
	}
	return sum;
}

/*
 * �n���ꂽ�������̖����ɏ�������
 */
DllExport bool MemToLib_SetBufferLast(MemoryDataToLib &data, BYTE val) {
	if (data.size <= 0) return true;	//err

	data.ptr[data.size - 1] = val;
	return false;	//success
}