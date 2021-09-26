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
using System.Runtime.InteropServices;


namespace Toolerino
{
	public partial class Toolerino : Form
	{
		string version = "1.1.2";

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool AllocConsole();

		List<Client> clients = new List<Client>();
		string OAuth;
		string Channel;

		int lastIndex = 0;
		private Client getConnection()
		{
			var readyClients = clients.Where(c => c.ready).ToArray();
			Console.WriteLine($"Getting a client out of {readyClients.Length} ready clients");
			lastIndex += 1;
			var pos = lastIndex % readyClients.Length;
			return readyClients[pos];
		}

		public Toolerino()
		{
			InitializeComponent();
			this.Text += $" {version}";
		}

		private void b_createClients_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < nud_connections.Value; i++)
			{
				Thread t = new Thread(() =>
				{
					var client = new Client($"oauth:{OAuth.Replace("oauth:", "")}", Channel, i);
					clients.Add(client);
				});
				t.Start();
			}

			// for (int i = 0; i < nud_connections.Value; i++)
			// {
			// 	Console.WriteLine($"Creating client {i}");
			// 	tb_Logs.AppendText($"Creating client {i}" + "\r\n");
			// 	clients.Add(new Client($"oauth:{OAuth.Replace("oauth:", "")}", Channel));
			// }

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
				var connection = getConnection();
				connection.client.SendRaw($"PRIVMSG #{Channel.ToLower()} :{tb_message.Text}");
				// getConnection().client.SendMessage(Channel, tb_message.Text);
			}
		}

		private void Toolerino_Load(object sender, EventArgs e)
		{
			AllocConsole();
			Console.WriteLine("Toolerino Loaded!");
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
				Console.WriteLine($"Sending first half of pyramid - #{Channel}");
				for (int i = 0; i < width; i++)
				{
					string line = "";
					for (int j = 1; j < i; j++)
					{
						line += $"{msg} ";
					}
					Thread.Sleep(100);
					var connection = getConnection();
					connection.client.SendRaw($"PRIVMSG #{Channel.ToLower()} :{line}");
				}

				Console.WriteLine($"Sending second half of pyramid - #{Channel}");
				for (int i = width - 1; i >= 0; i--)
				{
					string line = "";
					for (int j = 0; j < i; j++)
					{
						line += $"{msg} ";
					}
					Thread.Sleep(100);
					var connection = getConnection();
					connection.client.SendRaw($"PRIVMSG #{Channel.ToLower()} :{line}");
				}
			}
		}

		private void tb_channel_TextChanged(object sender, EventArgs e)
		{
			Channel = tb_channel.Text.Replace("#", "");
		}

		private void b_rejoin_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < clients.Count; i++)
			{
				clients[i].client.JoinChannel(tb_channel.Text);
			}
		}

		private void b_runList_Click(object sender, EventArgs e)
		{
			new Thread(() =>
			{
				if (clients.Count() == 0)
				{
					MessageBox.Show("No clients connected/created", "Toolerino", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				string[] lines = tb_list.Text.Split(new char[] { '\r', '\n' });
				if (lines.Length >= nud_chunkSize.Value)
				{
					// Chunk the list into arrays of arrays of lines
					List<string[]> chunks = new List<string[]>();
					int chunkSize = (int)nud_chunkSize.Value * 2;
					for (int i = 0; i < lines.Length; i += chunkSize)
					{
						chunks.Add(lines.Skip(i).Take(chunkSize).ToArray());
					}

					Console.WriteLine($"{chunks.Count} chunks created - #{Channel}");

					// Send each chunk
					foreach (string[] chunk in chunks)
					{
						foreach (string line in chunk)
						{
							if (r_ban.Checked)
							{
								var connection = getConnection();
								connection.client.SendRaw($"PRIVMSG #{Channel.ToLower()} :.ban {line}");
							}
							else if (r_unban.Checked)
							{
								var connection = getConnection();
								connection.client.SendRaw($"PRIVMSG #{Channel.ToLower()} :.unban {line}");
							}
							else if (r_say.Checked)
							{
								var connection = getConnection();
								connection.client.SendRaw($"PRIVMSG #{Channel.ToLower()} :{line}");
							}
						}
						// check if this is the last chunk
						if (chunks.IndexOf(chunk) != chunks.Count - 1)
						{
							Console.WriteLine($"Sleeping on chunk {chunks.IndexOf(chunk)}/{chunks.Count - 1} - #{Channel}");
							Thread.Sleep(30000);
						}
						else
						{
							Console.WriteLine($"{chunks.Count} chunks executed - #{Channel}");
						}
					}
				}
				else
				{
					Console.WriteLine($"Running list of {lines.Length} lines - #{Channel}");
					for (int i = 0; i < lines.Length; i++)
					{
						if (r_ban.Checked)
						{
							var connection = getConnection();
							connection.client.SendRaw($"PRIVMSG #{Channel.ToLower()} :.ban {lines[i]}");
						}
						else if (r_unban.Checked)
						{
							var connection = getConnection();
							connection.client.SendRaw($"PRIVMSG #{Channel.ToLower()} :.unban {lines[i]}");
						}
						else if (r_say.Checked)
						{
							var connection = getConnection();
							connection.client.SendRaw($"PRIVMSG #{Channel.ToLower()} :{lines[i]}");
						}
					}
				}
			}).Start();
		}
	}
}