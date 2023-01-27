using ABI_RC.Core.Player;
using ABI_RC.Core.Savior;
using UnityEngine;

namespace Misatyan
{

    class Effector : MonoBehaviour
    {
        static readonly Vector4 ms_pointVector = new Vector4(0f, 0f, 0f, 1f);
        static readonly Quaternion ms_rotationOffsetRigh = Quaternion.Euler(0f, 0f, -90f);
        static readonly Quaternion ms_rotationOffsetLeft = Quaternion.Euler(0f, 0f, 90f);
        Animator m_animator = null;
        Vector3 Head = new Vector3(0, 0, 0);
        
        int m_mainLayer = -1;
        
        public void Start()
        {
            m_animator = PlayerSetup.Instance._animator;
            m_mainLayer = m_animator.GetLayerIndex("Locomotion/Emotes");
            
        }

        public void OnAnimatorIK(int p_layerIndex)
        {

            if (p_layerIndex == m_mainLayer) // Only main Locomotion/Emotes layer
            {
                Transform l_camera = GameObject.Find("_PLAYERLOCAL/[CameraRigDesktop]/Camera/LookTarget/GrabOffset").transform;

                if (Input.GetKey(KeyCode.E))
                {
                    m_animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
                    m_animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
                    m_animator.SetIKPosition(AvatarIKGoal.RightHand, l_camera.position + new Vector3(0, 0, 0));
                    m_animator.SetIKRotation(AvatarIKGoal.RightHand, l_camera.rotation * ms_rotationOffsetRigh);
                }
                if (Input.GetKey(KeyCode.Q))
                {
                    m_animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
                    m_animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
                    m_animator.SetIKPosition(AvatarIKGoal.LeftHand, l_camera.position + new Vector3(0, 0, 0));
                    m_animator.SetIKRotation(AvatarIKGoal.LeftHand, l_camera.rotation * ms_rotationOffsetLeft);


                }

                if (Input.GetKey(KeyCode.LeftControl))
                {
                    Head += new Vector3(0, 0, 20 * Input.GetAxis("Mouse ScrollWheel"));
                    m_animator.SetBoneLocalRotation(HumanBodyBones.Head, Quaternion.Euler(Head));
                    var Camera = GameObject.Find("_PLAYERLOCAL/[CameraRigDesktop]");
                    Camera.transform.eulerAngles += new Vector3(0, 0, 20 * Input.GetAxis("Mouse ScrollWheel"));
                }

                if (Input.GetKeyUp(KeyCode.LeftControl))
                {
                    Head = new Vector3(0, 0, 0);
                    var Camera = GameObject.Find("_PLAYERLOCAL/[CameraRigDesktop]");
                    Camera.transform.eulerAngles = GameObject.Find("_PLAYERLOCAL/").transform.eulerAngles;
                }


                float leftGesture = CVRInputManager.Instance.gestureLeft;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    leftGesture += 10*Input.GetAxis("Mouse ScrollWheel");
                    if ((leftGesture == -1) || (leftGesture >= 1 && leftGesture <= 6))
                        CVRInputManager.Instance.gestureLeft = leftGesture;
                    if (leftGesture > -1 && leftGesture < 1)
                        CVRInputManager.Instance.gestureLeft = 0;
                }

                float rightGesture = CVRInputManager.Instance.gestureRight;
                if (Input.GetKey(KeyCode.F))
                {
                    rightGesture += 10*Input.GetAxis("Mouse ScrollWheel");
                    if( (rightGesture == -1) || (rightGesture >= 1 && rightGesture <= 6))
                        CVRInputManager.Instance.gestureRight = rightGesture;
                    if (rightGesture > -1 && rightGesture < 1)
                        CVRInputManager.Instance.gestureRight = 0;
                }

            }



        }


    }


}