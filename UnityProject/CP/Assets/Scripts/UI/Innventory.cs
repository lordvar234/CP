using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Innventory : MonoBehaviour
{
    public DataBase data;

    public PlayerStats stat;

    public GameObject Slot, BigInventory, MainInventory;
    public List<ItemInventory> items = new List<ItemInventory>();

    public int FirstFreeSlot;   

    public Camera Cam;
    public EventSystem es;

    public int currentID;
    public ItemInventory currentItem;

    public RectTransform movingObject;
    public Vector3 offset;
    public void Start()
    {
        AddGraphickInventory();
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            OpenInventory();
        }
        if (currentID != -1)
        {
            MoveObject();
        }
    }
    public void OpenInventory()
    {

        BigInventory.SetActive(!BigInventory.activeSelf);
    }
    public void AddGraphickInventory()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject newItem = Instantiate(Slot, MainInventory.transform) as GameObject;

            newItem.name = i.ToString();

            ItemInventory ii = new ItemInventory();
            ii.itemGameObj = newItem;

            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button Button = newItem.GetComponent<Button>();

            Button.onClick.AddListener(delegate { SelectedObject(); });

            items.Add(ii);
        }
        for (int i = 8; i < 36; i++)
        {
            GameObject newItem = Instantiate(Slot, BigInventory.transform) as GameObject;

            newItem.name = i.ToString();

            ItemInventory ii = new ItemInventory();
            ii.itemGameObj = newItem;

            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectedObject(); });

            items.Add(ii);
        }
    }
    public void AddItem(int id, ItemsList item)
    {
        items[id].id = item.id;
        items[id].itemGameObj.GetComponent<Image>().sprite = item.img;

        StatUp(id);
        FindFirstFreeSlot();
    }
    public void AddInventoryItem(int id, ItemInventory invItem)
    {
        items[id].id = invItem.id;
        items[id].itemGameObj.GetComponent<Image>().sprite = data.item[invItem.id].img;

        StatUp(id);
        FindFirstFreeSlot();
    }
    public void StatUp(int slot)
    {
        if (slot < 8)
        {
            stat.DamageItemBonus = 0;
            stat.MaxHealsItemBonus = 0;
            stat.MaxManaItemBonus = 0;
            stat.ManaRegenerationItemBonus = 0;
            stat.MoveSpeedItemBonus = 0;
            stat.AttackSpeedItemBonus = 0;
            stat.ArmorItemBonus = 0;
            stat.HealsRegenerationItemBonus = 0;

            for (int i = 0; i < 8; i++)
            {
                stat.DamageItemBonus += data.item[items[i].id].Damage;
                stat.MaxHealsItemBonus += data.item[items[i].id].MaxHeals;
                stat.MaxManaItemBonus += data.item[items[i].id].MaxMana;
                stat.ManaRegenerationItemBonus += data.item[items[i].id].ManaRegeneration;
                stat.MoveSpeedItemBonus += data.item[items[i].id].MoveSpeed;
                stat.AttackSpeedItemBonus += data.item[items[i].id].AttackSpeed;
                stat.ArmorItemBonus += data.item[items[i].id].Armor;
                stat.HealsRegenerationItemBonus += data.item[items[i].id].HealsRegeneration;
            }
            stat.StatsUpdate();
        }
    }
    private void FindFirstFreeSlot()
    {
        if (FirstFreeSlot < 38)
        {
            bool fFree = false;
            for (int i = 0 ; fFree == false; i++)
            {
                if (items[i].id == 0)
                {
                    FirstFreeSlot = i;
                    fFree = true;
                }
            }
        }
    }
    public void TakeItem(int Itemid)
    {
        AddItem(FirstFreeSlot, data.item[Itemid]);
    }
    public void RemoveItem(int Itemid)
    {
        AddItem(Itemid, data.item[0]);
    }
    public void SelectedObject()
    {
        if (currentID == -1)
        {
            currentID = int.Parse(es.currentSelectedGameObject.name);
            currentItem = CopyInventoryItem(items[currentID]);

            movingObject.gameObject.SetActive(true);
            movingObject.GetComponent<Image>().sprite = data.item[currentItem.id].img;

            if (currentItem.id > 0)
            {
                AddItem(currentID, data.item[0]);
            }
        }
        else
        {
            AddInventoryItem(currentID, items[int.Parse(es.currentSelectedGameObject.name)]);

            AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
            currentID = -1;

            movingObject.gameObject.SetActive(false);
        }
    }
    public void MoveObject()
    {
        Vector3 pos = Input.mousePosition + offset;
        movingObject.position = Cam.ScreenToWorldPoint(pos);
        movingObject.position = new Vector3(movingObject.position.x, movingObject.position.y, -1);
    }
    public ItemInventory CopyInventoryItem(ItemInventory old)
    {
        ItemInventory New = new ItemInventory();

        New.id = old.id;
        New.itemGameObj = old.itemGameObj;
        return New;
    }
}
[System.Serializable]
public class ItemInventory
{
    public int id;
    public GameObject itemGameObj;
}