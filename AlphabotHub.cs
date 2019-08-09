using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

public enum MovementStatus
{
    Stopped,
    Foeward,
    Backward,
    Left,
    Right,
}

public interface IAlphabotController
{
    Task OnMovementStatusChanged(MovementStatus status);    
}

public class AlphabotHub : Hub<IAlphabotController>
{/*
    public override Task OnConnectedAsync()
    {
        //return Clients.All.SendAsync("sendToAll", "__ADMIN__", $"{Context.ConnectionId} joined");
        
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        //return Clients.All.SendAsync("sendToAll", "__ADMIN__", $"{Context.ConnectionId} left");
    }
*/
    public Task StopAsync()
    {
        //return Clients.All.SendAsync("sendToAll", "__ECHO_BACK__", message);
        
        
        _ = Task.Run(() => Clients.All.OnMovementStatusChanged(MovementStatus.Stopped));

        return Task.CompletedTask;
    }


}
