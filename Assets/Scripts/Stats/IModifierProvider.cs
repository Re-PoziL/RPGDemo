using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IModifierProvider
    {
        IEnumerable<float> GetAdditiveModifiers(Stat currentStat);
        IEnumerable<float> GetPercentageModifiers(Stat currentStat);
    }
}