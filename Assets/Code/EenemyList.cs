using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class EnemyList
    {
        private static Dictionary<string, EnemyInfo> _enemyList = new Dictionary<string, EnemyInfo>
        {
            { "ミュータント", new EnemyInfo("ミュータント", 38000, 20000, Resources.Load<GameObject>("Prefabs/Monster/Mutant")) }
        };

        public static EnemyInfo GetEnemy(string name)
        {
            return _enemyList[name];
        }
    }


    public class EnemyInfo
    {
        public EnemyInfo(string name, int exp, int hp, GameObject model)
        {
            Name = name;
            Exp = exp;
            Hp = hp;
            _model = model;
        }
        public string Name;
        public int Exp;
        public int Hp;
        private GameObject _model;

        public GameObject GetModel()
        {
            return _model;
        }
    }
    
    
}
