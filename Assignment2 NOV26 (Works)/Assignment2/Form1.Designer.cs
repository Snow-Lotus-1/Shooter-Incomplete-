namespace Assignment2
{
    partial class Assignment2Form
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
            this.components = new System.ComponentModel.Container();
            this.tmrMovement = new System.Windows.Forms.Timer(this.components);
            this.txtEnemies = new System.Windows.Forms.TextBox();
            this.lblError = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tmrMovement
            // 
            this.tmrMovement.Enabled = true;
            this.tmrMovement.Interval = 1;
            this.tmrMovement.Tick += new System.EventHandler(this.tmrMovement_Tick);
            // 
            // txtEnemies
            // 
            this.txtEnemies.Location = new System.Drawing.Point(400, 12);
            this.txtEnemies.Name = "txtEnemies";
            this.txtEnemies.Size = new System.Drawing.Size(100, 20);
            this.txtEnemies.TabIndex = 0;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(539, 19);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(35, 13);
            this.lblError.TabIndex = 1;
            this.lblError.Text = "label1";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(328, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(57, 24);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "button1";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // Assignment2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 761);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.txtEnemies);
            this.DoubleBuffered = true;
            this.Name = "Assignment2Form";
            this.Text = "Assignment 2";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Assignment2Form_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Assignment2Form_KeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Assignment2Form_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Assignment2Form_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrMovement;
        private System.Windows.Forms.TextBox txtEnemies;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnStart;
    }
}

