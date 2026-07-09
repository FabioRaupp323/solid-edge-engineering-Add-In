Imports System.IO
Imports System.Reflection

Module AppSettings
    Private ReadOnly _settings As New Dictionary(Of String, String)

    Sub New()
        Dim assemblyFolder As String = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
        Dim secretsPath As String = Path.Combine(assemblyFolder, "app.secrets.config")

        If Not File.Exists(secretsPath) Then
            Throw New FileNotFoundException(
                "Arquivo de configuração não encontrado. Por favor crie app.secrets.config no mesmo local da DLL." &
                Environment.NewLine & "Caminho esperado: " & secretsPath)
        End If

        Dim doc As New Xml.XmlDocument()
        doc.Load(secretsPath)

        For Each node As Xml.XmlNode In doc.SelectNodes("//add")
            Dim key = node.Attributes("key")?.Value
            Dim value = node.Attributes("value")?.Value
            If key IsNot Nothing Then
                _settings(key) = value
            End If
        Next
    End Sub

    Public ReadOnly Property BomConnectionString As String
        Get
            Return GetRequired("BomConnectionString")
        End Get
    End Property

    Public ReadOnly Property ErpConnectionString As String
        Get
            Return GetRequired("ErpConnectionString")
        End Get
    End Property

    Public ReadOnly Property PdfDxfOutputPath As String
        Get
            Return GetRequired("PdfDxfServerPath")
        End Get
    End Property

    Public ReadOnly Property StepIgsOutputPath As String
        Get
            Return GetRequired("StepIgsServerPath")
        End Get
    End Property

    Public ReadOnly Property IconBomPath As String
        Get
            Return GetRequired("IconBomPath")
        End Get
    End Property

    Public ReadOnly Property IconDftPath As String
        Get
            Return GetRequired("IconDftPath")
        End Get
    End Property

    Public ReadOnly Property IconStepPath As String
        Get
            Return GetRequired("IconStepPath")
        End Get
    End Property

    Public ReadOnly Property IconIgsPath As String
        Get
            Return GetRequired("IconIgsPath")
        End Get
    End Property

    Public ReadOnly Property IconCadastrarProdutoPath As String
        Get
            Return GetRequired("IconCadastrarProdutoPath")
        End Get
    End Property

    Public ReadOnly Property IconRevisarPath As String
        Get
            Return GetRequired("IconRevisarPath")
        End Get
    End Property

    Public ReadOnly Property ErpDuplicatedColumns As String
        Get
            Return GetRequired("ErpDuplicatedColumns")
        End Get
    End Property

    Private Function GetRequired(key As String) As String
        If _settings.ContainsKey(key) Then
            Return _settings(key)
        End If
        Throw New KeyNotFoundException($"Key de configuração faltando: '{key}' em app.secrets.config")
    End Function

End Module