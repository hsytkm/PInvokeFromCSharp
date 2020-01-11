#pragma once

/*---------------------------------------------------------------
	C++クラスを提供して、C#にデストラクタをコールしてもらう
 ---------------------------------------------------------------*/
#include "Common.h"

namespace ClassDispose {
	class LibBuffer {

	private:
		int* dataPtr;
		int dataSize;

	public:
		LibBuffer() : dataPtr{ nullptr }, dataSize{ 0 }
		{
			dataSize = 5;

			int* data = new int[dataSize];
			for (int i = 0; i < dataSize; i++) {
				data[i] = i;
			}
			dataPtr = data;
		}

		~LibBuffer() {
			if (dataPtr != nullptr) {
				delete[] dataPtr;
				dataPtr = nullptr;
			}
		}

		int GetDataSum() {
			int sum = 0;
			for (int i = 0; i < dataSize; i++) {
				sum += i;
			}
			return sum;
		}

		int GetDataSize() { return dataSize; }
	};
}

/*------------------------------------------------
	Facade
 ------------------------------------------------*/
using namespace ClassDispose;

DllExport LibBuffer* CreateLibBufferClass()
{
	return new LibBuffer();
}

DllExport int DisposeLibBufferClass(LibBuffer* ptr)
{
	SafeDelete(&ptr);
	return 0;	// success
}

DllExport int LibBuffer_GetDataSum(LibBuffer* ptr)
{
	return ptr->GetDataSum();
}

DllExport int LibBuffer_GetDataSize(LibBuffer* ptr)
{
	return ptr->GetDataSize();
}
