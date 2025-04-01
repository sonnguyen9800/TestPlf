using UnityCommunity.UnitySingleton;

namespace _Custom
{
    public class ConfigManager : MonoSingleton<ConfigManager>
    {
        public int GetCoinPoint()
        {
            return 50;
        }
    }
}