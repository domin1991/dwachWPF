using System;

namespace DwachWPF.Sample
{
    [Flags]
    public enum FirstEnum
    {
        FirstFlag = 1,
        SecondFlag = 2,
        ThirdFlag = 4,
        FourthFlag = 8,
        FifthFlag = 16,
        SixthFlag = 32,
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
