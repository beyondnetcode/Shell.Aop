using System;

namespace BeyondNetCode.Shell.Aop
{
    public interface IPointCut
    {
        bool CanApply(IJoinPoint joinPoint, Type type);
    }
}
