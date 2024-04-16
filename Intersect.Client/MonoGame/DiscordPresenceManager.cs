using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordRPC;
using Intersect.Client.Framework.Content;

namespace Intersect.Client.MonoGame;
public static class DiscordPresenceManager
{
    private static DiscordRpcClient client;

    public static void Initialize(string applicationId)
    {
        client = new DiscordRpcClient(applicationId);
        client.Initialize();
    }

    public static void UpdatePresence(string state)
    {
        {
            var presence = new RichPresence
            {
                State = state,
                Details = "Gra w Westerre",
                Timestamps = new Timestamps()
                {
                    Start = DateTime.UtcNow
                },
                Assets = new Assets
                {
                    LargeImageKey = "intersect-logo-qu", // Klucz obrazka do wyświetlenia
                    LargeImageText = "Westerre" // Tekst pod dużym obrazkiem
                }
            };

            client.SetPresence(presence);
        }
    }

    public static void Dispose()
    {
        {
            client.Dispose();
        }
    }
}
