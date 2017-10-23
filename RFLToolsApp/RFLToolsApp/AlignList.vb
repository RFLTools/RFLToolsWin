Imports System.Math
Imports System.IO

Public Class AlignList
    Private Shared Tol As Double = 0.0000000001
    Private Shared TolFine As Double = 0.00000000000001

    Public Nodes() As DataTypes.AlignmentNode

    Sub New()
        GetAlign()
    End Sub

    Sub New(ByVal AlignListName As String)
        GetAlign(AlignListName)
    End Sub

    Public Function StaStart() As Double
        If (Nodes Is Nothing) Then
            Return Double.NaN
        Else
            Return Nodes(0).StaStart
        End If
    End Function

    Public Function SightDist(ByVal Sta As Double, ByVal Dist As Double, ByRef StaOut As Double) As Boolean
        If StaStart() = Double.NaN Then
            Return False
        Else
            If (Sta < StaStart()) Or (Sta > StaStart() + Length()) Then
                Return False
            ElseIf (Abs(Dist) < Tol) Then
                StaOut = Sta
                Return True
            ElseIf (Dist > 0.0) Then
                Dim Sta1, Sta2 As Double
                Dim P, P1, P2, POut, PTmp As DataTypes.Point2d
                PTmp.X = Sta
                PTmp.Y = 0.0
                If (XY(PTmp, P) = False) Then
                    Return False
                Else
                    Sta1 = Sta
                    P1.X = P.X
                    P1.Y = P.Y
                    Sta2 = StaStart() + Length()
                    Nodes(Nodes.Length - 1).GetP2(P2)
                    If P.DistanceTo(P2) < Dist Then
                        Return False
                    Else
                        While ((Sta2 - Sta1) > Tol)
                            StaOut = (Sta1 + Sta2) / 2.0
                            PTmp.X = StaOut
                            PTmp.Y = 0.0
                            XY(PTmp, POut)
                            If (P.DistanceTo(POut) > Dist) Then
                                Sta2 = StaOut
                                P2.X = POut.X
                                P2.Y = POut.Y
                            Else
                                Sta1 = StaOut
                                P1.X = POut.X
                                P1.Y = POut.Y
                            End If
                        End While
                        Return True
                    End If
                End If
            Else
                Dim Sta1, Sta2 As Double
                Dim P, P1, P2, POut, PTmp As DataTypes.Point2d
                PTmp.X = Sta
                PTmp.Y = 0.0
                If (XY(PTmp, P) = False) Then
                    Return False
                Else
                    Dist = Dist * -1.0
                    Sta1 = Sta
                    P1.X = P.X
                    P1.Y = P.Y
                    Sta2 = StaStart()
                    Nodes(0).GetP1(P2)
                    If P.DistanceTo(P2) < Dist Then
                        Return False
                    Else
                        While ((Sta1 - Sta2) > Tol)
                            StaOut = (Sta1 + Sta2) / 2.0
                            PTmp.X = StaOut
                            PTmp.Y = 0.0
                            XY(PTmp, POut)
                            If (P.DistanceTo(POut) > Dist) Then
                                Sta2 = StaOut
                                P2.X = POut.X
                                P2.Y = POut.Y
                            Else
                                Sta1 = StaOut
                                P1.X = POut.X
                                P1.Y = POut.Y
                            End If
                        End While
                        Return True
                    End If
                End If
            End If
        End If
    End Function

    Public Function StaOff(ByVal P As DataTypes.Point2d, ByRef StaOffResult As DataTypes.Point2d) As Boolean
        If Not (Nodes Is Nothing) Then
            Dim Node, Node0 As DataTypes.AlignmentNode
            Dim Sta, StaBest, Offset, OffsetBest As Double
            Dim Found As Boolean = False
            Dim StaOffNode As DataTypes.Point2d
            Dim FirstNode As Boolean = True
            Dim P11, P12, P21, P22 As DataTypes.Point2d

            For Each Node In Nodes
                If FirstNode Then
                    FirstNode = False
                Else
                    Node0.GetP1(P11)
                    Node0.GetP2(P12)
                    Node.GetP1(P21)
                    Node.GetP2(P22)
                    If ((P.DistanceTo(P12) < P.DistanceTo(P11)) And (P.DistanceTo(P21) < P.DistanceTo(P22))) Then
                        Sta = Node.StaStart
                        Offset = P.DistanceTo(P21)
                        If (Sin(P21.AngleTo(P) - Node.AngIn) > 0.0) Then Offset = Offset * -1.0
                        If Not Found Then
                            Found = True
                            StaBest = Sta
                            OffsetBest = Offset
                        ElseIf Abs(Offset) < Abs(OffsetBest) Then
                            StaBest = Sta
                            OffsetBest = Offset
                        End If
                    End If
                End If
                If Node.StaOff(P, StaOffNode) Then
                    If Not Found Then
                        Found = True
                        StaBest = StaOffNode.X
                        OffsetBest = StaOffNode.Y
                    ElseIf Abs(StaOffNode.Y) < Abs(OffsetBest) Then
                        StaBest = StaOffNode.X
                        OffsetBest = StaOffNode.Y
                    End If
                End If
                Node0 = Node
            Next
            If Found Then
                StaOffResult.X = StaBest
                StaOffResult.Y = OffsetBest
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Public Function XY(ByVal P As DataTypes.Point2d, ByRef XYResult As DataTypes.Point2d) As Boolean
        If Not (Nodes Is Nothing) Then
            Dim Node As DataTypes.AlignmentNode
            Dim Found As Boolean = False
            Dim C As Integer = 0

            Node = Nodes(C)
            If P.X >= Node.StaStart Then
                While C < Nodes.Length And Not Found
                    Node = Nodes(C)
                    If P.X <= Node.StaStart + Node.Length Then
                        Found = Node.XY(P, XYResult)
                    End If
                    C = C + 1
                End While
            End If
            If Found Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Public Function Length() As Double
        If (Nodes Is Nothing) Then
            Return Nothing
        Else
            Dim Node As DataTypes.AlignmentNode
            Length = 0.0
            For Each Node In Nodes
                Length = Length + Node.Length
            Next
            Return Length
        End If
    End Function

    Public Sub GetAlign()
        Nodes = GetAlignList("AlignList")
    End Sub

    Public Sub GetAlign(ByVal AlignListName As String)
        Nodes = GetAlignList(AlignListName)
    End Sub

    Public Function GetAlignList(ByVal AlignListName As String) As DataTypes.AlignmentNode()
        Dim NodeCount As Integer = 0
        Dim i As Integer

        Try
            Dim oReader = File.OpenText(AlignListName)
            Dim line As String

            line = oReader.ReadLine()

            If (line.ToUpper.ToUpper.CompareTo("#RFL HORIZONTAL ALIGNMENT FILE") <> 0) Then
                Return Nothing
            Else
                line = oReader.ReadLine()
                While (oReader.EndOfStream = False) And (line.ToUpper.CompareTo("#END DEFINITION") <> 0)
                    NodeCount = NodeCount + 1
                    For i = 1 To 5
                        line = oReader.ReadLine()
                    Next
                    If (line.ToUpper.CompareTo("SPIRAL") = 0) Then
                        For i = 1 To 8
                            line = oReader.ReadLine()
                        Next
                    Else
                        line = oReader.ReadLine()
                    End If
                End While

                oReader.BaseStream.Position = 0
                line = oReader.ReadLine()
                line = oReader.ReadLine()

                Dim AlignListResult(NodeCount - 1) As DataTypes.AlignmentNode
                Dim P1, P2, PLT, PLTST, PST As DataTypes.Point2d
                Dim StaStart, Bulge, Lo As Double

                i = 0
                While (oReader.EndOfStream = False) And (line.ToUpper.CompareTo("#END DEFINITION") <> 0)
                    Double.TryParse(line, StaStart)
                    line = oReader.ReadLine()
                    Double.TryParse(line, P1.X)
                    line = oReader.ReadLine()
                    Double.TryParse(line, P1.Y)
                    line = oReader.ReadLine()
                    Double.TryParse(line, P2.X)
                    line = oReader.ReadLine()
                    Double.TryParse(line, P2.Y)
                    line = oReader.ReadLine()
                    If (line.ToUpper.CompareTo("SPIRAL") = 0) Then
                        line = oReader.ReadLine()
                        Double.TryParse(line, PLT.X)
                        line = oReader.ReadLine()
                        Double.TryParse(line, PLT.Y)
                        line = oReader.ReadLine()
                        Double.TryParse(line, PLTST.X)
                        line = oReader.ReadLine()
                        Double.TryParse(line, PLTST.Y)
                        line = oReader.ReadLine()
                        Double.TryParse(line, PST.X)
                        line = oReader.ReadLine()
                        Double.TryParse(line, PST.Y)
                        line = oReader.ReadLine()
                        Double.TryParse(line, Lo)
                        line = oReader.ReadLine()
                        AlignListResult(i).AlignNodeType = DataTypes.AlignmentNodeType.Spiral
                        AlignListResult(i).AlignNodeSpiral.StaStart = StaStart
                        AlignListResult(i).AlignNodeSpiral.P1 = P1
                        AlignListResult(i).AlignNodeSpiral.P2 = P2
                        AlignListResult(i).AlignNodeSpiral.PLT = PLT
                        AlignListResult(i).AlignNodeSpiral.PLTST = PLTST
                        AlignListResult(i).AlignNodeSpiral.PST = PST
                        AlignListResult(i).AlignNodeSpiral.Lo = Lo
                    Else
                        Double.TryParse(line, Bulge)
                        If (Abs(Bulge) < TolFine) Then
                            AlignListResult(i).AlignNodeType = DataTypes.AlignmentNodeType.Tangent
                            AlignListResult(i).AlignNodeTangent.StaStart = StaStart
                            AlignListResult(i).AlignNodeTangent.P1 = P1
                            AlignListResult(i).AlignNodeTangent.P2 = P2
                        Else
                            AlignListResult(i).AlignNodeType = DataTypes.AlignmentNodeType.Arc
                            AlignListResult(i).AlignNodeArc.StaStart = StaStart
                            AlignListResult(i).AlignNodeArc.P1 = P1
                            AlignListResult(i).AlignNodeArc.P2 = P2
                            AlignListResult(i).AlignNodeArc.Bulge = Bulge
                        End If
                        line = oReader.ReadLine()
                    End If
                    i = i + 1
                End While
                Return AlignListResult
            End If
            oReader.Close()
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

End Class