Imports System.Runtime.InteropServices

Module Module1
    <StructLayout(LayoutKind.Sequential)>
    Public Structure DEVMODE
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)>
        Public dmDeviceName As String
        Public dmSpecVersion As Short
        Public dmDriverVersion As Short
        Public dmSize As Short
        Public dmDriverExtra As Short
        Public dmFields As Integer
        Public dmPositionX As Integer
        Public dmPositionY As Integer
        Public dmDisplayOrientation As Integer
        Public dmDisplayFixedOutput As Integer
        Public dmColor As Short
        Public dmDuplex As Short
        Public dmYResolution As Short
        Public dmTTOption As Short
        Public dmCollate As Short
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)>
        Public dmFormName As String
        Public dmLogPixels As Short
        Public dmBitsPerPel As Integer
        Public dmPelsWidth As Integer
        Public dmPelsHeight As Integer
        Public dmDisplayFlags As Integer
        Public dmDisplayFrequency As Integer
        Public dmICMMethod As Integer
        Public dmICMIntent As Integer
        Public dmMediaType As Integer
        Public dmDitherType As Integer
        Public dmReserved1 As Integer
        Public dmReserved2 As Integer
        Public dmPanningWidth As Integer
        Public dmPanningHeight As Integer
    End Structure

    <DllImport("user32.dll")>
    Public Function EnumDisplaySettings(ByVal lpszDeviceName As String, ByVal iModeNum As Integer, ByRef lpDevMode As DEVMODE) As Boolean
    End Function

    <DllImport("user32.dll")>
    Public Function ChangeDisplaySettings(ByRef lpDevMode As DEVMODE, ByVal dwFlags As Integer) As Integer
    End Function

    Sub Main()
        Dim devMode As DEVMODE = New DEVMODE()
        devMode.dmSize = CType(Marshal.SizeOf(GetType(DEVMODE)), Short)

        If EnumDisplaySettings(Nothing, 0, devMode) Then
            devMode.dmPelsWidth = 2560
            devMode.dmPelsHeight = 1440
            devMode.dmDisplayFrequency = 60
            devMode.dmFields = &H80000 Or &H40000 Or &H100000

            Dim result As Integer = ChangeDisplaySettings(devMode, 0)
            If result = 0 Then
                Console.WriteLine("Résolution et taux de rafraîchissement changés avec succès.")
            Else
                Console.WriteLine("Erreur lors du changement de la résolution et du taux de rafraîchissement.")
            End If
        Else
            Console.WriteLine("Impossible d'obtenir les paramètres d'affichage actuels.")
        End If
    End Sub
End Module
