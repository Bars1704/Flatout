#if UNITY_IOS || UNITY_TVOS || UNITY_EDITOR
namespace InControl
{
    using AOT;
    using System;
    using System.Runtime.InteropServices;


    public static class ICadeNative
    {
        [DllImport("__Internal")]
        private static extern void _SetActive(bool state);


        [DllImport("__Internal")]
        private static extern void _SetStateCallback(Action<int> cntCallback);


        [DllImport("__Internal")]
        private static extern int _GetState();


        public static void SetActive(bool state)
        {
            _SetActive(state);
        }


        [MonoPInvokeCallback(typeof(Action<int>))]
        private static void OnStateChanged(int state)
        {
            // Not used for now.
        }


        public static ICadeState GetState()
        {
            return (ICadeState)_GetState();
        }
    }
}
#endif

