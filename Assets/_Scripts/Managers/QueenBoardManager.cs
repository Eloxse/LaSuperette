using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System.Text;

public class QueenConnection : MonoBehaviour
{
    private TcpClient client;
    private StreamReader reader;
    private StreamWriter writer;

    public string serverIp = "127.0.0.1"; // Adresse IP locale
    public int port = 4444; // Port par défaut
    public string agentName = "Norbert"; // Nom de l'agent à définir

    void Start()
    {
        ConnectToServer();
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
