using System;
using System.Reflection;

namespace BeyondNetCode.Shell.Aop
{
    public interface IJoinPoint
    {
        object[] Arguments { get; set; }

        MethodInfo MethodInfo { get; }

        object Return { get; set; }

        object TargetObject { get; }

        Type TargetType { get; }

        void Proceed();
    }
}
