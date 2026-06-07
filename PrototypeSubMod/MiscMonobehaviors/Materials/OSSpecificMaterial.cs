using System;
using UnityEngine;

namespace PrototypeSubMod.MiscMonobehaviors.Materials;

public class OSSpecificMaterial : MonoBehaviour
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private int materialIndex = -1;

    private void OnValidate()
    {
        if (!renderer) TryGetComponent(out renderer);
    }

    private void Awake()
    {
        var mats = renderer.materials;
        if (materialIndex == -1)
        {
            foreach (var material in mats)
            {
                var shader = Plugin.ShadersAssetBundle.LoadAsset<Shader>(material.shader.name.Split('/')[material.name.Split('/').Length - 1]);
                material.shader = shader;
            }
        }
        else
        {
            var shaderName = mats[materialIndex].shader.name.Split('/');
            mats[materialIndex].shader =
                Plugin.ShadersAssetBundle.LoadAsset<Shader>(
                    shaderName[shaderName.Length - 1]);
        }

        renderer.materials = mats;
    }
}