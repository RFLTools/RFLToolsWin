Imports System.Math

Public Class DataTypes

    Private Shared Tol As Double = 0.0000000001
    Private Shared TolFine As Double = 0.00000000000001

    Public Structure Point2d
        Public X As Double
        Public Y As Double

        Public Function DistanceTo(ByVal P As Point2d) As Double
            DistanceTo = Sqrt(Pow((P.X - X), 2) + Pow((P.Y - Y), 2))
        End Function

        Public Function AngleTo(ByVal P As Point2d) As Double
            AngleTo = Atan2(P.Y - Y, P.X - X)
        End Function

    End Structure

    Public Structure Point3d
        Public X As Double
        Public Y As Double
        Public Z As Double

        Public Function DistanceTo(ByVal P As Point3d) As Double
            DistanceTo = Sqrt(Pow((P.X - X), 2) + Pow((P.Y - Y), 2) + Pow((P.Z - Z), 2))
        End Function

        Public Function DistanceTo2D(ByVal P As Point3d) As Double
            DistanceTo2D = Sqrt(Pow((P.X - X), 2) + Pow((P.Y - Y), 2))
        End Function

        Public Function AngleTo2D(ByVal P As Point3d) As Double
            AngleTo2D = Atan2(P.Y - Y, P.X - X)
        End Function

    End Structure

    Public Structure Tangent
        Public StaStart As Double
        Public P1 As Point2d
        Public P2 As Point2d

        Public Function Length() As Double
            Return P1.DistanceTo(P2)
        End Function

        Public Function StaOff(ByVal P As Point2d, ByRef StaOffResult As Point2d) As Boolean
            Dim Ang, D, D1, D11, D2, D22 As Double

            D = Length()
            D1 = P1.DistanceTo(P)
            D2 = P2.DistanceTo(P)
            D11 = ((D * D) + ((D1 * D1) - (D2 * D2))) / (2.0 * D)
            D22 = D - D11
            'If (D11 <= (D + Tol)) And (D22 <= (D + Tol)) Then
            If (D11 <= D) And (D22 <= D) Then
                StaOffResult.X = StaStart + D11
                StaOffResult.Y = Sqrt(Abs((D1 * D1) - (D11 * D11)))
                Ang = P1.AngleTo(P2) - P1.AngleTo(P)
                While Ang < 0.0
                    Ang = Ang + 2.0 * PI
                End While
                If Ang > (PI / 2.0) Then
                    StaOffResult.Y = StaOffResult.Y * -1.0
                End If
                Return True
            Else
                Return False
            End If
        End Function

        Public Function XY(ByVal P As DataTypes.Point2d, ByRef XYResult As DataTypes.Point2d) As Boolean
            Dim Sta As Double = P.X - StaStart
            Dim Offset As Double = P.Y
            Dim Ang As Double

            If Sta <= Length() Then
                Ang = P1.AngleTo(P2)
                XYResult.X = P1.X + Sta * Cos(Ang) + Offset * Sin(Ang)
                XYResult.Y = P1.Y + Sta * Sin(Ang) - Offset * Cos(Ang)
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetP1(ByRef PResult As DataTypes.Point2d) As Boolean
            PResult.X = P1.X
            PResult.Y = P1.Y
            Return True
        End Function

        Public Function GetP2(ByRef PResult As DataTypes.Point2d) As Boolean
            PResult.X = P2.X
            PResult.Y = P2.Y
            Return True
        End Function

        Public Function AngIn() As Double
            Return P1.AngleTo(P2)
        End Function

        Public Function AngOut() As Double
            Return P1.AngleTo(P2)
        End Function

    End Structure

    Public Structure Arc
        Public StaStart As Double
        Public P1 As Point2d
        Public P2 As Point2d
        Public Bulge As Double

        Public Function Length() As Double
            Dim ATotal, Chord, R As Double
            ATotal = 4.0 * Atan(Abs(Bulge))
            Chord = P1.DistanceTo(P2)
            R = Chord / (2.0 * Sin(ATotal / 2.0))
            Return R * ATotal
        End Function

        Private Function Radius() As Double
            Dim ATotal, Chord As Double

            ATotal = 4.0 * Atan(Abs(Bulge))
            Chord = P1.DistanceTo(P2)
            Return Chord / (2.0 * Sin(ATotal / 2.0))
        End Function

        Private Function Center() As Point2d
            Dim Ang, ATotal, Chord, R, X, Y As Double

            ATotal = 4.0 * Atan(Abs(Bulge))
            Chord = P1.DistanceTo(P2)
            Ang = P1.AngleTo(P2)
            R = Radius()
            X = Chord / 2.0
            Y = Sqrt((R * R) - (X * X)) * Sign(Bulge) * Sign(Abs(Bulge) - 1.0)
            Center.X = P1.X + X * Cos(Ang) + Y * Sin(Ang)
            Center.Y = P1.Y + X * Sin(Ang) - Y * Cos(Ang)
            Return Center
        End Function

        Public Function StaOff(ByVal P As Point2d, ByRef StaOffResult As Point2d) As Boolean
            Dim PC As Point2d
            Dim Ang1, Ang2, R As Double

            PC = Center()
            If Bulge < 0.0 Then
                Ang1 = PC.AngleTo(P1) - PC.AngleTo(P)
                Ang2 = PC.AngleTo(P1) - PC.AngleTo(P2)
            Else
                Ang1 = PC.AngleTo(P) - PC.AngleTo(P1)
                Ang2 = PC.AngleTo(P2) - PC.AngleTo(P1)
            End If
            While Ang1 < 0.0
                Ang1 = Ang1 + 2.0 * PI
            End While
            While Ang2 < 0.0
                Ang2 = Ang2 + 2.0 * PI
            End While
            'If Ang1 <= (Ang2 + TolFine) Then
            If Ang1 <= Ang2 Then
                R = Radius()
                StaOffResult.X = StaStart + R * Ang1
                StaOffResult.Y = Sign(Bulge) * (PC.DistanceTo(P) - R)
                Return True
            Else
                Return False
            End If
            Return False
        End Function

        Public Function XY(ByVal P As DataTypes.Point2d, ByRef XYResult As DataTypes.Point2d) As Boolean
            Dim Sta As Double = P.X - StaStart

            If Sta <= Length() Then
                Dim Offset As Double = P.Y
                Dim PC As Point2d = Center()
                Dim R As Double = Radius()
                Dim P2 As Point2d
                Dim Ang As Double

                Ang = PC.AngleTo(P1) + Sign(Bulge) * (Sta / R)
                P2.X = PC.X + R * Cos(Ang)
                P2.Y = PC.Y + R * Sin(Ang)

                If Bulge < 0.0 Then
                    Ang = P2.AngleTo(PC)
                Else
                    Ang = PC.AngleTo(P2)
                End If

                XYResult.X = P2.X + Offset * Cos(Ang)
                XYResult.Y = P2.Y + Offset * Sin(Ang)

                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetP1(ByRef PResult As DataTypes.Point2d) As Boolean
            PResult.X = P1.X
            PResult.Y = P1.Y
            Return True
        End Function

        Public Function GetP2(ByRef PResult As DataTypes.Point2d) As Boolean
            PResult.X = P2.X
            PResult.Y = P2.Y
            Return True
        End Function

        Public Function AngIn() As Double
            AngIn = P1.AngleTo(Center()) + System.Math.PI
            If (Bulge > 0.0) Then AngIn = AngIn * -1.0
        End Function

        Public Function AngOut() As Double
            AngOut = P2.AngleTo(Center()) + System.Math.PI
            If (Bulge > 0.0) Then AngOut = AngOut * -1.0
        End Function

    End Structure

    Public Structure Spiral
        Public StaStart As Double
        Public P1 As Point2d
        Public P2 As Point2d
        Public PLT As Point2d
        Public PLTST As Point2d
        Public PST As Point2d
        Public Lo As Double

        Public Function SpiralLs() As Double
            SpiralLs = 2.0 * Theta() * R()
        End Function

        Public Function Length() As Double
            Length = SpiralLs() - Lo
        End Function

        Public Function Theta() As Double
            Theta = Abs(PST.AngleTo(PLTST) - PLTST.AngleTo(PLT))
            If Theta < 0.0 Then Theta = Theta + 2.0 * PI
            If Theta > PI Then Theta = 2.0 * PI - Theta
        End Function

        Public Function R() As Double
            Return (PLTST.DistanceTo(PST) * Sin(Theta())) / SpiralCommands.SpiralFYR(Theta())
        End Function

        Private Function OddEven(ByVal N As Integer) As Integer
            Dim Remainder As Integer

            Remainder = N Mod 2
            If Remainder = 1 Then
                Return -1
            Else
                Return 1
            End If
        End Function

        Private Function SpiralFYR() As Double
            Return SpiralFYR(Theta())
        End Function

        Private Function SpiralFYR(ByVal Theta As Double) As Double
            Dim Ar2, Denominator, Numerator, Sum, Sum2 As Double
            Dim N As Integer

            Sum = -1.0
            Sum2 = 0.0
            Ar2 = 2.0 * Theta
            N = 1
            While (Abs(Sum - Sum2) > Tol)
                Sum = Sum2
                Numerator = OddEven(N + 1) * Pow(Ar2, ((2.0 * N) - 1.0))
                Denominator = Pow(2.0, ((2.0 * N) - 1.0)) * ((4.0 * N) - 1.0) * SpiralFact((2.0 * N) - 1.0)
                Sum2 = Sum2 + (Numerator / Denominator)
                N = N + 1
            End While
            Sum = Sum * Ar2
            Return Sum
        End Function

        Private Function SpiralFXR() As Double
            Return SpiralFXR(Theta())
        End Function

        Private Function SpiralFXR(ByVal Theta As Double) As Double
            Dim Ar2, Denominator, Numerator, Sum, Sum2 As Double
            Dim N As Integer

            Sum = -1.0
            Sum2 = 0.0
            Ar2 = 2.0 * Theta
            N = 1
            While (Abs(Sum - Sum2) > Tol)
                Sum = Sum2
                If Theta > TolFine Then
                    Numerator = OddEven(N + 1) * Pow(Ar2, (2.0 * (N - 1)))
                Else
                    Numerator = 0.0
                End If
                Denominator = Pow(2.0, (2.0 * (N - 1.0))) * ((4.0 * N) - 3.0) * SpiralFact(2.0 * (N - 1.0))
                Sum2 = Sum2 + (Numerator / Denominator)
                N = N + 1
            End While
            Sum = Sum * Ar2
            Return Sum
        End Function

        Private Function SpiralFact(ByVal N As Integer) As Double
            Dim F As Double
            F = 1.0
            While (N > 0)
                F = F * N
                N = N - 1
            End While
            SpiralFact = F
        End Function

        Private Function SpiralFact(ByVal N As Double) As Double
            Dim F As Double
            F = 1.0
            While (N > 0.0)
                F = F * N
                N = N - 1.0
            End While
            SpiralFact = F
        End Function

        Private Function SpiralP() As Double
            Return SpiralP(R(), SpiralLs())
        End Function

        Private Function SpiralP(ByVal R As Double, ByVal LS As Double) As Double
            Dim Theta As Double

            Theta = LS / (2.0 * R)
            SpiralP = R * (SpiralFYR(Theta) - (1.0 - Cos(Theta)))
        End Function

        Private Function SpiralPR() As Double
            Return SpiralPR(Theta())
        End Function

        Private Function SpiralPR(ByVal Theta As Double) As Double
            Return SpiralFYR(Theta) - (1.0 - Cos(Theta))
        End Function

        Private Function SpiralK() As Double
            Return SpiralK(R(), SpiralLs())
        End Function

        Private Function SpiralK(ByVal R As Double, ByVal LS As Double) As Double
            Dim Theta As Double

            Theta = LS / (2.0 * R)
            Return R * (SpiralFXR(Theta) - Sin(Theta))
        End Function

        Private Function SpiralKR() As Double
            Return SpiralKR(Theta())
        End Function

        Private Function SpiralKR(ByVal Theta As Double) As Double
            SpiralKR = SpiralFXR(Theta) - Sin(Theta)
        End Function

        Private Function StaOff_Fctn(ByVal Val As Double, ByVal Px As Double, ByVal Py As Double, ByVal A2 As Double, ByVal SpiralDirection As Double) As Double
            Dim R As Double

            If Abs(Val) < TolFine Then
                R = 0.0
            Else
                R = Sqrt(A2 / (2.0 * Val))
            End If

            If Abs(Val) < TolFine Then
                Return Px
            Else
                Return ((Px - (R * SpiralFXR(Val))) * Cos(Val)) + (SpiralDirection * (Py - (SpiralDirection * R * SpiralFYR(Val))) * Sin(Val))
            End If
        End Function

        Public Function Staoff(ByVal P As Point2d, ByRef StaOffResult As Point2d) As Boolean
            Dim A2, Alpha, F0, F1, F2, OffsetDirection, Px, Py, R0, SpiralDirection, Theta0, Theta1, Theta2 As Double
            Dim SP0, SP1 As Point2d

            If Sin(PLTST.AngleTo(PST) - PLT.AngleTo(PLTST)) > 0.0 Then
                SpiralDirection = 1.0
            Else
                SpiralDirection = -1.0
            End If
            Alpha = PLT.AngleTo(PLTST)
            Px = ((P.Y - PLT.Y) * Sin(Alpha)) + ((P.X - PLT.X) * Cos(Alpha))
            Py = ((P.Y - PLT.Y) * Cos(Alpha)) - ((P.X - PLT.X) * Sin(Alpha))
            A2 = 2.0 * R() * R() * Theta()
            Theta1 = (Lo * Lo) / (2.0 * A2)
            Theta2 = Theta()
            F1 = StaOff_Fctn(Theta1, Px, Py, A2, SpiralDirection)
            F2 = StaOff_Fctn(Theta2, Px, Py, A2, SpiralDirection)
            If (F1 * F2) > TolFine Then
                Return False
            Else
                If P.DistanceTo(PST) < TolFine Then
                    Theta0 = Theta()
                ElseIf P.DistanceTo(PLT) < TolFine Then
                    Theta0 = 0.0
                Else
                    Theta0 = (Theta1 + Theta2) / 2.0
                    F0 = StaOff_Fctn(Theta0, Px, Py, A2, SpiralDirection)
                    While (Abs(Theta1 - Theta2) > TolFine)
                        If (F0 * F2) > 0.0 Then
                            Theta2 = Theta0
                            F2 = F0
                        Else
                            Theta1 = Theta0
                            F1 = F0
                        End If
                        Theta0 = (Theta1 + Theta2) / 2.0
                        F0 = StaOff_Fctn(Theta0, Px, Py, A2, SpiralDirection)
                    End While
                End If
                If Abs(Theta0) < TolFine Then
                    R0 = 0.0
                Else
                    R0 = Sqrt(A2 / (2.0 * Theta0))
                End If
                If Abs(R0) < TolFine Then
                    StaOffResult.X = 0.0
                Else
                    StaOffResult.X = A2 / R0
                End If
                SP0.X = R0 * SpiralFXR(Theta0)
                SP0.Y = SpiralDirection * R0 * SpiralFYR(Theta0)
                SP1.X = Px
                SP1.Y = Py
                If Sin(SP0.AngleTo(SP1)) > 0.0 Then
                    OffsetDirection = -1.0
                Else
                    OffsetDirection = 1.0
                End If
                StaOffResult.Y = OffsetDirection * SP0.DistanceTo(SP1)
                If P2.DistanceTo(PST) < P1.DistanceTo(PST) Then
                    StaOffResult.X = StaOffResult.X + StaStart - Lo
                Else
                    StaOffResult.X = SpiralLs() + StaStart - StaOffResult.X
                    StaOffResult.Y = StaOffResult.Y * -1.0
                End If
                Return True
            End If
        End Function

        Public Function XY(ByVal P As DataTypes.Point2d, ByRef XYResult As DataTypes.Point2d) As Boolean
            Dim Sta As Double = P.X - StaStart

            If Sta <= Length() Then
                Dim Offset As Double = P.Y
                Dim Ang As Double = PLT.AngleTo(PLTST)
                Dim LS As Double = SpiralLs()
                Dim Ang2, SpiralDirection, Theta0, R0, X0, Y0 As Double
                Dim PS As Point2d

                If Sin(PLTST.AngleTo(PST) - PLT.AngleTo(PLTST)) > 0.0 Then
                    SpiralDirection = 1.0
                Else
                    SpiralDirection = -1.0
                End If

                If P2.DistanceTo(PST) < P1.DistanceTo(PST) Then
                    Sta = Sta + Lo
                Else
                    Sta = LS - Sta
                    Offset = Offset * -1.0
                End If
                If Sta < TolFine Then
                    PS = PLT
                    Theta0 = 0.0
                Else
                    Theta0 = Theta() * Pow((Sta / LS), 2.0)
                    If Sta < TolFine Then
                        R0 = 0.0
                        X0 = 0.0
                        Y0 = 0.0
                    Else
                        R0 = R() * (LS / Sta)
                        X0 = R0 * SpiralFXR(Theta0)
                        Y0 = SpiralDirection * R0 * SpiralFYR(Theta0)
                    End If
                    PS.X = PLT.X + (X0 * Cos(Ang)) - (Y0 * Sin(Ang))
                    PS.Y = PLT.Y + (X0 * Sin(Ang)) + (Y0 * Cos(Ang))
                End If
                Ang2 = Ang + (SpiralDirection * Theta0) - (PI / 2.0)

                'Dim doc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
                'Dim ed As Editor = doc.Editor

                XYResult.X = PS.X + Offset * Cos(Ang2)
                XYResult.Y = PS.Y + Offset * Sin(Ang2)
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetP1(ByRef PResult As DataTypes.Point2d) As Boolean
            PResult.X = P1.X
            PResult.Y = P1.Y
            Return True
        End Function

        Public Function GetP2(ByRef PResult As DataTypes.Point2d) As Boolean
            PResult.X = P2.X
            PResult.Y = P2.Y
            Return True
        End Function

        Public Function AngIn() As Double
            Return P1.AngleTo(PLTST)
        End Function

        Public Function AngOut() As Double
            Return PLTST.AngleTo(P2)
        End Function

    End Structure

    Public Enum AlignmentNodeType
        Tangent
        Arc
        Spiral
    End Enum

    Public Structure AlignmentNode
        Public AlignNodeType As AlignmentNodeType
        Public AlignNodeTangent As Tangent
        Public AlignNodeArc As Arc
        Public AlignNodeSpiral As Spiral

        Public Function StaStart() As Double
            If AlignNodeType = AlignmentNodeType.Tangent Then
                Return AlignNodeTangent.StaStart
            ElseIf AlignNodeType = AlignmentNodeType.Arc Then
                Return AlignNodeArc.StaStart
            ElseIf AlignNodeType = AlignmentNodeType.Spiral Then
                Return AlignNodeSpiral.StaStart
            Else
                Return Double.NaN
            End If
        End Function

        Public Function Length() As Double
            If AlignNodeType = AlignmentNodeType.Tangent Then
                Return AlignNodeTangent.Length
            ElseIf AlignNodeType = AlignmentNodeType.Arc Then
                Return AlignNodeArc.Length
            ElseIf AlignNodeType = AlignmentNodeType.Spiral Then
                Return AlignNodeSpiral.Length
            Else
                Return Double.NaN
            End If
        End Function

        Public Function StaOff(ByVal P As Point2d, ByRef StaOffResult As Point2d) As Boolean
            Select Case AlignNodeType
                Case AlignmentNodeType.Tangent
                    StaOff = AlignNodeTangent.StaOff(P, StaOffResult)
                Case AlignmentNodeType.Arc
                    StaOff = AlignNodeArc.StaOff(P, StaOffResult)
                Case AlignmentNodeType.Spiral
                    StaOff = AlignNodeSpiral.Staoff(P, StaOffResult)
                Case Else
                    StaOff = False
            End Select
            Return StaOff
        End Function

        Public Function XY(ByVal P As Point2d, ByRef XYResult As Point2d) As Boolean
            Select Case AlignNodeType
                Case AlignmentNodeType.Tangent
                    XY = AlignNodeTangent.XY(P, XYResult)
                Case AlignmentNodeType.Arc
                    XY = AlignNodeArc.XY(P, XYResult)
                Case AlignmentNodeType.Spiral
                    XY = AlignNodeSpiral.XY(P, XYResult)
                Case Else
                    XY = False
            End Select
            Return XY
        End Function

        Public Function GetP1(ByRef PResult As Point2d) As Boolean
            Select Case AlignNodeType
                Case AlignmentNodeType.Tangent
                    GetP1 = AlignNodeTangent.GetP1(PResult)
                Case AlignmentNodeType.Arc
                    GetP1 = AlignNodeArc.GetP1(PResult)
                Case AlignmentNodeType.Spiral
                    GetP1 = AlignNodeSpiral.GetP1(PResult)
                Case Else
                    GetP1 = False
            End Select
            Return GetP1
        End Function

        Public Function GetP2(ByRef PResult As Point2d) As Boolean
            Select Case AlignNodeType
                Case AlignmentNodeType.Tangent
                    GetP2 = AlignNodeTangent.GetP2(PResult)
                Case AlignmentNodeType.Arc
                    GetP2 = AlignNodeArc.GetP2(PResult)
                Case AlignmentNodeType.Spiral
                    GetP2 = AlignNodeSpiral.GetP2(PResult)
                Case Else
                    GetP2 = False
            End Select
            Return GetP2
        End Function

        Public Function AngIn() As Double
            If AlignNodeType = AlignmentNodeType.Tangent Then
                Return AlignNodeTangent.AngIn
            ElseIf AlignNodeType = AlignmentNodeType.Arc Then
                Return AlignNodeArc.AngIn
            ElseIf AlignNodeType = AlignmentNodeType.Spiral Then
                Return AlignNodeSpiral.AngIn
            Else
                Return Double.NaN
            End If
        End Function

        Public Function AngOut() As Double
            If AlignNodeType = AlignmentNodeType.Tangent Then
                Return AlignNodeTangent.AngOut
            ElseIf AlignNodeType = AlignmentNodeType.Arc Then
                Return AlignNodeArc.AngOut
            ElseIf AlignNodeType = AlignmentNodeType.Spiral Then
                Return AlignNodeSpiral.AngOut
            Else
                Return Double.NaN
            End If
        End Function

    End Structure

    Public Enum ProfileNodeType
        Parabolic
        Circular
    End Enum

    Public Structure ProfileNode
        Public ProfileNodeType As ProfileNodeType
        Public Station, Elevation, Value, G1, G2 As Double
    End Structure

    Public Structure SuperNode
        Public Station, Left, Right As Double
    End Structure

End Class