using System;

namespace TwinSharp
{

    public enum E_LicenseHResult : long
    {
        //success
        E_LHR_LicenseOK = 0,
        E_LHR_LicenseOK_Pending = 0x203,
        E_LHR_LicenseOK_Demo = 0x254,
        E_LHR_LicenseOK_OEM = 0x255,
        //error
        E_LHR_LicenseNoFound = (0x98110700 + 0x24),
        E_LHR_LicenseExpired = (0x98110700 + 0x25),
        E_LHR_LicenseExceeded = (0x98110700 + 0x26),
        E_LHR_LicenseInvalid = (0x98110700 + 0x27),
        E_LHR_LicenseSystemIdInvalid = (0x98110700 + 0x28),
        E_LHR_LicenseNoTimeLimit = (0x98110700 + 0x29),
        E_LHR_LicenseTimeInFuture = (0x98110700 + 0x2A),
        E_LHR_LicenseTimePeriodToLong = (0x98110700 + 0x2B),
        E_LHR_DeviceException = (0x98110700 + 0x2C),
        E_LHR_LicenseDuplicated = (0x98110700 + 0x2D),
        E_LHR_SignatureInvalid = (0x98110700 + 0x2E),
        E_LHR_CertificateInvalid = (0x98110700 + 0x2F),
        E_LHR_LicenseOemNotFound = (0x98110700 + 0x30),
        E_LHR_LicenseRestricted = (0x98110700 + 0x31),
        E_LHR_LicenseDemoDenied = (0x98110700 + 0x32),
        E_LHR_LicensePlatformLevelInv = (0x98110700 + 0x33)
    }


    public enum FileOpenModeFlags
    {
        FOPEN_MODEREAD = 0x1,       // "r": Opens for reading. If the file does not exist or cannot be found, the call fails. 
        FOPEN_MODEWRITE = 0x2,      // "w": Opens an empty file for writing. If the given file exists, its contents are destroyed. 
        FOPEN_MODEAPPEND = 0x4,     // "a": Opens for writing at the end of the file (appending) without removing the EOF marker before writing new data to the file; creates the file first if it doesnot exist. 
        FOPEN_MODEPLUS = 0x8,       // "+": Opens for reading and writing 
        FOPEN_MODEBINARY = 0x10,    // "b": Open in binary (untranslated) mode. 
        FOPEN_MODETEXT = 0x20,      // "t": Open in text (translated) mode.     }
    }

    public enum E_EnumCmdType
    {
        First = 0,
        Next,
        Abort
    }
}
