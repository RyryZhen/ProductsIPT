using Microsoft.AspNetCore.SignalR;

namespace AssignmentFinals.Hubs
{
    public class InventoryHub : Hub
    {
        public async Task BroadcastLowStock(string message)
        {
            await Clients.All.SendAsync("ReceiveLowStockAlert", message);
        }
    }
}