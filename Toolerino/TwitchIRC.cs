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
		public Boolean ready;

		public Client(String oauth, String channel)
		{
			ConnectionCredentials credentials = new ConnectionCredentials("oura_bot", "oauth:" + oauth.Replace("oauth:", ""));
			WebSocketClient customClient = new WebSocketClient();
			client = new TwitchClient(customClient);
			client.Initialize(credentials, channel);
			client.OnConnected += Client_OnConnected;
			client.Connect();
		}

		private void Client_OnConnected(object sender, OnConnectedArgs e)
		{
			Console.WriteLine($"Connected to {e.AutoJoinChannel}");
			this.ready = true;
		}
	}
}
