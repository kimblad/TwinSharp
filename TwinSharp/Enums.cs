using System;

namespace TwinSharp
{

    /// <summary>
    /// The E_LicenseHResult enumeration contains the possible results of the license check.
    /// </summary>
    public enum E_LicenseHResult : long
    {
        /// <summary>
        /// License is valid
        /// </summary>
        E_LHR_LicenseOK = 0,

        /// <summary>
        /// Validation of the licensing device (e.g. License Key Terminal) required, license is however still valid.
        /// </summary>
        E_LHR_LicenseOK_Pending = 0x203,

        /// <summary>
        /// Trial license is valid
        /// </summary>
        E_LHR_LicenseOK_Demo = 0x254,

        /// <summary>
        /// OEM license is valid
        /// </summary>
        E_LHR_LicenseOK_OEM = 0x255,

        //errors

        /// <summary>
        /// Missing license
        /// </summary>
        E_LHR_LicenseNoFound = (0x98110700 + 0x24),

        /// <summary>
        /// License expired
        /// </summary>
        E_LHR_LicenseExpired = (0x98110700 + 0x25),

        /// <summary>
        /// License has too few instances
        /// </summary>
        E_LHR_LicenseExceeded = (0x98110700 + 0x26),

        /// <summary>
        /// License is invalid
        /// </summary>
        E_LHR_LicenseInvalid = (0x98110700 + 0x27),

        /// <summary>
        /// Incorrect system ID for the license
        /// </summary>
        E_LHR_LicenseSystemIdInvalid = (0x98110700 + 0x28),

        /// <summary>
        /// License not limited in time
        /// </summary>
        E_LHR_LicenseNoTimeLimit = (0x98110700 + 0x29),

        /// <summary>
        /// License problem: Time of issue is in the future
        /// </summary>
        E_LHR_LicenseTimeInFuture = (0x98110700 + 0x2A),

        /// <summary>
        /// License period too long
        /// </summary>
        E_LHR_LicenseTimePeriodToLong = (0x98110700 + 0x2B),

        /// <summary>
        /// Exception at system startup
        /// </summary>
        E_LHR_DeviceException = (0x98110700 + 0x2C),

        /// <summary>
        /// License data read multiple times
        /// </summary>
        E_LHR_LicenseDuplicated = (0x98110700 + 0x2D),

        /// <summary>
        /// Invalid signature
        /// </summary>
        E_LHR_SignatureInvalid = (0x98110700 + 0x2E),

        /// <summary>
        /// Certificate is invalid
        /// </summary>
        E_LHR_CertificateInvalid = (0x98110700 + 0x2F),

        /// <summary>
        /// OEM license for unknown OEM
        /// </summary>
        E_LHR_LicenseOemNotFound = (0x98110700 + 0x30),

        /// <summary>
        /// License invalid for the system
        /// </summary>
        E_LHR_LicenseRestricted = (0x98110700 + 0x31),

        /// <summary>
        /// Trial license not allowed
        /// </summary>
        E_LHR_LicenseDemoDenied = (0x98110700 + 0x32),

        /// <summary>
        /// Invalid platform level for the license
        /// </summary>
        E_LHR_LicensePlatformLevelInv = (0x98110700 + 0x33)
    }

    /// <summary>
    /// The FileOpenModeFlags enum defines various modes for opening files, each represented by a unique flag. These flags can be combined using bitwise operations to specify multiple modes simultaneously.
    /// </summary>
    public enum FileOpenModeFlags
    {
        /// <summary>
        /// "r": Opens for reading. If the file does not exist or cannot be found, the call fails. 
        /// </summary>
        FOPEN_MODEREAD = 0x1,

        /// <summary>
        /// "w": Opens an empty file for writing. If the given file exists, its contents are destroyed.
        /// </summary>
        FOPEN_MODEWRITE = 0x2,

        /// <summary>
        /// "a": Opens for writing at the end of the file (appending) without removing the EOF marker before writing new data to the file; creates the file first if it doesnot exist.
        /// </summary>
        FOPEN_MODEAPPEND = 0x4,

        /// <summary>
        /// "+": Opens for both reading and writing. (The file must exist.)
        /// </summary>
        FOPEN_MODEPLUS = 0x8,

        /// <summary>
        /// "b": Open in binary (untranslated) mode.
        /// </summary>
        FOPEN_MODEBINARY = 0x10,

        /// <summary>
        /// "t": Open in text (translated) mode.
        /// </summary>
        FOPEN_MODETEXT = 0x20,
    }

    /// <summary>
    /// Control parameter for enumeration blocks. Not all parameters are used by each enumeration block!
    /// </summary>
    public enum E_EnumCmdType
    {
        /// <summary>
        /// Lists the first element
        /// </summary>
        First = 0,

        /// <summary>
        /// Lists the next element
        /// </summary>
        Next,

        /// <summary>
        /// Cancels the listing (closes open handles)
        /// </summary>
        Abort
    }
}
