using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;

namespace bwaPolaris.Components.Container
{
    public partial class Container_1<TData, TService> where TData : class
    where TService : class
    {
        #region Inject
        [Inject]
        [Parameter]
        public IJSRuntime? JSR { get; set; }
        [Inject]
        [Parameter]
        public NavigationManager? NM { get; set; }

        [Inject]
        public svcForm svcForm { get; set; }

		[Inject]
		public svcPrivileges svcPrivileges { get; set; }

		[Inject]
        public svcDataOption svcDataOption { get; set; }

        [Inject]
        public svcBaseService baseService { get; set; }
        #endregion


        private SfGrid<TData> DefaultGrid;
        //public string Platform = Preferences.Get("Platform", "");
        public string IdData { get; set; }
        public bool IsLoading { get; set; }
        public bool SembunyikanJudulDetilMobile { get; set; } = true;
        public bool DiperbolehkanEditPrivilege = false;
        public bool DiperbolehkanEditData { get; set; } = false;
        public List<TData> dsRekapitulasi { get; set; }
        public string _state;
        public RenderFragment? rfColumn { get; set; }
        public RenderFragment? rfDetil { get; set; }
        public RenderFragment? rfGrid_Detil { get; set; }

        public string IdForm { get; set; }
        public T0Form Form = new();

        public List<T1AtributForm> AtributForm = new();
        public T9Privileges AksesData = new();
        private string _idUser { get; set; }

        public TData DataDipilih { get; set; }

        protected override async void OnParametersSet()
        {
            _idUser = baseService.User.IdUser.ToString();
            AtributForm.Clear();
            AtributForm = await svcForm.GetDataAtributForm(IdForm);
            _contextMenu.Clear();
            Form = baseService.DaftarForm.FirstOrDefault(x => x.IdForm == IdForm);

            if (_idUser != "00000001-0000-0000-0000-000000000000")
            {
                AksesData = await svcPrivileges.GetPrivilegesById(_idUser, IdForm);
                if (!AksesData.IsAbleToAddData) TampilkanButtonTambah = false;
                else TampilkanButtonTambah= true;
            }
            else if(Form.IsAbleToAddData) { TampilkanButtonTambah = true; }
            this.StateHasChanged();
        }

        #region GridToolbar&ContextMenu
        private string[] _toolbarItems = new string[] { "PdfExport", "ExcelExport", "ColumnChooser" };
        private static List<ContextMenuItemModel> _contextMenu = new();
        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id == $"{IdData}_pdfexport")
            {
                await DefaultGrid.PdfExport();
            }
            else if (args.Item.Id == $"{IdData}_excelexport")
            {
                await DefaultGrid.ExcelExport();
            }
        }
        public async Task OnContextMenuClick(ContextMenuClickEventArgs<TData> args)
        {

            if (args.Item.Id == "reset")

            {
                await DefaultGrid.ResetPersistDataAsync();

            }
            else if (args.Item.Id == "get")
            {
                _state = await DefaultGrid.GetPersistDataAsync();

            }
            else if (args.Item.Id == "renameThisColumn")
            {
                _isMenuRenameColumnVisible = true;
                _kolomDipilih = AtributForm.FirstOrDefault(e => e.CaptionRekapitulasi == args.Column.HeaderText);

            }
            else if (args.Item.Id == "columnChooser")
            {
                await DefaultGrid.OpenColumnChooser();
            }
            else if (args.Item.Id == "hapusData")
            {
                DialogKonfirmasiHapus = true;
                DataDipilih = args.RowInfo.RowData;
            }
        }
        #endregion
        

        #region RenameColumn
        private bool _isMenuRenameColumnVisible;
        private T1AtributForm _kolomDipilih = new();
        private string _textBaru = "";
        private async void OnRenameColumn()
        {
            var data = AtributForm.FirstOrDefault(e => e.CaptionRekapitulasi == _kolomDipilih.CaptionRekapitulasi);
            data.CaptionRekapitulasi = _textBaru;
            data.CaptionDetil = _textBaru;

            //Fitur Lanjutan
            //var status = await _svcForm.UpdateAtributForm(data);
            //if (status)
            //{
            //    CloseRenameColumnDialog();
            //}
        }
        private void CloseRenameColumnDialog() => _isMenuRenameColumnVisible = false;
        #endregion

        #region Detil
        public string JudulDetil { get; set; } = "";
        public bool EnableControl { get; set; } = false;

        public virtual async void OnDataDipilih(RecordClickEventArgs<TData> args)
        {
            DataDipilih = args.RowData;
            if (_idUser != "00000001-0000-0000-0000-000000000000")
            {
                if (AksesData.IsAbleToDeleteData) TampilkanButtonHapus = true; else TampilkanButtonHapus = false;
                if (AksesData.IsAbleToAddData) TampilkanButtonTambah = true; else TampilkanButtonTambah = false;
                if (AksesData.IsAbleToEditData)
                {
                    TampilkanButtonSimpan = true;
                    TampilkanButtonBatal = true;
                    EnableControl = true;
                    DiperbolehkanEditData = true;
                    DiperbolehkanEditPrivilege = true;
                }

            }
            else
            {
                if(Form.IsAbleToAddData) TampilkanButtonTambah = true;
				if (Form.IsAbleToDeleteData) TampilkanButtonHapus = true;
				if (Form.IsAbleToEditData) TampilkanButtonSimpan = true;
                TampilkanButtonBatal = true;
                EnableControl = true;
                DiperbolehkanEditData = true;
                DiperbolehkanEditPrivilege = true;
            }

        }
        #endregion

        #region Konfirmasi_SebelumHapus
        public bool DialogKonfirmasiHapus { get; set; }
        async void SembunyikanKonfirmasiHapus() => DialogKonfirmasiHapus = false;
        async void TampilkanKonfirmasiHapus() => DialogKonfirmasiHapus = true;
        #endregion

        #region Navigasi
        public bool TampilkanButtonTambah = false;
        public bool TampilkanButtonHapus = false;
        public bool TampilkanButtonSimpan = false;
        public bool TampilkanButtonBatal = false;
        public virtual async void OnSimpanData()
        {
            if (_idUser != "00000001-0000-0000-0000-000000000000")
                if (AksesData.IsAbleToAddData) TampilkanButtonTambah = true; else TampilkanButtonTambah = false;
            else TampilkanButtonTambah = true;
            TampilkanButtonHapus = false;
            TampilkanButtonSimpan = false;
            TampilkanButtonBatal = false;
            EnableControl = false;
            DiperbolehkanEditData = false;
            DiperbolehkanEditPrivilege = false;
        }

        public async void Validasi_SebelumSimpan()
        {
            await JSR.InvokeAsync<string>("Submit");
        }

        public virtual async void OnHapusData()
        {
            if (_idUser != "00000001-0000-0000-0000-000000000000")
                if (AksesData.IsAbleToAddData) TampilkanButtonTambah = true; else TampilkanButtonTambah = false;
            else if(Form.IsAbleToAddData) TampilkanButtonTambah = true;
            TampilkanButtonHapus = false;
            TampilkanButtonSimpan = false;
            TampilkanButtonBatal = false;
            SembunyikanKonfirmasiHapus();
            EnableControl = false;
            DiperbolehkanEditData = false;
            DiperbolehkanEditPrivilege = false;

        }

        public virtual async void OnDataBaru()
        {
            JudulDetil = "MEMBUAT DATA BARU";
            TampilkanButtonTambah = false;
            TampilkanButtonHapus = false;
            TampilkanButtonSimpan = true;
            TampilkanButtonBatal = true;
            EnableControl = true;
            DiperbolehkanEditData = true;
            DiperbolehkanEditPrivilege = false;

        }

        public virtual async void OnBatal_SimpanData()
        {
            if (_idUser != "00000001-0000-0000-0000-000000000000")
                if (AksesData.IsAbleToAddData) TampilkanButtonTambah = true; else TampilkanButtonTambah = false;
            else if(Form.IsAbleToAddData) TampilkanButtonTambah = true;
            TampilkanButtonHapus = false;
            TampilkanButtonSimpan = false;
            TampilkanButtonBatal = false;
            JudulDetil = "";
            EnableControl = false;
            DiperbolehkanEditData = false;
            DiperbolehkanEditPrivilege = false;

        }
        #endregion
    }
}