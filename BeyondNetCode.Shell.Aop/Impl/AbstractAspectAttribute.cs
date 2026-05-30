using System;

namespace BeyondNetCode.Shell.Aop
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AbstractAspectAttribute : Attribute
    {
        public int Order { get; set; }
    }
}