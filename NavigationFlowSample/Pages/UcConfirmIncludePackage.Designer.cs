namespace NavigationFlowSample.Pages
{
    partial class UcConfirmIncludePackage
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
            btnYes = new Button();
            btnNo = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // btnYes
            // 
            btnYes.Anchor = AnchorStyles.Top;
            btnYes.Font = new Font("メイリオ", 21.75F);
            btnYes.Location = new Point(443, 165);
            btnYes.Name = "btnYes";
            btnYes.Size = new Size(255, 172);
            btnYes.TabIndex = 0;
            btnYes.Text = "はい";
            btnYes.UseVisualStyleBackColor = true;
            btnYes.Click += btnYes_Click;
            // 
            // btnNo
            // 
            btnNo.Anchor = AnchorStyles.Top;
            btnNo.Font = new Font("メイリオ", 21.75F);
            btnNo.Location = new Point(127, 165);
            btnNo.Name = "btnNo";
            btnNo.Size = new Size(255, 172);
            btnNo.TabIndex = 0;
            btnNo.Text = "いいえ";
            btnNo.UseVisualStyleBackColor = true;
            btnNo.Click += btnNo_Click;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("メイリオ", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnCancel.Location = new Point(0, 0);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(127, 44);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "キャンセル";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // UcConfirmIncludePackage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnCancel);
            Controls.Add(btnNo);
            Controls.Add(btnYes);
            Name = "UcConfirmIncludePackage";
            Size = new Size(825, 503);
            ResumeLayout(false);
        }

        #endregion

        private Button btnYes;
        private Button btnNo;
        private Button btnCancel;
    }
}
