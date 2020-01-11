#pragma once

/*------------------------------------------------
	C++側で確保しているメモリアドレスをC#に渡す
 ------------------------------------------------*/
#include "Common.h"

// Align C++ and C#
struct MemoryDataFromLib {
	BYTE* ptr;
	int size;	// sizeの型(最大値)がオススメ。Marshal.ReadByte(IntPtr, int) だから。
};

const static int MEMFROMLIB_BUFFER_SIZE = 10 * 1000 * 1000;
BYTE _MemFromLibBuffer[MEMFROMLIB_BUFFER_SIZE];

/*
 * 構造体を値で返す
 */
DllExport MemoryDataFromLib MemFromLib_GetBufferData() {

	MemoryDataFromLib data;
	data.ptr = _MemFromLibBuffer;
	data.size = MEMFROMLIB_BUFFER_SIZE;

	// メモリ内の BYTE を足したら 123 になる
	memset(data.ptr, 0, data.size);
	data.ptr[0] = 100;
	data.ptr[data.size / 2] = 20;
	data.ptr[data.size - 1] = 3;

	return data;
}

/*
 * 構造体を参照で返す(構造体の実体をstaticで定義する)
 */
MemoryDataFromLib _MemoryDataFromLib;

DllExport MemoryDataFromLib* MemFromLib_GetBufferDataRef() {

	MemoryDataFromLib& data = _MemoryDataFromLib;
	data.ptr = _MemFromLibBuffer;
	data.size = MEMFROMLIB_BUFFER_SIZE;

	// メモリ内の BYTE を足したら 123 になる
	memset(data.ptr, 0, data.size);
	data.ptr[0] = 100;
	data.ptr[data.size / 2] = 20;
	data.ptr[data.size - 1] = 3;

	return &data;
}
