// dllmain.h : Declaration of module class.

class Ctest4Module : public CAtlDllModuleT< Ctest4Module >
{
public :
	DECLARE_LIBID(LIBID_test4Lib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_TEST4, "{7F8F7CEA-24AB-4790-A3A7-4825F6A92359}")
};

extern class Ctest4Module _AtlModule;
