using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
    public Shop Instance;

    public int coinsBalance = 150;

    [System.Serializable]
    class ShopItem
    {
        public string itemName;
        public int price;
        public bool isPurchased = false;
        public bool isActivatedInInventory = false;
        public Button button;
        public TextMeshProUGUI purchasedText;
        public GameObject purchasedPanel;
        public Sprite itemImage;
        public GameObject inventoryPrefab;
      
    }

    [SerializeField] private List<ShopItem> shopItemList;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI coinsTextinventory;
    [SerializeField] private GameObject inventoryPanel;


    private void Start()
    {
        LoadPurchasedItems();
        LoadCoinsBalance();
        LoadInventoryItemsActivation();

        UpdateCoinsUI();
        SetupButtons();

    }

    private void SetupButtons()
    {
        foreach (var item in shopItemList)
        {
            if (item.button != null && item.isPurchased)
            {
                item.button.interactable = false;
                item.purchasedText.gameObject.SetActive(true);
                item.purchasedPanel.SetActive(true);
                AddItemToInventory(item);
            }
        }
    }

    public void BuyItem(int itemIndex)
    {
        if (itemIndex < 0 || itemIndex >= shopItemList.Count)
        {
            return;
        }

        ShopItem item = shopItemList[itemIndex];
        if (item.isPurchased)
        {
            return;
        }

        if (coinsBalance >= item.price)
        {
            coinsBalance -= item.price;
            item.isPurchased = true;

            if (item.button != null)
            {
                SavePurchasedItems();
                SaveCoinsBalance();

                UpdateCoinsUI();
                item.button.interactable = false;
                item.purchasedText.gameObject.SetActive(true);
                item.purchasedPanel.SetActive(true);
                AddItemToInventory(item);
            }
        }
        
    }

    public void UpdateCoinsUI()
    {
        if(coinsText != null)
        {
          coinsText.text = coinsBalance.ToString();
          coinsTextinventory.text = coinsBalance.ToString();

        }
    }

    private void AddItemToInventory(ShopItem item)
    {
        item.inventoryPrefab.SetActive(true);
        item.isActivatedInInventory = true;

        PlayerPrefs.SetInt("IsActivated_" + item.itemName, item.isActivatedInInventory ? 1 : 0);
        PlayerPrefs.Save();
    }
    private void LoadInventoryItemsActivation()
    {
        foreach (var item in shopItemList)
        {
            int activationState = PlayerPrefs.GetInt("ItemActivation_" + item.itemName, 0);
            bool isActivated = activationState == 1;
            item.inventoryPrefab.SetActive(isActivated);
        }
    }


    private void SavePurchasedItems()
    {
        for (int i = 0; i < shopItemList.Count; i++)
        {
            PlayerPrefs.SetInt("IsPurchased_" + i, shopItemList[i].isPurchased ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    private void LoadPurchasedItems()
    {
        for (int i = 0; i < shopItemList.Count; i++)
        {
            int isPurchased = PlayerPrefs.GetInt("IsPurchased_" + i, 0);
            shopItemList[i].isPurchased = isPurchased == 1;
        }
    }

    public void SaveCoinsBalance()
    {
        PlayerPrefs.SetInt("CoinsBalance", coinsBalance);
        PlayerPrefs.Save();
    }

    private void LoadCoinsBalance()
    {
        if (PlayerPrefs.HasKey("CoinsBalance"))
        {
            coinsBalance = PlayerPrefs.GetInt("CoinsBalance");
        }
        else
        {
            coinsBalance = 150;
        }
    }
}
