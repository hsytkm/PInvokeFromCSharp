#pragma once

/*------------------------------------------------
	C++���Ŋm�ۂ��Ă��郁�����A�h���X��C#�ɓn��
 ------------------------------------------------*/
#include "Common.h"

// Align C++ and C#
struct MemoryDataFromLib {
	BYTE* ptr;
	int size;	// size�̌^(�ő�l)���I�X�X���BMarshal.ReadByte(IntPtr, int) ������B
};

const static int MEMFROMLIB_BUFFER_SIZE = 10 * 1000 * 1000;
BYTE _MemFromLibBuffer[MEMFROMLIB_BUFFER_SIZE];

/*
 * �\���̂�l�ŕԂ�
 */
DllExport MemoryDataFromLib MemFromLib_GetBufferData() {

	MemoryDataFromLib data;
	data.ptr = _MemFromLibBuffer;
	data.size = MEMFROMLIB_BUFFER_SIZE;

	// ���������� BYTE �𑫂����� 123 �ɂȂ�
	memset(data.ptr, 0, data.size);
	data.ptr[0] = 100;
	data.ptr[data.size / 2] = 20;
	data.ptr[data.size - 1] = 3;

	return data;
}

/*
 * �\���̂��Q�ƂŕԂ�(�\���̂̎��̂�static�Œ�`����)
 */
MemoryDataFromLib _MemoryDataFromLib;

DllExport MemoryDataFromLib* MemFromLib_GetBufferDataRef() {

	MemoryDataFromLib& data = _MemoryDataFromLib;
	data.ptr = _MemFromLibBuffer;
	data.size = MEMFROMLIB_BUFFER_SIZE;

	// ���������� BYTE �𑫂����� 123 �ɂȂ�
	memset(data.ptr, 0, data.size);
	data.ptr[0] = 100;
	data.ptr[data.size / 2] = 20;
	data.ptr[data.size - 1] = 3;

	return &data;
}