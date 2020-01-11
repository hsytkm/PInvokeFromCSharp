#pragma once

/*---------------------------------------------------------
	C#から渡されたアンマネージメモリをC++で読み書きする
 ---------------------------------------------------------*/
#include "Common.h"

// Align C++ and C#
struct MemoryDataToLib {
	BYTE* ptr;
	int size;
};

/*
 * 渡されたメモリの読み取り(合計値)
 */
DllExport int MemToLib_GetBufferDataSum(const MemoryDataToLib& data) {
	int sum = 0;
	for (int i = 0; i < data.size; i++) {
		sum += *(data.ptr + i);
	}
	return sum;
}

/*
 * 渡されたメモリの末尾に書き込み
 */
DllExport bool MemToLib_SetBufferLast(MemoryDataToLib &data, BYTE val) {
	if (data.size <= 0) return true;	//err

	data.ptr[data.size - 1] = val;
	return false;	//success
}
