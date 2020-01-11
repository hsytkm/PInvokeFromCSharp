#pragma once

#define DllExport extern "C" __declspec(dllexport)

template <class T> void SafeDelete(T** ppT)
{
	if (*ppT != nullptr)
	{
		delete* ppT;
		*ppT = nullptr;
	}
}
