using System;

namespace DwachWPF.Sample
{
    [Flags]
    public enum FirstEnum
    {
        FirstFlag,
        SecondFlag,
        ThirdFlag,
        FourthFlag,
        FifthFlag,
        SixthFlag,
    }

    [Flags]
    public enum SecondEnum
    {
        Happy,
        Shy,
        Tall,
        Fat
    }
}
