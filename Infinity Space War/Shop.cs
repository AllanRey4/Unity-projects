
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Shop : MonoBehaviour
{
    static Score score;
    public Weapon[] weapons;
    public static Weapon weapon;
    static Hero hero;
    public Enemy enemy;
    
    [System.Serializable]class shopItem
    {
        public Texture image;
        public int price;
        public WeaponType type;
        public bool isPurchased = false;
        public float cooldownTime = 0;//перезарядка предметов в магазе
        public float nextTimeShop = 0;//перезарядка предметов в магазе 
    }

    [SerializeField] List<shopItem> shopItemsList;
    [SerializeField] Text coinsText;
    [SerializeField] Animator NoCoins;

    GameObject itemTemplate;
    GameObject itemI;
    GameObject g;
    [SerializeField] Transform shopScrollView;
    Button buyBtn;

    void Start()
    {   

        enemy = GetComponent<Enemy>();
        score = GetComponent<Score>();
        itemTemplate = shopScrollView.GetChild(0).gameObject;
        
        int len = shopItemsList.Count;
        for (int i = 0; i < len; i++)
        {
            //генерация товаров в магазе 
            g = Instantiate(itemTemplate, shopScrollView);
            g.transform.GetChild(0).GetComponent<RawImage>().texture = shopItemsList[i].image;
            g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text= shopItemsList[i].price.ToString();
            buyBtn = g.transform.GetChild(1).GetComponent<Button>();
            buyBtn.interactable = !shopItemsList[i].isPurchased;
            buyBtn.AddEventListener(i, OnShopItemBtnClicked);
        }
        Destroy(itemTemplate);
    }

    void Update()
    {
        for(int i = 0; i < 3; i++) 
        {
            UpdateCooldown(i);
        }       
    }

    public void OnShopItemBtnClicked(int itemIndex)
    {

        if (GameDataManager.CanSpendCoins(shopItemsList[itemIndex].price))
        {
            //покупка предмета
            shopItemsList[itemIndex].isPurchased = true;
            GameDataManager.SpendCoins(shopItemsList[itemIndex].price);
            GameSharedUI.Instance.UpdateCoinsUIText(); //обновить колличество денег после покупки           
            Store(itemIndex);//привязка лота к оружию и бонусам
            //отключить кнопку
            shopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Button>().interactable = false;
            
        }
        else {
            NoCoins.SetTrigger("NoCoins");
            Debug.Log("У Вас недостаточно средств");
        }      
    }

    public void Store(int itemIndex) //привязка лота к оружию и бонусам
    {        
        switch(itemIndex)
           {
               case 0: // установка doubleBlaster
               Hero.S.ClearWeapons();
               weapons[0].SetType(WeaponType.doubleBlaster);
               break;
               //case 1: // установка laser
               //Hero.S.ClearWeapons();
               //weapons[1].SetType(WeaponType.laser);
               //break;
               case 1:
               Enemy.pointsPerEnemy = 100; //Boost умножение score * 2
               break;
               case 2: //HP покупка хп
               if(Hero.S.HP < 3)
               {
                   Hero.S.HP++;
                   Hero.S.UpdateHP();
               }
               break;
               default: 
                Debug.Log("Ничего не куплено");
                break;
            }
    }

    public void UpdateCooldown(int itemIndex)//кулдаун на восстановление контректного лота
    {       
        if(Time.time > shopItemsList[itemIndex].nextTimeShop)
        {
            shopItemsList[itemIndex].nextTimeShop = Time.time + shopItemsList[itemIndex].cooldownTime;

            if(shopItemsList[itemIndex].isPurchased == true)
            {
                print("Кнопочка готова");
                shopItemsList[itemIndex].isPurchased = false;
                shopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Button>().interactable = true;
            }
        }  
    }
}
