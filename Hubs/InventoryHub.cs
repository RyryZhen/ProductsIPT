using Microsoft.AspNetCore.SignalR;

namespace AssignmentFinals.Hubs
{
    public class InventoryHub : Hub
    {
        // This method sends message to all connected users
        public async Task BroadcastLowStock(string message)
        {
            await Clients.All.SendAsync("ReceiveLowStockAlert", message);
        }
    }
}