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
		string OAuth;

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
				clients.Add(new Client($"oauth:{OAuth.Replace("oauth:", "")}", "auror6s"));
			}
		}

		private void b_sendMessage_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < nud_messageRepeat.Value; i++)
			{
				getConnection().client.SendMessage(tb_channel.Text.Replace("#", ""), tb_message.Text);
			}
		}

		private void Toolerino_Load(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(Properties.Settings.Default.oauth))
			{
				OAuth = Properties.Settings.Default.oauth;
				tb_oauth.Text = OAuth;
				Console.WriteLine(Properties.Settings.Default.oauth);
			}
		}

		private void Toolerino_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!String.IsNullOrEmpty(Properties.Settings.Default.oauth))
			{
				Properties.Settings.Default.oauth = OAuth;
				Properties.Settings.Default.Save();
			}
		}

		private void tb_oauth_TextChanged(object sender, EventArgs e)
		{
			OAuth = tb_oauth.Text;
		}

		private void b_sendPyramid_Click(object sender, EventArgs e)
		{

		}
	}
}
