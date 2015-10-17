using System;
using System.ComponentModel;

namespace DwachWPF.Sample
{
    [Flags]
    public enum FirstEnum
    {
        [Description("First flag")]
        FirstFlag = 1,
        [Description("Second flag")]
        SecondFlag = 2,
        [Description("Third flag")]
        ThirdFlag = 4,
        [Description("Fourth flag")]
        FourthFlag = 8,
        [Description("Fifth flag")]
        FifthFlag = 16,
        [Description("Sixth flag")]
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
