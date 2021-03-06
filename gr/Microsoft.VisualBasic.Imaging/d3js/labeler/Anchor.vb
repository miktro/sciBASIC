﻿#Region "Microsoft.VisualBasic::923be7e15bf8886a81448d6fd6a16eac, ..\sciBASIC#\gr\Microsoft.VisualBasic.Imaging\d3js\labeler\Anchor.vb"

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

Namespace d3js.Layout

    Public Class Anchor

        ''' <summary>
        ''' the x-coordinate of the anchor.
        ''' </summary>
        ''' <returns></returns>
        Public Property x As Double
        ''' <summary>
        ''' the y-coordinate of the anchor.
        ''' </summary>
        ''' <returns></returns>
        Public Property y As Double
        ''' <summary>
        ''' the anchor radius (assuming anchor is a circle).
        ''' </summary>
        ''' <returns></returns>
        Public Property r As Double

        Public Shared Widening Operator CType(anchor As Anchor) As Point
            With anchor
                Return New Point(.x, .y)
            End With
        End Operator

        Public Shared Widening Operator CType(anchor As Anchor) As PointF
            With anchor
                Return New PointF(.x, .y)
            End With
        End Operator

        Public Shared Widening Operator CType(anchor As Anchor) As RectangleF
            Dim r# = anchor.r

            Return New RectangleF With {
                .Location = anchor,
                .Size = New SizeF(r, r)
            }
        End Operator

        Public Shared Widening Operator CType(anchor As Anchor) As Rectangle
            With CType(anchor, RectangleF)
                Return New Rectangle(.Location.ToPoint, .Size.ToSize)
            End With
        End Operator
    End Class
End Namespace

