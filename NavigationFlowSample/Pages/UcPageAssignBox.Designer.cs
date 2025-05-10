namespace NavigationFlowSample.Pages
{
    partial class UcPageAssignBox
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
            btnBack = new Button();
            btnNext = new Button();
            rdbSortAsc = new RadioButton();
            rdbSortDesc = new RadioButton();
            pnlSort = new Panel();
            panel1 = new Panel();
            rdbRemaindAtStart = new RadioButton();
            rdbRemaindAtEnd = new RadioButton();
            dataGridView1 = new DataGridView();
            ColumnBoxNumber = new DataGridViewTextBoxColumn();
            ColumnNumOfProducts = new DataGridViewTextBoxColumn();
            ColumnDisplaySerialNo = new DataGridViewTextBoxColumn();
            pnlSort.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("メイリオ", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnCancel.Location = new Point(0, 0);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(127, 44);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "キャンセル";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnBack
            // 
            btnBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnBack.Font = new Font("メイリオ", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnBack.Location = new Point(0, 435);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(127, 44);
            btnBack.TabIndex = 3;
            btnBack.Text = "戻る";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnNext
            // 
            btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnNext.Font = new Font("メイリオ", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnNext.Location = new Point(591, 435);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(127, 44);
            btnNext.TabIndex = 4;
            btnNext.Text = "次へ";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // rdbSortAsc
            // 
            rdbSortAsc.Appearance = Appearance.Button;
            rdbSortAsc.Checked = true;
            rdbSortAsc.Font = new Font("メイリオ", 14.25F);
            rdbSortAsc.Location = new Point(0, 0);
            rdbSortAsc.Name = "rdbSortAsc";
            rdbSortAsc.Size = new Size(104, 47);
            rdbSortAsc.TabIndex = 5;
            rdbSortAsc.TabStop = true;
            rdbSortAsc.Text = "ASC";
            rdbSortAsc.TextAlign = ContentAlignment.MiddleCenter;
            rdbSortAsc.UseVisualStyleBackColor = true;
            rdbSortAsc.CheckedChanged += rdbSortAsc_CheckedChanged;
            // 
            // rdbSortDesc
            // 
            rdbSortDesc.Appearance = Appearance.Button;
            rdbSortDesc.Font = new Font("メイリオ", 14.25F);
            rdbSortDesc.Location = new Point(104, 0);
            rdbSortDesc.Name = "rdbSortDesc";
            rdbSortDesc.Size = new Size(104, 47);
            rdbSortDesc.TabIndex = 6;
            rdbSortDesc.TabStop = true;
            rdbSortDesc.Text = "DESC";
            rdbSortDesc.TextAlign = ContentAlignment.MiddleCenter;
            rdbSortDesc.UseVisualStyleBackColor = true;
            // 
            // pnlSort
            // 
            pnlSort.Controls.Add(rdbSortAsc);
            pnlSort.Controls.Add(rdbSortDesc);
            pnlSort.Location = new Point(22, 78);
            pnlSort.Name = "pnlSort";
            pnlSort.Size = new Size(208, 48);
            pnlSort.TabIndex = 7;
            // 
            // panel1
            // 
            panel1.Controls.Add(rdbRemaindAtStart);
            panel1.Controls.Add(rdbRemaindAtEnd);
            panel1.Location = new Point(22, 179);
            panel1.Name = "panel1";
            panel1.Size = new Size(208, 48);
            panel1.TabIndex = 8;
            // 
            // rdbRemaindAtStart
            // 
            rdbRemaindAtStart.Appearance = Appearance.Button;
            rdbRemaindAtStart.Checked = true;
            rdbRemaindAtStart.Font = new Font("メイリオ", 14.25F);
            rdbRemaindAtStart.Location = new Point(0, 0);
            rdbRemaindAtStart.Name = "rdbRemaindAtStart";
            rdbRemaindAtStart.Size = new Size(104, 47);
            rdbRemaindAtStart.TabIndex = 5;
            rdbRemaindAtStart.TabStop = true;
            rdbRemaindAtStart.Text = "AtStart";
            rdbRemaindAtStart.TextAlign = ContentAlignment.MiddleCenter;
            rdbRemaindAtStart.UseVisualStyleBackColor = true;
            rdbRemaindAtStart.CheckedChanged += rdbRemaindAtStart_CheckedChanged;
            // 
            // rdbRemaindAtEnd
            // 
            rdbRemaindAtEnd.Appearance = Appearance.Button;
            rdbRemaindAtEnd.Font = new Font("メイリオ", 14.25F);
            rdbRemaindAtEnd.Location = new Point(104, 0);
            rdbRemaindAtEnd.Name = "rdbRemaindAtEnd";
            rdbRemaindAtEnd.Size = new Size(104, 47);
            rdbRemaindAtEnd.TabIndex = 6;
            rdbRemaindAtEnd.TabStop = true;
            rdbRemaindAtEnd.Text = "AtEnd";
            rdbRemaindAtEnd.TextAlign = ContentAlignment.MiddleCenter;
            rdbRemaindAtEnd.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { ColumnBoxNumber, ColumnNumOfProducts, ColumnDisplaySerialNo });
            dataGridView1.Location = new Point(236, 78);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(456, 334);
            dataGridView1.TabIndex = 9;
            // 
            // ColumnBoxNumber
            // 
            ColumnBoxNumber.DataPropertyName = "BoxNumber";
            ColumnBoxNumber.HeaderText = "BoxNumber";
            ColumnBoxNumber.Name = "ColumnBoxNumber";
            // 
            // ColumnNumOfProducts
            // 
            ColumnNumOfProducts.DataPropertyName = "NumOfProducts";
            ColumnNumOfProducts.HeaderText = "NumOfProducts";
            ColumnNumOfProducts.Name = "ColumnNumOfProducts";
            // 
            // ColumnDisplaySerialNo
            // 
            ColumnDisplaySerialNo.DataPropertyName = "DisplaySerialNo";
            ColumnDisplaySerialNo.HeaderText = "DisplaySerialNo";
            ColumnDisplaySerialNo.Name = "ColumnDisplaySerialNo";
            ColumnDisplaySerialNo.Width = 200;
            // 
            // UcPageAssignBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Controls.Add(pnlSort);
            Controls.Add(btnNext);
            Controls.Add(btnBack);
            Controls.Add(btnCancel);
            Name = "UcPageAssignBox";
            Size = new Size(718, 479);
            pnlSort.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnCancel;
        private Button btnBack;
        private Button btnNext;
        private RadioButton rdbSortAsc;
        private RadioButton rdbSortDesc;
        private Panel pnlSort;
        private Panel panel1;
        private RadioButton rdbRemaindAtStart;
        private RadioButton rdbRemaindAtEnd;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn ColumnBoxNumber;
        private DataGridViewTextBoxColumn ColumnNumOfProducts;
        private DataGridViewTextBoxColumn ColumnDisplaySerialNo;
    }
}
