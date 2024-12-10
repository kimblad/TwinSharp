namespace TwinSharp
{
    /// <summary>
    /// Beckhoff does not describe all of its existing error codes in its enum in ADS Abstractions.
    /// So we "extend" it here.
    /// Mostly error codes recieved from MDP/IPC is missing.
    /// More info: https://infosys.beckhoff.com/english.php?content=../content/1033/tcplclib_tc2_mdp/178768395.html
    /// </summary>
    public enum ExtendedAdsErrorCodes : uint
    {
        /// <summary> Operation not supported. </summary>
        NotSupported = 0xECA61000,

    }
}
