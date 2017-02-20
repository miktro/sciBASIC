﻿#Region "Microsoft.VisualBasic::8828cc6c19f48d66435eb5a603df45cf, ..\sciBASIC#\Data_science\Mathematical\Plots\Scatter\ManhattanStatics.vb"

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
Imports System.Drawing.Drawing2D
Imports Microsoft.VisualBasic.ComponentModel.Algorithm.base.SlideWindow
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Drawing2D
Imports Microsoft.VisualBasic.Linq

Public Module ManhattanStatics

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="s"></param>
    ''' <param name="size"></param>
    ''' <param name="margin"></param>
    ''' <param name="bg$"></param>
    ''' <param name="fill$">正负误差之间的填充颜色</param>
    ''' <returns></returns>
    Public Function Plot(s As SerialData,
                         Optional size As Size = Nothing,
                         Optional margin As Size = Nothing,
                         Optional bg$ = "white",
                         Optional fill$ = Nothing,
                         Optional errPlusColor$ = "green",
                         Optional errMinusColor$ = "red",
                         Optional errInner$ = "black",
                         Optional errPtSize! = 3,
                         Optional absoluteScaling As Boolean = True) As Bitmap

        Dim fillColor As Color = If(
            String.IsNullOrEmpty(fill) OrElse fill = "none",
            Nothing,
            fill.ToColor)
        Dim epColor As New SolidBrush(errPlusColor.ToColor)
        Dim emColor As New SolidBrush(errMinusColor.ToColor)
        Dim eIColor As New SolidBrush(errInner.ToColor)

        Return GraphicsPlots(
            size, margin, bg,
            Sub(ByRef g, grect)
                Dim serrPlus As New SerialData With {
                    .color = s.color,
                    .lineType = DashStyle.Dash,
                    .PointSize = s.PointSize,
                    .width = s.width,
                    .title = NameOf(PointData.errPlus),
                    .pts = s.pts _
                        .ToArray(Function(err) New PointData With {
                            .pt = New PointF(err.pt.X, err.pt.Y + err.errPlus),
                            .errPlus = err.errPlus
                        })
                }
                Dim serrMinus As New SerialData With {
                    .color = s.color,
                    .lineType = DashStyle.Dash,
                    .PointSize = s.PointSize,
                    .width = s.width,
                    .title = NameOf(PointData.errMinus),
                    .pts = s.pts _
                        .ToArray(Function(err) New PointData With {
                            .pt = New PointF(err.pt.X, err.pt.Y + err.errMinus),
                            .errMinus = err.errMinus
                        })
                }
                Dim mapper As New Scaling({
                    serrPlus,
                    s,
                    serrMinus
                },
                absoluteScaling)

                ' 绘制线条以及正负误差线
                For Each line As SerialData In mapper.ForEach(size, margin)
                    Dim pts = line.pts.SlideWindows(2)
                    Dim pen As New Pen(color:=line.color, width:=line.width) With {
                        .DashStyle = line.lineType
                    }
                    Dim br As New SolidBrush(line.color)
                    Dim d = line.PointSize
                    Dim r As Single = line.PointSize / 2
                    Dim bottom! = size.Height - margin.Height

                    For Each pt In pts
                        Dim a As PointData = pt.First
                        Dim b As PointData = pt.Last

                        Call g.DrawLine(pen, a.pt, b.pt)
                    Next

                    Select Case line.title
                        Case NameOf(PointData.errMinus)
                            serrMinus = line
                        Case NameOf(PointData.errPlus)
                            serrPlus = line
                        Case Else
                            s = line
                    End Select
                Next

                ' 绘制误差点
                For Each pt As PointData In s.pts
                    Dim pp As PointData = Nothing
                    Dim pm As PointData = Nothing

                    For Each p As PointData In serrPlus.pts  ' 获取正误差点
                        If p.pt.Y <= pt.pt.Y Then ' 经过转换之后坐标系是颠倒过来的
                            pp = p
                            Exit For
                        End If
                    Next
                    For Each p As PointData In serrMinus.pts ' 获取负误差点
                        If p.pt.Y <= pt.pt.Y Then ' 经过转换之后坐标系是颠倒过来的
                            pm = p
                            Exit For
                        End If
                    Next

                    For Each st# In pt.Statics ' 只需要查看x是否在范围内就知道了
                        ' 查看是否在正误差外
                        ' 查看是否在负误差外
                        ' 则这个点就是在正负误差范围内
                    Next
                Next
            End Sub)
    End Function
End Module
