using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commands : MonoBehaviour {

    [Header("messages")]
    public string commandNotKnownMessage = "Unkown command. type 'help' for help.";
    [Header("commands")]
    public command[] commands;
    [Header("instances")]
    public Command_Prompt cmdPrompt;
    
    [System.Serializable]
    public class command
    {
        public string name;
        public string command_help;
        public string[] arguments;
    } 

    public static Commands instance;
    void Awake()
    {
        if (instance == null) instance = this;
    }

    
    public string execute_command(string command_name, List<string> arguments)
    {
        string output = commandNotKnownMessage;
        if(command_name == "help")
        {
            output = "Available commands:";
            foreach(command cmd in commands) { output += "\n - " + cmd.command_help; };
        }

        if(command_name == "color")
        {
            output = "Display color updated";
            if (arguments.Count == 0) output = "You have to reference a color.";
            else if (arguments[0] == "white") cmdPrompt.outputText.color = Color.white;
            else if (arguments[0] == "red") cmdPrompt.outputText.color = Color.red;
            else if (arguments[0] == "blue") cmdPrompt.outputText.color = Color.blue;
            else if (arguments[0] == "green") cmdPrompt.outputText.color = Color.green;
            else if (arguments[0] == "gray") cmdPrompt.outputText.color = Color.gray;
            else if (arguments[0] == "purple") cmdPrompt.outputText.color = Color.magenta;
            else if(arguments[0] == "yellow") cmdPrompt.outputText.color = Color.yellow;
            else if(arguments[0] == "cyan") cmdPrompt.outputText.color = Color.cyan;
            else if(arguments[0] == "black") cmdPrompt.outputText.color = Color.black;
            else output = "The color argument (" + arguments[0] + ") is not a valid color.";
        }
        
        if(command_name == "spawn_weapon")
        {
            Debug.Log("Hi i am julien and this command works");
            output = "Spawnned weapon: " + arguments[0];
        }

        return output;
    }

}
