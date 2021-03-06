﻿#Region "Microsoft.VisualBasic::5450a99a9e56085ffd06d11f2ed164ee, ..\sciBASIC#\Data_science\Graph\Testing\DijkstraTest.vb"

    ' Author:
    ' 
    '       asuka (amethyst.asuka@gcmodeller.org)
    '       xieguigang (xie.guigang@live.com)
    '       xie (genetics@smrucc.org)
    ' 
    ' Copyright (c) 2016 GPL3 Licensed
    ' 
    ' 
    ' GNU GENERAL PUBLIC LICENSE (GPL3)
    ' 
    ' This program is free software: you can redistribute it and/or modify
    ' it under the terms of the GNU General Public License as published by
    ' the Free Software Foundation, either version 3 of the License, or
    ' (at your option) any later version.
    ' 
    ' This program is distributed in the hope that it will be useful,
    ' but WITHOUT ANY WARRANTY; without even the implied warranty of
    ' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    ' GNU General Public License for more details.
    ' 
    ' You should have received a copy of the GNU General Public License
    ' along with this program. If not, see <http://www.gnu.org/licenses/>.

#End Region

Imports Microsoft.VisualBasic.Data.Graph

Module DijkstraTest

    Sub Main()
        Dim g As New Graph

        For i As Integer = 10 To 20
            g.AddVertex(label:=i)
        Next

        Dim weight As New Random

        g.AddEdge(0, 1, weight.NextDouble)
        g.AddEdge(1, 2, weight.NextDouble)
        g.AddEdge(2, 3, weight.NextDouble)
        g.AddEdge(3, 4, weight.NextDouble)
        g.AddEdge(4, 5, weight.NextDouble)
        g.AddEdge(5, 2, weight.NextDouble)
        g.AddEdge(5, 3, weight.NextDouble)
        g.AddEdge(4, 8, weight.NextDouble)
        g.AddEdge(8, 9, weight.NextDouble)
        g.AddEdge(3, 7, weight.NextDouble)
        g.AddEdge(7, 9, weight.NextDouble)
        g.AddEdge(9, 6, weight.NextDouble)

        Dim Dijkstra As New Dijkstra.DijkstraRouteFind(g)
        Dim route = Dijkstra.CalculateMinCost(g.Vertex(0))

        Pause()
    End Sub
End Module

