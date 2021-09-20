using SimpleJSON;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EasyFungus : EditorWindow
{
    private TextAsset JSONFile;
    private TextAsset luaFile;

    private StreamWriter sw;
    private JSONNode jsonData;

    [MenuItem("Window/Easy Fungus")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<EasyFungus>("Easy Fungus");
    }

    private void OnGUI()
    {
        GUILayout.Label("Lua Script Writer");
        JSONFile = EditorGUILayout.ObjectField("JSON File", JSONFile, typeof(TextAsset), true) as TextAsset;
        luaFile = EditorGUILayout.ObjectField("Lua Script", luaFile, typeof(TextAsset), true) as TextAsset;

        if (GUILayout.Button("Write"))
        {
            WriteLuaScript();
        }
    }

    (string, int) ModifyJson()
    {
        string result = "";
        bool isFirst = true;
        int itemNum = 0;
        string jsonString = JSONFile.ToString();
        int length = jsonString.Length;
        for (int i = 0; i < length; i++)
        {
            if (jsonString[i] == '{' && !isFirst)
            {
                string stringToAdd = itemNum.ToString() + " : {";
                result += stringToAdd;
                itemNum++;
            }
            else
            {
                isFirst = false;
                result += jsonString[i];
            }

        }
        return (result, itemNum);
    }

    void WriteLuaScript()
    {
        (string modifiedJson, int itemNum) = ModifyJson();
        Debug.Log(modifiedJson);
        jsonData = JSON.Parse(modifiedJson);
        string luaPath = AssetDatabase.GetAssetPath(luaFile);
        sw = new StreamWriter(luaPath);

        for (int i = 0; i < itemNum; i++)
        {
            string cmd = jsonData[i]["commandType"];
            if (cmd == "Say")
            {
                SayCommand(jsonData[i]["character"], jsonData[i]["text"]);
            }
            else if (cmd == "Move")
            {
                if (RemoveQuote(jsonData[i]["showhide"]) == "Show")
                {
                    if (jsonData[i]["start"] != null)
                    {
                        ShowMoveCommand(jsonData[i]["character"], jsonData[i]["portrait"], 
                            jsonData[i]["start"], jsonData[i]["end"]);
                    }
                    else
                    {
                        ShowCommand(jsonData[i]["character"], jsonData[i]["portrait"], jsonData[i]["end"]);
                    }
                }
                else
                {
                    HideCommand(jsonData[i]["character"], jsonData[i]["end"]);
                }
            }
        }
        sw.Close();

    }

    void SayCommand(string character, string text)
    {
        sw.WriteLine($"setcharacter({character.ToLower()})");
        sw.WriteLine($"say({AddQuote(text)})");
    }


    void ShowMoveCommand(string character, string portrait, string from, string to)
    {
        sw.WriteLine($"stage.show({character.ToLower()}, {AddQuote(portrait)}, " +
            $"{AddQuote(from.ToLower())}, {AddQuote(to.ToLower())})");
    }

    void ShowCommand(string character, string portrait, string position)
    {
        sw.WriteLine($"stage.show({character.ToLower()}, {AddQuote(portrait)}, " +
            $"{AddQuote(position.ToLower())})");
    }

    void HideCommand(string character, string position)
    {
        sw.WriteLine($"stage.hide({character.ToLower()}, {AddQuote(position.ToLower())})");
    }

    string AddQuote(string s)
    {
        return "\"" + s + "\"";
    }

    string RemoveQuote(string s)
    {
        return s.Replace("\"", "");
    }
}
