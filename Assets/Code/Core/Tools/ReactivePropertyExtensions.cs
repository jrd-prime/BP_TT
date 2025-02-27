using R3;

namespace Code.Core.Tools
{
    public static class ReactivePropertyExtensions
    {
        public static void NotifyIfDataIsClass<T>(this ReactiveProperty<T> property)
        {
            if (typeof(T).IsClass) property.ForceNotify();
        }
    }
}
