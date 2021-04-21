
using UnityEngine;



public enum WeaponType
{ 
    none,
    blaster,
    doubleBlaster,
    laser,
    booster,
    HP,
    timer,
    coin
}

[System.Serializable]
public class WeaponDefinition
{
    public WeaponType type = WeaponType.none;
    public GameObject firePrefab;
    public float damageOnHit = 0;
    public float laserDamage = 0; //урон в секунду
    public float delayBetweenShots = 0;
    public float velocity = 70;
    public  float lifeTime = 0;//время жизни оружия
    public float timeDelete = 0;//время жизни оружия
   
}

public class Weapon : MonoBehaviour
{
    static public Transform PROJECTILE_ANCHOR;

    [Header("Set Dynamically")] [SerializeField]
    private WeaponType _type = WeaponType.none;
    public WeaponDefinition def;
    public float lastShotTime;  //время последнего выстрела
    


    void Start()
    {
        SetType(_type);
        if (PROJECTILE_ANCHOR == null)
        {
            GameObject go = new GameObject("_ProjectileAnchor");
            PROJECTILE_ANCHOR = go.transform;
        }
        GameObject rootGO = transform.root.gameObject;
        if (rootGO.GetComponent<Hero>() != null)
        {
            rootGO.GetComponent<Hero>().fireDelegate += Fire;
        }
    }

    void Update()
    {
        TimeOfLife(def.lifeTime);
    }

    public WeaponType type
    {
        get { return (_type); }
        set { SetType(value); }
    }

    public void SetType(WeaponType wt)
    {
        _type = wt;
        if (type == WeaponType.none)
        {
            this.gameObject.SetActive(false);
            return;
        }
        else {
            this.gameObject.SetActive(true);
            def.timeDelete = Time.time + def.lifeTime;//присваивается время жизни оружию после его установки
        }
        def = Main.GetWeaponDefinition(_type);
        lastShotTime = 0;
    }

    public void Fire()
    {
        if (!gameObject.activeInHierarchy) return;
        if (Time.time - lastShotTime < def.delayBetweenShots)
        {
            return;
        }
        FireHero p; 
        Vector3 vel = Vector3.up * def.velocity;
        
        switch (type)
        {
            case WeaponType.blaster:
                p = MakeFireHero();
                p.rigid.velocity = vel;
                SoundEffects.PlaySound("blaster");
                break;
            case WeaponType.doubleBlaster: 
                p = MakeFireHero();
                p.transform.position = transform.position + Vector3.right;
                p.rigid.velocity = vel;
                p = MakeFireHero();
                p.transform.position = transform.position + Vector3.left;
                p.rigid.velocity = vel;
                SoundEffects.PlaySound("DoubleBlaster1");
                break;
            case WeaponType.laser:
                p = MakeFireHero();
                SoundEffects.PlaySound("laser3");                
                break;
        }
    }

    public FireHero MakeFireHero()
    {
        GameObject go = Instantiate<GameObject>(def.firePrefab);
        if (transform.parent.gameObject.tag == "Hero")
        {
            go.tag = "FireHero";
            go.layer = LayerMask.NameToLayer("FireHero");
        }
        else {
            go.tag = "FireEnemy";
            go.layer = LayerMask.NameToLayer("Fireenemy");
        }
        go.transform.position = transform.position;
        go.transform.SetParent(PROJECTILE_ANCHOR, true);
        FireHero p = go.GetComponent<FireHero>();
        p.type = type;
        lastShotTime = Time.time;
        return (p);
    }

    public void TimeOfLife(float lifeTime)//Время действия оружия
    {
        if(Time.time > def.timeDelete)
        {
            Hero.S.ClearWeapons();
            Hero.S.weapons[0].SetType(WeaponType.blaster);
        }
    }

    
   
}
