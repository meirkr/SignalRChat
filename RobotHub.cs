using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

public class RobotHub : Hub
{
    public override Task OnConnectedAsync()
    {
        return Clients.All.SendAsync("sendToAll", "__ADMIN__", $"{Context.ConnectionId} joined");
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        return Clients.All.SendAsync("sendToAll", "__ADMIN__", $"{Context.ConnectionId} left");
    }

    public Task Send(string message)
    {
        return Clients.All.SendAsync("sendToAll", "__ECHO_BACK__", message);
    }

    public int GetNumOfClients()
    {
        Clients.All.SendAsync("DoSomething", "param1", 2);
        return 0;
    }

    public void SendToAll(string name, string message)
    {
        Clients.All.SendAsync("sendToAll", name, message);
    }
}