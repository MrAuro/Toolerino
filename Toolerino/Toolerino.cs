using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
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
			for (int i = 0; i < nud_connections.Value; i++)
			{
				Console.WriteLine($"Creating client {i}");
				clients.Add(new Client($"oauth:{OAuth.Replace("oauth:", "")}", tb_channel.Text));
			}
		}

		private void b_sendMessage_Click(object sender, EventArgs e)
		{
			if (clients.Count() == 0)
			{
				MessageBox.Show("No clients connected/created", "Toolerino", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			for (int i = 0; i < nud_messageRepeat.Value; i++)
			{
				getConnection().client.SendMessage(tb_channel.Text.Replace("#", ""), tb_message.Text);
			}
		}

		private void Toolerino_Load(object sender, EventArgs e)
		{
			ToolTip mainToolTip = new ToolTip();
			mainToolTip.AutoPopDelay = 5000;
			mainToolTip.InitialDelay = 1000;
			mainToolTip.ReshowDelay = 500;
			mainToolTip.ShowAlways = true;

			mainToolTip.SetToolTip(this.tb_channel, "The channel to send messages in");
			mainToolTip.SetToolTip(this.tb_oauth, "OAuth to sign in under");
			mainToolTip.SetToolTip(this.nud_connections, "How many connections to use");
			mainToolTip.SetToolTip(this.tb_message, "Message to send");
			mainToolTip.SetToolTip(this.nud_messageRepeat, "How many times to repeat the message");
			mainToolTip.SetToolTip(this.tb_pyramidMsg, "Message to pyramid");
			mainToolTip.SetToolTip(this.nud_pyramidWidth, "How wide the pyramid should be");
			mainToolTip.SetToolTip(this.tb_list, "List of messages to say");
			mainToolTip.SetToolTip(this.nud_chunkSize, "How large each chunk should be (30s delay)");

			if (!String.IsNullOrEmpty(Properties.Settings.Default.oauth))
			{
				OAuth = Properties.Settings.Default.oauth;
				tb_oauth.Text = OAuth;
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
			if (clients.Count() == 0)
			{
				MessageBox.Show("No clients connected/created", "Toolerino", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			int width = (int)nud_pyramidWidth.Value++;
			string msg = tb_pyramidMsg.Text;

			if (width * msg.Length > 500)
			{
				MessageBox.Show("Message is too long for the pyramid", "Toolerino", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				for (int i = 0; i < width; i++)
				{
					string line = "";
					for (int j = 1; j < i; j++)
					{
						line += $"{msg} ";
					}
					Thread.Sleep(100);
					getConnection().client.SendMessage(tb_channel.Text.Replace("#", ""), line);
				}

				for (int i = width - 1; i >= 0; i--)
				{
					string line = "";
					for (int j = 0; j < i; j++)
					{
						line += $"{msg} ";
					}
					Thread.Sleep(100);
					getConnection().client.SendMessage(tb_channel.Text.Replace("#", ""), line);
				}
			}
		}
	}
}