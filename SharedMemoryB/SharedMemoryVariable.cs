using System;
using System.Collections.Generic;
using System.Text;

namespace SharedMemoryB
{
    [Serializable]
    class SharedMemoryVariable
    {
        UInt32 variable_number;
        UInt32 prev_value;
        UInt32 value;
        byte variable_changed;
        byte log_variable;
        short variable_delta;
    }
}
