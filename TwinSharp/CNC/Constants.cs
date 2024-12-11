namespace TwinSharp.CNC
{
    /// <summary>
    /// Constants used in the TwinCAT CNC system.
    /// </summary>
    public static class Constants
    {
        /// <summary> Maximum count of structure when V0 of visualization data is used. </summary>
        public const int MAX_SOLLKONT_VISU_DATA_COUNT_V0 = 15;

        /// <summary> Maximum count of structure when V1 of visualization data is used. </summary>
        public const int MAX_SOLLKONT_VISU_DATA_COUNT_V1 = 10;

        /// <summary> Maximum count of structure when V2 of visualization data is used. </summary>
        public const int MAX_SOLLKONT_VISU_DATA_COUNT_V2 = 5;

        /// <summary> Maximum count of structure when V3 of visualization data is used. </summary>
        public const int MAX_SOLLKONT_VISU_DATA_COUNT_V3 = 10;

        /// <summary> Maximum count of structure when V4 of visualization data is used. </summary>
        public const int MAX_SOLLKONT_VISU_DATA_COUNT_V4 = 7;

        /// <summary> Maximum count of structure when V5 of visualization data is used. </summary>
        public const int MAX_SOLLKONT_VISU_DATA_COUNT_V5 = 4;

        /// <summary> Maximum count of structure when V6 of visualization data is used. </summary>
        public const int MAX_SOLLKONT_VISU_DATA_COUNT_V6 = 7;

        /// <summary> Maximum count of structure when V7 of visualization data is used. </summary>
        public const int MAX_SOLLKONT_VISU_DATA_COUNT_V7 = 6;

        /// <summary> Maximum count of structure when V8 of visualization data is used. </summary>
        public const int MAX_SOLLKONT_VISU_DATA_COUNT_V8 = 4;

        /// <summary> Maximum count of structure when V9 of visualization data is used. </summary>
        public const int MAX_SOLLKONT_VISU_DATA_COUNT_V9 = 5;

        /// <summary> Maximum count of structure when V10 of visualization data is used. </summary>
        public const int MAX_SOLLKONT_VISU_DATA_COUNT_V10 = 4;

        /// <summary> Maximum count of structure when V11 of visualization data is used. </summary>
        public const int MAX_SOLLKONT_VISU_DATA_COUNT_V11 = 3;

        /// <summary> Number of axis to get coordinates for in graphic generation. </summary>
        public const int ANZ_SIMU_KOORD = 32;


        /// <summary> Maximum length of string of type module name. </summary>
        public const int HLI_MODUL_NAME_LAENGE = 15;

        /// <summary> Maximum index of HLI error mask. </summary>
        public const int HLI_ERR_MASK_MAXIDX = 583;

        /// <summary> Maximum index of HLI error value B data. </summary>
        public const int HLI_WERT_B_DATA_MAXIDX = 23;

        /// <summary> Maximum index of HLI error value  data. </summary>
        public const int HLI_WERT_DATA_MAXIDX = 7;

        /// <summary> Maximum string length of HLI objects. </summary>
        public const int HLI_LAENGE_NAME = 259;

        /// <summary> Maximum count of number of keys for manual jogging. </summary>
        public const short HLI_KEY_MAXIDX = 11;

        /// <summary> Maximum count of number of hand wheels for manual jogging. </summary>
        public const short HLI_HW_MAXIDX = 5;
    }
}
