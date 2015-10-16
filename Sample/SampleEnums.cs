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
        Neutral = 0,
        Happy = 1,
        Shy = 2,
        Tall = 4,
        Fat = 8
    }

    public enum Level
    {
        Easy,
        Normal,
        Hard
    }
}
