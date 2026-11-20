using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PelVR
{
    public class GuidedLearningPanel : MonoBehaviour
    {
        [Header("Content")]
        [SerializeField] LearningSequence sequence;

        [Header("UI refs")]
        [SerializeField] TMP_Text titleText;
        [SerializeField] TMP_Text latinText;
        [SerializeField] TMP_Text bodyText;
        [SerializeField] TMP_Text stepIndicator;
        [SerializeField] Slider progressBar;       
        [SerializeField] Button backButton;
        [SerializeField] Button nextButton;
        [SerializeField] Button languageButton;
        [SerializeField] TMP_Text languageButtonLabel;
        [SerializeField] TMP_Text nextButtonLabel;

        int index;

        void Awake()
        {
            backButton.onClick.AddListener(Back);
            nextButton.onClick.AddListener(Next);
            if (languageButton) languageButton.onClick.AddListener(ToggleLanguage);
        }

        void OnEnable()
        {
            PelVREvents.OnLanguageChanged += OnLanguageChanged;
            if (sequence != null) StartSequence(sequence);
        }

        void OnDisable() => PelVREvents.OnLanguageChanged -= OnLanguageChanged;

        public void StartSequence(LearningSequence s)
        {
            sequence = s;
            gameObject.SetActive(true);
            if (sequence == null || sequence.steps == null || sequence.steps.Length == 0)
            {
                Debug.LogWarning("GuidedLearningPanel: sequence is empty.");
                return;
            }
            index = 0;
            PelVREvents.RaiseSequenceStarted(sequence);
            Show();
        }

        public void Next()
        {
            if (sequence == null) return;
            if (index >= sequence.steps.Length - 1)
            {
                PelVREvents.RaiseSequenceFinished(sequence);
                gameObject.SetActive(false);
                return;
            }
            index++;
            Show();
        }

        public void Back()
        {
            if (sequence == null || index == 0) return;
            index--;
            Show();
        }

        void ToggleLanguage() =>
            PelVREvents.SetLanguage(PelVREvents.CurrentLang == Lang.TR ? Lang.EN : Lang.TR);

        void OnLanguageChanged(Lang _) { if (sequence != null) Refresh(); }

        void Show()
        {
            Refresh();
            PelVREvents.RaiseStepChanged(sequence.steps[index]);
        }

        void Refresh()
        {
            var lang = PelVREvents.CurrentLang;
            var step = sequence.steps[index];
            int total = sequence.steps.Length;

            titleText.text = step.Title(lang);
            if (latinText) latinText.text = string.IsNullOrEmpty(step.titleLatin) ? "" : step.titleLatin;
            bodyText.text = step.Body(lang);

            if (stepIndicator)
                stepIndicator.text = lang == Lang.TR ? $"Adým {index + 1} / {total}" : $"Step {index + 1} of {total}";
            if (progressBar) progressBar.value = (index + 1f) / total;

            backButton.interactable = index > 0;
            bool last = index == total - 1;
            if (nextButtonLabel)
                nextButtonLabel.text = last ? (lang == Lang.TR ? "Bitir" : "Finish") : (lang == Lang.TR ? "Ýleri" : "Next");
            if (languageButtonLabel) languageButtonLabel.text = lang == Lang.TR ? "EN" : "TR";
        }
    }
}