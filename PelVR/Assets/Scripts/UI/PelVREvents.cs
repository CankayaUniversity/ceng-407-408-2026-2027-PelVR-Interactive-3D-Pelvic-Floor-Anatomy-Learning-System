using System;
using UnityEngine;

namespace PelVR
{
    public enum Lang { TR, EN }


    public static class PelVREvents
    {
        public static event Action<LearningSequence> OnSequenceStarted;
        public static event Action<LearningStep> OnStepChanged;
        public static event Action<LearningSequence> OnSequenceFinished;
        public static event Action<string> OnStructureSelected;
        public static event Action<Lang> OnLanguageChanged;

        public static Lang CurrentLang { get; private set; } = Lang.TR;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void ResetStatics()
        {
            OnSequenceStarted = null; OnStepChanged = null; OnSequenceFinished = null;
            OnStructureSelected = null; OnLanguageChanged = null;
            CurrentLang = Lang.TR;
        }

        public static void RaiseSequenceStarted(LearningSequence s) => OnSequenceStarted?.Invoke(s);
        public static void RaiseStepChanged(LearningStep s) => OnStepChanged?.Invoke(s);
        public static void RaiseSequenceFinished(LearningSequence s) => OnSequenceFinished?.Invoke(s);
        public static void RaiseStructureSelected(string id) => OnStructureSelected?.Invoke(id);

        public static void SetLanguage(Lang l)
        {
            if (CurrentLang == l) return;
            CurrentLang = l;
            OnLanguageChanged?.Invoke(l);
        }
    }
}