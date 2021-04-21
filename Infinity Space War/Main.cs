
using System.Collections.Generic;
using UnityEngine;


public class Main : MonoBehaviour
{

    static public Main S;
    static Dictionary<WeaponType, WeaponDefinition> WEAP_DICT;


    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies;
    public float enemySpawn = 0.5f; //спаун в сек
    public float enemyDefPadding = 1.5f; //отступ
    public WeaponDefinition[] weaponDefinitions; //массив оружия и бонусов. 
    public WeaponType[] fireFrequency = new WeaponType[]
        { WeaponType.blaster, WeaponType.booster, WeaponType.doubleBlaster , WeaponType.HP,
         WeaponType.laser, WeaponType.timer};

  

    private BoundsCheck bndCheck;

    void Awake()
    {
        S = this;
        bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", 1f / enemySpawn); //спаун раз в 2 секунды.

        WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>();
        foreach (WeaponDefinition def in weaponDefinitions)
        {
            WEAP_DICT[def.type] = def;
        }
        Time.timeScale = 1; // исправляет баг при котором выход в меню и снова нажатие кнопки старт не работает.
        
    }

    public void SpawnEnemy()
    {
        int rand = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[rand]);

        float enemyPadding = enemyDefPadding;
        if (go.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        //начальные случайные координаты врага
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        go.transform.position = pos;

        Invoke("SpawnEnemy", 1f / enemySpawn);

    }

    public void DelayedRestart(float delay)
    {
        Invoke("Restart", delay);
    }


    static public WeaponDefinition GetWeaponDefinition(WeaponType wt)
    {
        if (WEAP_DICT.ContainsKey(wt))
        {
            return(WEAP_DICT[wt]);
        }
        return (new WeaponDefinition());
    }
}
