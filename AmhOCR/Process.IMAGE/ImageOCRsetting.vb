﻿
Imports System.IO
Imports AgImag = AForge.Imaging

'Copyright ©  Kumneger Hussien, kumneger.h@gmail.com, 2019 GPLv3
Public Class ImageOCRsetting


    Private Lock As Boolean = True

    Friend _MainImage As Image
    'Private _Image As Image
    Friend MyViewer As ImageEditControl

    Friend _MainHocrPage As HocrPage

    Private loaded As Boolean = False

    Public Sub InitializeImage(ByVal ImgEdit As Image, ByRef hocrage As HocrPage)

        _MainHocrPage = hocrage

        _MainHocrPage.SetSettings()

        _MainImage = ImgEdit
        ' _Image = _MainImage.Clone


        MyViewer.DisposeImage()
        MyViewer.ResetAllState()

        MyViewer.Image = _MainImage.Clone

    End Sub

    Private Sub ProcessImage_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        InitializeSetting()

    End Sub


    Private Sub InitializeSetting()
        Lock = True

        UserSettings.Gray = _MainHocrPage.PageOCRsettings.Gray

        UserSettings.Threshold = _MainHocrPage.PageOCRsettings.Threshold
        UserSettings.ThresholdValue = _MainHocrPage.PageOCRsettings.ThresholdValue

        UserSettings.Bright = _MainHocrPage.PageOCRsettings.Bright
        UserSettings.BrightValue = _MainHocrPage.PageOCRsettings.BrightValue

        UserSettings.Contrast = _MainHocrPage.PageOCRsettings.Contrast
        UserSettings.ContrastValue = _MainHocrPage.PageOCRsettings.ContrastValue

        UserSettings.Gamma = _MainHocrPage.PageOCRsettings.Gamma
        UserSettings.GammaValue = _MainHocrPage.PageOCRsettings.GammaValue

        UserSettings.Binaries = _MainHocrPage.PageOCRsettings.Binaries

        If UserSettings.Gray = True Then
            chkGray.Checked = True
            grpBoxThreshold.Enabled = True
        Else
            chkGray.Checked = False
            grpBoxThreshold.Enabled = False

        End If



        If UserSettings.Threshold = True Then
            TrackThresh.Enabled = True
            chkThreshold.Checked = True
        Else
            TrackThresh.Enabled = False
            chkThreshold.Checked = False

        End If



        TrackThresh.Value = UserSettings.ThresholdValue
        lblThreshold.Text = UserSettings.ThresholdValue

        If UserSettings.Bright = True Then
            TrackBright.Enabled = True
            chkBright.Checked = True

        Else
            TrackBright.Enabled = False
            chkBright.Checked = False
        End If

        TrackBright.Value = UserSettings.BrightValue
        lblBright.Text = UserSettings.BrightValue


        If UserSettings.Contrast = True Then
            TrackContrast.Enabled = True
            chkContrast.Checked = True
        Else
            TrackContrast.Enabled = False
            chkContrast.Checked = False

        End If

        TrackContrast.Value = UserSettings.ContrastValue
        lblContrast.Text = UserSettings.ContrastValue


        If UserSettings.Gamma = True Then
            TrackGamma.Enabled = True
            chkGamma.Checked = True
        Else
            TrackGamma.Enabled = False
            chkGamma.Checked = False
        End If

        TrackGamma.Value = CInt(UserSettings.GammaValue * 10).ToString
        lblGamma.Text = TrackGamma.Value.ToString

        If UserSettings.Binaries = True Then

            GroupBox7.Enabled = False
            chkGray.Enabled = False


            chkADthreshold.Checked = True

        Else
            GroupBox7.Enabled = True
            chkGray.Enabled = True

            chkADthreshold.Checked = False
        End If

        Lock = False
        loaded = True
        ApplyCorrections()

    End Sub


    Private Sub chkThreshold_CheckedChanged(sender As Object, e As EventArgs) Handles chkThreshold.CheckedChanged

        If loaded = False Then
            Exit Sub
        End If

        If grpBoxThreshold.Enabled = True Then
            Lock = True
            If chkThreshold.Checked = True Then

                TrackThresh.Value = 150
                TrackThresh.Enabled = True

            Else
                TrackThresh.Enabled = False
                TrackThresh.Value = 150

            End If

            Lock = False


            If chkThreshold.Checked = True Then

                UserSettings.Threshold = True

                UserSettings.ThresholdValue = TrackThresh.Value
            Else

                UserSettings.Threshold = False
                UserSettings.ThresholdValue = 150

            End If

            ApplyCorrections()


        End If

    End Sub

    Private Sub chkBright_CheckedChanged(sender As Object, e As EventArgs) Handles chkBright.CheckedChanged

        If loaded = False Then
            Exit Sub
        End If

        Lock = True

        If chkBright.Checked = True Then
            TrackBright.Enabled = True
        Else
            TrackBright.Enabled = False
        End If

        TrackBright.Value = 0

        Lock = False

        If chkBright.Checked = True Then

            UserSettings.Bright = True
            UserSettings.BrightValue = TrackBright.Value

        Else

            UserSettings.Bright = False
            UserSettings.BrightValue = 0

        End If


        ApplyCorrections()


    End Sub

    Private Sub chkContrast_CheckedChanged(sender As Object, e As EventArgs) Handles chkContrast.CheckedChanged

        If loaded = False Then
            Exit Sub
        End If

        Lock = True
        If chkContrast.Checked = True Then
            TrackContrast.Enabled = True
        Else
            TrackContrast.Enabled = False
        End If

        TrackContrast.Value = 0
        Lock = False

        If chkContrast.Checked = True Then

            UserSettings.Contrast = True
            UserSettings.ContrastValue = TrackContrast.Value

        Else

            UserSettings.Contrast = False
            UserSettings.ContrastValue = 0

        End If


        ApplyCorrections()


    End Sub

    Private Sub chkGamma_CheckedChanged(sender As Object, e As EventArgs) Handles chkGamma.CheckedChanged

        If loaded = False Then
            Exit Sub
        End If

        Lock = True
        If chkGamma.Checked = True Then
            TrackGamma.Enabled = True
        Else
            TrackGamma.Enabled = False
        End If

        TrackGamma.Value = 10

        Lock = False

        If chkGamma.Checked = True Then

            UserSettings.Gamma = True
            UserSettings.GammaValue = TrackGamma.Value / 10

        Else

            UserSettings.Gamma = False
            UserSettings.GammaValue = 1

        End If


        ApplyCorrections()


    End Sub


    Private Sub chkADthreshold_CheckedChanged(sender As Object, e As EventArgs) Handles chkADthreshold.CheckedChanged

        If loaded = False Then
            Exit Sub
        End If

        Lock = True

        If chkADthreshold.Checked = True Then
            UserSettings.Binaries = True
            GroupBox7.Enabled = False
            chkGray.Enabled = False
        Else
            UserSettings.Binaries = False
            GroupBox7.Enabled = True
            chkGray.Enabled = True

        End If




        Lock = False

        ApplyCorrections()
    End Sub
    Private Sub chkGray_CheckedChanged(sender As Object, e As EventArgs) Handles chkGray.CheckedChanged

        If loaded = False Then
            Exit Sub
        End If

        Lock = True
        If chkGray.Checked = True Then

            grpBoxThreshold.Enabled = True

        Else

            grpBoxThreshold.Enabled = False

            If chkThreshold.Checked = True Then
                chkThreshold.Checked = False
                TrackThresh.Enabled = False
                TrackThresh.Value = 150
            End If

        End If

        Lock = False

        ' _Image = _MainImage.Clone

        If chkGray.Checked = True Then

            UserSettings.Gray = True

        Else
            UserSettings.Threshold = False
            UserSettings.ThresholdValue = 150
            UserSettings.Gray = False

        End If

        ApplyCorrections()

    End Sub
    Private Sub TrackThresh_ValueChanged(sender As Object, e As EventArgs) Handles TrackThresh.ValueChanged

        If loaded = False Then
            Exit Sub
        End If


        lblThreshold.Text = TrackThresh.Value

        If TrackThresh.Enabled = True Then

            If chkThreshold.Checked = True Then

                UserSettings.Threshold = True

                UserSettings.ThresholdValue = TrackThresh.Value
            Else

                UserSettings.Threshold = False
                UserSettings.ThresholdValue = 150

            End If

            ApplyCorrections()
        End If
        lblThreshold.Refresh()



    End Sub

    Private Sub TrackBright_ValueChanged(sender As Object, e As EventArgs) Handles TrackBright.ValueChanged

        If loaded = False Then
            Exit Sub
        End If

        lblBright.Text = TrackBright.Value


        If TrackBright.Enabled = True Then

            If chkBright.Checked = True Then

                UserSettings.Bright = True
                UserSettings.BrightValue = TrackBright.Value

            Else

                UserSettings.Bright = False
                UserSettings.BrightValue = 0

            End If


            ApplyCorrections()
        End If

        lblBright.Refresh()

    End Sub

    Private Sub TrackGamma_ValueChanged(sender As Object, e As EventArgs) Handles TrackGamma.ValueChanged

        If loaded = False Then
            Exit Sub
        End If

        lblGamma.Text = (TrackGamma.Value / 10).ToString

        If TrackGamma.Enabled = True Then

            If chkGamma.Checked = True Then

                UserSettings.Gamma = True
                UserSettings.GammaValue = TrackGamma.Value / 10

            Else

                UserSettings.Gamma = False
                UserSettings.GammaValue = 1

            End If



            ApplyCorrections()
        End If

        lblGamma.Refresh()
    End Sub

    Private Sub TrackContrast_ValueChanged(sender As Object, e As EventArgs) Handles TrackContrast.ValueChanged

        If loaded = False Then
            Exit Sub
        End If

        lblContrast.Text = TrackContrast.Value

        If TrackContrast.Enabled = True Then

            If chkContrast.Checked = True Then

                UserSettings.Contrast = True
                UserSettings.ContrastValue = TrackContrast.Value

            Else

                UserSettings.Contrast = False
                UserSettings.ContrastValue = 0

            End If



            ApplyCorrections()
        End If

        lblContrast.Refresh()

    End Sub




    Private Async Sub ApplyCorrections()

        If loaded = False Then
            Exit Sub
        End If

        If Lock = False Then

            Lock = True
            Dim tsk =
                TaskEx.Run(
                Function() As Image


                    Dim Newimg As Image = _MainImage.Clone

                    Try
                        Newimg = PreProcessor.ApplyCorrections(Newimg)
                    Catch ex As Exception

                    End Try



                    Return Newimg

                End Function)



            Try
                Dim img = Await tsk
                MyViewer.UpdateImage(img)
                MyViewer.Invalidate()

            Catch ex As Exception

            End Try



            Lock = False
        End If

    End Sub



    Private Sub ProcessImage_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Lock = True

    End Sub

    Private Sub ProcessImage_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        Me.Focus()

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        loaded = False
        Dim pageSeting = _MainHocrPage.PageOCRsettings

        pageSeting.Gray = UserSettings.Gray
        pageSeting.Threshold = UserSettings.Threshold
        pageSeting.Bright = UserSettings.Bright
        pageSeting.Contrast = UserSettings.Contrast
        pageSeting.Gamma = UserSettings.Gamma
        pageSeting.Binaries = UserSettings.Binaries


        pageSeting.ThresholdValue = UserSettings.ThresholdValue
        pageSeting.BrightValue = UserSettings.BrightValue
        pageSeting.ContrastValue = UserSettings.ContrastValue
        pageSeting.GammaValue = UserSettings.GammaValue

        _MainHocrPage.PageOCRsettings = pageSeting

        Me.Close()

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        loaded = False
        InitializeSetting()
    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        Me.Close()
    End Sub
End Class