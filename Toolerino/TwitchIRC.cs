using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace Toolerino
{
	class Client
	{
		public TwitchClient client;
		public Boolean ready = false;

		public Client(String oauth, String channel, Int32 i)
		{
			Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Creating client {i}");
			ConnectionCredentials credentials = new ConnectionCredentials("oura_bot", "oauth:" + oauth.Replace("oauth:", ""));
			WebSocketClient customClient = new WebSocketClient();
			client = new TwitchClient(customClient);
			client.Initialize(credentials, channel);
			client.OnConnected += Client_OnConnected;
			client.OnJoinedChannel += Client_OnJoinedChannel;
			client.Connect();

			void Client_OnConnected(object sender, OnConnectedArgs e)
			{
				client.JoinChannel(channel, true);
			}
		}


		private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
		{
			this.ready = true;
			Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Joined {e.Channel}");
		}
	}
}
