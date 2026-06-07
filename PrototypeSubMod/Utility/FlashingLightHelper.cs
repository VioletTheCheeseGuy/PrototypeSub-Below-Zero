using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PrototypeSubMod.Utility
{
    public static class FlashingLightHelpers
    {
        // Token: 0x06001483 RID: 5251 RVA: 0x00068E29 File Offset: 0x00067029
        public static FlashingLightHelpers.ShaderVector4ScalerToken CreateUberShaderVector4ScalerToken(Material material)
        {
            FlashingLightHelpers.ShaderVector4ScalerToken shaderVector4ScalerToken = new FlashingLightHelpers.ShaderVector4ScalerToken(material);
            shaderVector4ScalerToken.AddProperty(ShaderPropertyID._MainTex2_Speed);
            return shaderVector4ScalerToken;
        }

        // Token: 0x06001484 RID: 5252 RVA: 0x00068E54 File Offset: 0x00067054
        public static List<FlashingLightHelpers.ShaderVector4ScalerToken> CreateUberShaderVector4ScalerTokens(params Material[] materials)
        {
            List<FlashingLightHelpers.ShaderVector4ScalerToken> list = new List<FlashingLightHelpers.ShaderVector4ScalerToken>();
            foreach (Material material in materials)
            {
                list.Add(FlashingLightHelpers.CreateUberShaderVector4ScalerToken(material));
            }
            return list;
        }

        // Token: 0x06001485 RID: 5253 RVA: 0x00068E88 File Offset: 0x00067088
        public static List<FlashingLightHelpers.ShaderVector4ScalerToken> CreateShaderVector4ScalerTokens(params Material[] materials)
        {
            List<FlashingLightHelpers.ShaderVector4ScalerToken> list = new List<FlashingLightHelpers.ShaderVector4ScalerToken>();
            foreach (Material material in materials)
            {
                list.Add(new FlashingLightHelpers.ShaderVector4ScalerToken(material));
            }
            return list;
        }

        // Token: 0x06001486 RID: 5254 RVA: 0x00068EBC File Offset: 0x000670BC
        public static void AddProperty(this List<FlashingLightHelpers.ShaderVector4ScalerToken> tokens, int propertyId)
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                tokens[i].AddProperty(propertyId);
            }
        }

        // Token: 0x06001487 RID: 5255 RVA: 0x00068EE8 File Offset: 0x000670E8
        public static void SetScale(this List<FlashingLightHelpers.ShaderVector4ScalerToken> tokens, float scale)
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                tokens[i].SetScale(scale);
            }
        }

        // Token: 0x06001488 RID: 5256 RVA: 0x00068F14 File Offset: 0x00067114
        public static void RestoreScale(this List<FlashingLightHelpers.ShaderVector4ScalerToken> tokens)
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                tokens[i].RestoreScale();
            }
        }


        // Token: 0x0600148A RID: 5258 RVA: 0x00068F67 File Offset: 0x00067167
        public static void SafeIntensityChangePerFrame(Light light, float intensity, float epilepticsSpeed = 0.3f)
        {
            if (MiscSettings.flashes)
            {
                light.intensity = intensity;
                return;
            }
            light.intensity = FlashingLightHelpers.LimitValueChangePreFrame(light.intensity, intensity, epilepticsSpeed);
        }

        // Token: 0x0600148B RID: 5259 RVA: 0x00068F8B File Offset: 0x0006718B
        public static void SafeRangeChangePreFrame(Light light, float range, float epilepticsSpeed = 0.3f)
        {
            if (MiscSettings.flashes)
            {
                light.range = range;
                return;
            }
            light.range = FlashingLightHelpers.LimitValueChangePreFrame(light.range, range, epilepticsSpeed);
        }

        // Token: 0x0600148C RID: 5260 RVA: 0x00068FAF File Offset: 0x000671AF
        public static void SafePositionChangePreFrame(Transform transform, Vector3 position, float epilepticsSpeed = 0.25f)
        {
            if (MiscSettings.flashes)
            {
                transform.position = position;
                return;
            }
            transform.position = FlashingLightHelpers.LimitPositionChangePreFrame(transform.position, position, epilepticsSpeed);
        }

        // Token: 0x0600148D RID: 5261 RVA: 0x00068FD4 File Offset: 0x000671D4
        private static float LimitValueChangePreFrame(float current, float target, float speed)
        {
            float num = Mathf.Abs(target - current);
            if (num > 0f)
            {
                return Mathf.Lerp(current, target, Time.deltaTime * speed / num);
            }
            return target;
        }

        // Token: 0x0600148E RID: 5262 RVA: 0x00069004 File Offset: 0x00067204
        private static Vector3 LimitPositionChangePreFrame(Vector3 current, Vector3 target, float speed)
        {
            float magnitude = (target - current).magnitude;
            if (magnitude > 0f)
            {
                return Vector3.Lerp(current, target, Time.deltaTime * speed / magnitude);
            }
            return target;
        }

        // Token: 0x040015A0 RID: 5536
        private const float defaultIntensitySpeed = 0.3f;

        // Token: 0x040015A1 RID: 5537
        private const float defaultMovementSpeed = 0.25f;

        // Token: 0x0200089A RID: 2202
        public class ShaderVector4ScalerToken
        {
            // Token: 0x06004B1E RID: 19230 RVA: 0x0017C9A4 File Offset: 0x0017ABA4
            public ShaderVector4ScalerToken(Material material)
            {
                this.material = material;
                this.properties = new List<FlashingLightHelpers.ShaderVector4ScalerToken.Data>();
            }

            // Token: 0x06004B1F RID: 19231 RVA: 0x0017C9C0 File Offset: 0x0017ABC0
            public void AddProperty(int propertyId)
            {
                this.properties.Add(new FlashingLightHelpers.ShaderVector4ScalerToken.Data
                {
                    propertyId = propertyId,
                    defaultValue = this.material.GetVector(propertyId)
                });
            }

            // Token: 0x06004B20 RID: 19232 RVA: 0x0017C9FC File Offset: 0x0017ABFC
            public void SetScale(float scale)
            {
                for (int i = 0; i < this.properties.Count; i++)
                {
                    FlashingLightHelpers.ShaderVector4ScalerToken.Data data = this.properties[i];
                    this.material.SetVector(data.propertyId, data.defaultValue * scale);
                }
            }

            // Token: 0x06004B21 RID: 19233 RVA: 0x0017CA49 File Offset: 0x0017AC49
            public void RestoreScale()
            {
                this.SetScale(1f);
            }

            // Token: 0x0400433B RID: 17211
            private Material material;

            // Token: 0x0400433C RID: 17212
            private List<FlashingLightHelpers.ShaderVector4ScalerToken.Data> properties;

            // Token: 0x02000C13 RID: 3091
            private struct Data
            {
                // Token: 0x04005100 RID: 20736
                public int propertyId;

                // Token: 0x04005101 RID: 20737
                public Vector4 defaultValue;
            }
        }
    }
}
