using System;
using UnityEngine;

namespace PelVR
{
    [Serializable]
    public class LearningStep
    {
        [Tooltip("Main structure id, e.g. ilium, sacrum, pubic_symphysis (lowercase_underscore)")]
        public string structureId;

        [Tooltip("Extra structures to highlight together (e.g. the 4 pelvic floor muscles)")]
        public string[] highlightIds;

        [Tooltip("Optional: layer/group to isolate, e.g. bones, pelvic_floor, organs")]
        public string isolateLayer;

        [Tooltip("Optional: measurement line to show, e.g. obstetric_conjugate")]
        public string measurementId;

        [Header("Names")]
        public string titleTR;
        public string titleEN;
        public string titleLatin;

        [Header("Body text"), TextArea(3, 8)] public string bodyTR;
        [TextArea(3, 8)] public string bodyEN;

        public string Title(Lang l) => l == Lang.TR ? titleTR : titleEN;
        public string Body(Lang l) => l == Lang.TR ? bodyTR : bodyEN;
    }

    [CreateAssetMenu(fileName = "NewLearningSequence", menuName = "PelVR/Learning Sequence")]
    public class LearningSequence : ScriptableObject
    {
        public string sequenceTitleTR;
        public string sequenceTitleEN;
        public LearningStep[] steps;
    }
}