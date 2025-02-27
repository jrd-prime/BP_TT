using Code.Core.Data.Constants;
using UnityEngine;

namespace Code.Core.Data.SO
{
    [CreateAssetMenu(fileName = "MainSettings", menuName = SOPathConst.ConfigPath + "Main Settings", order = 100)]
    public class MainSettings : SettingsBase
    {
        // public CharacterSettings character;

        private void OnValidate()
        {
        }
    }
}
