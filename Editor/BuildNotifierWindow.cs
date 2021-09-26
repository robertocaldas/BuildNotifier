using UnityEditor;
using UnityEngine;

namespace BuildNotifier
{

    internal class BuildNotifierWindow : EditorWindow
    {

        private BuildNotifier _notifier;

        [MenuItem("Tools/Build Notifier")]
        public static void ShowWindow()
        {
            GetWindow(typeof(BuildNotifierWindow), true, "Build Notifier");
        }

        private void OnGUI()
        {
            GUILayout.Label("Pushover Settings", EditorStyles.boldLabel);
            _notifier.Enabled = EditorGUILayout.BeginToggleGroup ("Enabled", _notifier.Enabled);
            _notifier.Token = EditorGUILayout.TextField("API Token", _notifier.Token);
            _notifier.User = EditorGUILayout.TextField("User Key", _notifier.User);
            _notifier.Title = EditorGUILayout.TextField("Title (optional)", _notifier.Title);

            if (GUILayout.Button("Test"))
            {
                _notifier.SendPushNotification("It works!!");
            }
            EditorGUILayout.EndToggleGroup ();
        }

        private void CreateGUI()
        {
            _notifier = new BuildNotifier();
        }

        private void OnLostFocus()
        {
            _notifier.Save();
        }

        private void OnDestroy()
        {
            _notifier.Save();
        }
    }
}