namespace LR6_CSH_Client
{
    partial class AuthorizForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btRegister = new System.Windows.Forms.Button();
            this.btLogIn = new System.Windows.Forms.Button();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelLogIn = new System.Windows.Forms.Panel();
            this.panelMainWin = new System.Windows.Forms.Panel();
            this.panelLogIn.SuspendLayout();
            this.SuspendLayout();
            // 
            // btRegister
            // 
            this.btRegister.BackColor = System.Drawing.Color.Gold;
            this.btRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btRegister.Location = new System.Drawing.Point(241, 144);
            this.btRegister.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btRegister.Name = "btRegister";
            this.btRegister.Size = new System.Drawing.Size(104, 51);
            this.btRegister.TabIndex = 0;
            this.btRegister.Text = "Sign up";
            this.btRegister.UseVisualStyleBackColor = false;
            this.btRegister.Click += new System.EventHandler(this.btRegister_Click);
            this.btRegister.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btRegister_MouseDown);
            // 
            // btLogIn
            // 
            this.btLogIn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btLogIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btLogIn.Location = new System.Drawing.Point(129, 144);
            this.btLogIn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btLogIn.Name = "btLogIn";
            this.btLogIn.Size = new System.Drawing.Size(104, 51);
            this.btLogIn.TabIndex = 1;
            this.btLogIn.Text = "Sing in";
            this.btLogIn.UseVisualStyleBackColor = false;
            this.btLogIn.Click += new System.EventHandler(this.btLogIn_Click);
            this.btLogIn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btLogIn_MouseClick);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(129, 104);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(215, 27);
            this.tbPassword.TabIndex = 2;
            // 
            // tbLogin
            // 
            this.tbLogin.Location = new System.Drawing.Point(129, 67);
            this.tbLogin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(215, 27);
            this.tbLogin.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 115);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Pasword";
            // 
            // panelLogIn
            // 
            this.panelLogIn.Controls.Add(this.tbPassword);
            this.panelLogIn.Controls.Add(this.label2);
            this.panelLogIn.Controls.Add(this.btRegister);
            this.panelLogIn.Controls.Add(this.label1);
            this.panelLogIn.Controls.Add(this.btLogIn);
            this.panelLogIn.Controls.Add(this.tbLogin);
            this.panelLogIn.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogIn.Location = new System.Drawing.Point(0, 0);
            this.panelLogIn.Name = "panelLogIn";
            this.panelLogIn.Size = new System.Drawing.Size(435, 229);
            this.panelLogIn.TabIndex = 6;
            // 
            // panelMainWin
            // 
            this.panelMainWin.Location = new System.Drawing.Point(0, 235);
            this.panelMainWin.Name = "panelMainWin";
            this.panelMainWin.Size = new System.Drawing.Size(435, 22);
            this.panelMainWin.TabIndex = 7;
            // 
            // AuthorizForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(435, 257);
            this.Controls.Add(this.panelMainWin);
            this.Controls.Add(this.panelLogIn);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AuthorizForm";
            this.Text = "Authorization";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AuthorizForm_FormClosing);
            this.panelLogIn.ResumeLayout(false);
            this.panelLogIn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btRegister;
        private System.Windows.Forms.Button btLogIn;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelLogIn;
        private System.Windows.Forms.Panel panelMainWin;
    }
}

