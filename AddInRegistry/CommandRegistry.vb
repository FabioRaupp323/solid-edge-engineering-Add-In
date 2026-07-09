Imports SolidEdgeFramework

Public Class CommandRegistry
    Private _addinEx As ISEAddInEx
    Private _addinFileName As String

    Public Sub New(addinEx As ISEAddInEx, addinFileName As String)
        _addinEx = addinEx
        _addinFileName = addinFileName
    End Sub

    Public Sub RegisterBOMGroup(EnvCatID As String, bFirstTime As Boolean)
        Dim names = {"ExportarBOM" & vbLf & "ExportarBOM" & vbLf & "Exporta a lista de componentes da montagem" & vbLf & "ExportarBOM"}
        Dim ids = {1001}

        _addinEx.SetAddInInfoEx(
                _addinFileName,
                EnvCatID,
                "Add-In TR",
                0, 0, 0, 0,
                1,
                names,
                ids
            )

        If bFirstTime Then
            Dim btn = _addinEx.AddCommandBarButton(EnvCatID, "Add-In TR", 1001)
            btn.Style = SeButtonStyle.seButtonIconAndCaptionBelow
            btn.LoadFace(AppSettings.IconBomPath)
        End If

    End Sub

    Public Sub RegisterExportDFTGroup(EnvCatID As String, bFirstTime As Boolean)
        Dim names = {"Exportar PDF e DXF" & vbLf & "Exportar PDF e DXF" & vbLf & "Exporta a aba Desenho do draft aberto como PDF e as abas LA/PO/POLA (dependendo de onde há conteúdo) como DXF." & vbLf & "Exportar PDF e DXF"}
        Dim ids = {2001}

        _addinEx.SetAddInInfoEx(
                _addinFileName,
                EnvCatID,
                "Add-In TR",
                0, 0, 0, 0,
                1,
                names,
                ids
            )

        If bFirstTime Then
            Dim btnPDF = _addinEx.AddCommandBarButton(EnvCatID, "Add-In TR", 2001)
            btnPDF.Style = SeButtonStyle.seButtonIconAndCaptionBelow
            btnPDF.LoadFace(AppSettings.IconDftPath)
        End If

    End Sub

    Public Sub RegisterExport3DGroup(EnvCatID As String, bFirstTime As Boolean)
        Dim names = {"Exportar STEP" & vbLf & "Exportar STEP" & vbLf & "Exporta o arquivo 3D no formato STEP." & vbLf & "Exportar STEP",
                                     "Exportar IGS" & vbLf & "Exportar IGS" & vbLf & "Exporta o arquivo 3D no formato IGS." & vbLf & "Exportar IGS"}
        Dim ids = {3001, 3002}

        _addinEx.SetAddInInfoEx(
                _addinFileName,
                EnvCatID,
                "Add-In TR",
                0, 0, 0, 0,
                2,
                names,
                ids
            )

        If bFirstTime Then

            Dim btnStep = _addinEx.AddCommandBarButton(EnvCatID, "Add-In TR", 3001)
            btnStep.Style = SeButtonStyle.seButtonIconAndCaptionBelow
            btnStep.LoadFace(AppSettings.IconStepPath)

            Dim btnIgs = _addinEx.AddCommandBarButton(EnvCatID, "Add-In TR", 3002)
            btnIgs.Style = SeButtonStyle.seButtonIconAndCaptionBelow
            btnIgs.LoadFace(AppSettings.IconIgsPath)
        End If

    End Sub

    Public Sub RegisterCadastrarProdutoGroup(EnvCatID As String, bFirstTime As Boolean)
        Dim names = {"Cadastrar Produto" & vbLf & "Cadastrar Produto" & vbLf & "Abre a janela de cadastro de produto no ERP" & vbLf & "Cadastrar Produto"}
        Dim ids = {4001}

        _addinEx.SetAddInInfoEx(
                _addinFileName,
                EnvCatID,
                "Add-In TR",
                0, 0, 0, 0,
                1,
                names,
                ids
            )

        If bFirstTime Then
            Dim btnCadastrarProduto = _addinEx.AddCommandBarButton(EnvCatID, "Add-In TR", 4001)
            btnCadastrarProduto.Style = SeButtonStyle.seButtonIconAndCaptionBelow
            btnCadastrarProduto.LoadFace(AppSettings.IconCadastrarProdutoPath)
        End If

    End Sub

    Public Sub RegisterRevisarGroup(EnvCatID As String, bFirstTime As Boolean)
        Dim names = {"Revisar" & vbLf & "Revisar" & vbLf & "Incrementa número da revisão e atribui o status ""Available""" & vbLf & "Revisar"}
        Dim ids = {5001}

        _addinEx.SetAddInInfoEx(
                _addinFileName,
                EnvCatID,
                "Add-In TR",
                0, 0, 0, 0,
                1,
                names,
                ids
            )

        If bFirstTime Then
            Dim btnRevisar = _addinEx.AddCommandBarButton(EnvCatID, "Add-In TR", 5001)
            btnRevisar.Style = SeButtonStyle.seButtonIconAndCaptionBelow
            btnRevisar.LoadFace(AppSettings.IconRevisarPath)
        End If

    End Sub
End Class
