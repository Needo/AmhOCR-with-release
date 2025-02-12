﻿
'Copyright ©  Kumneger Hussien, kumneger.h@gmail.com, 2019 GPLv3
Imports GhostscriptSharp.Settings

Public Class UserSettings

    Public Shared Property AnimateCroping As Boolean = True

    Public Shared Property OCRbackgroundView As BackgroundMode = BackgroundMode.EditedImage

    Public Shared Property AcceptedImageFormat As List(Of Imaging.PixelFormat)

    Public Shared Property ExcludedImageFormat As List(Of Imaging.PixelFormat)

    Public Shared Property isImageEditMode As Boolean = True

    Public Shared Property ResetBackground As Boolean = False

    Public Shared Property RemoveWhiteListChar As Boolean = False

    Public Shared Property NormalizeChar As Boolean = True

    Public Shared Property NormalizeNumerics As Boolean = True

    Public Shared Property SpellErrorColor As Color = Color.Red

    Public Shared Property UserSpelledColor As Color = Color.Black

    Public Shared Property MaxBatch As Integer = 4

    Public Shared Property Language As String = "amh"

    Public Shared Property Resolution As New Size(96, 96)

    Public Shared Property PageSize As New Size(2550, 3300)

    Public Shared Property OcrMode As OcrModel = OcrModel.LSTM

    Public Shared Property imgFormat As GhostscriptDevices = GhostscriptDevices.jpeg

    Public Shared Property isCustomePage As Boolean = True

    Public Shared Property MaximumColumn_Num As Integer = 1

    'Confidence level used to determine the number of column in the recognized page
    Public Shared Property ColumnRecog_ConfidenceLvl As Integer = 95

    Public Shared Property NativePage As GhostscriptPageSizes = GhostscriptPageSizes.letter

    Public Shared Property PageSegMode As PageSegMode = PageSegMode.sparsetext

    Private Shared _amhocrFont As Font
    Public Shared Property AmhocrFont As Font
        Get
            Return _amhocrFont
        End Get
        Set(ByVal value As Font)
            _amhocrFont = value
        End Set
    End Property

    Private Shared _defaultocrFont As Font
    Public Shared Property DefaultocrFont As Font
        Get
            Return _defaultocrFont
        End Get
        Set(ByVal value As Font)
            _defaultocrFont = value
        End Set
    End Property

    Private Shared _ocrFont As Font
    Public Shared Property ocrFont As Font
        Get
            Return _ocrFont
        End Get
        Set(ByVal value As Font)
            _ocrFont = value
        End Set
    End Property


    Public Shared Property MinimumWordLength As Integer = 2

    ''' <summary>
    ''' too long regonition task will be canceled after this timeout value, millisecond
    ''' </summary>
    ''' <returns></returns>
    Public Shared Property TimeOut As Integer = 120000

    Public Shared Property TimeOutPDF As Integer = 300000

    Public Shared Property EditMode As ocrEditMode = ocrEditMode.WordEdit

    Public Shared Property tesspath As String

    Public Shared Property AmhOcrDataFolder As String = "C:\AmhOCR\Data"

    Public Shared Property AmhOcrTempFolder As String = "C:\AmhOCR\temp"

    Public Shared Property ProjectMainFolder As String = "C:\AmhOCR\Projects"

    Public Shared Property ProjectTempFolder As String = "C:\AmhOCR\temp"

    Public Shared Property ProjectCopyFileFolder As String = "C:\AmhOCR\temp"

    Public Shared Property AmhOcrConvFolder As String = "C:\AmhOCR\Converts"

    Public Shared Property SourceImagaChenged As Boolean = False

    Public Shared Property ProjectFile As String = ""

    Public Shared Property ProjectOpen As Boolean = False

    Public Shared Property Binaries As Boolean = False

    Public Shared Property Gray As Boolean = False

    Public Shared Property Threshold As Boolean = False

    Public Shared Property Bright As Boolean = False

    Public Shared Property Contrast As Boolean = False

    Public Shared Property Gamma As Boolean = False



    Public Shared Property MinimumSkewAngle As Single = 0.1

    Public Shared Property ThresholdValue As Integer = 150

    Public Shared Property BrightValue As Integer = 0

    Public Shared Property ContrastValue As Integer = 0

    Public Shared Property GammaValue As Single = 1

    Public Shared Property PrefMaxBatch As Integer = 4

    Public Shared Property PrefLanguage As String = "amh"

    Public Shared Property PrefTimeOut As Integer = 120000

    Public Shared Property PrefSpellErrorColor As Color = Color.Red

    Public Shared Property PrefUserSpelledColor As Color = Color.Blue

    Public Shared Property PrefThreshold As Boolean = False

    Public Shared Property PrefGray As Boolean = False

    Public Shared Property PrefBinary As Boolean = False

    Public Shared Property PrefResolution As New Size(96, 96)


    Public Shared Sub SetDefault()

        MaxBatch = PrefMaxBatch
        Language = PrefLanguage

        SourceImagaChenged = False
        Gray = PrefGray
        Threshold = PrefThreshold
        Bright = False
        Contrast = False
        Gamma = False
        Binaries = PrefBinary
        ThresholdValue = 150
        BrightValue = 0
        ContrastValue = 0
        GammaValue = 1


    End Sub

    Public Shared Sub SetFont(ByVal lang As String)

        Language = lang
        If Language = "amh" Then

            'Added hack/fallback method to set the default fonts when it get disposed or unable to initialize.
            Try
                If AmhocrFont.Name Is Nothing Then

                End If
            Catch ex As Exception
                'Reset font in case it got disposed.
                AmhocrFont = New Font("Power Geez Unicode1", 11, FontStyle.Regular)
            End Try
            ocrFont = AmhocrFont.Clone
        Else
            ocrFont = DefaultocrFont.Clone
        End If


    End Sub



End Class
