namespace DigitBridge.CommerceCentral.YoPoco
{
    internal static class Singleton<T> where T : new()
    {
        public static T Instance = new T();
    }
}