using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Scriptable Objects/GameSettings")]
public class GameSettingsSO : ScriptableObject
{
    [field: SerializeField] public int Rounds { get; private set; }
    [field: SerializeField] public int TotalUtility { get; private set; }
    [field: SerializeField] public int GoalUtility { get; private set; }
}
