// dllmain.cpp : Implementation of DllMain.

#include "stdafx.h"
#include "resource.h"
#include "test4_i.h"
#include "dllmain.h"
#include "compreg.h"
#include "dlldatax.h"

Ctest4Module _AtlModule;

class Ctest4App : public CWinApp
{
public:

// Overrides
	virtual BOOL InitInstance();
	virtual int ExitInstance();

	DECLARE_MESSAGE_MAP()
};

BEGIN_MESSAGE_MAP(Ctest4App, CWinApp)
END_MESSAGE_MAP()

Ctest4App theApp;

BOOL Ctest4App::InitInstance()
{
#ifdef _MERGE_PROXYSTUB
	if (!PrxDllMain(m_hInstance, DLL_PROCESS_ATTACH, NULL))
		return FALSE;
#endif
	return CWinApp::InitInstance();
}

int Ctest4App::ExitInstance()
{
	return CWinApp::ExitInstance();
}
