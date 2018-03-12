using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace JelzBot.Modules {

    [Group("admin"), RequireOwner]
    public class AdminCommands : ModuleBase<SocketCommandContext> {
        [Command("commands")]
        public async Task Commands () {
            var embed = new EmbedBuilder();

            embed.WithTitle("Admin Commands");
            embed.AddField("user", "Displays all user information");
            embed.AddField("client", "Displays all client information");
            embed.AddField("guild", "Displays all guild information");
            embed.WithColor(Color.Red);

            await ReplyAsync("", false, embed.Build());
        }

        [Command("user")]
        public async Task UserAsync () {
            EmbedBuilder builder = new EmbedBuilder();

            var user = Context.User;

            var client = Context.Client;
            var guild = Context.Guild;

            var description = "";

            description += "user.AvatarId : " + user.AvatarId + "\n";
            description += "user.CreatedAt : " + user.CreatedAt + "\n";
            description += "user.Discriminator : " + user.Discriminator + "\n";
            description += "user.DiscriminatorValue : " + user.DiscriminatorValue + "\n";
            description += "user.Game : " + user.Game + "\n";
            description += "user.GetAvatarUrl() : " + user.GetAvatarUrl() + "\n";
            description += "user.GetHashCode() : " + user.GetHashCode() + "\n";
            description += "user.Id : " + user.Id + "\n";
            description += "user.IsBot : " + user.IsBot + "\n";
            description += "user.IsWebhook : " + user.IsWebhook + "\n";
            description += "user.Mention : " + user.Mention + "\n";
            description += "user.Status : " + user.Status + "\n";
            description += "user.Username : " + user.Username + "\n";

            builder.WithTitle("User")
                .WithDescription(description);


            await ReplyAsync("", false, builder.Build());
        }

        [Command("client")]
        public async Task ClientAsync () {
            EmbedBuilder builder = new EmbedBuilder();

            var client = Context.Client;

            var description = "";

            description += "client.ConnectionState : " + client.ConnectionState + "\n";
            description += "client.CurrentUser : " + client.CurrentUser + "\n";
            description += "client.DMChannels : " + client.DMChannels + "\n";
            description += "client.GroupChannels : " + client.GroupChannels + "\n";
            description += "client.Guilds : " + client.Guilds + "\n";
            description += "client.Latency : " + client.Latency + "\n";
            description += "client.LoginState : " + client.LoginState + "\n";
            description += "client.PrivateChannels : " + client.PrivateChannels + "\n";
            description += "client.ShardId : " + client.ShardId + "\n";
            description += "client.TokenType : " + client.TokenType + "\n";
            description += "client.VoiceRegions : " + client.VoiceRegions + "\n";

            builder.WithTitle("Client")
                .WithDescription(description);

            await ReplyAsync("", false, builder.Build());
        }

        [Command("guild")]
        public async Task GuildAsync () {
            EmbedBuilder builder = new EmbedBuilder();

            var guild = Context.Guild;

            var description = "";

            description += "guild.AFKChannel : " + guild.AFKChannel + "\n";
            description += "guild.AFKTimeout : " + guild.AFKTimeout + "\n";
            description += "guild.AudioClient : " + guild.AudioClient + "\n";
            description += "guild.Channels : " + guild.Channels + "\n";
            description += "guild.CreatedAt : " + guild.CreatedAt + "\n";
            description += "guild.CurrentUser : " + guild.CurrentUser + "\n";
            description += "guild.DefaultChannel : " + guild.DefaultChannel + "\n";
            description += "guild.DefaultMessageNotifications : " + guild.DefaultMessageNotifications + "\n";
            description += "guild.DownloadedMemberCount : " + guild.DownloadedMemberCount + "\n";
            description += "guild.DownloaderPromise : " + guild.DownloaderPromise + "\n";
            description += "guild.EmbedChannel : " + guild.EmbedChannel + "\n";
            description += "guild.Emotes : " + guild.Emotes + "\n";
            description += "guild.EveryoneRole : " + guild.EveryoneRole + "\n";
            description += "guild.Features : " + guild.Features + "\n";
            description += "guild.HasAllMembers : " + guild.HasAllMembers + "\n";
            description += "guild.IconId : " + guild.IconId + "\n";
            description += "guild.IconUrl : " + guild.IconUrl + "\n";
            description += "guild.Id : " + guild.Id + "\n";
            description += "guild.IsConnected : " + guild.IsConnected + "\n";
            description += "guild.IsEmbeddable : " + guild.IsEmbeddable + "\n";
            description += "guild.IsSynced : " + guild.IsSynced + "\n";
            description += "guild.MemberCount : " + guild.MemberCount + "\n";
            description += "guild.MfaLevel : " + guild.MfaLevel + "\n";
            description += "guild.Name : " + guild.Name + "\n";
            description += "guild.Owner : " + guild.Owner + "\n";
            description += "guild.OwnerId : " + guild.OwnerId + "\n";
            description += "guild.Roles : " + guild.Roles + "\n";
            description += "guild.SplashId : " + guild.SplashId + "\n";
            description += "guild.SplashUrl : " + guild.SplashUrl + "\n";
            description += "guild.TextChannels : " + guild.TextChannels + "\n";
            description += "guild.Users : " + guild.Users + "\n";
            description += "guild.VerificationLevel : " + guild.VerificationLevel + "\n";
            description += "guild.VoiceChannels : " + guild.VoiceChannels + "\n";
            description += "guild.VoiceRegionId : " + guild.VoiceRegionId + "\n";

            builder.WithTitle("Guild")
                .WithDescription(description);

            await ReplyAsync("", false, builder.Build());
        }
    }
}
