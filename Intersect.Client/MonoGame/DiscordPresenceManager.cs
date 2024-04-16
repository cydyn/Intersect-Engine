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
    private static bool isUpdatingPresence = false;
    private static string currentState = "W grze";

    public static void Initialize(string applicationId)
    {
        client = new DiscordRpcClient(applicationId);
        client.Initialize();
    }

    public static void UpdatePresence(string state)
    {
        currentState = state;
        UpdatePresenceInternal();
    }

    private static async void UpdatePresenceInternal()
    {
        if (!isUpdatingPresence)
        {
            isUpdatingPresence = true;

            try
            {
                var presence = new RichPresence
                {
                    State = currentState,
                    Details = "Gra w Westerre",
                    Assets = new Assets
                    {
                        LargeImageKey = "westerre",
                        LargeImageText = "Westerre",
                        SmallImageKey = "gif",  // Klucz małego obrazka GIF
                        SmallImageText = "Verified" // Tekst wyświetlany po najechaniu na obrazek
                    },
                    Buttons = new Button[]
                    {
                        new Button() { Label = "Dołącz do nas na Discord!", Url = "https://discord.gg/ztKp93zvzb" },
                        new Button() { Label = "Odwiedź naszą stronę", Url = "https://westerre.pl" }
                    }
                };

                client.SetPresence(presence);

                // Poczekaj przez 15 sekund, a następnie zaktualizuj obecność ponownie
                await Task.Delay(15000);

                isUpdatingPresence = false;

                // Jeśli stan się nie zmienił podczas oczekiwania, zaktualizuj obecność ponownie
                if (currentState == presence.State)
                {
                    UpdatePresenceInternal();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas aktualizowania obecności Discord: {ex.Message}");
                isUpdatingPresence = false;
            }
        }
    }

    public static void Dispose()
    {
        client?.Dispose();
    }
}
