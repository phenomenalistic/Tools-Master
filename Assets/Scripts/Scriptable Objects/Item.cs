using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Форма для хранения значений item'ов store'a
*/
[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public string itemName = "any name"; // название игрового предмета
    public string itemCode = "any itemCode"; // тип и серийный номер предмета. Пример: 1
    //[Space(20)]
    //public TypeObject typeObject; // = new string[] { "ring", "emitter", "theme" } тип игрового предмета

    public enum TypeObject
    {
        [InspectorName("Character")] character,
        [InspectorName("Cube")] cube,
        [InspectorName("Theme")] theme,
    }


    public bool hidden = false; // отображается ли предмет до покупки (чтобы можно было сделать скрытые предметы, которые можно только выйграть или получить по коду)

    [Space(10)]
    public Currencies currencie;  // = new string[] { "money", "ad" } валюта за которую продается этот элемент

    public enum Currencies
    {
        [InspectorName("Money")] money,
        [InspectorName("Ad")] ad,
    }

    public int cost = 1000; // цена игового элемента

    [Space(20)]
    public Sprite sprite; // изображение товара

    public List<Material> materials; // изображение товара

    public Color selectColor;

    public GameObject gameObject;

    //public List<Mesh> meshes;

    //public List<ParticleSystem> particleSystems;

    int materialPointer = 0;
    public Material GetMaterial()
    {
        materialPointer = GetNextPointer(materialPointer, materials);
        return materials[materialPointer];
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    /*
    public List<Mesh> GetMesh()
    {
        return meshes;
    }
    */

    public Sprite GetSprite()
    {
        return sprite;
    }

    /*
    int particleSystemPointer = 0;
    public ParticleSystem GetParticleSystem()
    {
        particleSystemPointer = GetNextPointer(particleSystemPointer, particleSystems);
        return particleSystems[particleSystemPointer];
    }
    */
    int GetNextPointer<T>(int pointer, List<T> list)
    {
        pointer++;
        if (pointer > list.Count-1) { pointer = 0; }
        return pointer;
    }
}
