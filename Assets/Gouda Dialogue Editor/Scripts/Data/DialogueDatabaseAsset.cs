using UnityEngine;
using UnityEngine.UIElements;

namespace GoudaDialogueEditor
{
    [CreateAssetMenu(fileName = "LinesDatabase_", menuName = "Gouda/Lines database", order = 150)]
    public class DialogueDatabaseAsset : ScriptableObject
    {
        #region Fields and Properties
        public DialogueLine[] dialogueLines = new DialogueLine[] { };
        #endregion

        #region Methods 

        #endregion
    }
}
