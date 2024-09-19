using UnityEngine;

namespace Waypoints.Utility
{
    /// <summary>A component that disables all renderers in the parent and child GameObject(s) on Start().</summary>
    /// Author: Intuitive Gaming Solutions
    public class DisableRenderersOnStart : MonoBehaviour
    {
        // Unity callback(s).
        void Start()
        {
            // Iterate over each Renderer in the component's gameObject and it's children and disable them.
            foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = false;
            }
        }
    }
}