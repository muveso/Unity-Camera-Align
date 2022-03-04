using UnityEngine;
#if UNITY_EDITOR
#endif

namespace CameraAlignment
{
    public class CameraHolder : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] public bool EnableAlignment { get; set; }
#endif
    }
}