using System;
using System.Linq;
using System.Runtime.Serialization;

namespace CodeGen
{
    public enum OutputEnum
    {
        [EnumMember(Value = "OutputDir")]
        OutputDir,

        [EnumMember(Value = "ProjectDir")]
        ProjectDir

    }
}
