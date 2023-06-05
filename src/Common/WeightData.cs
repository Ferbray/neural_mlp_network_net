using System.Collections.Generic;

namespace src.Common
{
    public static class WeightData
    {
        public static Dictionary<string, double> Datas = new () {
            { "отдаленные районы", 0 }, { "центр", 1 },
            { "панельный", 0 }, { "кирпичный", 1 }, { "шлакоблочный", 2 },
            { "евроремонт", 0 }, { "косметический", 1 }, { "требует ремонта", 2 },
        };
    }
}
