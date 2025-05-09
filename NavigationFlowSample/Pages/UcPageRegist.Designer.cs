namespace NavigationFlowSample.Pages
{
    partial class UcPageRegist
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            btnComplete = new Button();
            SuspendLayout();
            // 
            // btnComplete
            // 
            btnComplete.Anchor = AnchorStyles.Bottom;
            btnComplete.Font = new Font("メイリオ", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnComplete.Location = new Point(217, 490);
            btnComplete.Name = "btnComplete";
            btnComplete.Size = new Size(417, 64);
            btnComplete.TabIndex = 5;
            btnComplete.Text = "終了";
            btnComplete.UseVisualStyleBackColor = true;
            btnComplete.Click += btnComplete_Click;
            // 
            // UcPageRegist
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnComplete);
            Name = "UcPageRegist";
            Size = new Size(850, 569);
            ResumeLayout(false);
        }

        #endregion

        private Button btnComplete;
    }
}
