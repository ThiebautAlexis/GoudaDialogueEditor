using UnityEditor;
using UnityEngine;

namespace GoudaDialogueEditor.Editor
{
    [CustomEditor(typeof(DialogueAsset))]
    public class DialogueAssetEditor : UnityEditor.Editor
    {
        #region Fields and Properties

        #endregion

        #region Methods 
        public override void OnInspectorGUI()
        {
            GUIStyle _style = new GUIStyle();
            _style.fontSize = 18;
            _style.fontStyle = FontStyle.Bold;
            _style.wordWrap = true;
            _style.alignment = TextAnchor.MiddleCenter;
            GUIContent _content = new GUIContent("You can't edit this asset from the Inspector.\nPlease use the Dialogue Graph!");
            GUILayout.Label(_content, _style);
            if(GUILayout.Button(new GUIContent("Open Dialogue Graph")))
            {
                DialogueGraph.OpenDialogueGraph();
            }
        }
        #endregion
    }
}
