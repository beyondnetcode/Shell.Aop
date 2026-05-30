using System;

namespace BeyondNetCode.Shell.Aop.Aspects
{
    public interface IFactory<T>
    {
        T Create(Type type);
    }
}
