﻿namespace LR6_CSH_Client.View
{
    partial class MainForm
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
            this.pActiveUsers = new System.Windows.Forms.Panel();
            this.dgvAllUsers = new System.Windows.Forms.DataGridView();
            this.Login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pCurrentUserInfo = new System.Windows.Forms.Panel();
            this.lbStatus = new System.Windows.Forms.Label();
            this.lbUserName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbInitial = new System.Windows.Forms.Label();
            this.lBoxMessages = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btSent = new System.Windows.Forms.Button();
            this.tbUserMessage = new System.Windows.Forms.TextBox();
            this.pActiveUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllUsers)).BeginInit();
            this.pCurrentUserInfo.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pActiveUsers
            // 
            this.pActiveUsers.Controls.Add(this.dgvAllUsers);
            this.pActiveUsers.Controls.Add(this.pCurrentUserInfo);
            this.pActiveUsers.Dock = System.Windows.Forms.DockStyle.Left;
            this.pActiveUsers.Location = new System.Drawing.Point(0, 0);
            this.pActiveUsers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pActiveUsers.Name = "pActiveUsers";
            this.pActiveUsers.Size = new System.Drawing.Size(235, 480);
            this.pActiveUsers.TabIndex = 0;
            // 
            // dgvAllUsers
            // 
            this.dgvAllUsers.AllowUserToAddRows = false;
            this.dgvAllUsers.AllowUserToDeleteRows = false;
            this.dgvAllUsers.AllowUserToResizeColumns = false;
            this.dgvAllUsers.AllowUserToResizeRows = false;
            this.dgvAllUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAllUsers.BackgroundColor = System.Drawing.Color.PeachPuff;
            this.dgvAllUsers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvAllUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllUsers.ColumnHeadersVisible = false;
            this.dgvAllUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Login});
            this.dgvAllUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAllUsers.GridColor = System.Drawing.Color.DarkOliveGreen;
            this.dgvAllUsers.Location = new System.Drawing.Point(0, 76);
            this.dgvAllUsers.MultiSelect = false;
            this.dgvAllUsers.Name = "dgvAllUsers";
            this.dgvAllUsers.ReadOnly = true;
            this.dgvAllUsers.RowHeadersVisible = false;
            this.dgvAllUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAllUsers.Size = new System.Drawing.Size(235, 404);
            this.dgvAllUsers.TabIndex = 1;
            this.dgvAllUsers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVAllUsers_CellClick);
            this.dgvAllUsers.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAllUsers_CellEnter);
            this.dgvAllUsers.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAllUsers_CellLeave);
            // 
            // Login
            // 
            this.Login.DataPropertyName = "Login";
            this.Login.FillWeight = 149.2386F;
            this.Login.HeaderText = "Column1";
            this.Login.Name = "Login";
            this.Login.ReadOnly = true;
            // 
            // pCurrentUserInfo
            // 
            this.pCurrentUserInfo.BackColor = System.Drawing.Color.Cornsilk;
            this.pCurrentUserInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pCurrentUserInfo.Controls.Add(this.lbStatus);
            this.pCurrentUserInfo.Controls.Add(this.lbUserName);
            this.pCurrentUserInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pCurrentUserInfo.Location = new System.Drawing.Point(0, 0);
            this.pCurrentUserInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pCurrentUserInfo.Name = "pCurrentUserInfo";
            this.pCurrentUserInfo.Size = new System.Drawing.Size(235, 76);
            this.pCurrentUserInfo.TabIndex = 0;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbStatus.Location = new System.Drawing.Point(4, 43);
            this.lbStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(58, 20);
            this.lbStatus.TabIndex = 1;
            this.lbStatus.Text = "Online";
            // 
            // lbUserName
            // 
            this.lbUserName.AutoSize = true;
            this.lbUserName.Location = new System.Drawing.Point(4, 9);
            this.lbUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbUserName.Name = "lbUserName";
            this.lbUserName.Size = new System.Drawing.Size(51, 20);
            this.lbUserName.TabIndex = 0;
            this.lbUserName.Text = "Login";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.panel1.Controls.Add(this.lbInitial);
            this.panel1.Controls.Add(this.lBoxMessages);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(235, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 426);
            this.panel1.TabIndex = 1;
            // 
            // lbInitial
            // 
            this.lbInitial.AutoSize = true;
            this.lbInitial.Location = new System.Drawing.Point(80, 215);
            this.lbInitial.Name = "lbInitial";
            this.lbInitial.Size = new System.Drawing.Size(149, 20);
            this.lbInitial.TabIndex = 2;
            this.lbInitial.Text = "Choose the person";
            // 
            // lBoxMessages
            // 
            this.lBoxMessages.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.lBoxMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lBoxMessages.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lBoxMessages.FormattingEnabled = true;
            this.lBoxMessages.ItemHeight = 15;
            this.lBoxMessages.Location = new System.Drawing.Point(0, 0);
            this.lBoxMessages.Name = "lBoxMessages";
            this.lBoxMessages.Size = new System.Drawing.Size(302, 426);
            this.lBoxMessages.TabIndex = 1;
            this.lBoxMessages.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lBoxMessages_DrawItem);
            this.lBoxMessages.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lBoxMessages_MeasureItem);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.btSent);
            this.panel2.Controls.Add(this.tbUserMessage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(235, 426);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(302, 54);
            this.panel2.TabIndex = 2;
            // 
            // btSent
            // 
            this.btSent.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btSent.Dock = System.Windows.Forms.DockStyle.Top;
            this.btSent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSent.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSent.Location = new System.Drawing.Point(249, 0);
            this.btSent.Name = "btSent";
            this.btSent.Size = new System.Drawing.Size(53, 52);
            this.btSent.TabIndex = 1;
            this.btSent.Text = "Sent";
            this.btSent.UseVisualStyleBackColor = false;
            this.btSent.Click += new System.EventHandler(this.btSent_Click);
            // 
            // tbUserMessage
            // 
            this.tbUserMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbUserMessage.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbUserMessage.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tbUserMessage.Location = new System.Drawing.Point(0, 0);
            this.tbUserMessage.Multiline = true;
            this.tbUserMessage.Name = "tbUserMessage";
            this.tbUserMessage.Size = new System.Drawing.Size(249, 54);
            this.tbUserMessage.TabIndex = 0;
            this.tbUserMessage.Text = "type a message...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 480);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pActiveUsers);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Your Chats";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pActiveUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllUsers)).EndInit();
            this.pCurrentUserInfo.ResumeLayout(false);
            this.pCurrentUserInfo.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pActiveUsers;
        private System.Windows.Forms.DataGridView dgvAllUsers;
        private System.Windows.Forms.Panel pCurrentUserInfo;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label lbUserName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tbUserMessage;
        private System.Windows.Forms.Button btSent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Login;
        private System.Windows.Forms.ListBox lBoxMessages;
        private System.Windows.Forms.Label lbInitial;
    }
}