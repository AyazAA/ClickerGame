using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        [Range(1,100)]
        public int StartHp = 10;
        
        [Range(1,100)]
        public int DeltaNextLevelHp = 10;
        
        [Range(1,30)]
        public float ReceivedDamage = 10;
        
        public GameObject Prefab;
    }
}