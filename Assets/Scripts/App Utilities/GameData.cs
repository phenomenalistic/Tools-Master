using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class GameData
{
    // This file belongs to the game company "Hands Up", it is forbidden to use it to anyone else!
    private static List<int> intKeysList = new List<int> { };
    private static List<int> intValuesList = new List<int> { };

    private static List<int> floatKeysList = new List<int> { };
    private static List<float> floatValuesList = new List<float> { };

    private static List<int> stringKeysList = new List<int> { };
    private static List<string> stringValuesList = new List<string> { };

    private static List<int> ulongKeysList = new List<int> { };
    private static List<ulong> ulongValuesList = new List<ulong> { };


    private static List<int> listIntKeysList = new List<int> { };
    private static List<List<int>> listIntValuesList = new List<List<int>> { };

    private static List<int> listFloatKeysList = new List<int> { };
    private static List<List<float>> listFloatValuesList = new List<List<float>> { };

    private static List<int> listStringKeysList = new List<int> { };
    private static List<List<string>> listStringValuesList = new List<List<string>> { };

    private static string intCode = "#016#", floatCode = "#136#", stringCode = "#209#", ulongCode = "#671#", listIntCode = "#535#", listFloatCode = "#328#", listStringCode = "#413#";
    private static string fileName = "hu.hu";
    private static string spacer = "#912#", listSpacer = "#725#";

    public enum Valuse
    {
        unappliedSave = -1,
        money = 0,
        level = 1,
        purchasedItems = 2,
        vibration = 3,
        sound = 4,
        gameCount = 5,
        aiComplexityCycleLenght = 6
    }

    public static int GetInt(Valuse key)
    {
        for (int i = 0; i < intKeysList.Count; i++)
        {
            if (intKeysList[i] == (int)key) { return intValuesList[i]; }
        }
        return 0;
    }

    public static void SetInt(Valuse key, int value)
    {
        int _key = (int)key;
        bool hasInList = false;
        for (int i = 0; i < intKeysList.Count; i++)
        {
            if (intKeysList[i] == _key) { intValuesList[i] = value; hasInList = true; }
        }
        if (!hasInList)
        {
            intKeysList.Add(_key);
            intValuesList.Add(value);
        }
    }

    public static bool HasInt(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < intKeysList.Count; i++)
        {
            if (intKeysList[i] == _key) { return true; }
        }
        return false;
    }

    public static void DeleteInt(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < intKeysList.Count; i++)
        {
            if (intKeysList[i] == _key)
            {
                intKeysList.RemoveAt(i);
                intValuesList.RemoveAt(i);
            }
        }
    }

    public static float GetFloat(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < floatKeysList.Count; i++)
        {
            if (floatKeysList[i] == _key) { return floatValuesList[i]; }
        }
        return 0.0f;
    }

    public static void SetFloat(Valuse key, float value)
    {
        int _key = (int)key;
        bool hasInList = false;
        for (int i = 0; i < floatKeysList.Count; i++)
        {
            if (floatKeysList[i] == _key) { floatValuesList[i] = value; hasInList = true; }
        }
        if (!hasInList)
        {
            floatKeysList.Add(_key);
            floatValuesList.Add(value);
        }
    }

    public static bool HasFloat(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < floatKeysList.Count; i++)
        {
            if (floatKeysList[i] == _key) { return true; }
        }
        return false;
    }

    public static void DeleteFloat(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < floatKeysList.Count; i++)
        {
            if (floatKeysList[i] == _key)
            {
                floatKeysList.RemoveAt(i);
                floatValuesList.RemoveAt(i);
            }
        }
    }

    public static string GetString(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < stringKeysList.Count; i++)
        {
            if (stringKeysList[i] == _key) { return stringValuesList[i]; }
        }
        return "";
    }

    public static void SetString(Valuse key, string value)
    {
        int _key = (int)key;
        bool hasInList = false;
        for (int i = 0; i < stringKeysList.Count; i++)
        {
            if (stringKeysList[i] == _key) { stringValuesList[i] = value; hasInList = true; }
        }
        if (!hasInList)
        {
            stringKeysList.Add(_key);
            stringValuesList.Add(value);
        }
    }

    public static bool HasString(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < stringKeysList.Count; i++)
        {
            if (stringKeysList[i] == _key) { return true; }
        }
        return false;
    }

    public static void DeleteString(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < stringKeysList.Count; i++)
        {
            if (stringKeysList[i] == _key)
            {
                stringKeysList.RemoveAt(i);
                stringValuesList.RemoveAt(i);
            }
        }
    }

    public static ulong GetUlong(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < ulongKeysList.Count; i++)
        {
            if (ulongKeysList[i] == _key) { return ulongValuesList[i]; }
        }
        return 0;
    }

    public static void SetUlong(Valuse key, ulong value)
    {
        int _key = (int)key;
        bool hasInList = false;
        for (int i = 0; i < ulongKeysList.Count; i++)
        {
            if (ulongKeysList[i] == _key) { ulongValuesList[i] = value; hasInList = true; }
        }
        if (!hasInList)
        {
            ulongKeysList.Add(_key);
            ulongValuesList.Add(value);
        }
    }

    public static bool HasUlong(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < ulongKeysList.Count; i++)
        {
            if (ulongKeysList[i] == _key) { return true; }
        }
        return false;
    }

    public static void DeleteUlong(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < ulongKeysList.Count; i++)
        {
            if (ulongKeysList[i] == _key)
            {
                ulongKeysList.RemoveAt(i);
                ulongValuesList.RemoveAt(i);
            }
        }
    }

    public static List<int> GetListInt(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < listIntKeysList.Count; i++)
        {
            if (listIntKeysList[i] == _key) { return listIntValuesList[i]; }
        }
        return new List<int> { };
    }

    public static void SetListInt(Valuse key, List<int> value)
    {
        int _key = (int)key;
        bool hasInList = false;
        for (int i = 0; i < listIntKeysList.Count; i++)
        {
            if (listIntKeysList[i] == _key) { listIntValuesList[i] = value; hasInList = true; }
        }
        if (!hasInList)
        {
            listIntKeysList.Add(_key);
            listIntValuesList.Add(value);
        }
    }

    public static bool HasListInt(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < listIntKeysList.Count; i++)
        {
            if (listIntKeysList[i] == _key) { return true; }
        }
        return false;
    }

    public static void DeleteListInt(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < listIntKeysList.Count; i++)
        {
            if (listIntKeysList[i] == _key)
            {
                listIntKeysList.RemoveAt(i);
                listIntValuesList.RemoveAt(i);
            }
        }
    }

    public static List<float> GetListFloat(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < listFloatKeysList.Count; i++)
        {
            if (listFloatKeysList[i] == _key) { return listFloatValuesList[i]; }
        }
        return new List<float> { };
    }

    public static void SetListFloat(Valuse key, List<float> value)
    {
        int _key = (int)key;
        bool hasInList = false;
        for (int i = 0; i < listFloatKeysList.Count; i++)
        {
            if (listFloatKeysList[i] == _key) { listFloatValuesList[i] = value; hasInList = true; }
        }
        if (!hasInList)
        {
            listFloatKeysList.Add(_key);
            listFloatValuesList.Add(value);
        }
    }

    public static bool HasListFloat(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < listFloatKeysList.Count; i++)
        {
            if (listFloatKeysList[i] == _key) { return true; }
        }
        return false;
    }

    public static void DeleteListFloat(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < listFloatKeysList.Count; i++)
        {
            if (listFloatKeysList[i] == _key)
            {
                listFloatKeysList.RemoveAt(i);
                listFloatValuesList.RemoveAt(i);
            }
        }
    }

    public static List<string> GetListString(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < listStringKeysList.Count; i++)
        {
            if (listStringKeysList[i] == _key) { return listStringValuesList[i]; }
        }
        return new List<string> { };
    }

    public static void SetListString(Valuse key, List<string> value)
    {
        int _key = (int)key;
        bool hasInList = false;
        for (int i = 0; i < listStringKeysList.Count; i++)
        {
            if (listStringKeysList[i] == _key) { listStringValuesList[i] = value; hasInList = true; }
        }
        if (!hasInList)
        {
            listStringKeysList.Add(_key);
            listStringValuesList.Add(value);
        }
    }

    public static bool HasListString(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < listStringKeysList.Count; i++)
        {
            if (listStringKeysList[i] == _key) { return true; }
        }
        return false;
    }

    public static void DeleteListString(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < listStringKeysList.Count; i++)
        {
            if (listStringKeysList[i] == _key)
            {
                listStringKeysList.RemoveAt(i);
                listStringValuesList.RemoveAt(i);
            }
        }
    }

    public static bool HasValue(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < intKeysList.Count; i++)
        {
            if (intKeysList[i] == _key) { return true; }
        }
        for (int i = 0; i < floatKeysList.Count; i++)
        {
            if (floatKeysList[i] == _key) { return true; }
        }
        for (int i = 0; i < stringKeysList.Count; i++)
        {
            if (stringKeysList[i] == _key) { return true; }
        }
        for (int i = 0; i < ulongKeysList.Count; i++)
        {
            if (ulongKeysList[i] == _key) { return true; }
        }
        for (int i = 0; i < listIntKeysList.Count; i++)
        {
            if (listIntKeysList[i] == _key) { return true; }
        }
        for (int i = 0; i < listFloatKeysList.Count; i++)
        {
            if (listFloatKeysList[i] == _key) { return true; }
        }
        for (int i = 0; i < listStringKeysList.Count; i++)
        {
            if (listStringKeysList[i] == _key) { return true; }
        }
        return false;
    }

    public static void DeleteValue(Valuse key)
    {
        int _key = (int)key;
        for (int i = 0; i < intKeysList.Count; i++)
        {
            if (intKeysList[i] == _key)
            {
                intKeysList.RemoveAt(i);
                intValuesList.RemoveAt(i);
            }
        }
        for (int i = 0; i < floatKeysList.Count; i++)
        {
            if (floatKeysList[i] == _key)
            {
                floatKeysList.RemoveAt(i);
                floatValuesList.RemoveAt(i);
            }
        }
        for (int i = 0; i < stringKeysList.Count; i++)
        {
            if (stringKeysList[i] == _key)
            {
                stringKeysList.RemoveAt(i);
                stringValuesList.RemoveAt(i);
            }
        }
        for (int i = 0; i < ulongKeysList.Count; i++)
        {
            if (ulongKeysList[i] == _key)
            {
                ulongKeysList.RemoveAt(i);
                ulongValuesList.RemoveAt(i);
            }
        }
        for (int i = 0; i < listIntKeysList.Count; i++)
        {
            if (listIntKeysList[i] == _key)
            {
                listIntKeysList.RemoveAt(i);
                listIntValuesList.RemoveAt(i);
            }
        }
        for (int i = 0; i < listFloatKeysList.Count; i++)
        {
            if (listFloatKeysList[i] == _key)
            {
                listFloatKeysList.RemoveAt(i);
                listFloatValuesList.RemoveAt(i);
            }
        }
        for (int i = 0; i < listStringKeysList.Count; i++)
        {
            if (listStringKeysList[i] == _key)
            {
                listStringKeysList.RemoveAt(i);
                listStringValuesList.RemoveAt(i);
            }
        }
    }

    public static void DeleteAll()
    {
        intKeysList.Clear();
        intValuesList.Clear();
        floatKeysList.Clear();
        floatValuesList.Clear();
        stringKeysList.Clear();
        stringValuesList.Clear();
        ulongKeysList.Clear();
        ulongValuesList.Clear();
        listIntKeysList.Clear();
        listIntValuesList.Clear();
        listFloatKeysList.Clear();
        listFloatValuesList.Clear();
        listStringKeysList.Clear();
        listStringValuesList.Clear();
    }

    public static void Zeroing() // обнулить все значения, в том числе и в облаке
    {
        intKeysList.Clear();
        intValuesList.Clear();
        floatKeysList.Clear();
        floatValuesList.Clear();
        stringKeysList.Clear();
        stringValuesList.Clear();
        listIntKeysList.Clear();
        listIntValuesList.Clear();
        listFloatKeysList.Clear();
        listFloatValuesList.Clear();
        listStringKeysList.Clear();
        listStringValuesList.Clear();
    }

    private static string GameDataToString()
    {
        string gameData = "";

        for (int i = 0; i < intKeysList.Count; i++)
        {
            gameData += intCode + spacer + intKeysList[i] + spacer + intValuesList[i] + spacer;
        }
        for (int i = 0; i < floatKeysList.Count; i++)
        {
            gameData += floatCode + spacer + floatKeysList[i] + spacer + floatValuesList[i] + spacer;
        }
        for (int i = 0; i < stringKeysList.Count; i++)
        {
            gameData += stringCode + spacer + stringKeysList[i] + spacer + stringValuesList[i] + spacer;
        }
        for (int i = 0; i < ulongKeysList.Count; i++)
        {
            gameData += ulongCode + spacer + ulongKeysList[i] + spacer + ulongValuesList[i] + spacer;
        }
        for (int i = 0; i < listIntKeysList.Count; i++)
        {
            string listIntValues = "";

            for (int u = 0; u < listIntValuesList[i].Count; u++)
            {
                listIntValues += listIntValuesList[i][u] + listSpacer;
            }

            gameData += listIntCode + spacer + listIntKeysList[i] + spacer + listIntValues + spacer;
        }
        for (int i = 0; i < listFloatKeysList.Count; i++)
        {
            string listFloatValues = "";

            for (int u = 0; u < listFloatValuesList[i].Count; u++)
            {
                listFloatValues += listFloatValuesList[i][u] + listSpacer;
            }

            gameData += listFloatCode + spacer + listFloatKeysList[i] + spacer + listFloatValues + spacer;
        }
        for (int i = 0; i < listStringKeysList.Count; i++)
        {
            string listStringValues = "";

            for (int u = 0; u < listStringValuesList[i].Count; u++)
            {
                listStringValues += listStringValuesList[i][u] + listSpacer;
            }

            gameData += listStringCode + spacer + listStringKeysList[i] + spacer + listStringValues + spacer;
        }
        return gameData + "end";
    }

    private static void StringToGameData(string decryptedString)
    {
        string[] gameDataStr = decryptedString.Split(new string[] { spacer }, StringSplitOptions.None);

        ClearCacheOfAllLists();

        for (int pointer = 0; pointer < gameDataStr.Length - 1;)
        {
            if (gameDataStr[pointer] == intCode)
            {
                pointer++;

                intKeysList.Add(int.Parse(gameDataStr[pointer++]));
                intValuesList.Add(int.Parse(gameDataStr[pointer++]));
            }
            else if (gameDataStr[pointer] == floatCode)
            {
                pointer++;

                floatKeysList.Add(int.Parse(gameDataStr[pointer++]));
                floatValuesList.Add(float.Parse(gameDataStr[pointer++]));
            }
            else if (gameDataStr[pointer] == stringCode)
            {
                pointer++;

                stringKeysList.Add(int.Parse(gameDataStr[pointer++]));
                stringValuesList.Add(gameDataStr[pointer++]);
            }
            else if (gameDataStr[pointer] == ulongCode)
            {
                pointer++;

                ulongKeysList.Add(int.Parse(gameDataStr[pointer++]));
                ulongValuesList.Add(ulong.Parse(gameDataStr[pointer++]));
            }
            else if (gameDataStr[pointer] == listIntCode)
            {
                pointer++;

                listIntKeysList.Add(int.Parse(gameDataStr[pointer++]));
                string[] intListStr = gameDataStr[pointer++].Split(new string[] { listSpacer }, StringSplitOptions.None);
                List<int> newListInt = new List<int> { };
                for (int i = 0; i < intListStr.Length - 1; i++)
                {
                    newListInt.Add(int.Parse(intListStr[i]));
                }
                listIntValuesList.Add(newListInt);
            }
            else if (gameDataStr[pointer] == listFloatCode)
            {
                pointer++;

                listFloatKeysList.Add(int.Parse(gameDataStr[pointer++]));
                string[] floatListStr = gameDataStr[pointer++].Split(new string[] { listSpacer }, StringSplitOptions.None);
                List<float> newListFloat = new List<float> { };
                for (int i = 0; i < floatListStr.Length - 1; i++)
                {
                    newListFloat.Add(float.Parse(floatListStr[i]));
                }
                listFloatValuesList.Add(newListFloat);
            }
            else if (gameDataStr[pointer] == listStringCode)
            {
                pointer++;

                listStringKeysList.Add(int.Parse(gameDataStr[pointer++]));
                string[] stringListStr = gameDataStr[pointer++].Split(new string[] { listSpacer }, StringSplitOptions.None);
                List<string> newListString = new List<string> { };
                for (int i = 0; i < stringListStr.Length - 1; i++)
                {
                    newListString.Add(stringListStr[i]);
                }
                listStringValuesList.Add(newListString);
            }
        }
    }

    private static void ClearCacheOfAllLists()
    {
        intKeysList.Clear();
        intValuesList.Clear();

        floatKeysList.Clear();
        floatValuesList.Clear();

        stringKeysList.Clear();
        stringValuesList.Clear();

        ulongKeysList.Clear();
        ulongValuesList.Clear();


        listIntKeysList.Clear();
        listIntValuesList.Clear();

        listFloatKeysList.Clear();
        listFloatValuesList.Clear();

        listStringKeysList.Clear();
        listStringValuesList.Clear();
    }

    private static void SaveGameData(string gameData)
    {
        File.WriteAllText(Application.persistentDataPath + "/" + fileName, Encrypt(gameData));
    }

    private static string LoadGameData()
    {
        return Decrypt(File.ReadAllText(Application.persistentDataPath + "/" + fileName));
    }

    public static string GetCryptedGameData() // для сохранения в облако
    {
        return File.ReadAllText(Application.persistentDataPath + "/" + fileName);
    }
    public static void RecoverDataFromCloud(string cryptedString)
    {
        SetString((Valuse.unappliedSave), cryptedString);

        //Save();
    }
    public static void SaveCryptedGameData(string cryptedGameData) // для восстановления из блочных сохранений
    {
        File.WriteAllText(Application.persistentDataPath + "/" + fileName, cryptedGameData);
    }

    public static void Load() // загрузка данных из памяти устройства в оператиную
    {
        if (File.Exists(Application.persistentDataPath + "/" + fileName)) { StringToGameData(LoadGameData()); }
    }

    public static void RecoverSave()
    {
        StringToGameData(Decrypt(GameData.GetString(Valuse.unappliedSave)));
        DeleteString(Valuse.unappliedSave);
        Save();
    }

    public static void Save() // сохранение данных на устройство
    {
        Debug.Log("Save");
        SetUlong(Valuse.unappliedSave, GetUlong(Valuse.unappliedSave) + 1);
        SaveGameData(GameDataToString());
    }

    private static String Encrypt(string decryptedString)
    {
        byte[] Key = { 230, 5, 182, 92, 201, 1, 177, 222 };
        byte[] IV = { 230, 5, 182, 92, 201, 1, 177, 222 };

        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        ICryptoTransform iCripto = des.CreateEncryptor(Key, IV);

        try
        {
            byte[] byteString = ASCIIEncoding.ASCII.GetBytes(decryptedString);
            byte[] encryptedByteString = iCripto.TransformFinalBlock(byteString, 0, byteString.Length);
            return Convert.ToBase64String(encryptedByteString);
        }
        catch (Exception)
        {
            return null;
        }
    }

    private static string Decrypt(string encryptedString)
    {
        byte[] Key = { 230, 5, 182, 92, 201, 1, 177, 222 };
        byte[] IV = { 230, 5, 182, 92, 201, 1, 177, 222 };

        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        ICryptoTransform iCrypto = des.CreateDecryptor(Key, IV);

        try
        {
            byte[] encryptedByteString = Convert.FromBase64String(encryptedString);
            byte[] byteString = iCrypto.TransformFinalBlock(encryptedByteString, 0, encryptedByteString.Length);
            return ASCIIEncoding.ASCII.GetString(byteString);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static ulong CheckVersion(string cryptedString)
    {
        cryptedString = Decrypt(cryptedString);

        string[] gameDataStr = cryptedString.Split(new string[] { spacer }, StringSplitOptions.None);

        for (int pointer = 0; pointer < gameDataStr.Length - 1;)
        {
            if (gameDataStr[pointer] == ulongCode)
            {
                pointer++;

                if (gameDataStr[pointer++] == "saveVersion") { return ulong.Parse(gameDataStr[pointer++]); }
                else { pointer++; }
            }
            else
            {
                pointer += 3;
            }
        }
        return 0;
    }

}

