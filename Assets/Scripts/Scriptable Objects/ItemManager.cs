using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Форма для хранения item'ов store'a
*/
[CreateAssetMenu(fileName = "ItemManager", menuName = "ScriptableObjects/ItemManager")]
public class ItemManager : ScriptableObject
{
    public List<Item> characters = new List<Item>();
    public List<Item> emitters = new List<Item>();
    public List<Item> holders = new List<Item>();
    public List<Item> themes = new List<Item>();


    public Item GetItem(string itemCode)
    {
        if (itemCode.StartsWith("character"))
        {
            return FindItem(itemCode, characters);
        }
        else if (itemCode.StartsWith("emitter"))
        {
            return FindItem(itemCode, emitters);
        }
        else if (itemCode.StartsWith("holder"))
        {
            return FindItem(itemCode, holders);
        }
        else if (itemCode.StartsWith("theme"))
        {
            return FindItem(itemCode, themes);
        }
        return FindItem(itemCode, characters);
    }

    public Item GetRandomItem(GameData.Valuse type)
    {
        return GetList(type)[Random.Range(0, GetList(type).Count)];
    }

    public void SetItem(GameData.Valuse type, string itemCode)
    {
        GameData.SetString(type, itemCode);
        GameData.Save();
    }

    public void SetItem(string itemCode)
    {
        GameData.SetString(DefineType(itemCode), itemCode);
        GameData.Save();
    }

    private Item FindItem(string itemCode, List<Item> itemList)
    {
        foreach (Item i in itemList)
        {
            if (i.itemCode == itemCode) { return i; }
        }
        return itemList[0];
    }

    public void AddItem(string itemCode)
    {
        List<string> list = GameData.GetListString(GameData.Valuse.purchasedItems);
        if (list.Find(x => x == itemCode) == null)
        {
            list.Add(itemCode);
            GameData.SetListString(GameData.Valuse.purchasedItems, list);
        }
    }

    public bool HasItem(string itemCode)
    {
        return GameData.GetListString(GameData.Valuse.purchasedItems).Find(x => x == itemCode) == null ? false : true;
    }

    public GameData.Valuse DefineType(string itemCode)
    {
        /*
        if (itemCode.StartsWith("character"))
        {
            return GameData.Valuse.character;
        }
        else if (itemCode.StartsWith("emitter"))
        {
            return GameData.Valuse.emitter;
        }
        else if (itemCode.StartsWith("holder"))
        {
            return GameData.Valuse.holder;
        }
        else if (itemCode.StartsWith("theme"))
        {
            return GameData.Valuse.theme;
        }
        */
        return GameData.Valuse.level;
    }

    private List<Item> GetList(GameData.Valuse type)
    {
        /*
        if (type == GameData.Valuse.character)
        {
            return characters;
        }
        else if (type == GameData.Valuse.emitter)
        {
            return emitters;
        }
        else if (type == GameData.Valuse.holder)
        {
            return holders;
        }
        else if (type == GameData.Valuse.theme)
        {
            return themes;
        }
        */
        return characters;
    }

}
