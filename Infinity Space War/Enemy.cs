using UnityEngine;

public class Enemy : MonoBehaviour
{
    static public Enemy EN;
    [Header("Set Dynamically")]
    public bool notifiedOfDestruction = false;

    [Header("Set in Inspector: Enemy")]
    public float speed = 10;
    public float fireRate = 0.3f;
    public float health = 10;
    public GameObject fireEnemyPrefab;
    public float fireSpeed = 70;
    public float delayFire = 2;
    public static int pointsPerEnemy = 50;
    public GameObject exp;
    

    protected BoundsCheck bndCheck;

    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        Invoke("Fire", 0.5f);    
    }

    public Vector3 pos
    {
        get {
            return (this.transform.position);
        }
        set {
            this.transform.position = value;
        }
    }

    void Update()
    {
        Move();

        if (bndCheck != null && bndCheck.offDown)
        {
            Destroy(gameObject);   
        }
    }

   

   
    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    private void OnTriggerEnter(Collider coll)
    {
        GameObject otherGO = coll.gameObject;
        switch (otherGO.tag)
        {
            case "FireHero":
                FireHero h = otherGO.GetComponent<FireHero>();
                if (!bndCheck.isOnScreen)
                {
                    Destroy(otherGO);
                    break;
                }
                health -= Main.GetWeaponDefinition(h.type).damageOnHit;
                if (health <= 0)
                {
                    notifiedOfDestruction = true;
                    Explosion();
                    Destroy(this.gameObject);
                    SoundEffects.PlaySound("explosion");
                    GameDataManager.AddCoins(10); //даётся за раз
                    Score.ScoreBonus(pointsPerEnemy);//поинты за убийство. 
                    GameSharedUI.Instance.UpdateCoinsUIText();//обновляет текст
                }
                Destroy(otherGO);
                break;
            default:
                Explosion();//взрыв при столкновении
                Score.ScoreBonus(pointsPerEnemy);
                break;
        }
    }

    //снаряды врага
    public virtual void Fire()
    {
        GameObject fireEn = Instantiate<GameObject>(fireEnemyPrefab);
        fireEn.transform.position = transform.position;
        Rigidbody rigidB = fireEn.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.down * fireSpeed;
        
        Invoke("Fire", delayFire);
    }

    public void Explosion()
    {
        GameObject _exp = Instantiate(exp, transform.position, Quaternion.Euler(0,0,-180)); //ВЗРЫВ
        Destroy(_exp,1.3f); 
    }
}
