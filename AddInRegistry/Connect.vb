Imports System.IO
Imports System.Net
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Runtime.InteropServices.ComTypes
Imports System.Threading
Imports System.Windows.Forms
Imports SolidEdgeFramework

<ComVisible(True)>
<Guid("A1B2C3D4-E5F6-47D1-98A1-B23456789ABC")>
<ProgId("AddInTR")>
Public Class Connect
    Implements ISolidEdgeAddIn
    Implements ISEAddInEvents

    Private _app As SolidEdgeFramework.Application
    Private _addinEx As ISEAddInEx
    Private _sinkCookie As Integer
    Private _addinFileName As String

    Public Sub OnConnection(Application As Object,
                            ConnectMode As SeConnectMode,
                            AddInInstance As AddIn) _
                            Implements ISolidEdgeAddIn.OnConnection

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf CurrentDomain_AssemblyResolve

        _app = CType(Application, SolidEdgeFramework.Application)
        AddInInstance.Description = Chr(10) & "AddIn TR"
        _addinEx = AddInInstance
        _addinEx.GuiVersion = 19
        _addinFileName = Me.GetType().Module.FullyQualifiedName

        Dim cpc = DirectCast(_addinEx, IConnectionPointContainer)
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
            Dim reg As New CommandRegistry(_addinEx, _addinFileName)

            Select Case EnvCatID
                Case CATID_SEAssembly
                    reg.RegisterBOMGroup(EnvCatID, bFirstTime)
                    reg.RegisterCadastrarProdutoGroup(EnvCatID, bFirstTime)

                Case CATID_SEDraft
                    reg.RegisterExportDFTGroup(EnvCatID, bFirstTime)

                Case CATID_SEPart
                    reg.RegisterExport3DGroup(EnvCatID, bFirstTime)
                    reg.RegisterCadastrarProdutoGroup(EnvCatID, bFirstTime)

                Case CATID_SESheetMetal
                    reg.RegisterExport3DGroup(EnvCatID, bFirstTime)
                    reg.RegisterCadastrarProdutoGroup(EnvCatID, bFirstTime)

            End Select
        Catch ex As Exception
            MessageBox.Show(New WindowWrapper(_app.hWnd), "Erro: " & ex.ToString, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

            Case 4001
                RegisterProduct.RegisterProduct(_app)

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