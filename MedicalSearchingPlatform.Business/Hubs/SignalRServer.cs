using Microsoft.AspNetCore.SignalR;

namespace MedicalSearchingPlatform.Business.Hubs
{
    public class SignalRServer : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var user = Context.User;
            if (user.IsInRole("Doctor"))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Doctor");
            }
            else if (user.IsInRole("Patient"))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Patient");
            }
            await base.OnConnectedAsync();
        }
    }
}
