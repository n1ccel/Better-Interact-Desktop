using ABI_RC.Core.Player;
using MelonLoader;
using System;
using System.Reflection;

namespace Misatyan
{
    public class Misa : MelonMod
    {

        public static Misa Instance { get; private set; }
        
        static Misa ms_instance = null;
        
        Effector m_localPuller = null;
        public MelonLogger.Instance Logger => Instance.LoggerInstance;

        public override void OnInitializeMelon()
        {
            if (ms_instance == null)
                ms_instance = this;

            HarmonyInstance.Patch(
                typeof(PlayerSetup).GetMethod(nameof(PlayerSetup.ClearAvatar)),
                null,
                new HarmonyLib.HarmonyMethod(typeof(Misa).GetMethod(nameof(OnAvatarClear_Postfix), BindingFlags.NonPublic | BindingFlags.Static))
            );
            HarmonyInstance.Patch(
                typeof(PlayerSetup).GetMethod(nameof(PlayerSetup.SetupAvatar)),
                null,
                new HarmonyLib.HarmonyMethod(typeof(Misa).GetMethod(nameof(OnSetupAvatar_Postfix), BindingFlags.Static | BindingFlags.NonPublic))
            );
                        
        }

        static void OnAvatarClear_Postfix() => ms_instance?.OnAvatarClear();
        void OnAvatarClear()
        {
            try
            {
                m_localPuller = null;
            }
            catch (Exception e)
            {
                MelonLoader.MelonLogger.Error(e);
            }
        }

        static void OnSetupAvatar_Postfix() => ms_instance?.OnSetupAvatar();
        void OnSetupAvatar()
        {
            try
            {
                if (!Utils.IsInVR())
                    m_localPuller = PlayerSetup.Instance._avatar.AddComponent<Effector>();
            }
            catch (Exception e)
            {
                MelonLoader.MelonLogger.Error(e);
            }
        }

        public override void OnUpdate()
        {
            //Riticle On|Off by SDraw
            ReticleSwitch.TurnOff();
        }

        //Swap shit CVR's riticle to good red point
        [Obsolete]
        public override void OnApplicationStart()
        {
            MelonCoroutines.Start(ReticleSwitch.WaitForMenu());
            //MelonCoroutines.Start(MainGUI.WaitForMenu());
            Instance = this;
        }
    }
}

