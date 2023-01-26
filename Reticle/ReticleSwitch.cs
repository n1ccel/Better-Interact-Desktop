using ABI_RC.Core.UI;
using ReticleSW;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Misatyan
{
    internal class ReticleSwitch
    {
        public static void TurnOff()//
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
            {
                if ((CohtmlHud.Instance != null) && (CohtmlHud.Instance.desktopPointer != null))
                    CohtmlHud.Instance.desktopPointer.SetActive(!CohtmlHud.Instance.desktopPointer.activeSelf);
            }
        }

        public static IEnumerator WaitForMenu()
        {
            while (!GameObject.Find("Cohtml/CohtmlWorldView")) { yield return new WaitForSeconds(1); }
            var reticle = GameObject.Find("_PLAYERLOCAL/[CameraRigDesktop]/Camera/Canvas/Image");
            reticle.GetComponent<Image>().sprite = Asset.Reticle();
            reticle.GetComponent<Image>().material.shader = Shader.Find("Sprites/Default");
            reticle.transform.localScale = new Vector2(1, 1);
        }
    }
}
