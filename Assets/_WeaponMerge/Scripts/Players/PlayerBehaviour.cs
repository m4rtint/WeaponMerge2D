using _WeaponMerge.Scripts.Players;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private PlayerMovementBehaviour _playerMovementBehaviour = null;
    
    private void Awake()
    {
        _playerMovementBehaviour = GetComponent<PlayerMovementBehaviour>();
    }
    
    public void Initialize(ControlInput controlInput)
    {
        _playerMovementBehaviour.Initialize(controlInput);
    }
}
