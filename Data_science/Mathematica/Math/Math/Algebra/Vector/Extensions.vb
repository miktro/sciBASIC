﻿#Region "Microsoft.VisualBasic::e5b7c3fc7a48400eb9a0eac205950096, ..\sciBASIC#\Data_science\Mathematica\Math\Math\Algebra\Vector\Extensions.vb"

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

Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Ranges
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Math.SyntaxAPI.Vectors

Namespace LinearAlgebra

    Public Module Extensions

        Public Iterator Function Points(x As Vector, y As Vector) As IEnumerable(Of PointF)
            For i As Integer = 0 To x.Length - 1
                Yield New PointF(x(i), y(i))
            Next
        End Function

        <Extension>
        Public Function Point2D(polygon As (X As Vector, Y As Vector)) As IEnumerable(Of PointF)
            With polygon
                Return Points(.X, .Y)
            End With
        End Function

        <Extension> Public Function Top(vector As Vector, n%) As BooleanVector
            Dim desc = vector.OrderByDescending(Function(x) x).Take(n).AsList
            Return New BooleanVector(vector.Select(Function(x) desc.IndexOf(x) > -1))
        End Function

        ''' <summary>
        ''' Simplified (numPy) take method: 1) axis is always 0, 2) first argument is always a vector
        ''' </summary>
        ''' <param name="v">List of values</param>
        ''' <param name="indices">List of indices</param>
        ''' <returns>Vector containing elements from vector 1 at the indicies in vector 2</returns>
        <Extension>
        Public Function Take(v As Vector, indices As IEnumerable(Of Integer)) As Vector
            Return New Vector(v.Takes(indices.ToArray))
        End Function

        ''' <summary>
        ''' 返回目标长度的[0-1]之间的随机数向量
        ''' </summary>
        ''' <param name="size%"></param>
        ''' <returns></returns>
        Public Function rand(size%) As Vector
            Dim rnd As New Random
            Dim list As New List(Of Double)

            For i As Integer = 0 To size - 1
                Call list.Add(rnd.NextDouble)
            Next

            Return New Vector(list)
        End Function

        ''' <summary>
        ''' 获取一个指定长度的由目标区间范围内的随机数所构成的向量
        ''' </summary>
        ''' <param name="range"></param>
        ''' <param name="size%">函数所返回的向量的长度</param>
        ''' <returns></returns>
        <Extension>
        Public Function rand(range As DoubleRange, size%) As Vector
            Dim v#() = rand(size).RangeTransform([to]:=range)
            Return New Vector(v)
        End Function

        <Extension>
        Public Function AsVector(range As DoubleRange, Optional counts% = 1000) As Vector
            Return New Vector(range.Enumerate(counts))
        End Function
    End Module
End Namespace
