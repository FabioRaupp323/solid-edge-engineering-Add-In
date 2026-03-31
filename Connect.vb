Imports System.IO
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Runtime.InteropServices.ComTypes
Imports SolidEdgeFramework
Imports System.Net

<ComVisible(True)>
<Guid("A1B2C3D4-E5F6-47D1-98A1-B23456789ABC")>
<ProgId("AddInTR")>
Public Class Connect
    Implements ISolidEdgeAddIn
    Implements ISEAddInEvents

    Private _app As SolidEdgeFramework.Application
    Private _addin As AddIn
    Private _sinkCookie As Integer

    Public Sub OnConnection(Application As Object,
                            ConnectMode As SeConnectMode,
                            AddInInstance As AddIn) _
                            Implements ISolidEdgeAddIn.OnConnection

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf CurrentDomain_AssemblyResolve

        _app = CType(Application, SolidEdgeFramework.Application)
        _addin = AddInInstance

        _addin.GuiVersion = 5

        Dim cpc = DirectCast(_addin, IConnectionPointContainer)
        Dim cp As IConnectionPoint = Nothing
        Dim guid = GetType(ISEAddInEvents).GUID

        cpc.FindConnectionPoint(guid, cp)
        cp.Advise(Me, _sinkCookie)

    End Sub

    Private Function CurrentDomain_AssemblyResolve(sender As Object, args As ResolveEventArgs) As Assembly
        Try
            Dim folderPath As String = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            Dim assemblyName As New AssemblyName(args.Name)

            If assemblyName.Name.EndsWith(".resources") Then Return Nothing

            If assemblyName.Name.StartsWith("System.Diagnostics.DiagnosticSource") Then
                Dim specificPath = Path.Combine(folderPath, "System.Diagnostics.DiagnosticSource.dll")
                If File.Exists(specificPath) Then
                    Return Assembly.LoadFrom(specificPath)
                End If
            End If

            Dim dllPath As String = Path.Combine(folderPath, assemblyName.Name & ".dll")
            If File.Exists(dllPath) Then
                Return Assembly.LoadFrom(dllPath)
            End If

        Catch ex As Exception
        End Try
        Return Nothing
    End Function

    Public Sub OnConnectToEnvironment(EnvCatID As String,
                                      pEnvironmentDispatch As Object,
                                      bFirstTime As Boolean) _
                                      Implements ISolidEdgeAddIn.OnConnectToEnvironment

        Try
            Select Case EnvCatID
                Case "{26618395-09D6-11D1-BA07-080036230602}"

                    Dim names = {"ExportarBOM" & vbLf & "ExportarBOM" & vbLf & "Exporta a lista de componentes da montagem" & vbLf & "ExportarBOM"}
                    Dim ids = {1001}

                    _addin.SetAddInInfo(
                            Marshal.GetHINSTANCE(Me.GetType().Module).ToInt32(),
                            EnvCatID,
                            "Análise de BOM",
                            0, 0, 0, 0,
                            1,
                            names,
                            ids
                        )

                    If bFirstTime Then
                        Dim btn = _addin.AddCommandBarButton(EnvCatID, "Análise de BOM", 1001)
                        btn.Style = SeButtonStyle.seButtonIconAndCaptionBelow
                        btn.LoadFace(AppSettings.IconBomPath)
                    End If

                Case "{08244193-B78D-11D2-9216-00C04F79BE98}"

                    Dim names = {"Exportar PDF e DXF" & vbLf & "Exportar PDF e DXF" & vbLf & "Exporta a aba Desenho do draft aberto como PDF e as abas LA/PO/POLA (dependendo de onde há conteúdo) como DXF." & vbLf & "Exportar PDF e DXF"}
                    Dim ids = {2001}

                    _addin.SetAddInInfo(
                    Marshal.GetHINSTANCE(Me.GetType().Module).ToInt32(),
                    EnvCatID,
                    "Exportar DFT",
                    0, 0, 0, 0,
                    1,
                    names,
                    ids
                    )

                    If bFirstTime Then
                        Dim btnPDF = _addin.AddCommandBarButton(EnvCatID, "Exportar DFT", 2001)
                        btnPDF.Style = SeButtonStyle.seButtonIconAndCaptionBelow
                        btnPDF.LoadFace(AppSettings.IconDftPath)
                    End If

                Case "{26618396-09D6-11D1-BA07-080036230602}", "{26618398-09D6-11D1-BA07-080036230602}"

                    Dim names = {"Exportar STEP" & vbLf & "Exportar STEP" & vbLf & "Exporta o arquivo 3D no formato STEP." & vbLf & "Exportar STEP",
                                 "Exportar IGS" & vbLf & "Exportar IGS" & vbLf & "Exporta o arquivo 3D no formato IGS." & vbLf & "Exportar IGS"}
                    Dim ids = {3001, 3002}

                    _addin.SetAddInInfo(
                    Marshal.GetHINSTANCE(Me.GetType().Module).ToInt32(),
                    EnvCatID,
                    "Exportar 3D",
                    0, 0, 0, 0,
                    2,
                    names,
                    ids
                    )

                    If bFirstTime Then

                        Dim btnStep = _addin.AddCommandBarButton(EnvCatID, "Exportar 3D", 3001)
                        btnStep.Style = SeButtonStyle.seButtonIconAndCaptionBelow
                        btnStep.LoadFace(AppSettings.IconStepPath)

                        Dim btnIgs = _addin.AddCommandBarButton(EnvCatID, "Exportar 3D", 3002)
                        btnIgs.Style = SeButtonStyle.seButtonIconAndCaptionBelow
                        btnIgs.LoadFace(AppSettings.IconIgsPath)
                    End If

            End Select
        Catch ex As Exception
            MsgBox("Erro: " & ex.Message)
        End Try
    End Sub

    Public Sub OnCommand(CommandID As Integer) _
        Implements ISEAddInEvents.OnCommand
        Select Case CommandID

            Case 1001
                ExtractBOM.Run(_app)

            Case 2001
                ExportDft.ExportPdfDxf(_app)

            Case 3001
                Export3D.Export3D(_app, ".step")

            Case 3002
                Export3D.Export3D(_app, ".igs")

        End Select
    End Sub

    Public Sub OnDisconnection(DisconnectMode As SeDisconnectMode) _
        Implements ISolidEdgeAddIn.OnDisconnection
    End Sub

    Public Sub OnCommandHelp(hFrameWnd As Integer, HelpCommandID As Integer, CommandID As Integer) _
        Implements ISEAddInEvents.OnCommandHelp
    End Sub

    Public Sub OnCommandUpdateUI(CommandID As Integer,
                                 ByRef CommandFlags As Integer,
                                 ByRef MenuItemText As String,
                                 ByRef BitmapID As Integer) _
                                 Implements ISEAddInEvents.OnCommandUpdateUI
    End Sub
End Class