using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WorkstationScriptableObject", order = 1)]
public class WorkstationScriptableObject : ScriptableObject
{
    /**
    This is a workstation class. Use this class to create workstation objects by right-clicking in the Project file explorer in the Unity editor.
    **/

    [System.Serializable]
    public struct Workstation {
        public string name;
        public Sprite ingredientSprite;
        public Sprite workstationSpriteMain; 
        public Sprite workstationSpriteSecondary;
        public Vector3 toolTipOffset;
    }
    public Workstation[] workstations;
    public WorkstationScriptableObject(Workstation[] workstations) {
        this.workstations = workstations;
    }
}
