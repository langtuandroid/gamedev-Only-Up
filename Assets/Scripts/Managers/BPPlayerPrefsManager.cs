using UnityEngine;

namespace Scripts.Gameplay.Managers
{
    public static class BPPlayerPrefsManager
    {
        static string COINS_KEY = "COINS_KEY";
        static string ITEM_UNLOCKED_KEY = "ITEM_UNLOCKED_KEY";
        static string ITEM_SELECTED_KEY = "ITEM_SELECTED_KEY";
        static string SOUND_KEY = "SOUND_KEY";
        static string LEVEL_KEY = "LEVEL";
        
        public static int GetCoins()
        { return PlayerPrefs.GetInt(COINS_KEY); }

        public static void SaveCoins(int coinsAmount)
        { PlayerPrefs.SetInt(COINS_KEY, coinsAmount); }

        public static bool GetItemUnlockedState(int itemIndex)
        { return PlayerPrefs.GetInt(ITEM_UNLOCKED_KEY + itemIndex, 0) == 1; }

        public static void SetItemUnlockedState(int itemIndex)
        { PlayerPrefs.SetInt(ITEM_UNLOCKED_KEY + itemIndex, 1); }

        public static int GetSoundState()
        { return PlayerPrefs.GetInt(SOUND_KEY, 1); }

        public static void SetSoundState(int state)
        { PlayerPrefs.SetInt(SOUND_KEY, state); }
        
        public static int GetLevel()
        { return PlayerPrefs.GetInt(LEVEL_KEY); }

        public static void SaveLevel(int level)
        { PlayerPrefs.SetInt(LEVEL_KEY, level); }

        public static bool GetItemSelectedState(int itemType, int buttonIndex)
        { return PlayerPrefs.GetInt(ITEM_SELECTED_KEY + itemType + buttonIndex, 0) == 1; }

        public static void SetItemSelectedState(int itemType, int buttonIndex)
        { PlayerPrefs.SetInt(ITEM_SELECTED_KEY + itemType + buttonIndex, 1); }

        public static void SetItemUnSelectedState(int itemType, int buttonIndex)
        { PlayerPrefs.SetInt(ITEM_SELECTED_KEY + itemType + buttonIndex, 0); }
    }
}
