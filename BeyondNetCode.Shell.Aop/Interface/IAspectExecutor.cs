namespace BeyondNetCode.Shell.Aop
{
    public interface IAspectExecutor
    {
        void Execute(IJoinPoint joinPoint);
    }
}
