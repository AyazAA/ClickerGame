using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "GameData", menuName = "StaticData/Game")]
    public class GameStaticData : ScriptableObject
    {
        [Range(10,120)]
        public float StartTime = 60;

        [Range(10,120)]
        public float RewardTime = 30;
        
        [Range(1,15)]
        public int LevelsCount = 10;
    }
}