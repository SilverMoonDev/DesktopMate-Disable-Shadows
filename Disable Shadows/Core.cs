using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(Disable_Shadows.Core), "Disable Shadows", "1.0.0", "Joanpixer", null)]
[assembly: MelonGame("infiniteloop", "DesktopMate")]

namespace Disable_Shadows
{
    public class Core : MelonMod
    {
        private int characterID;
        private Transform rootTransform;

        public override void OnLateUpdate()
        {
            if (rootTransform == null || rootTransform.childCount == 0) return;

            var newCharacter = rootTransform.GetChild(0).gameObject;
            if (newCharacter.GetInstanceID() != characterID)
            {
                OnAvatarChange(newCharacter);
            }
        }
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName == "Main" && rootTransform == null)
            {
                rootTransform = GameObject.Find("CharactersRoot")?.transform;
            }
        }

        private void OnAvatarChange(GameObject avatar)
        {
            if (avatar == null) return;
            characterID = avatar.GetInstanceID();
            DisableShadows(avatar);
        }

        private void DisableShadows(GameObject avatar)
        {
            var meshRenderers = avatar.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer meshRenderer in meshRenderers)
            {
                meshRenderer.castShadows = false;
            }
        }
    }
}