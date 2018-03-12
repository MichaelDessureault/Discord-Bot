using Discord.Commands;
using System.Threading.Tasks;

namespace JelzBot.Interface {
    interface IDictionaryCommands {
        Task AddOrUpdate (string key, [Remainder] string word = null);
        Task Remove (string key);
        Task Clear ();
        Task ShowAll ();
    }
}
