using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public class ImportMapDataEditorWindow : EditorWindow
{
    private Material _roadMaterial;
    private Material _buildingMaterial;
    private string _mapFilePath = "None (Choose OpenMap File)";
    private string _progresText;
    private float _progress;
    private bool _disableUI;
    private bool _validFile;
    private bool _importing;


    [MenuItem("Window/Import OpenMap Data")]
    public static void ShowEditorWindow()
    {
        var window = EditorWindow.GetWindow<ImportMapDataEditorWindow>();
        window.titleContent = new GUIContent("Import OpenMap");
        window.Show();
    }

    public void ResetProgress()
    {
        _progress = 0f;
        _progresText = "";
    }

    public void UpdateProgress(float progress, string progressText, bool done)
    {
        _progress = progress;
        _progresText = progressText;
        if (!done)
            EditorUtility.DisplayProgressBar("Importing Map",
                                             string.Format("{0} {1:%}", progressText, progress),
                                             progress);
        else
            EditorUtility.ClearProgressBar();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.TextField(_mapFilePath);
        EditorGUI.EndDisabledGroup();
        if (GUILayout.Button("..."))
        {
            var filePath = EditorUtility.OpenFilePanel("Select OpenMap File",
                                                        Application.dataPath,
                                                        "txt");
            if (filePath.Length > 0) 
                _mapFilePath = filePath;

            _validFile = _mapFilePath.Length > 0;
        }

        EditorGUILayout.EndHorizontal();


        _roadMaterial = EditorGUILayout.ObjectField("Road Material",
                                                    _roadMaterial,
                                                    typeof(Material),
                                                    false) as Material;
        _buildingMaterial = EditorGUILayout.ObjectField("Building Material",
                                                        _buildingMaterial,
                                                        typeof(Material),
                                                        false) as Material;

        EditorGUI.BeginDisabledGroup(!_validFile || _disableUI || _importing);
        if (GUILayout.Button("Import Map File"))
        {
            _importing = true;

            var mapWraper = new ImportMapWrapper(this,
                                                 _mapFilePath,
                                                 _roadMaterial,
                                                 _buildingMaterial);

            mapWraper.Import();
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            _importing = false;

        }
        
        EditorGUI.EndDisabledGroup();

        if (_disableUI)
        {
            EditorGUILayout.HelpBox("The current scene has not been saved yet!",
                                    MessageType.Warning,
                                    true);
        }
    }

    private void Update()
    {
        _disableUI = EditorSceneManager.GetActiveScene().isDirty;
    }


}
