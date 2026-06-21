#include "pch.h"

extern "C" __declspec(dllexport) double Add(double a, double b)
{
    return a + b;
}

extern "C" __declspec(dllexport) double Subtract(double a, double b)
{
    return a - b;
}

extern "C" __declspec(dllexport) double Multiply(double a, double b)
{
    return a * b;
}

extern "C" __declspec(dllexport) double Divide(double a, double b)
{
    if (b == 0)
    {
        return 0;
    }

    return a / b;
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD ul_reason_for_call, LPVOID lpReserved)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }

    return TRUE;
}