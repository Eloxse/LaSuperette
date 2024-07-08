using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System.Text;

public class Test : MonoBehaviour
{
    #region Variables

    [Header("Connection to Queen Board")]
    [SerializeField] private string serverIp = "127.0.0.1";
    [SerializeField] private int port = 4444; // Default port.

    // Reference to LanguageSelector.
    [Header("Language Selector")]
    [SerializeField] private LanguageSelector languageSelector;

    // Connection to Queen Board.
    private TcpClient _client;
    private StreamReader _reader;
    private StreamWriter _writer;

    #endregion

    #region Built-In Methods

    private void Start()
    {
        ConnectToServer();
        SendGetCommand("int_alpha");
    }

    private void Update()
    {
        ReceiveMessage();
    }

    private void OnApplicationQuit()
    {
        DisconnectFromServer();
    }

    #endregion

    #region Queen Board connection

    private void ConnectToServer()
    {
        try
        {
            _client = new TcpClient(serverIp, port);
            _reader = new StreamReader(_client.GetStream(), Encoding.ASCII);
            _writer = new StreamWriter(_client.GetStream(), Encoding.ASCII) { AutoFlush = true };
        }
        catch (SocketException e)
        {
            Debug.LogError("SocketException: " + e.Message);
        }
        catch (IOException e)
        {
            Debug.LogError("IOException: " + e.Message);
        }
    }

    private void SendCommand(string command)
    {
        if (_client == null || !_client.Connected)
        {
            return;
        }

        try
        {
            _writer.WriteLine(command);
        }
        catch (IOException e)
        {
            Debug.LogError("IOException: " + e.Message);
        }
    }

    private void SendGetCommand(string variable)
    {
        string command = $"get out.cachelock.state";
        SendCommand(command);
    }

    private void DisconnectFromServer()
    {
        if (_writer != null) _writer.Close();
        if (_reader != null) _reader.Close();
        if (_client != null) _client.Close();
    }

    #endregion

    #region Custom Methods

    private void ReceiveMessage()
    {
        if (_client != null && _client.Connected)
        {
            try
            {
                if (_client.GetStream().DataAvailable)
                {
                    string message = _reader.ReadLine();
                    LogCharacters(message);

                    if (!string.IsNullOrEmpty(message))
                    {
                        message = message.Trim();
                        string[] parts = message.Split('=');

                        if (message.StartsWith("<response") && message.EndsWith("</response>"))
                        {
                            ParseVariableMessage(message);
                        }
                        else
                        {
                            ParseXmlMessage(message);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                Debug.LogError("IOException: " + e.Message);
            }
        }
    }

    private void ParseXmlMessage(string message)
    {
        try
        {
            int typeStart = message.IndexOf("type='") + 6;
            int typeEnd = message.IndexOf("'", typeStart);
            string type = message.Substring(typeStart, typeEnd - typeStart);

            int nameStart = message.IndexOf("name='") + 6;
            int nameEnd = message.IndexOf("'", nameStart);
            string name = message.Substring(nameStart, nameEnd - nameStart);

            string content = message.Substring(message.IndexOf(">") + 1);
            content = content.Substring(0, content.IndexOf("</response>"));
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to parse XML message: " + e.Message);
        }
    }

    private void ParseVariableMessage(string message)
    {
        string[] parts = message.Split('=');
            Debug.Log("OUI");
        if (parts.Length == 2)
        {
            string variableName = parts[0].Trim();
            string valueStr = parts[1].Trim();

            if (int.TryParse(valueStr, out int value))
            {
                Debug.Log("Received message for variable: " + variableName + " with value: " + value);

                switch (variableName)
                {
                    case "int_alpha":
                        Debug.Log("Processing int_alpha: " + value);
                        // Example action: Set language to English if int_alpha is 1
                        if (value == 1)
                        {
                            SetLanguage("english");
                        }
                        break;
                    case "int_bravo":
                        Debug.Log("Processing int_bravo: " + value);
                        break;
                    case "int_charlie":
                        Debug.Log("Processing int_charlie: " + value);
                        break;
                    default:
                        Debug.Log("Unknown variable: " + variableName);
                        break;
                }
            }
            else
            {
                Debug.LogError("Failed to parse value: " + valueStr);
            }
        }
        else
        {
            Debug.LogError("Invalid message format: " + message);
        }
    }

    private void LogCharacters(string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            Debug.Log("Message is null or empty");
            return;
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("Message characters: ");
        foreach (char c in message)
        {
            sb.Append($"[{(int)c} '{c}'] ");
        }
        Debug.Log(sb.ToString());
    }

    private void SetLanguage(string languageCode)
    {
        if (languageSelector != null)
        {
            languageSelector.SetLanguage(languageCode);
        }
        else
        {
            Debug.LogError("LanguageSelector is not set.");
        }
    }

    #endregion
}
