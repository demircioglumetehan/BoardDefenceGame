using System;
using System.Collections.Generic;
using BoardDefenceGame.UI;

namespace BoardDefenceGame.Events
{
    public static class GameEventsUI
    {
        public static Action<List<LevelBasedDefenceItem>> OnRemainingDefenceItemsSet;
        public static Action<DefenceItemFeatureScriptableObject> OnSpawnDefenceButtonPressed;
        public static Action<DefenceItemFeatureScriptableObject, SpawnDefenceItemButton> OnDefenceItemStartedToDrag;
        public static Action OnStartBattleButtonPressed;
        public static Action<int> OnPlayerHealthInitialized;
        public static Action<int> OnPlayerHealthChanged;
    }
}
