using UnityEngine;
using Fungus;
using UnityEditor;

/*public class EasyFungus : MonoBehaviour
{

    public Flowchart fc;
    // Start is called before the first frame update
    void Start()
    {
        Command c;
        
        //fc.AddSelectedCommand();
        Block b = new Block();
        fc.CreateBlock(new Vector2(0.2f, 0.2f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}*/

public class EasyFungus : EditorWindow
{
    /*public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Test"))
            Debug.Log("It's alive: " + target.name);
    }*/
    [MenuItem("Window/Easy Fungus")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<EasyFungus>("Easy Fungus");
    }

    private void OnGUI()
    {
        
    }
}
