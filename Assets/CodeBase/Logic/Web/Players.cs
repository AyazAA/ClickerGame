using System;
using System.Collections.Generic;

namespace CodeBase.Logic.Web
{
    [Serializable]
    public class Players
    {
        public List<PlayerStat> PlayerStats;
    }

    [Serializable]
    public class PlayerStat
    {
        public string name;
        public int score;
    }
}