namespace AvorionServerManager.Setup
{
    partial class SetupHelperForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupHelperForm));
            this.helperRichTextBox = new System.Windows.Forms.RichTextBox();
            this.nextStepButton = new System.Windows.Forms.Button();
            this.previousStepButton = new System.Windows.Forms.Button();
            this.skipSetipButton = new System.Windows.Forms.Button();
            this.doItButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // helperRichTextBox
            // 
            this.helperRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.helperRichTextBox.Location = new System.Drawing.Point(12, 12);
            this.helperRichTextBox.Name = "helperRichTextBox";
            this.helperRichTextBox.ReadOnly = true;
            this.helperRichTextBox.Size = new System.Drawing.Size(776, 359);
            this.helperRichTextBox.TabIndex = 0;
            this.helperRichTextBox.Text = "";
            // 
            // nextStepButton
            // 
            this.nextStepButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextStepButton.Location = new System.Drawing.Point(695, 408);
            this.nextStepButton.Name = "nextStepButton";
            this.nextStepButton.Size = new System.Drawing.Size(93, 23);
            this.nextStepButton.TabIndex = 1;
            this.nextStepButton.Text = "Next Step";
            this.nextStepButton.UseVisualStyleBackColor = true;
            this.nextStepButton.Click += new System.EventHandler(this.nextStepButton_Click);
            // 
            // previousStepButton
            // 
            this.previousStepButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.previousStepButton.Location = new System.Drawing.Point(12, 408);
            this.previousStepButton.Name = "previousStepButton";
            this.previousStepButton.Size = new System.Drawing.Size(93, 23);
            this.previousStepButton.TabIndex = 2;
            this.previousStepButton.Text = "Previous Step";
            this.previousStepButton.UseVisualStyleBackColor = true;
            this.previousStepButton.Click += new System.EventHandler(this.previousStepButton_Click);
            // 
            // skipSetipButton
            // 
            this.skipSetipButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.skipSetipButton.Location = new System.Drawing.Point(349, 408);
            this.skipSetipButton.Name = "skipSetipButton";
            this.skipSetipButton.Size = new System.Drawing.Size(93, 23);
            this.skipSetipButton.TabIndex = 3;
            this.skipSetipButton.Text = "Skip Setup Help";
            this.skipSetipButton.UseVisualStyleBackColor = true;
            this.skipSetipButton.Click += new System.EventHandler(this.skipSetipButton_Click);
            // 
            // doItButton
            // 
            this.doItButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.doItButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.doItButton.Location = new System.Drawing.Point(273, 377);
            this.doItButton.Name = "doItButton";
            this.doItButton.Size = new System.Drawing.Size(249, 23);
            this.doItButton.TabIndex = 4;
            this.doItButton.Text = "Complete this step for me";
            this.doItButton.UseVisualStyleBackColor = true;
            this.doItButton.Click += new System.EventHandler(this.doItButton_Click);
            // 
            // SetupHelperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 443);
            this.Controls.Add(this.doItButton);
            this.Controls.Add(this.skipSetipButton);
            this.Controls.Add(this.previousStepButton);
            this.Controls.Add(this.nextStepButton);
            this.Controls.Add(this.helperRichTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetupHelperForm";
            this.Text = "Avorion Server Manager Setup Helper";
            this.Load += new System.EventHandler(this.SetupHelperForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox helperRichTextBox;
        private System.Windows.Forms.Button nextStepButton;
        private System.Windows.Forms.Button previousStepButton;
        private System.Windows.Forms.Button skipSetipButton;
        private System.Windows.Forms.Button doItButton;
    }
}