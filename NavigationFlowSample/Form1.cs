using NavigationFlowSample.Core;
using NavigationFlowSample.Models;
using NavigationFlowSample.Pages;

namespace NavigationFlowSample
{
    public partial class Form1 : Form
    {
        ServiceProvider _provider;
        RegistData _registData;
        NavigationFlowService _nav;
        SnapshotManager<RegistData> _snapshotManager;

        public Form1()
        {
            InitializeComponent();
            _provider = new ServiceProvider();
            _registData = new RegistData();
            _snapshotManager = new SnapshotManager<RegistData>(_registData);
            _nav = new NavigationFlowService(_provider, this, null, OnComplete, OnCancel, OnTerminate);
            _nav.PreNavigatedEvent += PreNavigatedEvent;
            _nav.PostNavigatedEvent += PostNavigatedEvent;
            _provider.RegisterSingleton(_provider);
            _provider.RegisterSingleton(_nav);
            _provider.RegisterSingleton(new UcPageLogin(_provider, _registData));
            _provider.RegisterSingleton(new UcPageNumOfAllBoxes(_provider, _registData));
            _provider.RegisterSingleton(new UcPageOrderInfo(_provider, _registData));
            _provider.RegisterSingleton(new UcPageNumOfProductsPerBox(_provider, _registData));
            _provider.RegisterSingleton(new UcPageAssignBox(_provider, _registData));
            _provider.RegisterSingleton(new UcConfirmIncludePackage(_provider, _registData));
            _provider.RegisterSingleton(new UcPageRegist(_provider, _registData));
        }

        private void PreNavigatedEvent(object? sender, NavigationService.NavigationEventArgs e)
        {
            try
            {
                switch (e.ActionType)
                {
                    case NavigateActionType.Next:
                        // ����ʂɐi�ލۂɃX�i�b�v�V���b�g��ۑ�
                        _snapshotManager.SaveSnapshot();
                        System.Diagnostics.Debug.WriteLine($"�X�i�b�v�V���b�g�ۑ�: {e.FromPageType?.Name ?? "�s��"} �� {e.ToPageType?.Name ?? "�s��"}");
                        break;

                    case NavigateActionType.Back:
                        // �O�̉�ʂɖ߂�ꍇ�A���O�̃X�i�b�v�V���b�g�𕜌�
                        if (_snapshotManager.CanUndo)
                        {
                            bool result = _snapshotManager.Undo();
                            System.Diagnostics.Debug.WriteLine($"�X�i�b�v�V���b�g����: {e.FromPageType?.Name ?? "�s��"} �� {e.ToPageType?.Name ?? "�s��"}, ����: {(result ? "����" : "���s")}");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("�x��: �߂鑀�삪�v������܂������A�����\�ȃX�i�b�v�V���b�g������܂���");
                        }
                        break;

                    case NavigateActionType.Cancel:
                    case NavigateActionType.Complete:
                    case NavigateActionType.Terminate:
                        // �����̃A�N�V�����ł͓��ʂȏ����͕s�v�iOnComplete/OnCancel�ŃN���A���������{�j
                        System.Diagnostics.Debug.WriteLine($"���[�N�t���[����: {e.ActionType}");
                        break;

                    default:
                        // �f�t�H���g�ł͉������Ȃ�
                        System.Diagnostics.Debug.WriteLine($"���̑��̃i�r�Q�[�V����: {e.ActionType}");
                        break;
                }
            }
            catch (Exception ex)
            {
                // �X�i�b�v�V���b�g���쒆�̃G���[�����O�ɋL�^
                System.Diagnostics.Debug.WriteLine($"�X�i�b�v�V���b�g����G���[: {ex.Message}");
                MessageBox.Show($"�f�[�^�̕ۑ�/�������ɃG���[���������܂���: {ex.Message}",
                    "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PostNavigatedEvent(object? sender, NavigationService.NavigationEventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"��ʑJ�ڊ���: {e.ToPageType?.Name ?? "�s��"} (Undo�\: {_snapshotManager.CanUndo}, Redo�\: {_snapshotManager.CanRedo})");

                // �K�v�ɉ�����UI�̍X�V�Ȃǂ��s��
                // ��: �߂�{�^���̗L��/������_snapshotManager.CanUndo�ɍ��킹�Đݒ肷��Ȃ�
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"��ʑJ�ڌ㏈���ŃG���[: {ex.Message}");
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            _nav.Start<UcPageLogin>();
        }

        private NavigationResult OnComplete(NavigationContext context)
        {
            _snapshotManager.Clear();
            return NavigationResult.Complete();
        }

        private NavigationResult OnCancel(NavigationContext context)
        {
            if (MessageBox.Show("�ҏW���̃f�[�^������܂����I�����܂����H", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _snapshotManager.Clear();
                return NavigationResult.Cancel();
            }
            return NavigationResult.None();
        }

        private NavigationResult OnTerminate(NavigationContext context)
        {
            _snapshotManager.Clear();
            return NavigationResult.Cancel();
        }

    }
}
