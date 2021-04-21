
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Hero : MonoBehaviour
{

    static public Hero S;
    static Shop shop;
    private Rigidbody rb;


    [Header("Set in Inspector")]
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float restartGameDelay = 2f;
    public GameObject fireHeroPrefab;
    public float fireHeroSpeed = 40;
    public Weapon[] weapons;
    public RawImage[] hearts;
    public Texture heartF;
    public Texture heartE;
    public GameObject GameOverPanel;

    [Header("Set Dynamically")]
    [SerializeField]
    public float _HP = 3;

    private GameObject lastTriggerGo = null;

    //объявление нового делегата типа WeaponFireDelegate
    public delegate void WeaponFireDelegate();
    //Создать поле типа WeaponFireDelegate с именем fireDelegate
    public WeaponFireDelegate fireDelegate;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //при старте игры установка бластера.
        S = this;
        ClearWeapons();
        weapons[0].SetType(WeaponType.blaster);   
    }
    

    void Update()
    {       
        float xAxis = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        
        transform.position = pos;

        transform.rotation = Quaternion.Euler(-90, xAxis * rollMult, 0);

        if (fireDelegate != null) //Выстрелы
        {
            fireDelegate();
        }

    }

    void TempFire()
    {
        GameObject projGO = Instantiate<GameObject>(fireHeroPrefab);
        projGO.transform.position = transform.position;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        FireHero proj = projGO.GetComponent<FireHero>();
        proj.type = WeaponType.blaster;
        float tSpeed = Main.GetWeaponDefinition(proj.type).velocity;
        rigidB.velocity = Vector3.up * tSpeed;
    }


    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        if (go == lastTriggerGo)
        {
            return;
        }
        lastTriggerGo = go;

        if (go.layer == 9)
        {
            HP--;
            Destroy(go);
            SoundEffects.PlaySound("explosion");
            GameDataManager.AddCoins(10); //даётся за раз
            GameSharedUI.Instance.UpdateCoinsUIText();//обновляет текст
        }

        if (go.layer == 11)
        {
            HP--;
            Destroy(go);
        }
        //сердечки
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < HP)
            {
                hearts[i].texture = heartF;
            }
            else
            {
                hearts[i].texture = heartE;
            }
        }
    }

    public float HP
    {       
        get {
            return (_HP);
        }
        set {
            _HP = Mathf.Min(value, 3);
            if (value  < 1)
            {
                Destroy(this.gameObject);
                GameOverPanel.SetActive(true); //вызывает панель Game Over
                Time.timeScale = 0; //стоп игра после смерти и должна появиться заставка с перезапуском
                
            }
        }      
    }

    public void ClearWeapons() 
    {
        foreach (Weapon w in weapons)
        {
            w.SetType(WeaponType.none);
        }
    }

    public void UpdateHP()//обновляет колличество сердец
    {
        for(int i = 0; i<hearts.Length; i++)
        {
            if(i < HP)
            {
                hearts[i].texture = heartF;
            }
        }
    }
}
