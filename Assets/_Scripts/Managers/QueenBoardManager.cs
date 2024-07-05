using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Collections;

public class QueenBoardManager : MonoBehaviour
{
    private TcpClient client;
    private StreamReader reader;
    private StreamWriter writer;

    public string serverIp = "127.0.0.1"; // Adresse IP locale
    public int port = 4444; // Port par défaut
    public string agentName = "Norbert"; // Nom de l'agent à définir

    private string[] variables = { "int_alpha", "int_bravo", "int_charlie", "int_delta", "int_echo" };

    void Start()
    {
        ConnectToServer();
        StartCoroutine(CheckVariablesPeriodically());
    }

    void ConnectToServer()
    {
        try
        {
            client = new TcpClient(serverIp, port);
            reader = new StreamReader(client.GetStream(), Encoding.ASCII);
            writer = new StreamWriter(client.GetStream(), Encoding.ASCII) { AutoFlush = true };
            Debug.Log("Connected to QUEEN server.");

            // Envoyer la commande pour définir le nom de l'agent
            SetAgentName(agentName);

            // Envoyer une commande ping pour vérifier la connexion
            SendCommand("ping");
        }
        catch (SocketException e)
        {
            Debug.LogError("SocketException: " + e.Message);
        }
    }

    void SetAgentName(string name)
    {
        SendCommand($"setname {name}");
    }

    void SendCommand(string command)
    {
        if (client == null || !client.Connected)
        {
            Debug.LogError("Not connected to the server.");
            return;
        }

        try
        {
            writer.WriteLine(command);
            Debug.Log("Sent command: " + command);

            // Lire la réponse du serveur
            string response = reader.ReadLine();
            Debug.Log("Received response: " + response);
        }
        catch (IOException e)
        {
            Debug.LogError("IOException: " + e.Message);
        }
    }

    IEnumerator CheckVariablesPeriodically()
    {
        while (true)
        {
            foreach (var variable in variables)
            {
                CheckVariable(variable);
            }
            yield return new WaitForSeconds(5); // Attendre 5 secondes avant de vérifier à nouveau
        }
    }

    void CheckVariable(string variable)
    {
        SendCommand($"get var.{variable}.state");
        string response = reader.ReadLine();
        ProcessVariableResponse(variable, response);
    }

    void ProcessVariableResponse(string variable, string response)
    {
        if (response.Contains("status='ok'"))
        {
            int value;
            if (int.TryParse(response.Split('>')[1].Split('<')[0], out value))
            {
                int firstDigit = value / 10;
                int secondDigit = value % 10;

                if (secondDigit == 2)
                {
                    switch (firstDigit)
                    {
                        case 0:
                            HandleLanguageChange(variable, "french");
                            break;
                        case 1:
                            HandleLanguageChange(variable, "english");
                            break;
                        case 2:
                            HandleLanguageChange(variable, "dutch");
                            break;
                        default:
                            Debug.Log($"Variable {variable} has an unassigned language code: {firstDigit}{secondDigit}");
                            break;
                    }
                }
            }
        }
    }

    void HandleLanguageChange(string variable, string language)
    {
        Debug.Log($"Variable {variable} indicates language: {language}");
        // Implémenter la logique souhaitée ici en fonction de la détection du langage
    }

    void OnApplicationQuit()
    {
        DisconnectFromServer();
    }

    void DisconnectFromServer()
    {
        if (writer != null) writer.Close();
        if (reader != null) reader.Close();
        if (client != null) client.Close();
        Debug.Log("Disconnected from QUEEN server.");
    }
}
