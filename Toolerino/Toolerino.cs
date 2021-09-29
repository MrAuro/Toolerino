using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;

namespace Toolerino
{
	public partial class Toolerino : Form
	{
		string version = "1.3.1";
		string accessToken = "";

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
			Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} FETCHING CLIENT - {readyClients.Length} TOTAL");
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
			Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Toolerino Loaded!");
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
				Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Sending first half of pyramid - #{Channel}");
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

				Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Sending second half of pyramid - #{Channel}");
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

		private async void b_runList_Click(object sender, EventArgs e)
		{

			if (r_block.Checked)
			{
				if (!Properties.Settings.Default.blockWarning)
				{
					Properties.Settings.Default.blockWarning = true;
					Properties.Settings.Default.Save();
					if (MessageBox.Show("Blocking bots may decrease your follow count. Blocks may take some time to appear on Twitch. Do not run any huge lists with this.\n\nAre you sure you want to continue?", "Toolerino", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
					{
						return;
					}
				}

				string[] loginList = tb_list.Text.Split(new char[] { '\r', '\n' }).Select(x => x.Trim()).Where(x => !String.IsNullOrEmpty(x)).ToArray();

				HttpClient httpClient = new HttpClient();
				httpClient.DefaultRequestHeaders.Clear();

				if (string.IsNullOrWhiteSpace(accessToken))
				{
					System.Diagnostics.Process.Start("https://toolerino-oauth.mrauro.dev");
					accessToken = Interaction.InputBox("Please enter the access token the webpage that just opened (https://toolerino-oauth.mrauro.dev/)", "Toolerino", "");
					if (string.IsNullOrWhiteSpace(accessToken)) return;
				}


				httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
				httpClient.DefaultRequestHeaders.Add("Client-Id", "us5x1xwywro4qbi2fqba536ynjd99d");

				List<string> userIds = new List<string> { };
				if (loginList.Length > 100)
				{
					int chunkSize = 100;
					int chunkCount = (int)Math.Ceiling((double)loginList.Length / chunkSize);
					Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Chunking user list into {chunkCount} chunks");
					for (int i = 0; i < chunkCount; i++)
					{
						string[] chunk = loginList.Skip(i * chunkSize).Take(chunkSize).ToArray();
						string chunkString = string.Join("&login=", chunk);
						string url = "https://api.twitch.tv/helix/users?login=" + chunkString;
						// Console.WriteLine(url);
						var response = await httpClient.GetAsync(url);
						var responseString = await response.Content.ReadAsStringAsync();
						var responseJson = JsonConvert.DeserializeObject<dynamic>(responseString);

						foreach (var user in responseJson.data)
						{
							userIds.Add((string)user.id);
						}

					}
				}
				else
				{
					Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Fetching user IDs");
					string url = $"https://api.twitch.tv/helix/users?login=" + String.Join("&login=", loginList);
					// Console.WriteLine(url);
					var response = await httpClient.GetAsync(url);
					var responseString = await response.Content.ReadAsStringAsync();
					var responseJson = JsonConvert.DeserializeObject<dynamic>(responseString);
					foreach (var user in responseJson.data)
					{
						userIds.Add((string)user.id);
					}
				}

				// Console.WriteLine("USERIDS");
				// Console.WriteLine(String.Join("\n", userIds));
				Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Found {userIds.Count} valid user IDs");

				// block each user
				Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Blocking users");
				foreach (string userId in userIds)
				{
					var response = await httpClient.PutAsync($"https://api.twitch.tv/helix/users/blocks?target_user_id={userId}", null);
					// check if the response is not 204, if so, print the error
					if (response.StatusCode != HttpStatusCode.NoContent)
					{
						var responseString = await response.Content.ReadAsStringAsync();
						var responseJson = JsonConvert.DeserializeObject<dynamic>(responseString);
						Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Error blocking user {userId}");
						Console.WriteLine(responseJson.message);
					}
					else
					{
						Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Blocked user {userId}");
					}

					// check if the bucket is empty, if so, wait for the reset time
					// -- !! THIS IS UNTESTED !! --
					if (response.Headers.Contains("ratelimit-remaining"))
					{
						int remaining = int.Parse(response.Headers.GetValues("ratelimit-remaining").First());
						if (remaining == 0)
						{
							int reset = int.Parse(response.Headers.GetValues("ratelimit-reset").First());
							DateTime resetTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(reset);
							Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Waiting for reset time {resetTime.ToString("HH:mm:ss.fff")}");
							await Task.Delay(resetTime - DateTime.Now);
						}
					}
				}
			}
			else
			{
				new Thread(() =>
				{
					if (clients.Count() == 0)
					{
						MessageBox.Show("No clients connected/created", "Toolerino", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}

					string[] lines = tb_list.Text.Split(new char[] { '\r', '\n' }).Where(x => !String.IsNullOrEmpty(x)).ToArray();
					if (lines.Length >= nud_chunkSize.Value)
					{
						// Chunk the list into arrays of arrays of lines
						List<string[]> chunks = new List<string[]>();
						int chunkSize = (int)nud_chunkSize.Value * 2;
						for (int i = 0; i < lines.Length; i += chunkSize)
						{
							chunks.Add(lines.Skip(i).Take(chunkSize).ToArray());
						}

						Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} {chunks.Count} chunks created - #{Channel}");

						pb_file.BeginInvoke((Action)delegate ()
						{
							pb_file.Value = 0;
							pb_file.Minimum = 0;
							pb_file.Maximum = chunks.Count;
						});

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

							pb_file.BeginInvoke((Action)delegate ()
							{
								pb_file.Value = chunks.IndexOf(chunk) + 1;
							});
							// check if this is the last chunk
							if (chunks.IndexOf(chunk) != chunks.Count - 1)
							{
								Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Sleeping on chunk {chunks.IndexOf(chunk)}/{chunks.Count - 1} - #{Channel}");
								Thread.Sleep(30000);
							}
							else
							{
								Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} {chunks.Count} chunks executed - #{Channel}");
							}
						}
					}
					else
					{
						Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Running list of {lines.Length} lines - #{Channel}");
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
}