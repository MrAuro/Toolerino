using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;


namespace Toolerino
{
	public partial class Toolerino : Form
	{
		List<Client> clients = new List<Client>();

		int lastIndex = 0;
		private Client getConnection()
		{
			var readyClients = clients.Where(c => c.ready).ToArray();
			Console.WriteLine(readyClients.Length);
			lastIndex += 1;
			var pos = lastIndex % readyClients.Length;
			return readyClients[pos];
		}

		public Toolerino()
		{
			InitializeComponent();
		}

		private void b_createClients_Click(object sender, EventArgs e)
		{
			Console.WriteLine("click");
			for (int i = 0; i < nud_connections.Value; i++)
			{
				Console.WriteLine($"Creating client {i}");
				clients.Add(new Client("oauth:3r2tg5y02afb1drqph7eumkmk4n29x", "auror6s"));
			}
		}

		private void b_sendMessage_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < nud_messageRepeat.Value; i++)
			{
				getConnection().client.SendMessage(tb_channel.Text.Replace("#", ""), tb_message.Text);
			}
		}
	}
}
