using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace BuildNotifier
{
    internal class BuildNotifier
    {
        public bool Enabled;
        public string Token;
        public string User;
        public string Title;

        public BuildNotifier()
        {
            Load();
        }
        
        public void SendPushNotification(string message)
        {
            if (!Enabled)
            {
                return;
            }
            
            var form = new WWWForm();
            form.AddField("token", Token);
            form.AddField("user", User);
            form.AddField("title", String.IsNullOrEmpty(Title) ? "Build Notifier" : Title);
            form.AddField("message", message);

            var request = UnityWebRequest.Post("https://api.pushover.net/1/messages.json", form);
            request.SendWebRequest().completed += (aop) =>
            {
                Debug.Log("Build Notifier; response from Pushover: " + request.downloadHandler.text);
                request.Dispose();
            };
        }

        private void Load()
        {
            Enabled = EditorPrefs.GetBool("BuildNotifier/enabled");
            Token = EditorPrefs.GetString("BuildNotifier/token", "");
            User = EditorPrefs.GetString("BuildNotifier/user", "");
            Title = EditorPrefs.GetString("BuildNotifier/title", "");
        }

        public void Save()
        {
            EditorPrefs.SetBool("BuildNotifier/enabled", Enabled);
            EditorPrefs.SetString("BuildNotifier/token", Token);
            EditorPrefs.SetString("BuildNotifier/user", User);
            EditorPrefs.SetString("BuildNotifier/title", Title);
        }
    }
}