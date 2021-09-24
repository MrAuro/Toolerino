﻿
namespace Toolerino
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tb_channel = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.b_createClients = new System.Windows.Forms.Button();
			this.nud_connections = new System.Windows.Forms.NumericUpDown();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.nud_messageRepeat = new System.Windows.Forms.NumericUpDown();
			this.b_sendMessage = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.tb_message = new System.Windows.Forms.TextBox();
			this.nud_pyramidWidth = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.tb_pyramidMsg = new System.Windows.Forms.TextBox();
			this.b_sendPyramid = new System.Windows.Forms.Button();
			this.b_runList = new System.Windows.Forms.Button();
			this.tb_list = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.nud_chunkSize = new System.Windows.Forms.NumericUpDown();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.r_ban = new System.Windows.Forms.RadioButton();
			this.r_unban = new System.Windows.Forms.RadioButton();
			this.r_say = new System.Windows.Forms.RadioButton();
			this.tb_oauth = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nud_connections)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nud_messageRepeat)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nud_pyramidWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nud_chunkSize)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.tb_oauth);
			this.groupBox1.Controls.Add(this.nud_connections);
			this.groupBox1.Controls.Add(this.b_createClients);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.tb_channel);
			this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(13, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(445, 100);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Connection";
			// 
			// tb_channel
			// 
			this.tb_channel.Location = new System.Drawing.Point(9, 37);
			this.tb_channel.Name = "tb_channel";
			this.tb_channel.Size = new System.Drawing.Size(176, 23);
			this.tb_channel.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(6, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(51, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "Channel";
			// 
			// b_createClients
			// 
			this.b_createClients.Location = new System.Drawing.Point(9, 66);
			this.b_createClients.Name = "b_createClients";
			this.b_createClients.Size = new System.Drawing.Size(375, 23);
			this.b_createClients.TabIndex = 2;
			this.b_createClients.Text = "Create clients";
			this.b_createClients.UseVisualStyleBackColor = true;
			// 
			// nud_connections
			// 
			this.nud_connections.Location = new System.Drawing.Point(390, 66);
			this.nud_connections.Name = "nud_connections";
			this.nud_connections.Size = new System.Drawing.Size(49, 23);
			this.nud_connections.TabIndex = 3;
			this.nud_connections.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.r_say);
			this.groupBox2.Controls.Add(this.r_unban);
			this.groupBox2.Controls.Add(this.r_ban);
			this.groupBox2.Controls.Add(this.b_runList);
			this.groupBox2.Controls.Add(this.tb_list);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.nud_chunkSize);
			this.groupBox2.Controls.Add(this.b_sendPyramid);
			this.groupBox2.Controls.Add(this.tb_pyramidMsg);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.nud_pyramidWidth);
			this.groupBox2.Controls.Add(this.nud_messageRepeat);
			this.groupBox2.Controls.Add(this.b_sendMessage);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.tb_message);
			this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(13, 118);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(445, 306);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Messaging";
			// 
			// nud_messageRepeat
			// 
			this.nud_messageRepeat.Location = new System.Drawing.Point(390, 66);
			this.nud_messageRepeat.Name = "nud_messageRepeat";
			this.nud_messageRepeat.Size = new System.Drawing.Size(49, 23);
			this.nud_messageRepeat.TabIndex = 3;
			this.nud_messageRepeat.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// b_sendMessage
			// 
			this.b_sendMessage.Location = new System.Drawing.Point(9, 66);
			this.b_sendMessage.Name = "b_sendMessage";
			this.b_sendMessage.Size = new System.Drawing.Size(375, 23);
			this.b_sendMessage.TabIndex = 2;
			this.b_sendMessage.Text = "Send message(s)";
			this.b_sendMessage.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(6, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "Message";
			// 
			// tb_message
			// 
			this.tb_message.Location = new System.Drawing.Point(9, 37);
			this.tb_message.Name = "tb_message";
			this.tb_message.Size = new System.Drawing.Size(430, 23);
			this.tb_message.TabIndex = 0;
			// 
			// nud_pyramidWidth
			// 
			this.nud_pyramidWidth.Location = new System.Drawing.Point(390, 111);
			this.nud_pyramidWidth.Name = "nud_pyramidWidth";
			this.nud_pyramidWidth.Size = new System.Drawing.Size(49, 23);
			this.nud_pyramidWidth.TabIndex = 4;
			this.nud_pyramidWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(6, 92);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(51, 15);
			this.label3.TabIndex = 5;
			this.label3.Text = "Pyramid";
			// 
			// tb_pyramidMsg
			// 
			this.tb_pyramidMsg.Location = new System.Drawing.Point(9, 110);
			this.tb_pyramidMsg.Name = "tb_pyramidMsg";
			this.tb_pyramidMsg.Size = new System.Drawing.Size(375, 23);
			this.tb_pyramidMsg.TabIndex = 6;
			// 
			// b_sendPyramid
			// 
			this.b_sendPyramid.Location = new System.Drawing.Point(9, 139);
			this.b_sendPyramid.Name = "b_sendPyramid";
			this.b_sendPyramid.Size = new System.Drawing.Size(430, 23);
			this.b_sendPyramid.TabIndex = 7;
			this.b_sendPyramid.Text = "Send pyramid";
			this.b_sendPyramid.UseVisualStyleBackColor = true;
			// 
			// b_runList
			// 
			this.b_runList.Location = new System.Drawing.Point(9, 277);
			this.b_runList.Name = "b_runList";
			this.b_runList.Size = new System.Drawing.Size(333, 23);
			this.b_runList.TabIndex = 11;
			this.b_runList.Text = "Run list";
			this.b_runList.UseVisualStyleBackColor = true;
			// 
			// tb_list
			// 
			this.tb_list.Location = new System.Drawing.Point(9, 183);
			this.tb_list.Multiline = true;
			this.tb_list.Name = "tb_list";
			this.tb_list.Size = new System.Drawing.Size(333, 88);
			this.tb_list.TabIndex = 10;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(6, 165);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(25, 15);
			this.label4.TabIndex = 9;
			this.label4.Text = "File";
			// 
			// nud_chunkSize
			// 
			this.nud_chunkSize.Location = new System.Drawing.Point(348, 277);
			this.nud_chunkSize.Maximum = new decimal(new int[] {
            7500,
            0,
            0,
            0});
			this.nud_chunkSize.Name = "nud_chunkSize";
			this.nud_chunkSize.Size = new System.Drawing.Size(91, 23);
			this.nud_chunkSize.TabIndex = 8;
			this.nud_chunkSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(13, 431);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(445, 23);
			this.progressBar1.TabIndex = 5;
			// 
			// r_ban
			// 
			this.r_ban.AutoSize = true;
			this.r_ban.Location = new System.Drawing.Point(348, 184);
			this.r_ban.Name = "r_ban";
			this.r_ban.Size = new System.Drawing.Size(45, 19);
			this.r_ban.TabIndex = 12;
			this.r_ban.TabStop = true;
			this.r_ban.Text = "Ban";
			this.r_ban.UseVisualStyleBackColor = true;
			// 
			// r_unban
			// 
			this.r_unban.AutoSize = true;
			this.r_unban.Location = new System.Drawing.Point(348, 209);
			this.r_unban.Name = "r_unban";
			this.r_unban.Size = new System.Drawing.Size(60, 19);
			this.r_unban.TabIndex = 13;
			this.r_unban.TabStop = true;
			this.r_unban.Text = "Unban";
			this.r_unban.UseVisualStyleBackColor = true;
			// 
			// r_say
			// 
			this.r_say.AutoSize = true;
			this.r_say.Location = new System.Drawing.Point(348, 234);
			this.r_say.Name = "r_say";
			this.r_say.Size = new System.Drawing.Size(43, 19);
			this.r_say.TabIndex = 14;
			this.r_say.TabStop = true;
			this.r_say.Text = "Say";
			this.r_say.UseVisualStyleBackColor = true;
			// 
			// tb_oauth
			// 
			this.tb_oauth.Location = new System.Drawing.Point(191, 37);
			this.tb_oauth.Name = "tb_oauth";
			this.tb_oauth.PasswordChar = '*';
			this.tb_oauth.Size = new System.Drawing.Size(248, 23);
			this.tb_oauth.TabIndex = 4;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(188, 19);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(42, 15);
			this.label5.TabIndex = 5;
			this.label5.Text = "OAuth";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(470, 466);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "Form1";
			this.Text = "Toolerino";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nud_connections)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nud_messageRepeat)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nud_pyramidWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nud_chunkSize)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.NumericUpDown nud_connections;
		private System.Windows.Forms.Button b_createClients;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tb_channel;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton r_say;
		private System.Windows.Forms.RadioButton r_unban;
		private System.Windows.Forms.RadioButton r_ban;
		private System.Windows.Forms.Button b_runList;
		private System.Windows.Forms.TextBox tb_list;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown nud_chunkSize;
		private System.Windows.Forms.Button b_sendPyramid;
		private System.Windows.Forms.TextBox tb_pyramidMsg;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown nud_pyramidWidth;
		private System.Windows.Forms.NumericUpDown nud_messageRepeat;
		private System.Windows.Forms.Button b_sendMessage;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tb_message;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tb_oauth;
	}
}
