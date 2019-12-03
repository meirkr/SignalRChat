using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

public class MyObj
{
    public int MyProperty1 { get; set; }
    public int MyProperty2 { get; set; }
    public string MessageProperty { get; set; }
}
public interface IClient
{
    Task SendAsync(string obj, string obj2, object obj3);
    Task SendObjAsync(MyObj obj);
}
public class RobotHub : Hub<IClient>
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
        return //Clients.All.SendAsync("sendToAll", "__ECHO_BACK__", message);
            Clients.All.SendObjAsync(
                new MyObj 
                {
                    MyProperty1 = 1,
                    MyProperty2 = 2,
                    MessageProperty = message,
                }
            );
    }

    static IDisposable _move;
    public Task StartMoving(string message)
    {
        _move?.Dispose();
        var clients = Clients.All;
        _move = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .StartWith(1)
                .Subscribe(_ =>
            clients.SendObjAsync(
                new MyObj 
                {
                    MyProperty1 = 1,
                    MyProperty2 = 2,
                    MessageProperty = message,
                }
            ));
            return Task.CompletedTask;
    }

    public Task StopMoving()
    {
        _move?.Dispose();

        return Task.CompletedTask;
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