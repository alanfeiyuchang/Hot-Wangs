using MoonSharp.Interpreter.Interop.BasicDescriptors;
using System;

namespace MoonSharp.Interpreter.Interop.StandardDescriptors.HardwiredDescriptors
{
    public abstract class HardwiredUserDataDescriptor : DispatchingUserDataDescriptor
    {
        protected HardwiredUserDataDescriptor(Type T) :
            base(T, "::hardwired::" + T.Name)
        {

        }

    }
}
