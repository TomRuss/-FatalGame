using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class Command_Prompt : MonoBehaviour {

    public GameObject command;
    public InputField command_line;
    public Text outputText;
    public RectTransform auto_complete_background;
    public Text auto_complete;
    public KeyCode openCommandPrompt;
    public KeyCode[] closeCommandPromptKeys;

    public static Command_Prompt instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        // we disable the command prompt
        command.SetActive(false);
        // we add the listener to get whenever the user ends the command
        command_line.onEndEdit.AddListener(delegate { onInputValueEndEdit(); });
    }

    void onInputValueEndEdit()
    {
        if (command_line.text == "") return;

        executeCommand();
        // We reset the values
        command_line.text = "";
        command_line.textComponent.text = "";
    }
    

    List<string> autoComplete()
    {
        // a bit to hard to explain, just know that this detects the autocomplete words
        string current_word = command_line.text;
        List<string> methods = new List<string>();
        foreach(Commands.command cmd in Commands.instance.commands)
        {
            if (cmd.name.StartsWith(current_word.Split(' ')[0]))
            {
                if (current_word.Contains(" "))
                {
                    foreach (string _argument in cmd.arguments)
                    { if (_argument.StartsWith(current_word.Split(' ')[current_word.Split(' ').Length - 1])) methods.Add(_argument); }
                }
                else methods.Add(cmd.name);
            }
        }
        return methods;
    }

    int currentAutocompleteIndex = -1;
    private void Update()
    {
        // Here we get the autocomplete words, we print them and we adjust the background's size
        List<string> methods = autoComplete();
        auto_complete.text = "";
        int index = -1;
        foreach (string met in methods)
        {
            index++;
            if(index == currentAutocompleteIndex) auto_complete.text += " > " + met + "\n";
            else auto_complete.text += met + "\n";
        }
        auto_complete_background.sizeDelta = new Vector2(auto_complete_background.sizeDelta.x, 10.8f * methods.Count);

        // We get if the user is pressing an arrow
        if (currentAutocompleteIndex >= methods.Count) currentAutocompleteIndex = -1;
        if (Input.GetKeyDown(KeyCode.DownArrow) && currentAutocompleteIndex < methods.Count) currentAutocompleteIndex++;
        if (Input.GetKeyDown(KeyCode.UpArrow) && currentAutocompleteIndex > -1) currentAutocompleteIndex--;
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentAutocompleteIndex != -1)
        {
            string last_word = command_line.text.Split(' ')[command_line.text.Split(' ').Length - 1];
            if(last_word != null && last_word != "") command_line.text.Replace(last_word, "");
            string[] _words = command_line.text.Split(' ');
            command_line.text = "";
            for (int i = 0; i < _words.Length-1; i++) { command_line.text += _words[i] + " "; };
            command_line.text += methods[currentAutocompleteIndex];
            currentAutocompleteIndex = -1;
        }
        
        // Get of the user is pressing the key to open the command prompt
        if (Input.GetKeyDown(openCommandPrompt) && !command.activeSelf)
        {
            // it enables the command prompt...
            command.SetActive(true);
            // it unhides and unlock the cursor...
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            // We reset the command's position
            command.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
        } 
        else
        {
            // Here we check if one of the keys that close the command prompt is pressed
            foreach (KeyCode close in closeCommandPromptKeys)
            { if (Input.GetKeyDown(close)) closeCommandPrompt(); }
        }
    }

    public void closeCommandPrompt()
    {
        // We disable the commandPrompt
        command.SetActive(false);
        // We hide the cursor
        Cursor.visible = false;
    }

    public void print(string outpout_line)
    { outputText.text += outpout_line + "\n"; }

    public void executeCommand()
    {
        // We print the command line
        // We get the command name and it's arguments
        outputText.text += " > " + command_line.text + "\n";
        List<string> args = new List<string>();
        foreach(string argument in command_line.text.Split(' '))
        { args.Add(argument); }
        string command_name = args[0];
        args.Remove(command_name);
        // execute the command and print the output
        outputText.text += Commands.instance.execute_command(command_name, args) + "\n";

        // to hard to explain... just know that you don't need that to code
        string[] lines = outputText.text.Split('\n');
        if (lines.Length <= 28) return;
        int to_delete = lines.Length - 28;
        List<string> keep_lines = new List<string>();
        foreach(string _line in lines)
        { keep_lines.Add(_line); }
        for(int i = 0; i <= to_delete; i++)
        {
            keep_lines.Remove(lines[i]);
        }
        keep_lines[keep_lines.Count-1].Replace("\n", "");
        outputText.text = "";
        foreach(string output_line in keep_lines)
        { outputText.text += output_line + "\n"; }
    }
}
