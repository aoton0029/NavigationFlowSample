namespace NavigationFlowSample.Pages
{
    partial class UcPageNumOfProductsPerBox
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
            btnCancel = new Button();
            btnNext = new Button();
            btnBack = new Button();
            nudNumOfProductsPerBox = new NumericUpDown();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)nudNumOfProductsPerBox).BeginInit();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("メイリオ", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnCancel.Location = new Point(0, 0);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(127, 44);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "キャンセル";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnNext
            // 
            btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnNext.Font = new Font("メイリオ", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnNext.Location = new Point(605, 508);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(127, 44);
            btnNext.TabIndex = 7;
            btnNext.Text = "次へ";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // btnBack
            // 
            btnBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnBack.Font = new Font("メイリオ", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnBack.Location = new Point(0, 508);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(127, 44);
            btnBack.TabIndex = 6;
            btnBack.Text = "戻る";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // nudNumOfProductsPerBox
            // 
            nudNumOfProductsPerBox.Font = new Font("メイリオ", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            nudNumOfProductsPerBox.Location = new Point(286, 208);
            nudNumOfProductsPerBox.Name = "nudNumOfProductsPerBox";
            nudNumOfProductsPerBox.Size = new Size(120, 60);
            nudNumOfProductsPerBox.TabIndex = 10;
            nudNumOfProductsPerBox.TextAlign = HorizontalAlignment.Right;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("メイリオ", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label1.Location = new Point(412, 222);
            label1.Name = "label1";
            label1.Size = new Size(39, 36);
            label1.TabIndex = 9;
            label1.Text = "台";
            // 
            // UcPageNumOfProductsPerBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(nudNumOfProductsPerBox);
            Controls.Add(label1);
            Controls.Add(btnNext);
            Controls.Add(btnBack);
            Controls.Add(btnCancel);
            Name = "UcPageNumOfProductsPerBox";
            Size = new Size(732, 552);
            ((System.ComponentModel.ISupportInitialize)nudNumOfProductsPerBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancel;
        private Button btnNext;
        private Button btnBack;
        private NumericUpDown nudNumOfProductsPerBox;
        private Label label1;
    }
}
