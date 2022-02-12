using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = UnityEngine.Random;

public class insanagrams : MonoBehaviour
{

    public KMBombInfo info;
    public KMBombModule module;
    public KMAudio newAudio;

    private static int moduleIdCounter = 1;
    private int moduleId;
    private bool moduleSolved;

    public KMSelectable[] buttons;
    public KMSelectable submit, clear;
    public GameObject[] buttonObjects;

    public TextMesh ana, ans;

    [SerializeField]
#pragma warning disable IDE0044
    private string _anagramsUrl;
#pragma warning restore IDE0044

    private bool _hasRemoteError;

    private String answer;

    private Dictionary<string, string> modules = new Dictionary<string, string>();
    private Dictionary<char, int> keys = new Dictionary<char, int>();
    private String[] moduleNames;

    private void Awake()
    {
        moduleId = moduleIdCounter++;
        foreach (KMSelectable button in buttons)
            button.OnInteract += delegate () { PressButton(button); return false; };
        submit.OnInteract += delegate () { PressSubmit(); return false; };
        clear.OnInteract += delegate () { PressClear(); return false; };
    }

    void Start()
    {
        SetupDict();

        ans.text = "";
        ana.text = "connecting\u2026";

        foreach (GameObject but in buttonObjects)
            but.SetActive(false);

        StartCoroutine(FetchModules());
    }

	// Retrieve anagrams remotely
    private IEnumerator FetchModules()
	{
        WWW request = new WWW(_anagramsUrl);
        yield return request;
        if (request.error != null)
		{
            _hasRemoteError = true;
            Debug.LogFormat("[Insanagrams #{0}] Encountered remote error ({1})", moduleId, request.error);
            ans.text = "connection error!";
            ana.text = "press \"submit\" to solve";
            yield break;
		}

        string[] modules = request.text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        Debug.LogFormat("[Insanagrams #{0}] Fetched remote list ({1} anagrams)", moduleId, modules.Length);

        string[] pair = modules.PickRandom().Split(new[] { "::" }, StringSplitOptions.None);
        answer = pair[0];
        ana.text = pair[1].ToUpperInvariant();
        Debug.LogFormat("[Insanagrams #{0}] Anagram: \"{1}\", Answer: \"{2}\"", moduleId, pair[1], answer);

        foreach (char letter in answer.ToUpper().ToCharArray())
            buttonObjects[keys[letter]].SetActive(true);
    }

	void PressButton(KMSelectable button)
    {
        newAudio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, button.transform);
        button.AddInteractionPunch(.5f);
        if (answer == null || moduleSolved)
            return;
        if (Array.IndexOf(buttons, button) == 45)
            ans.text += " ";
        else
            ans.text += button.GetComponentInChildren<TextMesh>().text;
    }

    void PressSubmit()
    {
        newAudio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, submit.transform);
        if ((answer == null && !_hasRemoteError) || moduleSolved)
            return;
        if (_hasRemoteError || ans.text.ToUpper().Equals(answer.ToUpper()))
        {
            module.HandlePass();
            moduleSolved = true;
            newAudio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, submit.transform);
            Debug.LogFormat("[Insanagrams #{0}] Solved!", moduleId);
        }
        else
        {
            module.HandleStrike();
            Debug.LogFormat("[Insanagrams #{0}] Strike! Inputted: '{1}'. If you feel like this is an error, contact TasThing#5896 on Discord with a copy of this log file.", moduleId, ans.text);
            ans.text = "";
        }
    }

    void PressClear()
    {
        newAudio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, submit.transform);
        if (answer == null || moduleSolved)
            return;
        Debug.LogFormat("[Insanagrams #{0}] Clear pressed. Text cleared: '{1}'.", moduleId, ans.text);
        ans.text = "";
    }

    void SetupDict()
    {
        keys.Add('A', 0);
        keys.Add('B', 1);
        keys.Add('C', 2);
        keys.Add('D', 3);
        keys.Add('E', 4);
        keys.Add('F', 5);
        keys.Add('G', 6);
        keys.Add('H', 7);
        keys.Add('I', 8);
        keys.Add('J', 9);
        keys.Add('K', 10);
        keys.Add('L', 11);
        keys.Add('M', 12);
        keys.Add('N', 13);
        keys.Add('O', 14);
        keys.Add('P', 15);
        keys.Add('Q', 16);
        keys.Add('R', 17);
        keys.Add('S', 18);
        keys.Add('T', 19);
        keys.Add('U', 20);
        keys.Add('V', 21);
        keys.Add('W', 22);
        keys.Add('X', 23);
        keys.Add('Y', 24);
        keys.Add('Z', 25);
        keys.Add('1', 26);
        keys.Add('2', 27);
        keys.Add('3', 28);
        keys.Add('4', 29);
        keys.Add('5', 30);
        keys.Add('6', 31);
        keys.Add('7', 32);
        keys.Add('8', 33);
        keys.Add('9', 34);
        keys.Add('0', 35);
        keys.Add('-', 36);
        keys.Add('[', 37);
        keys.Add(']', 38);
        keys.Add('\'', 39);
        keys.Add('.', 40);
        keys.Add('!', 41);
        keys.Add('?', 42);
        keys.Add('&', 43);
        keys.Add('^', 44);
        keys.Add(' ', 45);
        keys.Add(',', 46);
    }

    // Twitch Plays
    #pragma warning disable 414
    private string TwitchHelpMessage = "Use '!{0} submit Cheap Checkout' to submit Cheap Checkout as your answer.";
    #pragma warning restore 414

    KMSelectable[] ProcessTwitchCommand(string input)
    {
        if (Regex.IsMatch(input, @"^\s*(submit|press|enter)$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) && _hasRemoteError)
		{
            PressSubmit();
            return null;
        }

        var match = Regex.Match(input, @"^\s*(submit|press|enter)\s+(.+)$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        if (!match.Success)
            return null;
        var text = match.Groups[2].Value.ToUpperInvariant();
        var btns = new List<KMSelectable>();
        foreach (var ch in text)
        {
            if (!keys.ContainsKey(ch))
                return null;
            btns.Add(buttons[keys[ch]]);
        }
        btns.Add(submit);
        return btns.ToArray();
    }

}
